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
    public partial class SelectObject : Form
    {
        private string[] ObjectArray;

        public SelectObject()
        {
            InitializeComponent();
        }

        public void SetArray(string[] pArray)
        {
            ObjectArray = pArray;
        }

        public string GetString()
        {
            return ObjectArrayComboBox.SelectedItem.ToString();
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void SelectObject_Load(object sender, EventArgs e)
        {
            ObjectArrayComboBox.Items.AddRange(ObjectArray);
            ObjectArrayComboBox.SelectedIndex = 0;
        }
    }
}
