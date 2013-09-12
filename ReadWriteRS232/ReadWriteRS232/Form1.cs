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

		private SerialPort port;
		private int startAddress;
		private int length;
		private int lengthRequested;
		private int timeout;
		private int maxRetry;
		private int tryCount;
		private byte device;
		private byte functNum;
		private bool hasReceive;
		private bool mustReceive;
		private byte[] message;
		private Thread writeThread;

		#endregion

		#region Delegates

		delegate void SetTextCallback(byte[] text);
		delegate void ChangeConnectBtnCallback(bool enabled);
		delegate void ChangeStopBtnCallback(bool enabled);

		#endregion

		#region Events

		public Form1()
		{
			InitializeComponent();

			GetPorts();
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

		private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				if (mustReceive && !hasReceive && port.BytesToRead <= 256)
				{
					byte[] respuesta = new byte[256];

					lengthRequested = port.BytesToRead;
					port.Read(respuesta, 0, lengthRequested);

					hasReceive = true;

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

		private void Connect()
		{
			try
			{
				if (port != null && port.IsOpen)
				{
					port.Close();
				}

				// Obtención de datos de la interfaz
				device = (byte)int.Parse(numDevice.Value.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
				functNum = (byte)int.Parse(cbFunction.SelectedItem.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
				startAddress = Convert.ToInt32(numAddress.Value);
				length = Convert.ToInt32(numLength.Value);
				timeout = Convert.ToInt32(numTimeout.Value);
				maxRetry = Convert.ToInt32(numRetry.Value);

				// Si la función seleccionada es la 6,
				// no se permite que el valor ingresado sea mayor a 255
				if (functNum == 6 && length > 255)
				{
					throw new Exception("Para la función 6, el valor debe estar entre 0 y 255");
				}

				// Conexión por el puerto serie
				port = new SerialPort(cbPort.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);

				port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

				// Se abre la conexión con el puerto
				port.Open();

				// Se habilita el botón para detener el proceso y deshabilita el botón para iniciarlo
				btConnect.Enabled = false;
				btStop.Enabled = true;

				Send();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void Send()
		{
			try
			{
				string startAddressStr, lengthStr;
				byte length1, length2, startAddress1, startAddress2;

				hasReceive = false;
				mustReceive = true;
				tryCount = 0;

				startAddressStr = string.Format("{0:X4}", startAddress);
				startAddress1 = (byte)int.Parse(startAddressStr.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
				startAddress2 = (byte)int.Parse(startAddressStr.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

				if (functNum == 3)
				{
					if (length <= 125)
					{
						lengthStr = string.Format("{0:X4}", length);
						length = 0;
					}
					else
					{
						lengthStr = string.Format("{0:X4}", 125);
						length -= 125;
						startAddress += 125;
					}
				}
				else
				{
					lengthStr = string.Format("{0:X4}", length);
					length = 0;
				}

				length1 = (byte)int.Parse(lengthStr.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
				length2 = (byte)int.Parse(lengthStr.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

				message = MakeMessage(startAddress1, startAddress2, length1, length2);

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

		private void ShowMessage(byte[] message)
		{
			try
			{
				txtActivity.Text += "Tx: ";

				for (int i = 0; i < message.Length; i++)
				{
					txtActivity.Text += string.Format("[{0:X2}]", message[i]);
				}

				txtActivity.Text += "\r\n";
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

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

		private void ShowMessageText(byte[] text)
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

		private void ShowResponse(byte[] response)
		{
			try
			{
				txtActivity.Text += "Rx: ";

				for (int i = 0; i < lengthRequested; i++)
				{
					txtActivity.Text += string.Format("[{0:X2}]", response[i]);
				}

				txtActivity.Text += "\r\n";

				if (length > 0)
				{
					Send();
				}
				else
				{
					Stop();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Stop()
		{
			try
			{
				if (btConnect.InvokeRequired)
				{
					ChangeConnectBtnCallback d = new ChangeConnectBtnCallback(SetChangeConnectBtn);
					this.Invoke(d, new object[] { true });
				}
				else
				{
					ChangeConnectBtn(true);
				}

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

		private void ChangeConnectBtn(bool enabled)
		{
			btConnect.Enabled = enabled;
		}

		private void ChangeStopBtn(bool enabled)
		{
			btStop.Enabled = enabled;
		}

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

		private void WriteThread()
		{
			try
			{
				while (!hasReceive && tryCount++ < maxRetry)
				{
					if (txtActivity.InvokeRequired)
					{
						SetTextCallback d = new SetTextCallback(ShowMessageText);
						this.Invoke(d, new object[] { message });
					}
					else
					{
						ShowMessage(message);
					}

					port.Write(message, 0, 8);

					Thread.Sleep(timeout);
				}

				if (!hasReceive)
				{
					mustReceive = false;
					MessageBox.Show("Reintentos agotados");
					Stop();
				}
			}
			catch (Exception ex)
			{
				Stop();
			}
		}

		#endregion
	}
}