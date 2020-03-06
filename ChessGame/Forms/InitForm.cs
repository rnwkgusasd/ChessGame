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
    public partial class InitForm : Form
    {
        public InitForm()
        {
            InitializeComponent();
        }

        private void LOCAL_BTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HOST_BTN_Click(object sender, EventArgs e)
        {
            GameForm MainFrm = new GameForm(false);

            MainFrm.Show();

            this.Close();
        }

        private void CLIENT_BTN_Click(object sender, EventArgs e)
        {
            IPSetForm frm = new IPSetForm();
            frm.eventHandler += GetIPEvent;

            if(frm.ShowDialog() == DialogResult.OK)
            {
                GameForm MainFrm = new GameForm(false);

                MainFrm.Show();

                //this.Close();
            }
        }

        private void GetIPEvent(string pData)
        {
            MessageBox.Show(pData);
        }

        private void InitForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
