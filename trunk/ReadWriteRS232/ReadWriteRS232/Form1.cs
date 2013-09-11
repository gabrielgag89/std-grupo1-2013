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
		private byte device;
		private byte functNum;

		#endregion

		#region Delegates

		delegate void SetTextCallback(byte[] text);

		#endregion

		#region Events

		public Form1()
		{
			InitializeComponent();

			cbFunction.SelectedIndex = 0;
			cbPort.SelectedIndex = 0;
		}

		private void btConnect_Click(object sender, EventArgs e)
		{
			try
			{
				Connect();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				byte[] respuesta = new byte[256];

				lengthRequested = port.BytesToRead;
				port.Read(respuesta, 0, lengthRequested);

				if (txtResponse.InvokeRequired)
				{
					SetTextCallback d = new SetTextCallback(SetText);
					this.Invoke(d, new object[] { respuesta });
				}
				else
				{
					ShowResponse(respuesta);
				}
			}
			catch (Exception ex)
			{
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

		#endregion

		#region Methods

		private void Connect()
		{
			try
			{
				// Se habilita el botón para detener el proceso y deshabilita el botón para iniciarlo
				btConnect.Enabled = false;
				btStop.Enabled = true;

				// Conexión por el puerto serie
				port = new SerialPort(cbPort.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);
				port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

				// Obtención de datos de la interfaz
				device = (byte)int.Parse(numDevice.Value.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
				functNum = (byte)int.Parse(cbFunction.SelectedItem.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
				startAddress = Convert.ToInt32(numAddress.Value);
				length = Convert.ToInt32(numLength.Value);

				port.Open();
				Send();
			}
			catch (Exception ex)
			{
				Stop();

				throw ex;
			}
		}

		private void Send()
		{
			try
			{
				string startAddressStr, lengthStr;
				byte length1, length2, startAddress1, startAddress2;

				startAddressStr = string.Format("{0:X4}", startAddress);
				startAddress1 = (byte)int.Parse(startAddressStr.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
				startAddress2 = (byte)int.Parse(startAddressStr.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

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

				length1 = (byte)int.Parse(lengthStr.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
				length2 = (byte)int.Parse(lengthStr.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

				byte[] message = MakeMessage(startAddress1, startAddress2, length1, length2);

				ShowMessage(message);

				port.Write(message, 0, 8);
			}
			catch (Exception ex)
			{
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
				txtResponse.Text += "Tx: ";

				for (int i = 0; i < message.Length; i++)
				{
					txtResponse.Text += string.Format("[{0:X2}]", message[i]);
				}

				txtResponse.Text += "\r\n";
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void SetText(byte[] text)
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

		private void ShowResponse(byte[] response)
		{
			try
			{
				txtResponse.Text += "Rx: ";

				for (int i = 0; i < lengthRequested; i++)
				{
					txtResponse.Text += string.Format("[{0:X2}]", response[i]);
				}

				txtResponse.Text += "\r\n";

				if (length > 0)
				{
					Send();
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
				if (port.IsOpen)
				{
					port.Close();
					btConnect.Enabled = true;
					btStop.Enabled = false;
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