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
		private SerialPort port;
        delegate void SetTextCallback(byte[] text);

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
				port = new SerialPort(cbPort.SelectedItem.ToString(), 19200, Parity.None, 8, StopBits.One);

				port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

				port.Open();

				btConnect.Enabled = false;
				btStop.Enabled = true;

				byte[] crc = new byte[2];

				byte[] mensaje =
				{
					byte.Parse(numDevice.Value.ToString()),
					byte.Parse(cbFunction.SelectedItem.ToString()),
					0x00,
					numAddress.Value > 0 ? byte.Parse((numAddress.Value - 1).ToString()) : byte.Parse("1"),
					0x00,
					byte.Parse(numLength.Value.ToString()),
					0x00,
					0x00
				};

				GetCRC(mensaje, ref crc);

				mensaje[6] = crc[0];
                mensaje[7] = crc[1];

                txtResponse.Text += "Tx: ";

                foreach (var item in mensaje)
                {
                    txtResponse.Text += string.Format("[{0:X2}]", item); ;
                }

                txtResponse.Text += "\r\n";

				port.Write(mensaje, 0, 8);
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
                int cant = port.Read(respuesta, 0, (5 + 2 * int.Parse(numLength.Value.ToString())));

                if (txtResponse.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    this.Invoke(d, new object[] { respuesta });
                }
                else
                {
                    ShowResponse(respuesta);
                    btConnect.Enabled = true;
                    btStop.Enabled = false;
                }

				port.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btStop_Click(object sender, EventArgs e)
		{
			if (port.IsOpen)
			{
				port.Close();
				btConnect.Enabled = true;
				btStop.Enabled = false;
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

        private void SetText(byte[] text)
        {
            try
            {
                ShowResponse(text);

                btConnect.Enabled = true;
                btStop.Enabled = false;
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

                for (int i = 0; i < (5 + 2 * int.Parse(numLength.Value.ToString())); i++)
                {
                    txtResponse.Text += string.Format("[{0:X2}]", response[i]);
                }

                txtResponse.Text += "\r\n";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}
}