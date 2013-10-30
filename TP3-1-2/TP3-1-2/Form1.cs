using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TP3_1_2
{
	public partial class Form1 : Form
	{
		#region Events

		public Form1()
		{
			InitializeComponent();
			
		}

		private void KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ValueChanged(object sender, EventArgs e)
		{
			try
			{
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region Methods

		public void BuildTrama()
		{
			try
			{
				string idMachine = Convert.ToString(int.Parse(nudIdMachine.Text), 2).PadLeft(10, '0');
				string corrientes = Convert.ToString(int.Parse(corriente.Text), 2).PadLeft(6, '0');
				string temperaturas = Convert.ToString(int.Parse(temperatura.Text), 2).PadLeft(8, '0');
				string tensions = Convert.ToString(int.Parse(tension.Text), 2).PadLeft(4, '0');
				DateTime anioBase = DateTime.Parse("2013-01-01 00:00:00.000");
                DateTime fechaAct = DateTime.Now;
				TimeSpan difFecha = fechaAct - anioBase;

				string tiempo = Convert.ToString((int)difFecha.TotalMinutes, 2).PadLeft(29, '0');

				txtIdMachine.Text = idMachine;
				corrienteEnv.Text = corrientes;
				tempEnviada.Text = temperaturas;
				tensionEnv.Text = tensions;
                FechayHoraEnviada.Text = fechaAct.ToString("dd/MM/yyyy hh:mm");

				txtTrama.Text = string.Format("[{0} {1} {2} {3} 0 0 0 0 0 0 0 {4} ]",
												idMachine,
												corrientes,
												temperaturas,
												tensions,
                                                tiempo);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion

        private void lbStartAddress_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuildTrama();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FechayHora.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
        }
	}
}