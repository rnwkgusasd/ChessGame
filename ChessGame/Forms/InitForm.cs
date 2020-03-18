using ChessGame.Classes;
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
            GameForm MainFrm = new GameForm(false);

            MainFrm.Show();

            this.Hide();
        }

        private void HOST_BTN_Click(object sender, EventArgs e)
        {
            GameForm MainFrm = new GameForm(false, true);

            MainFrm.Show();

            this.Hide();
            
            GlobalVariable.ServerSocket.Open("127.0.0.1", 11000, 5);
        }

        private string mIP;

        private void CLIENT_BTN_Click(object sender, EventArgs e)
        {
            IPSetForm frm = new IPSetForm();
            frm.eventHandler += GetIPEvent;

            if(frm.ShowDialog() == DialogResult.OK)
            {
                GameForm MainFrm = new GameForm(false);

                MainFrm.Show();

                GlobalVariable.ClientSocket.Open("127.0.0.1", 11000);

                this.Hide();
            }
        }

        private void GetIPEvent(string pData)
        {
            mIP = pData;
            //MessageBox.Show(mIP);
        }

        private void InitForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //GlobalVariable.ClientSocket.Send("red,p,4-4,5-5");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //GlobalVariable.ServerSocket.Send("blue,p,4-4,5-5");
        }
    }
}
