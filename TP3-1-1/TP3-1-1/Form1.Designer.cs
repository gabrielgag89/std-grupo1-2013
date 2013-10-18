namespace TP3_1_1
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.gbInputData = new System.Windows.Forms.GroupBox();
			this.nudVarQuantity = new System.Windows.Forms.NumericUpDown();
			this.lbVarQuantity = new System.Windows.Forms.Label();
			this.nudStartAddress = new System.Windows.Forms.NumericUpDown();
			this.lbStartAddress = new System.Windows.Forms.Label();
			this.nudFunctCode = new System.Windows.Forms.NumericUpDown();
			this.lbFunctCode = new System.Windows.Forms.Label();
			this.nudIdMachine = new System.Windows.Forms.NumericUpDown();
			this.lbIdMachine = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtTrama = new System.Windows.Forms.TextBox();
			this.txtIdMachine = new System.Windows.Forms.TextBox();
			this.txtStartAddress = new System.Windows.Forms.TextBox();
			this.txtFunctCode = new System.Windows.Forms.TextBox();
			this.txtVarQuantity = new System.Windows.Forms.TextBox();
			this.gbInputData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudVarQuantity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudStartAddress)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudFunctCode)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdMachine)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbInputData
			// 
			this.gbInputData.Controls.Add(this.txtVarQuantity);
			this.gbInputData.Controls.Add(this.txtFunctCode);
			this.gbInputData.Controls.Add(this.txtStartAddress);
			this.gbInputData.Controls.Add(this.txtIdMachine);
			this.gbInputData.Controls.Add(this.nudVarQuantity);
			this.gbInputData.Controls.Add(this.lbVarQuantity);
			this.gbInputData.Controls.Add(this.nudStartAddress);
			this.gbInputData.Controls.Add(this.lbStartAddress);
			this.gbInputData.Controls.Add(this.nudFunctCode);
			this.gbInputData.Controls.Add(this.lbFunctCode);
			this.gbInputData.Controls.Add(this.nudIdMachine);
			this.gbInputData.Controls.Add(this.lbIdMachine);
			this.gbInputData.Location = new System.Drawing.Point(13, 13);
			this.gbInputData.Name = "gbInputData";
			this.gbInputData.Size = new System.Drawing.Size(485, 136);
			this.gbInputData.TabIndex = 0;
			this.gbInputData.TabStop = false;
			this.gbInputData.Text = "Datos de entrada";
			// 
			// nudVarQuantity
			// 
			this.nudVarQuantity.Location = new System.Drawing.Point(352, 76);
			this.nudVarQuantity.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.nudVarQuantity.Name = "nudVarQuantity";
			this.nudVarQuantity.Size = new System.Drawing.Size(120, 20);
			this.nudVarQuantity.TabIndex = 7;
			this.nudVarQuantity.ValueChanged += new System.EventHandler(this.ValueChanged);
			this.nudVarQuantity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp);
			// 
			// lbVarQuantity
			// 
			this.lbVarQuantity.AutoSize = true;
			this.lbVarQuantity.Location = new System.Drawing.Point(234, 80);
			this.lbVarQuantity.Name = "lbVarQuantity";
			this.lbVarQuantity.Size = new System.Drawing.Size(112, 13);
			this.lbVarQuantity.TabIndex = 6;
			this.lbVarQuantity.Text = "Cantidad de variables:";
			// 
			// nudStartAddress
			// 
			this.nudStartAddress.Location = new System.Drawing.Point(352, 24);
			this.nudStartAddress.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudStartAddress.Name = "nudStartAddress";
			this.nudStartAddress.Size = new System.Drawing.Size(120, 20);
			this.nudStartAddress.TabIndex = 5;
			this.nudStartAddress.ValueChanged += new System.EventHandler(this.ValueChanged);
			this.nudStartAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp);
			// 
			// lbStartAddress
			// 
			this.lbStartAddress.AutoSize = true;
			this.lbStartAddress.Location = new System.Drawing.Point(234, 28);
			this.lbStartAddress.Name = "lbStartAddress";
			this.lbStartAddress.Size = new System.Drawing.Size(97, 13);
			this.lbStartAddress.TabIndex = 4;
			this.lbStartAddress.Text = "Dirección de inicio:";
			// 
			// nudFunctCode
			// 
			this.nudFunctCode.Location = new System.Drawing.Point(108, 76);
			this.nudFunctCode.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.nudFunctCode.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudFunctCode.Name = "nudFunctCode";
			this.nudFunctCode.Size = new System.Drawing.Size(120, 20);
			this.nudFunctCode.TabIndex = 3;
			this.nudFunctCode.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudFunctCode.ValueChanged += new System.EventHandler(this.ValueChanged);
			this.nudFunctCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp);
			// 
			// lbFunctCode
			// 
			this.lbFunctCode.AutoSize = true;
			this.lbFunctCode.Location = new System.Drawing.Point(6, 80);
			this.lbFunctCode.Name = "lbFunctCode";
			this.lbFunctCode.Size = new System.Drawing.Size(96, 13);
			this.lbFunctCode.TabIndex = 2;
			this.lbFunctCode.Text = "Código de función:";
			// 
			// nudIdMachine
			// 
			this.nudIdMachine.Location = new System.Drawing.Point(108, 24);
			this.nudIdMachine.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.nudIdMachine.Name = "nudIdMachine";
			this.nudIdMachine.Size = new System.Drawing.Size(120, 20);
			this.nudIdMachine.TabIndex = 1;
			this.nudIdMachine.ValueChanged += new System.EventHandler(this.ValueChanged);
			this.nudIdMachine.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp);
			// 
			// lbIdMachine
			// 
			this.lbIdMachine.AutoSize = true;
			this.lbIdMachine.Location = new System.Drawing.Point(6, 28);
			this.lbIdMachine.Name = "lbIdMachine";
			this.lbIdMachine.Size = new System.Drawing.Size(56, 13);
			this.lbIdMachine.TabIndex = 0;
			this.lbIdMachine.Text = "ID equipo:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtTrama);
			this.groupBox1.Location = new System.Drawing.Point(13, 155);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(485, 60);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Trama";
			// 
			// txtTrama
			// 
			this.txtTrama.Enabled = false;
			this.txtTrama.Location = new System.Drawing.Point(9, 23);
			this.txtTrama.Name = "txtTrama";
			this.txtTrama.Size = new System.Drawing.Size(460, 20);
			this.txtTrama.TabIndex = 0;
			// 
			// txtIdMachine
			// 
			this.txtIdMachine.Enabled = false;
			this.txtIdMachine.Location = new System.Drawing.Point(108, 50);
			this.txtIdMachine.Name = "txtIdMachine";
			this.txtIdMachine.Size = new System.Drawing.Size(120, 20);
			this.txtIdMachine.TabIndex = 8;
			// 
			// txtStartAddress
			// 
			this.txtStartAddress.Enabled = false;
			this.txtStartAddress.Location = new System.Drawing.Point(352, 50);
			this.txtStartAddress.Name = "txtStartAddress";
			this.txtStartAddress.Size = new System.Drawing.Size(120, 20);
			this.txtStartAddress.TabIndex = 9;
			// 
			// txtFunctCode
			// 
			this.txtFunctCode.Enabled = false;
			this.txtFunctCode.Location = new System.Drawing.Point(108, 102);
			this.txtFunctCode.Name = "txtFunctCode";
			this.txtFunctCode.Size = new System.Drawing.Size(120, 20);
			this.txtFunctCode.TabIndex = 10;
			// 
			// txtVarQuantity
			// 
			this.txtVarQuantity.Enabled = false;
			this.txtVarQuantity.Location = new System.Drawing.Point(352, 102);
			this.txtVarQuantity.Name = "txtVarQuantity";
			this.txtVarQuantity.Size = new System.Drawing.Size(120, 20);
			this.txtVarQuantity.TabIndex = 11;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(514, 228);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gbInputData);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.gbInputData.ResumeLayout(false);
			this.gbInputData.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudVarQuantity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudStartAddress)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudFunctCode)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudIdMachine)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbInputData;
		private System.Windows.Forms.NumericUpDown nudFunctCode;
		private System.Windows.Forms.Label lbFunctCode;
		private System.Windows.Forms.NumericUpDown nudIdMachine;
		private System.Windows.Forms.Label lbIdMachine;
		private System.Windows.Forms.NumericUpDown nudVarQuantity;
		private System.Windows.Forms.Label lbVarQuantity;
		private System.Windows.Forms.NumericUpDown nudStartAddress;
		private System.Windows.Forms.Label lbStartAddress;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtTrama;
		private System.Windows.Forms.TextBox txtVarQuantity;
		private System.Windows.Forms.TextBox txtFunctCode;
		private System.Windows.Forms.TextBox txtStartAddress;
		private System.Windows.Forms.TextBox txtIdMachine;
	}
}

