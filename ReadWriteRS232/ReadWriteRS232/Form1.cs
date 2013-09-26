using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReadWriteRS232
{
	public partial class Form1 : Form
	{
		#region Properties

		private ModbusComm commPort;

		#endregion

		#region Delegates

		// Delegados a utilizar cuando los hilos quieren modificar controles de la GUI
		delegate void AppendTextAreaCallback(string text);
		delegate void SetValuesDGVCallback(List<Variable> variables);
		delegate void ChangeConnectBtnCallback(bool enabled);
		delegate void ChangeStopBtnCallback(bool enabled);

		#endregion

		#region Events

		public Form1()
		{
			try
			{
				InitializeComponent();

				// Se obtienen los puertos disponibles
				GetPorts();
				// Se selecciona el primer elemento del combo
				cbFunction.SelectedIndex = 0;
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		private void btConnect_Click(object sender, EventArgs e)
		{
			try
			{
				Connect();
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
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
				ShowMessageBox(ex.Message);
			}
		}

		private void clcTextArea_Click(object sender, EventArgs e)
		{
			try
			{
				txtActivity.Text = "";
				dataGridView1.DataSource = new List<Variable>();
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		private void cbComPort_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (cbComPort.SelectedItem.ToString().Equals("TCP/IP"))
				{
					lbAppPort.Enabled = true;
					numAppPort.Enabled = true;
				}
				else
				{
					lbAppPort.Enabled = false;
					numAppPort.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				ShowMessageBox(ex.Message);
			}
		}

		#endregion

		#region Private methods

		private void GetPorts()
		{
			try
			{
				List<string> ports = SerialPort.GetPortNames().ToList();
				ports.Add("TCP/IP");

				cbComPort.DataSource = ports;

				cbComPort_SelectedIndexChanged(null, null);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void Connect()
		{
			try
			{
				if (cbComPort.SelectedItem.ToString().Equals("TCP/IP"))
				{
					commPort = new TCPModbusComm { AppPort = Convert.ToInt32(numAppPort.Value) };
				}
				else
				{
					commPort = new SerialModbusComm { CommPort = cbComPort.SelectedItem.ToString() };
				}

				commPort.Device = (byte)int.Parse(numDevice.Value.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
				commPort.FunctNum = (byte)int.Parse(cbFunction.SelectedItem.ToString(), System.Globalization.NumberStyles.AllowHexSpecifier);
				commPort.StartAddress = Convert.ToInt32(numAddress.Value);
				commPort.Length = Convert.ToInt32(numLength.Value);
				commPort.Timeout = Convert.ToInt32(numTimeout.Value);
				commPort.MaxRetry = Convert.ToInt32(numRetry.Value);
				commPort.Form = this;

				if (commPort.ConnectSlave())
				{
					// Se habilita el botón para detener el proceso y deshabilita el botón para iniciarlo
					btConnect.Enabled = false;
					btStop.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void AppendTextArea(string text)
		{
			try
			{
				txtActivity.Text = txtActivity.Text + text;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void SetValuesDGV(List<Variable> variables)
		{
			try
			{
				dataGridView1.DataSource = variables;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void ChangeConnectBtn(bool enabled)
		{
			try
			{
				btConnect.Enabled = enabled;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void ChangeStopBtn(bool enabled)
		{
			try
			{
				btStop.Enabled = enabled;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

		#region Public methods

		public void InvokeShowTextArea(string text)
		{
			try
			{
				if (txtActivity.InvokeRequired)
				{
					AppendTextAreaCallback d = new AppendTextAreaCallback(AppendTextArea);
					this.Invoke(d, text);
				}
				else
				{
					AppendTextArea(text);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void InvokeSetValuesDGV(List<Variable> variables)
		{
			try
			{
				if (dataGridView1.InvokeRequired)
				{
					SetValuesDGVCallback d = new SetValuesDGVCallback(SetValuesDGV);
					this.Invoke(d, variables);
				}
				else
				{
					SetValuesDGV(variables);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void Stop()
		{
			try
			{
				// Habilita el botón "Conectar"
				if (btConnect.InvokeRequired)
				{
					ChangeConnectBtnCallback d = new ChangeConnectBtnCallback(ChangeConnectBtn);
					this.Invoke(d, true );
				}
				else
				{
					ChangeConnectBtn(true);
				}

				// Deshabilita el botón "Detener"
				if (btStop.InvokeRequired)
				{
					ChangeStopBtnCallback d = new ChangeStopBtnCallback(ChangeStopBtn);
					this.Invoke(d, false );
				}
				else
				{
					ChangeStopBtn(false);
				}

				commPort.CloseConnection();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void ShowMessageBox(string message)
		{
			MessageBox.Show(message);
		}

		#endregion
	}
}