namespace ReadWriteRS232
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbComPort = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.numLength = new System.Windows.Forms.NumericUpDown();
			this.numDevice = new System.Windows.Forms.NumericUpDown();
			this.numAddress = new System.Windows.Forms.NumericUpDown();
			this.txtActivity = new System.Windows.Forms.TextBox();
			this.btConnect = new System.Windows.Forms.Button();
			this.btStop = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.cbFunction = new System.Windows.Forms.ComboBox();
			this.numTimeout = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.numRetry = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.clcTextArea = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.numAppPort = new System.Windows.Forms.NumericUpDown();
			this.lbAppPort = new System.Windows.Forms.Label();
			this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDevice)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numAddress)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numRetry)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numAppPort)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Dirección:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Dispositivo:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(275, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Largo/valor:";
			// 
			// cbComPort
			// 
			this.cbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbComPort.FormattingEnabled = true;
			this.cbComPort.Location = new System.Drawing.Point(85, 64);
			this.cbComPort.Name = "cbComPort";
			this.cbComPort.Size = new System.Drawing.Size(173, 21);
			this.cbComPort.TabIndex = 2;
			this.cbComPort.SelectedIndexChanged += new System.EventHandler(this.cbComPort_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Puerto com.:";
			// 
			// numLength
			// 
			this.numLength.Location = new System.Drawing.Point(348, 38);
			this.numLength.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
			this.numLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numLength.Name = "numLength";
			this.numLength.Size = new System.Drawing.Size(173, 20);
			this.numLength.TabIndex = 1;
			this.numLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numDevice
			// 
			this.numDevice.Location = new System.Drawing.Point(85, 12);
			this.numDevice.Name = "numDevice";
			this.numDevice.Size = new System.Drawing.Size(173, 20);
			this.numDevice.TabIndex = 9;
			this.numDevice.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numAddress
			// 
			this.numAddress.Location = new System.Drawing.Point(85, 38);
			this.numAddress.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.numAddress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numAddress.Name = "numAddress";
			this.numAddress.Size = new System.Drawing.Size(173, 20);
			this.numAddress.TabIndex = 11;
			this.numAddress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// txtActivity
			// 
			this.txtActivity.Location = new System.Drawing.Point(12, 146);
			this.txtActivity.Multiline = true;
			this.txtActivity.Name = "txtActivity";
			this.txtActivity.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtActivity.Size = new System.Drawing.Size(509, 203);
			this.txtActivity.TabIndex = 8;
			this.txtActivity.WordWrap = false;
			// 
			// btConnect
			// 
			this.btConnect.Location = new System.Drawing.Point(15, 117);
			this.btConnect.Name = "btConnect";
			this.btConnect.Size = new System.Drawing.Size(75, 23);
			this.btConnect.TabIndex = 5;
			this.btConnect.Text = "Conectar";
			this.btConnect.UseVisualStyleBackColor = true;
			this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
			// 
			// btStop
			// 
			this.btStop.Enabled = false;
			this.btStop.Location = new System.Drawing.Point(183, 117);
			this.btStop.Name = "btStop";
			this.btStop.Size = new System.Drawing.Size(75, 23);
			this.btStop.TabIndex = 6;
			this.btStop.Text = "Detener";
			this.btStop.UseVisualStyleBackColor = true;
			this.btStop.Click += new System.EventHandler(this.btStop_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(275, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 13);
			this.label5.TabIndex = 15;
			this.label5.Text = "Función:";
			// 
			// cbFunction
			// 
			this.cbFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFunction.FormattingEnabled = true;
			this.cbFunction.Items.AddRange(new object[] {
            "3",
            "6"});
			this.cbFunction.Location = new System.Drawing.Point(348, 12);
			this.cbFunction.Name = "cbFunction";
			this.cbFunction.Size = new System.Drawing.Size(173, 21);
			this.cbFunction.TabIndex = 10;
			// 
			// numTimeout
			// 
			this.numTimeout.Location = new System.Drawing.Point(85, 91);
			this.numTimeout.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.numTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numTimeout.Name = "numTimeout";
			this.numTimeout.Size = new System.Drawing.Size(173, 20);
			this.numTimeout.TabIndex = 3;
			this.numTimeout.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 95);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(70, 13);
			this.label6.TabIndex = 16;
			this.label6.Text = "Timeout (ms):";
			// 
			// numRetry
			// 
			this.numRetry.Location = new System.Drawing.Point(348, 91);
			this.numRetry.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.numRetry.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numRetry.Name = "numRetry";
			this.numRetry.Size = new System.Drawing.Size(173, 20);
			this.numRetry.TabIndex = 4;
			this.numRetry.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(275, 95);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(61, 13);
			this.label7.TabIndex = 18;
			this.label7.Text = "Reintentos:";
			// 
			// clcTextArea
			// 
			this.clcTextArea.Location = new System.Drawing.Point(446, 117);
			this.clcTextArea.Name = "clcTextArea";
			this.clcTextArea.Size = new System.Drawing.Size(75, 23);
			this.clcTextArea.TabIndex = 7;
			this.clcTextArea.Text = "Limpiar";
			this.clcTextArea.UseVisualStyleBackColor = true;
			this.clcTextArea.Click += new System.EventHandler(this.clcTextArea_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.valor});
			this.dataGridView1.Location = new System.Drawing.Point(527, 16);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(271, 333);
			this.dataGridView1.TabIndex = 19;
			// 
			// numAppPort
			// 
			this.numAppPort.Enabled = false;
			this.numAppPort.Location = new System.Drawing.Point(348, 66);
			this.numAppPort.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
			this.numAppPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numAppPort.Name = "numAppPort";
			this.numAppPort.Size = new System.Drawing.Size(173, 20);
			this.numAppPort.TabIndex = 20;
			this.numAppPort.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
			// 
			// lbAppPort
			// 
			this.lbAppPort.AutoSize = true;
			this.lbAppPort.Enabled = false;
			this.lbAppPort.Location = new System.Drawing.Point(275, 70);
			this.lbAppPort.Name = "lbAppPort";
			this.lbAppPort.Size = new System.Drawing.Size(69, 13);
			this.lbAppPort.TabIndex = 21;
			this.lbAppPort.Text = "Puerto aplic.:";
			// 
			// nombre
			// 
			this.nombre.DataPropertyName = "nombre";
			this.nombre.HeaderText = "Nombre";
			this.nombre.Name = "nombre";
			this.nombre.ReadOnly = true;
			// 
			// valor
			// 
			this.valor.DataPropertyName = "valor";
			this.valor.HeaderText = "Valor";
			this.valor.Name = "valor";
			this.valor.ReadOnly = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(814, 368);
			this.Controls.Add(this.numAppPort);
			this.Controls.Add(this.lbAppPort);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.clcTextArea);
			this.Controls.Add(this.numRetry);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.numTimeout);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cbFunction);
			this.Controls.Add(this.btStop);
			this.Controls.Add(this.btConnect);
			this.Controls.Add(this.txtActivity);
			this.Controls.Add(this.numAddress);
			this.Controls.Add(this.numDevice);
			this.Controls.Add(this.numLength);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cbComPort);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDevice)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numAddress)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numRetry)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numAppPort)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbComPort;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numLength;
		private System.Windows.Forms.NumericUpDown numDevice;
		private System.Windows.Forms.NumericUpDown numAddress;
		private System.Windows.Forms.Button btConnect;
		private System.Windows.Forms.Button btStop;
		private System.Windows.Forms.TextBox txtActivity;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbFunction;
		private System.Windows.Forms.NumericUpDown numTimeout;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numRetry;
		private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button clcTextArea;
        private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.NumericUpDown numAppPort;
		private System.Windows.Forms.Label lbAppPort;
		private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn valor;

	}
}

