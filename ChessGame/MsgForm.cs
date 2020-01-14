using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class MsgForm : Form
    {
        public MsgForm(string pMsg)
        {
            InitializeComponent();

            lblText.Text = pMsg;
        }

        public MsgForm(string pMsg, string pWinner)
        {
            InitializeComponent();

            lblText.Text = String.Format($"{pMsg} {pWinner}");
        }

        public static DialogResult ShowDialog(string pMsg)
        {
            MsgForm frm = new MsgForm(pMsg);

            return frm.ShowDialog();
        }

        public static DialogResult ShowDialog(string pMsg, string pWinner)
        {
            MsgForm frm = new MsgForm(pMsg, pWinner);

            return frm.ShowDialog();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
