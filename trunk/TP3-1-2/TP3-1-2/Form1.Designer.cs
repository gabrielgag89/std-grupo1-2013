namespace TP3_1_2
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
            this.components = new System.ComponentModel.Container();
            this.gbInputData = new System.Windows.Forms.GroupBox();
            this.tensionEnv = new System.Windows.Forms.TextBox();
            this.corrienteEnv = new System.Windows.Forms.TextBox();
            this.tempEnviada = new System.Windows.Forms.TextBox();
            this.txtIdMachine = new System.Windows.Forms.TextBox();
            this.tension = new System.Windows.Forms.NumericUpDown();
            this.lbVarQuantity = new System.Windows.Forms.Label();
            this.temperatura = new System.Windows.Forms.NumericUpDown();
            this.lbStartAddress = new System.Windows.Forms.Label();
            this.corriente = new System.Windows.Forms.NumericUpDown();
            this.lbFunctCode = new System.Windows.Forms.Label();
            this.nudIdMachine = new System.Windows.Forms.NumericUpDown();
            this.lbIdMachine = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTrama = new System.Windows.Forms.TextBox();
            this.FechayHoraEnviada = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FechayHora = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbInputData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tension)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.corriente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIdMachine)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInputData
            // 
            this.gbInputData.Controls.Add(this.button1);
            this.gbInputData.Controls.Add(this.FechayHora);
            this.gbInputData.Controls.Add(this.FechayHoraEnviada);
            this.gbInputData.Controls.Add(this.label1);
            this.gbInputData.Controls.Add(this.tensionEnv);
            this.gbInputData.Controls.Add(this.corrienteEnv);
            this.gbInputData.Controls.Add(this.tempEnviada);
            this.gbInputData.Controls.Add(this.txtIdMachine);
            this.gbInputData.Controls.Add(this.tension);
            this.gbInputData.Controls.Add(this.lbVarQuantity);
            this.gbInputData.Controls.Add(this.temperatura);
            this.gbInputData.Controls.Add(this.lbStartAddress);
            this.gbInputData.Controls.Add(this.corriente);
            this.gbInputData.Controls.Add(this.lbFunctCode);
            this.gbInputData.Controls.Add(this.nudIdMachine);
            this.gbInputData.Controls.Add(this.lbIdMachine);
            this.gbInputData.Location = new System.Drawing.Point(13, 13);
            this.gbInputData.Name = "gbInputData";
            this.gbInputData.Size = new System.Drawing.Size(485, 210);
            this.gbInputData.TabIndex = 0;
            this.gbInputData.TabStop = false;
            this.gbInputData.Text = "Datos de entrada";
            // 
            // tensionEnv
            // 
            this.tensionEnv.Enabled = false;
            this.tensionEnv.Location = new System.Drawing.Point(352, 102);
            this.tensionEnv.Name = "tensionEnv";
            this.tensionEnv.Size = new System.Drawing.Size(120, 20);
            this.tensionEnv.TabIndex = 11;
            // 
            // corrienteEnv
            // 
            this.corrienteEnv.Enabled = false;
            this.corrienteEnv.Location = new System.Drawing.Point(108, 102);
            this.corrienteEnv.Name = "corrienteEnv";
            this.corrienteEnv.Size = new System.Drawing.Size(120, 20);
            this.corrienteEnv.TabIndex = 10;
            // 
            // tempEnviada
            // 
            this.tempEnviada.Enabled = false;
            this.tempEnviada.Location = new System.Drawing.Point(352, 50);
            this.tempEnviada.Name = "tempEnviada";
            this.tempEnviada.Size = new System.Drawing.Size(120, 20);
            this.tempEnviada.TabIndex = 9;
            // 
            // txtIdMachine
            // 
            this.txtIdMachine.Enabled = false;
            this.txtIdMachine.Location = new System.Drawing.Point(108, 50);
            this.txtIdMachine.Name = "txtIdMachine";
            this.txtIdMachine.Size = new System.Drawing.Size(120, 20);
            this.txtIdMachine.TabIndex = 8;
            // 
            // tension
            // 
            this.tension.Location = new System.Drawing.Point(352, 76);
            this.tension.Maximum = new decimal(new int[] {
            220,
            0,
            0,
            0});
            this.tension.Name = "tension";
            this.tension.Size = new System.Drawing.Size(120, 20);
            this.tension.TabIndex = 7;
            this.tension.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.tension.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp);
            // 
            // lbVarQuantity
            // 
            this.lbVarQuantity.AutoSize = true;
            this.lbVarQuantity.Location = new System.Drawing.Point(298, 78);
            this.lbVarQuantity.Name = "lbVarQuantity";
            this.lbVarQuantity.Size = new System.Drawing.Size(48, 13);
            this.lbVarQuantity.TabIndex = 6;
            this.lbVarQuantity.Text = "Tensión:";
            // 
            // temperatura
            // 
            this.temperatura.Location = new System.Drawing.Point(352, 24);
            this.temperatura.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.temperatura.Name = "temperatura";
            this.temperatura.Size = new System.Drawing.Size(120, 20);
            this.temperatura.TabIndex = 5;
            this.temperatura.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.temperatura.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp);
            // 
            // lbStartAddress
            // 
            this.lbStartAddress.AutoSize = true;
            this.lbStartAddress.Location = new System.Drawing.Point(276, 26);
            this.lbStartAddress.Name = "lbStartAddress";
            this.lbStartAddress.Size = new System.Drawing.Size(70, 13);
            this.lbStartAddress.TabIndex = 4;
            this.lbStartAddress.Text = "Temperatura:";
            this.lbStartAddress.Click += new System.EventHandler(this.lbStartAddress_Click);
            // 
            // corriente
            // 
            this.corriente.Location = new System.Drawing.Point(108, 76);
            this.corriente.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.corriente.Name = "corriente";
            this.corriente.Size = new System.Drawing.Size(120, 20);
            this.corriente.TabIndex = 3;
            this.corriente.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.corriente.ValueChanged += new System.EventHandler(this.ValueChanged);
            this.corriente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyUp);
            // 
            // lbFunctCode
            // 
            this.lbFunctCode.AutoSize = true;
            this.lbFunctCode.Location = new System.Drawing.Point(50, 78);
            this.lbFunctCode.Name = "lbFunctCode";
            this.lbFunctCode.Size = new System.Drawing.Size(52, 13);
            this.lbFunctCode.TabIndex = 2;
            this.lbFunctCode.Text = "Corriente:";
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
            this.lbIdMachine.Location = new System.Drawing.Point(46, 26);
            this.lbIdMachine.Name = "lbIdMachine";
            this.lbIdMachine.Size = new System.Drawing.Size(56, 13);
            this.lbIdMachine.TabIndex = 0;
            this.lbIdMachine.Text = "ID equipo:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTrama);
            this.groupBox1.Location = new System.Drawing.Point(13, 229);
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
            // FechayHoraEnviada
            // 
            this.FechayHoraEnviada.Enabled = false;
            this.FechayHoraEnviada.Location = new System.Drawing.Point(208, 173);
            this.FechayHoraEnviada.Name = "FechayHoraEnviada";
            this.FechayHoraEnviada.Size = new System.Drawing.Size(120, 20);
            this.FechayHoraEnviada.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Fecha y Hora:";
            // 
            // FechayHora
            // 
            this.FechayHora.Enabled = false;
            this.FechayHora.Location = new System.Drawing.Point(208, 146);
            this.FechayHora.Name = "FechayHora";
            this.FechayHora.Size = new System.Drawing.Size(120, 20);
            this.FechayHora.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(369, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 34);
            this.button1.TabIndex = 16;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 301);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbInputData);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.gbInputData.ResumeLayout(false);
            this.gbInputData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tension)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperatura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.corriente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIdMachine)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbInputData;
		private System.Windows.Forms.NumericUpDown corriente;
		private System.Windows.Forms.Label lbFunctCode;
		private System.Windows.Forms.NumericUpDown nudIdMachine;
		private System.Windows.Forms.Label lbIdMachine;
		private System.Windows.Forms.NumericUpDown tension;
		private System.Windows.Forms.Label lbVarQuantity;
		private System.Windows.Forms.NumericUpDown temperatura;
		private System.Windows.Forms.Label lbStartAddress;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtTrama;
		private System.Windows.Forms.TextBox tensionEnv;
		private System.Windows.Forms.TextBox corrienteEnv;
		private System.Windows.Forms.TextBox tempEnviada;
		private System.Windows.Forms.TextBox txtIdMachine;
        private System.Windows.Forms.TextBox FechayHoraEnviada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FechayHora;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
	}
}

