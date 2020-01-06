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
    public partial class Form1 : Form
    {
        public List<Button[]> ButtonList = new List<Button[]>();

        public static int BOARD_ROW_COUNT       = 8;
        public static int BOARD_COLUMN_COUNT    = 8;
        
        public class ObjectsNames
        {
            public static string PAWN = "pa";
            public static string CASTLE = "ca";
            public static string KNIGHT = "kn";
            public static string VISHOP = "vi";
            public static string QUEEN = "Qu";
            public static string KING = "Ki";
        }

        public Form1()
        {
            this.BackColor = Color.Gray;
            InitializeComponent();

            InitBoard();
            ShowBoard();
        }

        private void InitBoard()
        {
            ButtonList.Clear();
            
            for(int i = 0; i < BOARD_ROW_COUNT; i++)
            {
                Button[] tButtons = new Button[BOARD_COLUMN_COUNT];

                for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                {
                    tButtons[j] = new Button();
                    tButtons[j].FlatStyle = FlatStyle.Flat;
                    tButtons[j].FlatAppearance.BorderColor = (i + j) % 2 == 0 ? Color.White : Color.Black;
                    tButtons[j].BackColor = (i + j) % 2 == 0 ? Color.White : Color.Black;
                    tButtons[j].ForeColor = Color.Yellow;
                    tButtons[j].Size = new Size(50, 50);
                }

                ButtonList.Add(tButtons);
            }
        }

        private void ShowBoard()
        {
            for(int i = 0; i < BOARD_ROW_COUNT; i++)
            {
                Label Alphabet = new Label();
                
                Alphabet.Text = Convert.ToChar('A' + i).ToString();
                Alphabet.Size = new Size(20, 20);
                Alphabet.Location = new Point(190 + (i * 55), 18);

                Controls.Add(Alphabet);

                for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                {
                    ButtonList[i][j].Location = ButtonList[i][j].PointToClient(new Point(180 + (j * 55), 70 + (i * 55)));

                    if (j == 0)
                    {
                        Label Number = new Label();

                        Number.Text = (8 - i).ToString();
                        Number.Size = new Size(20, 20);
                        Number.Location = new Point(150, 60 + (i * 55));
                        
                        Controls.Add(Number);
                    }

                    Controls.Add(ButtonList[i][j]);
                }
            }
        }

        private void SetBoard()
        {

        }
    }
}
