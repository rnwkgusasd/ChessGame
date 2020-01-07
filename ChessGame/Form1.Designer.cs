namespace ChessGame
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.lblBtnloc = new System.Windows.Forms.Label();
            this.lblBtnText = new System.Windows.Forms.Label();
            this.lblBtnColor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(30, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lblBtnloc
            // 
            this.lblBtnloc.AutoSize = true;
            this.lblBtnloc.Location = new System.Drawing.Point(28, 163);
            this.lblBtnloc.Name = "lblBtnloc";
            this.lblBtnloc.Size = new System.Drawing.Size(103, 12);
            this.lblBtnloc.TabIndex = 1;
            this.lblBtnloc.Text = "CLICK_BTN_LOC";
            // 
            // lblBtnText
            // 
            this.lblBtnText.AutoSize = true;
            this.lblBtnText.Location = new System.Drawing.Point(28, 188);
            this.lblBtnText.Name = "lblBtnText";
            this.lblBtnText.Size = new System.Drawing.Size(110, 12);
            this.lblBtnText.TabIndex = 2;
            this.lblBtnText.Text = "CLICK_BTN_TEXT";
            // 
            // lblBtnColor
            // 
            this.lblBtnColor.AutoSize = true;
            this.lblBtnColor.Location = new System.Drawing.Point(28, 209);
            this.lblBtnColor.Name = "lblBtnColor";
            this.lblBtnColor.Size = new System.Drawing.Size(120, 12);
            this.lblBtnColor.TabIndex = 3;
            this.lblBtnColor.Text = "CLICK_BTN_COLOR";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 524);
            this.Controls.Add(this.lblBtnColor);
            this.Controls.Add(this.lblBtnText);
            this.Controls.Add(this.lblBtnloc);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblBtnloc;
        private System.Windows.Forms.Label lblBtnText;
        private System.Windows.Forms.Label lblBtnColor;
    }
}

