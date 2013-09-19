using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace ReadWriteRS232
{
	public partial class Form1 : Form
	{
		#region Properties

		private SerialPort port; // Instancia de la conexión con el puerto serie
		private int startAddress; // Dirección de inicio
		private int countAddress; // Dirección de inicio
		private int length; // Cantidad de variables solicitadas, o valor a guardar
		private int lengthReceived; // Cantidad de bytes recibidos en la respuesta
		private int timeout; // Tiempo de espera por la respuesta
		private int maxRetry; // Cantidad máxima de reintentos de recibir la respuesta
		private int tryCount; // Cantidad de intentos realizados
		private byte device; // Número de dispositivo al que se le envía el mensaje
		private byte functNum; // Número de la función a utilizar
		private bool hasReceive; // Indica si se ha recibido la respuesta
		private bool mustReceive; // Indica si se espera recibir una respuesta
		private byte[] message; // Mensaje a enviar
		private Thread writeThread; // Hilo que actualmente maneja el envío de mensajes
        private List<Variable> listadevariables = new List<Variable>();
        private DataTable Tabla; //Declaramos una variable de tipo DataTable y a su vez la inicializamos para usarla mas tarde. 
        private DataRow Renglon;//Esta variable de tipo DataRow solo la declaramos y mas adelante la utilizaremos para agregarsela al dataTable que ya declaramos arriba 

		#endregion

		#region Delegates

		// Delegados a utilizar cuando los hilos quieren modificar controles de la GUI
		delegate void SetTextCallback(byte[] text);
		delegate void ChangeConnectBtnCallback(bool enabled);
		delegate void ChangeStopBtnCallback(bool enabled);

		#endregion

		#region Events

		public Form1()
		{
			InitializeComponent();

			// Se obtienen los puertos disponibles
			GetPorts();
			// Se selecciona el primer elemento del combo
			cbFunction.SelectedIndex = 0;
		}

		private void btConnect_Click(object sender, EventArgs e)
		{
			try
			{
				Connect();
			}
			catch (Exception ex)
			{
				Stop();
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Evento que recibe la respuesta por el puerto serie.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				// Si se está esperando recibir una respuesta, ésta no se ha recibido aún y
				// la cantidad de bytes que se han recibido por el puerto es menot o igual a 256
				if (mustReceive && !hasReceive && port.BytesToRead <= 256)
				{
					byte[] respuesta = new byte[256];

					// Se guarda globalmente la cantidad de bytes recibidos
					lengthReceived = port.BytesToRead;
					// Se leen los bytes recibidos
					port.Read(respuesta, 0, lengthReceived);

					// Se indica que se recibió la respuesta
					hasReceive = true;

					// Se muestra en el TextArea el mensaje recibido
					if (txtActivity.InvokeRequired)
					{
						SetTextCallback d = new SetTextCallback(SetResponseText);
						this.Invoke(d, new object[] { respuesta });
					}
					else
					{
						ShowResponse(respuesta);
					}
				}
			}
			catch (Exception ex)
			{
				Stop();
				MessageBox.Show(ex.Message);
			}
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			try
			{
				Stop();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void clcTextArea_Click(object sender, EventArgs e)
		{
			txtActivity.Text = "";
		}

		#endregion

		#region Methods

		/// <summary>
		/// Obtiene los puertos disponibles en la PC.
		/// </summary>
		private void GetPorts()
		{
			try
			{
				cbPort.DataSource = SerialPort.GetPortNames();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Obtiene los datos desde la GUI y realiza la conexión con el puerto seleccionado.
		/// </summary>
		private void Connect()
		{
			try
			{
				Tabla = new DataTable();
				Tabla.Columns.Add(new DataColumn("Nombre"));
				Tabla.Columns.Add(new DataColumn("Valor"));

				// Cierra el puerto si estaba abierto
				if (port != null && port.IsOpen)
				{
					port.Close();
				}

				// Obtención de datos de la interfaz
				device = (byte)int.Parse(numDevice.Value.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
				functNum = (byte)int.Parse(cbFunction.SelectedItem.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
				countAddress = startAddress = Convert.ToInt32(numAddress.Value);
				length = Convert.ToInt32(numLength.Value);
				timeout = Convert.ToInt32(numTimeout.Value);
				maxRetry = Convert.ToInt32(numRetry.Value);

				// Si la función seleccionada es la 6,
				// no se permite que el valor ingresado sea mayor a 255
				if (functNum == 6 && length > 32767)
				{
					throw new Exception("Para la función 6, el valor debe estar entre 0 y 32767");
				}

				// Conexión por el puerto serie
				port = new SerialPort(cbPort.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);
				port.WriteTimeout = timeout;

				// Se declara el evento que recibirá las respuestas
				port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

				// Se abre la conexión con el puerto
				port.Open();

				// Se habilita el botón para detener el proceso y deshabilita el botón para iniciarlo
				btConnect.Enabled = false;
				btStop.Enabled = true;

				// Se preparan los datos a enviar
				PrepareSend();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Prepara los datos a enviar.
		/// </summary>
		private void PrepareSend()
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
				Stop();
				throw ex;
			}
		}

		/// <summary>
		/// Arma el mensaje a enviar.
		/// </summary>
		/// <param name="address1"></param>
		/// <param name="address2"></param>
		/// <param name="length1"></param>
		/// <param name="length2"></param>
		/// <returns></returns>
		private byte[] MakeMessage(byte address1, byte address2, byte length1, byte length2)
		{
			try
			{
				byte[] message =
				{
					device,
					functNum,
					address1,
					address2,
					length1,
					length2,
					0x00,
					0x00
				};
				byte[] crc = new byte[2];

				GetCRC(message, ref crc);

				message[6] = crc[0];
				message[7] = crc[1];

				return message;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Calcula el CRC del mensaje a enviar.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="CRC"></param>
		private void GetCRC(byte[] message, ref byte[] CRC)
		{
			//Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
			//return the CRC values:

			try
			{
				ushort CRCFull = 0xFFFF;
				byte CRCHigh = 0xFF, CRCLow = 0xFF;
				char CRCLSB;

				for (int i = 0; i < (message.Length) - 2; i++)
				{
					CRCFull = (ushort)(CRCFull ^ message[i]);

					for (int j = 0; j < 8; j++)
					{
						CRCLSB = (char)(CRCFull & 0x0001);
						CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

						if (CRCLSB == 1)
							CRCFull = (ushort)(CRCFull ^ 0xA001);
					}
				}

				CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
				CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Muestra los mensajes enviados en el TextArea.
		/// </summary>
		/// <param name="message"></param>
		private void ShowMessage(byte[] message)
		{
			try
			{
				txtActivity.Text += "Tx: ";

				for (int i = 0; i < message.Length; i++)
				{
					// Muestra el byte en formato hexadecimal
					txtActivity.Text += string.Format("[{0:X2}]", message[i]);
				}

				txtActivity.Text += "\r\n";
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Muestra en el TextArea el mensaje recibido
		/// </summary>
		/// <param name="response"></param>
		private void ShowResponse(byte[] response)
		{
			try
			{
				int cantidadfilasmostradas = 0;

				txtActivity.Text += "Rx: ";

				for (int i = 0; i < lengthReceived; i++)
				{
					// Muestra el byte en formato hexadecimal
					txtActivity.Text += string.Format("[{0:X2}]", response[i]);

					if (functNum == 3 && i >= 3 && i < lengthReceived - 2 && (i % 2) == 1)
					{
						cantidadfilasmostradas += 1;
						Renglon = Tabla.NewRow();
						Renglon[0] = countAddress++;
						Renglon[1] = string.Format("[{0:X4}]", response[i].ToString("X2") + response[i + 1].ToString("X2"));

						Tabla.Rows.Add(Renglon);
					}
				}
				
				if (functNum == 6)
				{
					Renglon = Tabla.NewRow();
					Renglon[0] = countAddress++;
					Renglon[1] = string.Format("[{0:X4}]", response[4].ToString("X2") + response[5].ToString("X2"));

					Tabla.Rows.Add(Renglon);
				}

				txtActivity.Text += "\r\n";

				CheckResponse(response);

				dataGridView1.DataSource = Tabla;

				// Si todavía quedan variables a pedir, prepara los datos del nuevo mensaje
				if (length > 0)
				{
					PrepareSend();
				}
				else // Sino, detiene el proceso
				{
					Stop();
				}
			}
			catch (Exception ex)
			{
				Stop();
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Método utilizado cuando un hilo quiere escribir el mensaje recibido en el TextArea.
		/// </summary>
		/// <param name="text"></param>
		private void SetResponseText(byte[] text)
		{
			try
			{
				ShowResponse(text);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Método utilizado cuando un hilo quiere escribir el mensaje enviado en el TextArea.
		/// </summary>
		/// <param name="text"></param>
		private void SetMessageText(byte[] text)
		{
			try
			{
				ShowMessage(text);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Detiene el proceso de envío del mensaje.
		/// </summary>
		private void Stop()
		{
			try
			{
				// Habilita el botón "Conectar"
				if (btConnect.InvokeRequired)
				{
					ChangeConnectBtnCallback d = new ChangeConnectBtnCallback(SetChangeConnectBtn);
					this.Invoke(d, new object[] { true });
				}
				else
				{
					ChangeConnectBtn(true);
				}

				// Deshabilita el botón "Detener"
				if (btStop.InvokeRequired)
				{
					ChangeStopBtnCallback d = new ChangeStopBtnCallback(SetChangeStopBtn);
					this.Invoke(d, new object[] { false });
				}
				else
				{
					SetChangeStopBtn(false);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Método utilizado cuando un hilo quiere cambiar el estado del botón "Conectar".
		/// </summary>
		/// <param name="enabled"></param>
		private void ChangeConnectBtn(bool enabled)
		{
			btConnect.Enabled = enabled;
		}

		/// <summary>
		/// Método utilizado cuando un hilo quiere cambiar el estado del botón "Detener".
		/// </summary>
		/// <param name="enabled"></param>
		private void ChangeStopBtn(bool enabled)
		{
			btStop.Enabled = enabled;
		}

		/// <summary>
		/// Cambia el estado del botón "Conectar".
		/// </summary>
		/// <param name="enabled"></param>
		private void SetChangeConnectBtn(bool enabled)
		{
			try
			{
				ChangeConnectBtn(enabled);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Cambia el estado del botón "Detener".
		/// </summary>
		/// <param name="enabled"></param>
		private void SetChangeStopBtn(bool enabled)
		{
			try
			{
				ChangeStopBtn(enabled);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Hilo que envía los mensajes y controla el timeout.
		/// </summary>
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
					if (txtActivity.InvokeRequired)
					{
						SetTextCallback d = new SetTextCallback(SetMessageText);
						this.Invoke(d, new object[] { message });
					}
					else
					{
						ShowMessage(message);
					}

					// Envía el mensaje por el puerto
					try
					{
						port.Write(message, 0, 8);
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
						MessageBox.Show("No se pudo conectar al dispositivo");
					}

					throw new Exception("Reintentos agotados");
				}
			}
			catch (ThreadAbortException) // Manejo de la excepción lanzada el detener el hilo
			{
				Stop();
			}
			catch (TimeoutException) // Manejo de la excepción lanzada si se agotó el tiempo de espera para enviar por el puerto
			{
				MessageBox.Show("No pudo conectarse al dispositivo por el puerto");
				Stop();
			}
			catch (Exception ex) // Manejo general de excepciones del hilo
			{
				MessageBox.Show(ex.Message);
				Stop();
			}
		}

		/// <summary>
		/// Comprueba la consistencia del mensaje recibido
		/// </summary>
		/// <param name="response"></param>
		private void CheckResponse(byte[] response)
		{
			try
			{
				// Comprueba los errores de la respuesta mediante el CRC
				byte[] crc = new byte[2];
				byte[] responseTemp = new byte[lengthReceived];

				for (int i = 0; i < lengthReceived; i++)
				{
					responseTemp[i] = response[i];
				}

				GetCRC(responseTemp, ref crc);

				if (responseTemp[lengthReceived - 2] != crc[0] || responseTemp[lengthReceived - 1] != crc[1])
				{
					throw new Exception("El mensaje se recibió con errores, el CRC no coincide");
				}

				// Comprueba que el dispositovo del que se recibe sea del mismo al que se le envía
				if (response[0] != device)
				{
					throw new Exception("El dispositivo del que se recibió el mensaje no es al que se le envió");
				}

				// Comprueba si el mensaje recibido es de error
				if ((functNum == 3 && response[1] == Convert.ToByte(0x83)) || (functNum == 6 && response[1] == Convert.ToByte(0x86)))
				{
					throw new Exception(string.Format("Error en la función {0:X2}: {1}", functNum, GetErrorMessage(response[2])));
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private string GetErrorMessage(byte errorCode)
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

		#endregion
	}
}