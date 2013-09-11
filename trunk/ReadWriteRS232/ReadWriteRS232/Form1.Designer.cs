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
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.numDevice = new System.Windows.Forms.NumericUpDown();
            this.numAddress = new System.Windows.Forms.NumericUpDown();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.btConnect = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbFunction = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDevice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 73);
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
            this.label2.TabIndex = 3;
            this.label2.Text = "Dispositivo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Largo/valor:";
            // 
            // cbPort
            // 
            this.cbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Items.AddRange(new object[] {
            "COM3",
            "COM4",
            "COM7",
            "COM8"});
            this.cbPort.Location = new System.Drawing.Point(85, 127);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(173, 21);
            this.cbPort.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Puerto:";
            // 
            // numLength
            // 
            this.numLength.Location = new System.Drawing.Point(85, 99);
            this.numLength.Maximum = new decimal(new int[] {
            1000,
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
            this.numLength.TabIndex = 8;
            this.numLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numDevice
            // 
            this.numDevice.Location = new System.Drawing.Point(85, 14);
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
            this.numAddress.Location = new System.Drawing.Point(85, 71);
            this.numAddress.Name = "numAddress";
            this.numAddress.Size = new System.Drawing.Size(173, 20);
            this.numAddress.TabIndex = 10;
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(12, 183);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResponse.Size = new System.Drawing.Size(246, 203);
            this.txtResponse.TabIndex = 11;
            this.txtResponse.WordWrap = false;
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(15, 154);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 12;
            this.btConnect.Text = "Conectar";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(183, 154);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(75, 23);
            this.btStop.TabIndex = 13;
            this.btStop.Text = "Detener";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 44);
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
            this.cbFunction.Location = new System.Drawing.Point(85, 42);
            this.cbFunction.Name = "cbFunction";
            this.cbFunction.Size = new System.Drawing.Size(173, 21);
            this.cbFunction.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 389);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbFunction);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.numAddress);
            this.Controls.Add(this.numDevice);
            this.Controls.Add(this.numLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDevice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAddress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbPort;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numLength;
		private System.Windows.Forms.NumericUpDown numDevice;
		private System.Windows.Forms.NumericUpDown numAddress;
		private System.Windows.Forms.Button btConnect;
		private System.Windows.Forms.Button btStop;
		private System.Windows.Forms.TextBox txtResponse;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cbFunction;

	}
}

