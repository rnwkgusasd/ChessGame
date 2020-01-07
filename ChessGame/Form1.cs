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

        public List<ChessObject> BlueTeamList   = new List<ChessObject>();
        public List<ChessObject> RedTeamList    = new List<ChessObject>();

        public static int BOARD_ROW_COUNT       = 8;
        public static int BOARD_COLUMN_COUNT    = 8;

        private int ClickX = 0;
        private int ClickY = 0;

        private bool ChessDoing = false;
        
        public class ChessObject
        {
            private readonly string  Name        = "";
            private int     MoveCount   = 0;
            private int     X           = 0;
            private int     Y           = 0;

            private readonly Color TeamColor;

            public ChessObject(string pName, Color tColor)
            {
                Name = pName;
                TeamColor = tColor;
            }

            public string GetName()
            {
                return Name;
            }

            public Color GetColor()
            {
                return TeamColor;
            }

            public int GetMoveCount()
            {
                return MoveCount;
            }

            public void SetLocation(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void MoveTo(int x, int y)
            {
                SetLocation(x, y);
                MoveCount++;
            }

            public static string PAWN   = "pa";
            public static string CASTLE = "ca";
            public static string KNIGHT = "kn";
            public static string VISHOP = "vi";
            public static string QUEEN  = "Qu";
            public static string KING   = "Ki";
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
                    tButtons[j] = new Button
                    {
                        FlatStyle = FlatStyle.Flat,
                        Size = new Size(50, 50),
                        BackColor = (i + j) % 2 == 0 ? Color.White : Color.Black
                    };
                    tButtons[j].FlatAppearance.BorderColor = Color.Gray;
                    tButtons[j].Click += ButtonClickEvent;
                }
                ButtonList.Add(tButtons);
            }
        }

        private void ButtonClickEvent(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;

            if(tmp.BackColor != Color.Chartreuse)
            {
                ClickX = (tmp.Location.X - 172) / 55;
                ClickY = (tmp.Location.Y - 39) / 55;

                char ch = Convert.ToChar('A' + ClickX);

                lblBtnloc.Text = String.Format($"X : {ch}, Y : {8 - ClickY}");

                if(!ChessDoing)
                {
                    if (tmp.ForeColor.ToString() != "Color [ControlText]") lblBtnColor.Text = String.Format($"{tmp.ForeColor.ToString()}");
                    if (tmp.Text != "") lblBtnText.Text = String.Format($"{tmp.Text}");
                }

                MoveableLocation(ClickX, ClickY, ButtonList[ClickY][ClickX]);
            }
            else
            {

            }
        }

        private void MoveableLocation(int x, int y, Button pButton)
        {
            Color pBorderColor = pButton.FlatAppearance.BorderColor;

            if (pButton.Text == "" && pBorderColor == Color.Gray) return;

            if(!ChessDoing)
            {
                for (int i = 0; i < BOARD_ROW_COUNT; i++)
                {
                    for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                    {
                        ButtonList[i][j].FlatAppearance.BorderColor = Color.Gray;
                    }
                }
            }

            if (pBorderColor == Color.Crimson)
            {
                ButtonList[y][x].Text = lblBtnText.Text;
                ButtonList[y][x].ForeColor = lblBtnColor.Text == "Color [Red]" ? Color.Red : Color.Blue;
                ChessDoing = false;

                for (int i = 0; i < BOARD_ROW_COUNT; i++)
                {
                    for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                    {
                        ButtonList[i][j].FlatAppearance.BorderColor = Color.Gray;
                    }
                }

                return;
            }

            if(!ChessDoing)
            {
                switch (pButton.Text)
                {
                    case "pa":
                        if (true)
                        {
                            int CanMove = 1;

                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (BlueTeamList[x].GetMoveCount() == 0) CanMove = 2;

                                for (int i = 1; i <= CanMove && i + y < BOARD_ROW_COUNT; i++)
                                {
                                    ButtonList[y + i][x].FlatAppearance.BorderColor = Color.Crimson;
                                }
                            }
                            else
                            {
                                if (RedTeamList[x].GetMoveCount() == 0) CanMove = 2;

                                for (int i = 1; i <= CanMove && y - i < BOARD_ROW_COUNT; i++)
                                {
                                    ButtonList[y - i][x].FlatAppearance.BorderColor = Color.Crimson;
                                }
                            }

                            ButtonList[y][x].Text = "";
                            ChessDoing = true;
                        }
                        break;

                    case "ca":
                        if (pButton.BackColor == Color.Blue)
                        {

                        }
                        else if (pButton.BackColor == Color.Red)
                        {

                        }
                        break;

                    case "kn":
                        if (pButton.BackColor == Color.Blue)
                        {

                        }
                        else if (pButton.BackColor == Color.Red)
                        {

                        }
                        break;

                    case "vi":
                        if (pButton.BackColor == Color.Blue)
                        {

                        }
                        else if (pButton.BackColor == Color.Red)
                        {

                        }
                        break;

                    case "Qu":
                        if (pButton.BackColor == Color.Blue)
                        {

                        }
                        else if (pButton.BackColor == Color.Red)
                        {

                        }
                        break;

                    case "Ki":
                        if (pButton.BackColor == Color.Blue)
                        {

                        }
                        else if (pButton.BackColor == Color.Red)
                        {

                        }
                        break;
                }
            }
        }

        private void ShowBoard()
        {
            for(int i = 0; i < BOARD_ROW_COUNT; i++)
            {
                Label Alphabet = new Label
                {
                    Text = Convert.ToChar('A' + i).ToString(),
                    Size = new Size(20, 20),
                    Location = new Point(190 + (i * 55), 18)
                };

                Controls.Add(Alphabet);

                for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                {
                    ButtonList[i][j].Location = ButtonList[i][j].PointToClient(new Point(180 + (j * 55), 70 + (i * 55)));

                    if (j == 0)
                    {
                        Label Number = new Label
                        {
                            Text = (8 - i).ToString(),
                            Size = new Size(20, 20),
                            Location = new Point(150, 60 + (i * 55))
                        };

                        Controls.Add(Number);
                    }
                    Controls.Add(ButtonList[i][j]);
                }
            }
            SetBoard();
        }

        private void SetChess()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 0) BlueTeamList.Add(new ChessObject(ChessObject.PAWN, Color.Blue));

                    if (i == 1) RedTeamList.Add(new ChessObject(ChessObject.PAWN, Color.Red));
                }
            }

            ListSet(ref BlueTeamList, Color.Blue);
            ListSet(ref RedTeamList, Color.Red);
        }

        private void ListSet(ref List<ChessObject> pList, Color pColor)
        {
            pList.Add(new ChessObject(ChessObject.CASTLE, pColor));
            pList.Add(new ChessObject(ChessObject.KNIGHT, pColor));
            pList.Add(new ChessObject(ChessObject.VISHOP, pColor));
            pList.Add(new ChessObject(ChessObject.KNIGHT, pColor));
            pList.Add(new ChessObject(ChessObject.QUEEN,  pColor));
            pList.Add(new ChessObject(ChessObject.VISHOP, pColor));
            pList.Add(new ChessObject(ChessObject.KNIGHT, pColor));
            pList.Add(new ChessObject(ChessObject.CASTLE, pColor));
        }

        private void SetBoard()
        {
            SetChess();

            for (int i = 0; i < BOARD_ROW_COUNT; i++)
            {
                for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                {
                    if (i == 1) ButtonList[i][j].Text = BlueTeamList[i].GetName();
                    if (i == 6) ButtonList[i][j].Text = RedTeamList[i].GetName();

                    if (i == 0 || i == 1) ButtonList[i][j].ForeColor = Color.Blue;
                    if (i == 6 || i == 7) ButtonList[i][j].ForeColor = Color.Red;
                }
            }

            ButtonSet();
        }

        private void ButtonSet()
        {
            ButtonList[0][0].Text = BlueTeamList[8].GetName();
            ButtonList[0][1].Text = BlueTeamList[9].GetName();
            ButtonList[0][2].Text = BlueTeamList[10].GetName();
            ButtonList[0][3].Text = BlueTeamList[11].GetName();
            ButtonList[0][4].Text = BlueTeamList[12].GetName();
            ButtonList[0][5].Text = BlueTeamList[13].GetName();
            ButtonList[0][6].Text = BlueTeamList[14].GetName();
            ButtonList[0][7].Text = BlueTeamList[15].GetName();

            ButtonList[7][0].Text = RedTeamList[8].GetName();
            ButtonList[7][1].Text = RedTeamList[9].GetName();
            ButtonList[7][2].Text = RedTeamList[10].GetName();
            ButtonList[7][3].Text = RedTeamList[11].GetName();
            ButtonList[7][4].Text = RedTeamList[12].GetName();
            ButtonList[7][5].Text = RedTeamList[13].GetName();
            ButtonList[7][6].Text = RedTeamList[14].GetName();
            ButtonList[7][7].Text = RedTeamList[15].GetName();
        }
    }
}
