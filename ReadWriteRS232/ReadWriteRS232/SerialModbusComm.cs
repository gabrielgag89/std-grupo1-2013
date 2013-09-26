using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace ReadWriteRS232
{
	public class SerialModbusComm : ModbusComm
	{
		#region Public properties

		public string CommPort
		{
			get { return commPort; }
			set { commPort = value; }
		}

		#endregion

		#region Private properties

		private SerialPort serialPort;
		private string commPort;

		#endregion

		#region Public methods

		public override bool ConnectSlave()
		{
			try
			{
				// Cierra el puerto si estaba abierto
				if (serialPort != null && serialPort.IsOpen)
				{
					serialPort.Close();
				}

				countAddress = startAddress;

				// Si la función seleccionada es la 6,
				// no se permite que el valor ingresado sea mayor a 255
				if (functNum == 6 && length > 32767)
				{
					throw new Exception("Para la función 6, el valor debe estar entre 0 y 32767");
				}

				// Conexión por el puerto serie
				serialPort = new SerialPort(commPort, 19200, Parity.None, 8, StopBits.One);
				serialPort.WriteTimeout = timeout;

				// Se declara el evento que recibirá las respuestas
				serialPort.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);

				// Se abre la conexión con el puerto
				serialPort.Open();

				// Se preparan los datos a enviar
				PrepareSend();

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
				if (serialPort != null && serialPort.IsOpen)
				{
					serialPort.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region Private methods

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

		#endregion

		#region Protected methods

		protected override byte[] MakeMessage(byte address1, byte address2, byte length1, byte length2)
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

		protected override void SendMessage()
		{
			try
			{
				if (serialPort != null && serialPort.IsOpen)
				{
					serialPort.Write(message, 0, 8); 
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

		protected override List<Variable> GetVariables(byte[] response)
		{
			try
			{
				if (functNum == 3)
				{
					for (int i = 3; i < lengthReceived - 2; i += 2)
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
						valor = string.Format("{0:X4}", response[4].ToString("X2") + response[5].ToString("X2"))
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

		private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				// Si se está esperando recibir una respuesta, ésta no se ha recibido aún y
				// la cantidad de bytes que se han recibido por el puerto es menot o igual a 256
				if (mustReceive && !hasReceive && serialPort.BytesToRead <= 256)
				{
					byte[] response = new byte[256];

					// Se guarda globalmente la cantidad de bytes recibidos
					lengthReceived = serialPort.BytesToRead;
					// Se leen los bytes recibidos
					serialPort.Read(response, 0, lengthReceived);

					// Se indica que se recibió la respuesta
					hasReceive = true;

					ShowResponse(response);
				}
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}

		#endregion
	}
}