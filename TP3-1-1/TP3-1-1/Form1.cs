using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TP3_1_1
{
	public partial class Form1 : Form
	{
		#region Events

		public Form1()
		{
			InitializeComponent();
			BuildTrama();
		}

		private void KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				BuildTrama();
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
				BuildTrama();
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
				string idMachine = Convert.ToString(int.Parse(nudIdMachine.Value.ToString()), 2).PadLeft(9, '0');
				string functCode = Convert.ToString(int.Parse(nudFunctCode.Value.ToString()), 2).PadLeft(5, '0');
				string startAddress = Convert.ToString(int.Parse(nudStartAddress.Value.ToString()), 2).PadLeft(16, '0');
				string varQuantity = Convert.ToString(int.Parse(nudVarQuantity.Value.ToString()), 2).PadLeft(9, '0');

				txtIdMachine.Text = idMachine;
				txtFunctCode.Text = functCode;
				txtStartAddress.Text = startAddress;
				txtVarQuantity.Text = varQuantity;

				txtTrama.Text = string.Format("[{0} {1} {2} 0 {3}]",
												idMachine,
												functCode,
												varQuantity,
												startAddress);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		#endregion
	}
}