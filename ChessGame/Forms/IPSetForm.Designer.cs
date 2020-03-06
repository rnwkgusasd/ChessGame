namespace ChessGame
{
    partial class IPSetForm
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
            this.CANC_BTN = new System.Windows.Forms.Button();
            this.IP_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CONN_BTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CANC_BTN
            // 
            this.CANC_BTN.Location = new System.Drawing.Point(26, 115);
            this.CANC_BTN.Name = "CANC_BTN";
            this.CANC_BTN.Size = new System.Drawing.Size(102, 39);
            this.CANC_BTN.TabIndex = 0;
            this.CANC_BTN.Text = "CANCEL";
            this.CANC_BTN.UseVisualStyleBackColor = true;
            this.CANC_BTN.Click += new System.EventHandler(this.CANC_BTN_Click);
            // 
            // IP_TextBox
            // 
            this.IP_TextBox.Font = new System.Drawing.Font("굴림", 12F);
            this.IP_TextBox.Location = new System.Drawing.Point(68, 54);
            this.IP_TextBox.Name = "IP_TextBox";
            this.IP_TextBox.Size = new System.Drawing.Size(176, 26);
            this.IP_TextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 12F);
            this.label1.Location = new System.Drawing.Point(24, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP : ";
            // 
            // CONN_BTN
            // 
            this.CONN_BTN.Location = new System.Drawing.Point(142, 115);
            this.CONN_BTN.Name = "CONN_BTN";
            this.CONN_BTN.Size = new System.Drawing.Size(102, 39);
            this.CONN_BTN.TabIndex = 4;
            this.CONN_BTN.Text = "CONNECT";
            this.CONN_BTN.UseVisualStyleBackColor = true;
            this.CONN_BTN.Click += new System.EventHandler(this.CONN_BTN_Click);
            // 
            // IPSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(275, 166);
            this.Controls.Add(this.CONN_BTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IP_TextBox);
            this.Controls.Add(this.CANC_BTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "IPSetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IPSetForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.IPSetForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CANC_BTN;
        private System.Windows.Forms.TextBox IP_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CONN_BTN;
    }
}