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
    public delegate void DataGetEventHandler(string pData);

    public partial class IPSetForm : Form
    {
        public DataGetEventHandler eventHandler;

        public IPSetForm()
        {
            InitializeComponent();
        }

        private void IPSetForm_Load(object sender, EventArgs e)
        {

        }

        private void CANC_BTN_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CONN_BTN_Click(object sender, EventArgs e)
        {
            eventHandler(IP_TextBox.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
