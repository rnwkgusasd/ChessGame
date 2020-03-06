namespace ChessGame
{
    partial class InitForm
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
            this.LOCAL_BTN = new System.Windows.Forms.Button();
            this.HOST_BTN = new System.Windows.Forms.Button();
            this.CLIENT_BTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LOCAL_BTN
            // 
            this.LOCAL_BTN.Location = new System.Drawing.Point(117, 184);
            this.LOCAL_BTN.Name = "LOCAL_BTN";
            this.LOCAL_BTN.Size = new System.Drawing.Size(271, 65);
            this.LOCAL_BTN.TabIndex = 0;
            this.LOCAL_BTN.Text = "Local Game";
            this.LOCAL_BTN.UseVisualStyleBackColor = true;
            this.LOCAL_BTN.Click += new System.EventHandler(this.LOCAL_BTN_Click);
            // 
            // HOST_BTN
            // 
            this.HOST_BTN.Location = new System.Drawing.Point(117, 257);
            this.HOST_BTN.Name = "HOST_BTN";
            this.HOST_BTN.Size = new System.Drawing.Size(271, 65);
            this.HOST_BTN.TabIndex = 1;
            this.HOST_BTN.Text = "Host";
            this.HOST_BTN.UseVisualStyleBackColor = true;
            this.HOST_BTN.Click += new System.EventHandler(this.HOST_BTN_Click);
            // 
            // CLIENT_BTN
            // 
            this.CLIENT_BTN.Location = new System.Drawing.Point(117, 328);
            this.CLIENT_BTN.Name = "CLIENT_BTN";
            this.CLIENT_BTN.Size = new System.Drawing.Size(271, 65);
            this.CLIENT_BTN.TabIndex = 2;
            this.CLIENT_BTN.Text = "Client";
            this.CLIENT_BTN.UseVisualStyleBackColor = true;
            this.CLIENT_BTN.Click += new System.EventHandler(this.CLIENT_BTN_Click);
            // 
            // InitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 437);
            this.Controls.Add(this.CLIENT_BTN);
            this.Controls.Add(this.HOST_BTN);
            this.Controls.Add(this.LOCAL_BTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InitForm";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.InitForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LOCAL_BTN;
        private System.Windows.Forms.Button HOST_BTN;
        private System.Windows.Forms.Button CLIENT_BTN;
    }
}