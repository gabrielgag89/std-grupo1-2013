using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ReadWriteRS232
{
	public abstract class ModbusComm
	{
		#region Protected Properties

		protected int startAddress; // Dirección de inicio
		protected int countAddress; // Dirección de inicio
		protected int length; // Cantidad de variables solicitadas, o valor a guardar
		protected int timeout; // Tiempo de espera por la respuesta
		protected int maxRetry; // Cantidad máxima de reintentos de recibir la respuesta
		protected byte device; // Número de dispositivo al que se le envía el mensaje
		protected byte functNum; // Número de la función a utilizar
		protected byte[] message; // Mensaje a enviar
		protected List<Variable> listadevariables = new List<Variable>();
		protected Form1 form;
		protected bool hasReceive; // Indica si se ha recibido la respuesta
		protected bool mustReceive; // Indica si se espera recibir una respuesta
		protected int lengthReceived; // Cantidad de bytes recibidos en la respuesta
		protected int tryCount; // Cantidad de intentos realizados
		protected Thread writeThread; // Hilo que actualmente maneja el envío de mensajes
		protected List<Variable> variables = new List<Variable>(); // Variables a mostrar

		#endregion

		#region Public properties

		public int StartAddress
		{
			get { return startAddress; }
			set { startAddress = value; }
		}
		public int Length
		{
			get { return length; }
			set { length = value; }
		}
		public int Timeout
		{
			get { return timeout; }
			set { timeout = value; }
		}
		public int MaxRetry
		{
			get { return maxRetry; }
			set { maxRetry = value; }
		}
		public byte Device
		{
			get { return device; }
			set { device = value; }
		}
		public byte FunctNum
		{
			get { return functNum; }
			set { functNum = value; }
		}
		public Form1 Form
		{
			get { return form; }
			set { form = value; }
		}

		#endregion

		#region Abstract methods

		public abstract bool ConnectSlave();
		public abstract void CloseConnection();
		protected abstract byte[] MakeMessage(byte address1, byte address2, byte length1, byte length2);
		protected abstract void SendMessage();
		protected abstract void CheckResponse(byte[] response);
		protected abstract List<Variable> GetVariables(byte[] response);

		#endregion

		#region Protected methods

		protected void PrepareSend()
		{
			try
			{
				string startAddressStr, lengthStr;
				byte length1, length2, startAddress1, startAddress2;

				// Se indica que no se han recibido mensajes aún
				hasReceive = false;
				// Se indica que se esperan recibir mensajes
				mustReceive = true;
				// Se inicializa la cantidad de intentos realizados
				tryCount = 0;

				// Se parsea a bytes la dirección de inicio
				startAddressStr = string.Format("{0:X4}", startAddress - 1);
				startAddress1 = (byte)int.Parse(startAddressStr.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
				startAddress2 = (byte)int.Parse(startAddressStr.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

				// Si la función a utilizar es la 3
				if (functNum == 3)
				{
					// Si la cantidad de variables solicitadas es menor que 126, no es necesario enviar más mensajes
					if (length <= 125)
					{
						lengthStr = string.Format("{0:X4}", length);
						length = 0;
					}
					else // Sino, si es mayor que 125, deberán enviarse más mensajes para solicitar las demás variables
					{
						lengthStr = string.Format("{0:X4}", 125);
						length -= 125; // Cantidad de variables que restan por pedir
						startAddress += 125; // Dirección desde la que se comenzará a pedir variables en el próximo mensaje
					}
				}
				else // Sino, si es la función 6, no es necesario enviar más mensajes
				{
					lengthStr = string.Format("{0:X4}", length);
					length = 0;
				}

				// Se parsea a bytes la cantidad de variables/valor a escribir
				length1 = (byte)int.Parse(lengthStr.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
				length2 = (byte)int.Parse(lengthStr.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

				// Se arma el mensaje
				message = MakeMessage(startAddress1, startAddress2, length1, length2);

				// Se envía el mensaje mediante un hilo, que se encargará de los reintentos y timeouts
				if (writeThread == null || writeThread.ThreadState == ThreadState.Stopped)
				{
					writeThread = new Thread(WriteThread);
				}
				else
				{
					writeThread.Abort();
					writeThread = new Thread(WriteThread);
				}

				writeThread.Start();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected void ShowResponse(byte[] response)
		{
			try
			{
				string messageBuffer = "Rx: ";

				for (int i = 0; i < lengthReceived; i++)
				{
					// Muestra el byte en formato hexadecimal
					messageBuffer += string.Format("[{0:X2}]", response[i]);
				}

				messageBuffer += "\r\n";

				form.InvokeShowTextArea(messageBuffer);

				CheckResponse(response);
				GetVariables(response);

				// Si todavía quedan variables a pedir, prepara los datos del nuevo mensaje
				if (length > 0)
				{
					PrepareSend();
				}
				else // Sino, detiene el proceso
				{
					form.InvokeSetValuesDGV(variables);
					Stop();
				}
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}

		protected string GetErrorMessage(byte errorCode)
		{
			try
			{
				switch (errorCode)
				{
					case 0x01:
						return "ILLEGAL FUNCTION";
					case 0x02:
						return "ILLEGAL DATA ADDRESS";
					case 0x03:
						return "ILLEGAL DATA VALUE";
					case 0x04:
						return "SLAVE DEVICE FAILURE";
					case 0x05:
						return "ACKNOWLEDGE";
					case 0x06:
						return "SLAVE DEVICE BUSY";
					case 0x08:
						return "MEMORY PARITY ERROR";
					case 0x0A:
						return "GATEWAY PATH UNAVAILABLE";
					case 0x0B:
						return "GATEWAY TARGET DEVICE FAILED TO RESPOND";
					default:
						return "ACKNOWLEDGE";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected void ErrorHandler(Exception ex)
		{
			Stop();
			form.ShowMessageBox(ex.Message);
		}

		protected void Stop()
		{
			try
			{
				form.Stop();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region Private methods

		private void ShowMessage(byte[] message)
		{
			try
			{
				string messageBuffer = "Tx: ";

				for (int i = 0; i < message.Length; i++)
				{
					// Muestra el byte en formato hexadecimal
					messageBuffer += string.Format("[{0:X2}]", message[i]);
				}

				messageBuffer += "\r\n";

				form.InvokeShowTextArea(messageBuffer);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void WriteThread()
		{
			try
			{
				bool isDeviceDisconnected = false;

				// Mientras no se haya recibido la respuesta del mensaje y queden
				// intentos, se envía el mensaje
				while (!hasReceive && tryCount++ < maxRetry)
				{
					// Muestra en el TextArea el mensaje enviado
					ShowMessage(message);

					// Envía el mensaje por el puerto
					try
					{
						SendMessage();
					}
					catch (TimeoutException)
					{
						isDeviceDisconnected = true;
					}

					// Espera durante un tiempo a recibir la respuesta
					Thread.Sleep(timeout);
				}

				// Si se terminaron los intentos, y aún no se ha recibido la respuesta
				// muestra un mensaje y termina el proceso
				if (!hasReceive)
				{
					// Se indica que no se esperan recibir respuestas
					mustReceive = false;

					if (isDeviceDisconnected)
					{
						form.ShowMessageBox("No se pudo conectar al dispositivo");
					}

					throw new Exception("Reintentos agotados");
				}
			}
			catch (ThreadAbortException) // Manejo de la excepción lanzada el detener el hilo
			{

			}
			catch (TimeoutException) // Manejo de la excepción lanzada si se agotó el tiempo de espera para enviar por el puerto
			{
				ErrorHandler(new Exception("No pudo conectarse al dispositivo por el puerto"));
			}
			catch (Exception ex) // Manejo general de excepciones del hilo
			{
				ErrorHandler(ex);
			}
		}

		#endregion
	}
}