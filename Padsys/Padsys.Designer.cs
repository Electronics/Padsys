namespace Padsys {
    partial class Padsys {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.textBox_ip = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox_port = new System.Windows.Forms.TextBox();
			this.textBox_execPage = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// buttonPanel
			// 
			this.buttonPanel.Location = new System.Drawing.Point(2, 40);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(924, 823);
			this.buttonPanel.TabIndex = 0;
			// 
			// textBox_ip
			// 
			this.textBox_ip.Location = new System.Drawing.Point(76, 10);
			this.textBox_ip.Name = "textBox_ip";
			this.textBox_ip.Size = new System.Drawing.Size(86, 22);
			this.textBox_ip.TabIndex = 1;
			this.textBox_ip.Text = "127.0.0.1";
			this.textBox_ip.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_ip_Validating);
			this.textBox_ip.Validated += new System.EventHandler(this.textBox_ip_Validated);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "OSC IP:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(168, 13);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "OSC Port";
			// 
			// textBox_port
			// 
			this.textBox_port.Location = new System.Drawing.Point(241, 10);
			this.textBox_port.Name = "textBox_port";
			this.textBox_port.Size = new System.Drawing.Size(86, 22);
			this.textBox_port.TabIndex = 4;
			this.textBox_port.Text = "8000";
			this.textBox_port.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_port_Validating);
			this.textBox_port.Validated += new System.EventHandler(this.textBox_port_Validated);
			// 
			// textBox_execPage
			// 
			this.textBox_execPage.Location = new System.Drawing.Point(418, 10);
			this.textBox_execPage.Name = "textBox_execPage";
			this.textBox_execPage.Size = new System.Drawing.Size(39, 22);
			this.textBox_execPage.TabIndex = 6;
			this.textBox_execPage.Text = "2";
			this.textBox_execPage.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_execPage_Validating);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(333, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "Exec Page:";
			// 
			// Padsys
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1122, 1007);
			this.Controls.Add(this.textBox_execPage);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBox_port);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox_ip);
			this.Controls.Add(this.buttonPanel);
			this.Name = "Padsys";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Padsys_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel buttonPanel;
		private System.Windows.Forms.TextBox textBox_ip;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox_port;
		private System.Windows.Forms.TextBox textBox_execPage;
		private System.Windows.Forms.Label label3;
	}
}

