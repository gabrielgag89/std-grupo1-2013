using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ReadWriteRS232
{
	public class TCPModbusComm : ModbusComm
	{
		#region Public properties

		public int AppPort
		{
			get { return appPort; }
			set { appPort = value; }
		}

		#endregion

		#region Private properties

		private TcpClient tcpClient;
		private Stream tcpStream;
		private IPEndPoint endPoint;
		private ManualResetEvent connectDone = new ManualResetEvent(false);
		private int appPort;
		private int transId;
		private byte[] response = new byte[512];

		#endregion

		#region Public methods

		public override bool ConnectSlave()
		{
			try
			{
				// Cierra el puerto si estaba abierto
				if (tcpClient != null && tcpClient.Connected)
				{
					tcpClient.Close();
				}

				if (tcpStream != null)
				{
					tcpStream.Close();
				}

				countAddress = startAddress;

				// Si la función seleccionada es la 6,
				// no se permite que el valor ingresado sea mayor a 255
				if (functNum == 6 && length > 32767)
				{
					throw new Exception("Para la función 6, el valor debe estar entre 0 y 32767");
				}

				// Arma IP:Puerto
				endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), appPort);

				// Conexión por TCP/IP
				tcpClient = new TcpClient();
				tcpClient.BeginConnect(endPoint.Address, endPoint.Port, new AsyncCallback(OnConnect), null);
				tcpClient.SendTimeout = timeout;
				tcpClient.ReceiveTimeout = timeout;

				connectDone.WaitOne();

				if (!tcpClient.Connected)
				{
					Thread.Sleep(timeout);
				}

				if (tcpClient.Connected)
				{
					tcpStream = tcpClient.GetStream();

					// Se preparan los datos a enviar
					PrepareSend();
				}
				else
				{
					throw new Exception("No se ha podido conectar al dispositivo");
				}

				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public override void CloseConnection()
		{
			try
			{
				if (tcpClient != null && tcpClient.Connected)
				{
					tcpClient.Close();
				}

				if (tcpStream != null)
				{
					tcpStream.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region Protected methods

		protected override byte[] MakeMessage(byte address1, byte address2, byte length1, byte length2)
		{
			try
			{
				string transIdStr = string.Format("{0:X4}", transId++);
				byte transId1 = (byte)int.Parse(transIdStr.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
				byte transId2 = (byte)int.Parse(transIdStr.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

				byte[] message =
				{
					transId1, transId2, // Id transacción
					0x00, 0x00, // Protocolo
					0x00, 0x06, // Cantidad de bytes siguientes
					device, // Dispositivo
					functNum, // Número de función
					address1, address2, // Dirección de inicio
					length1, length2 // Cantidad de variables/valor a establecer
				};

				return message;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected override void SendMessage()
		{
			try
			{
				if (tcpClient != null && tcpClient.Connected && tcpStream != null)
				{
					tcpStream.Write(message, 0, message.Length);
					tcpStream.BeginRead(response, 0, response.Length, new AsyncCallback(OnDataReceived), null); 
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected override void CheckResponse(byte[] response)
		{
			try
			{
				// Comprueba que el dispositovo del que se recibe sea del mismo al que se le envía
				if (response[6] != device)
				{
					throw new Exception("El dispositivo del que se recibió el mensaje no es al que se le envió");
				}

				// Comprueba si el mensaje recibido es de error
				if ((functNum == 3 && response[7] == Convert.ToByte(0x83)) || (functNum == 6 && response[7] == Convert.ToByte(0x86)))
				{
					throw new Exception(string.Format("Error en la función {0:X2}: {1}", functNum, GetErrorMessage(response[8])));
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected override List<Variable> GetVariables(byte[] response)
		{
			try
			{
				if (functNum == 3)
				{
					for (int i = 9; i < lengthReceived; i += 2)
					{
						variables.Add(new Variable
						{
							nombre = countAddress++,
							valor = string.Format("{0:X4}", response[i].ToString("X2") + response[i + 1].ToString("X2"))
						});
					}
				}
				else if (functNum == 6)
				{
					variables.Add(new Variable
					{
						nombre = countAddress++,
						valor = string.Format("{0:X4}", response[10].ToString("X2") + response[11].ToString("X2"))
					});
				}

				return variables;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region Events

		private void OnConnect(IAsyncResult ar)
		{
			try
			{
				tcpClient.EndConnect(ar);
				connectDone.Set();
			}
			catch (SocketException)
			{
				connectDone.Set();
				Stop();
			}
			catch (Exception ex)
			{
				connectDone.Set();
				ErrorHandler(ex);
			}
		}

		private void OnDataReceived(IAsyncResult ar)
		{
			try
			{
				if (tcpClient != null && tcpClient.Connected && tcpStream != null)
				{
					// Se guarda globalmente la cantidad de bytes recibidos
					lengthReceived = tcpStream.EndRead(ar);

					// Si se está esperando recibir una respuesta, ésta no se ha recibido aún y
					// la cantidad de bytes que se han recibido por el puerto es menot o igual a 256
					if (mustReceive && !hasReceive)
					{
						// Se indica que se recibió la respuesta
						hasReceive = true;

						ShowResponse(response);
					}
				}
			}
			catch (Exception)
			{
				Stop();
			}
		}

		#endregion
	}
}