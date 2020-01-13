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
        #region Member Variables
        public List<Button[]> ButtonList = new List<Button[]>();  // It contains all of board tiles.

        public List<ChessObject> BlueTeamList   = new List<ChessObject>(); // Blue team's chess objects.
        public List<ChessObject> RedTeamList    = new List<ChessObject>(); // Red team's chess objects.

        public static int BOARD_ROW_COUNT       = 8;
        public static int BOARD_COLUMN_COUNT    = 8;

        private int ClickX = 0;
        private int ClickY = 0;

        private int PrevX = 0;
        private int PrevY = 0;

        private bool ChessDoing = false;
        #endregion

        public class ChessObject
        {
            private readonly string  Name        = "";
            private int              MoveCount   = 0;
            private int              X           = 0;
            private int              Y           = 0;

            private readonly Color TeamColor;

            /// <summary>
            /// Make chess object with team color.
            /// </summary>
            /// <param name="pName"></param>
            /// <param name="tColor"></param>
            public ChessObject(string pName, Color tColor)
            {
                Name = pName;
                TeamColor = tColor;
            }

            /// <summary>
            /// Get object name.
            /// </summary>
            /// <returns></returns>
            public string GetName()
            {
                return Name;
            }

            /// <summary>
            /// Get object color.
            /// </summary>
            /// <returns></returns>
            public Color GetColor()
            {
                return TeamColor;
            }

            /// <summary>
            /// Get object move count.
            /// </summary>
            /// <returns></returns>
            public int GetMoveCount()
            {
                return MoveCount;
            }

            /// <summary>
            /// Get object current location.
            /// </summary>
            /// <returns></returns>
            public Point GetLocation()
            {
                return new Point(X, Y);
            }


            /// <summary>
            /// Set the object location.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public void SetLocation(int x, int y)
            {
                X = x;
                Y = y;
            }

            /// <summary>
            /// Move object location.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public void MoveTo(int x, int y)
            {
                SetLocation(x, y);
                MoveCount++;
            }

            #region Static Variables
            public static string PAWN   = "pa";
            public static string CASTLE = "ca";
            public static string KNIGHT = "kn";
            public static string VISHOP = "vi";
            public static string QUEEN  = "Qu";
            public static string KING   = "Ki";
            #endregion
        }

        public Form1()
        {
            this.BackColor = Color.Gray;
            InitializeComponent();

            InitBoard();
            ShowBoard();
        }

        /// <summary>
        /// Initialize chess board. (Create ButtonList)
        /// </summary>
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
                        BackColor = (i + j) % 2 == 0 ? Color.White : Color.Black,
                        ForeColor = Color.Black
                    };
                    tButtons[j].FlatAppearance.BorderColor = Color.Gray;  // Button design
                    tButtons[j].FlatAppearance.BorderSize = 2;
                    tButtons[j].Click += ButtonClickEvent;
                }
                ButtonList.Add(tButtons);
            }
        }

        /// <summary>
        /// Tiles click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickEvent(object sender, EventArgs e)
        {
            Button tmp = (Button)sender;
            
            if (tmp.BackColor != Color.Chartreuse)
            {
                ClickX = (tmp.Location.X - 172) / 55;
                ClickY = (tmp.Location.Y - 39) / 55;

                char ch = Convert.ToChar('A' + ClickX);

                lblBtnloc.Text = String.Format($"X : {ch}, Y : {8 - ClickY}");

                if(!ChessDoing)
                {
                    if (tmp.ForeColor.Name != "Black") lblBtnColor.Text = String.Format($"{tmp.ForeColor.Name}");
                    if (tmp.Text != "") lblBtnText.Text = String.Format($"{tmp.Text}");
                }
                
                MoveableLocation(ClickX, ClickY, ButtonList[ClickY][ClickX]);
            }
            else
            {

            }
        }

        /// <summary>
        /// Show moveable location (overlay) and move chess object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="pButton"></param>
        private void MoveableLocation(int x, int y, Button pButton)
        {
            Color pBorderColor = pButton.FlatAppearance.BorderColor;

            if (pButton.Text == "" && pBorderColor == Color.Gray) return;

            if (ChessDoing && pButton.Text == ChessObject.KING)
            {
                MessageBox.Show(String.Format(@"{0} Team Win", pButton.ForeColor == Color.Red ? "Blue" : "Red"));

                this.Dispose();
            }

            if (!ChessDoing)
            {
                for (int i = 0; i < BOARD_ROW_COUNT; i++)
                {
                    for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                    {
                        if(ButtonList[i][j].FlatAppearance.BorderColor != Color.Gray)
                            ButtonList[i][j].FlatAppearance.BorderColor = Color.Gray;
                    }
                } // All button remove overlay
            }

            if (pBorderColor == Color.Crimson || pBorderColor == Color.Chartreuse)
            {
                ButtonList[y][x].Text = lblBtnText.Text;
                ButtonList[y][x].ForeColor = lblBtnColor.Text == "Red" ? Color.Red : Color.Blue;
                ChessDoing = false;

                for (int i = 0; i < BOARD_ROW_COUNT; i++)
                {
                    for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                    {
                        ButtonList[i][j].FlatAppearance.BorderColor = Color.Gray;
                    }
                }

                if (pButton.ForeColor == Color.Blue && pBorderColor != Color.Chartreuse)
                {
                    for (int i = 0; i < BlueTeamList.Count; i++)
                    {
                        if (BlueTeamList[i].GetLocation() == new Point(PrevX, PrevY))
                        {
                            BlueTeamList[i].MoveTo(x, y);
                            break;
                        }
                    }
                }
                else if (pButton.ForeColor == Color.Red && pBorderColor != Color.Chartreuse)
                {
                    for (int i = 0; i < RedTeamList.Count; i++)
                    {
                        if (RedTeamList[i].GetLocation() == new Point(PrevX, PrevY))
                        {
                            RedTeamList[i].MoveTo(x, y);
                            break;
                        }
                    }
                }
                
                int redMove = 0;
                int blueMove = 0;
                
                redMove += RedTeamList.Sum(moveCount => moveCount.GetMoveCount());
                blueMove += BlueTeamList.Sum(moveCount => moveCount.GetMoveCount());

                lblTeamRedMove.Text = String.Format($"{redMove}");
                lblTeamBlueMove.Text = String.Format($"{blueMove}");

                return;
            }

            if(!ChessDoing)
            {
                #region Chess Object Move

                switch (pButton.Text)
                {
                    case "pa":
                        #region Pawn
                        int CanMove = 1;
                        int pMoveCount = 0;
                        ButtonList[y][x].Text = "";

                        if (pButton.ForeColor == Color.Blue)
                        {
                            for (int i = 0; i < BlueTeamList.Count; i++)
                            {
                                if (BlueTeamList[i].GetLocation() == new Point(x, y))
                                    pMoveCount = BlueTeamList[i].GetMoveCount();
                            } // find click locations chess object

                            if (pMoveCount == 0) CanMove = 2;

                            for (int i = 0; i <= CanMove && i + y < BOARD_ROW_COUNT && ButtonList[y + i][x].Text == ""; i++)
                            {
                                ButtonList[y + i][x].FlatAppearance.BorderColor = Color.Crimson;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < BlueTeamList.Count; i++)
                            {
                                if (RedTeamList[i].GetLocation() == new Point(x, y))
                                    pMoveCount = RedTeamList[i].GetMoveCount();
                            } // find click locations chess object

                            if (pMoveCount == 0) CanMove = 2;

                            for (int i = 0; i <= CanMove && y - i < BOARD_ROW_COUNT && ButtonList[y - i][x].Text == ""; i++)
                            {
                                ButtonList[y - i][x].FlatAppearance.BorderColor = Color.Crimson;
                            }
                        }

                        ButtonList[y][x].ForeColor = Color.Black;
                        ChessDoing = true;
                        #endregion
                        break;

                    case "ca":
                        #region Castle
                        ButtonList[y][x].Text = "";

                        for (int i = y - 1; i >= 0 && pButton.ForeColor != ButtonList[i][x].ForeColor; i--)
                        {
                            ButtonList[i][x].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[i][x].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[i][x].ForeColor == Color.Blue) break;
                            }
                        }

                        for (int i = y + 1; i < BOARD_ROW_COUNT && pButton.ForeColor != ButtonList[i][x].ForeColor; i++)
                        {
                            ButtonList[i][x].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[i][x].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[i][x].ForeColor == Color.Blue) break;
                            }
                        }

                        for (int i = x - 1; i >= 0 && pButton.ForeColor != ButtonList[y][i].ForeColor; i--)
                        {
                            ButtonList[y][i].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[y][i].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[y][i].ForeColor == Color.Blue) break;
                            }
                        }

                        for (int i = x + 1; i < BOARD_COLUMN_COUNT && pButton.ForeColor != ButtonList[y][i].ForeColor; i++)
                        {
                            ButtonList[y][i].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[y][i].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[y][i].ForeColor == Color.Blue) break;
                            }
                        }
                        
                        ButtonList[y][x].ForeColor = Color.Black;
                        ChessDoing = true;
                        #endregion
                        break;

                    case "kn":
                        #region Knight
                        ButtonList[y][x].Text = "";

                        if (true) // Up Side
                        {
                            if (y + 2 < BOARD_ROW_COUNT && x - 1 >= 0 && ButtonList[y + 2][x - 1].ForeColor != pButton.ForeColor)
                                ButtonList[y + 2][x - 1].FlatAppearance.BorderColor = Color.Crimson;

                            if (y + 2 < BOARD_ROW_COUNT && x + 1 < BOARD_COLUMN_COUNT && ButtonList[y + 2][x + 1].ForeColor != pButton.ForeColor)
                                ButtonList[y + 2][x + 1].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        if(true) // Down Side
                        {
                            if (y - 2 >= 0 && x - 1 >= 0 && ButtonList[y - 2][x - 1].ForeColor != pButton.ForeColor)
                                ButtonList[y - 2][x - 1].FlatAppearance.BorderColor = Color.Crimson;

                            if (y - 2 >= 0 && x + 1 < BOARD_COLUMN_COUNT && ButtonList[y - 2][x + 1].ForeColor != pButton.ForeColor)
                                ButtonList[y - 2][x + 1].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        if(true) // Left Side
                        {
                            if (y - 1 >= 0 && x - 2 >= 0 && ButtonList[y - 1][x - 2].ForeColor != pButton.ForeColor)
                                ButtonList[y - 1][x - 2].FlatAppearance.BorderColor = Color.Crimson;

                            if (y + 1 < BOARD_ROW_COUNT && x - 2 >= 0 && ButtonList[y + 1][x - 2].ForeColor != pButton.ForeColor)
                                ButtonList[y + 1][x - 2].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        if(true) // Right Side
                        {
                            if (y - 1 >= 0 && x + 2 < BOARD_COLUMN_COUNT && ButtonList[y - 1][x + 2].ForeColor != pButton.ForeColor)
                                ButtonList[y - 1][x + 2].FlatAppearance.BorderColor = Color.Crimson;

                            if (y + 1 <= BOARD_ROW_COUNT && x + 2 < BOARD_COLUMN_COUNT && ButtonList[y + 1][x + 2].ForeColor != pButton.ForeColor)
                                ButtonList[y + 1][x + 2].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        ButtonList[y][x].ForeColor = Color.Black;
                        ChessDoing = true;
                        #endregion
                        break;

                    case "vi":
                        #region Vishop
                        int idx = 1;
                        ButtonList[y][x].Text = "";

                        for (int i = y - 1; i >= 0 && x - idx >= 0 && pButton.ForeColor != ButtonList[i][x - idx].ForeColor; i--, idx++)
                        {
                            ButtonList[i][x - idx].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[i][x - idx].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[i][x - idx].ForeColor == Color.Blue) break;
                            }
                        }

                        idx = 1;
                        for (int i = y + 1; i < BOARD_ROW_COUNT && x + idx < BOARD_COLUMN_COUNT && pButton.ForeColor != ButtonList[i][x + idx].ForeColor; i++, idx++)
                        {
                            ButtonList[i][x + idx].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[i][x + idx].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[i][x + idx].ForeColor == Color.Blue) break;
                            }
                        }

                        idx = 1;
                        for (int i = x - 1; i >= 0 && y + idx < BOARD_ROW_COUNT && pButton.ForeColor != ButtonList[y + idx][i].ForeColor; i--, idx++)
                        {
                            ButtonList[y + idx][i].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[y + idx][i].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[y + idx][i].ForeColor == Color.Blue) break;
                            }
                        }

                        idx = 1;
                        for (int i = x + 1; i < BOARD_COLUMN_COUNT && y - idx >= 0 && pButton.ForeColor != ButtonList[y - idx][i].ForeColor; i++, idx++)
                        {
                            ButtonList[y - idx][i].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[y - idx][i].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[y - idx][i].ForeColor == Color.Blue) break;
                            }
                        }

                        ButtonList[y][x].ForeColor = Color.Black;
                        ChessDoing = true;
                        #endregion
                        break;
                        
                    case "Qu":
                        #region Queen
                        ButtonList[y][x].Text = "";

                        for (int i = y - 1; i >= 0 && pButton.ForeColor != ButtonList[i][x].ForeColor; i--)
                        {
                            ButtonList[i][x].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[i][x].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[i][x].ForeColor == Color.Blue) break;
                            }
                        }

                        for (int i = y + 1; i < BOARD_ROW_COUNT && pButton.ForeColor != ButtonList[i][x].ForeColor; i++)
                        {
                            ButtonList[i][x].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[i][x].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[i][x].ForeColor == Color.Blue) break;
                            }
                        }

                        for (int i = x - 1; i >= 0 && pButton.ForeColor != ButtonList[y][i].ForeColor; i--)
                        {
                            ButtonList[y][i].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[y][i].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[y][i].ForeColor == Color.Blue) break;
                            }
                        }

                        for (int i = x + 1; i < BOARD_COLUMN_COUNT && pButton.ForeColor != ButtonList[y][i].ForeColor; i++)
                        {
                            ButtonList[y][i].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[y][i].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[y][i].ForeColor == Color.Blue) break;
                            }
                        }

                        int idxQ = 1;

                        for (int i = y - 1; i >= 0 && x - idxQ >= 0 && pButton.ForeColor != ButtonList[i][x - idxQ].ForeColor; i--, idxQ++)
                        {
                            ButtonList[i][x - idxQ].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[i][x - idxQ].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[i][x - idxQ].ForeColor == Color.Blue) break;
                            }
                        }

                        idxQ = 1;
                        for (int i = y + 1; i < BOARD_ROW_COUNT && x + idxQ < BOARD_COLUMN_COUNT && pButton.ForeColor != ButtonList[i][x + idxQ].ForeColor; i++, idxQ++)
                        {
                            ButtonList[i][x + idxQ].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[i][x + idxQ].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[i][x + idxQ].ForeColor == Color.Blue) break;
                            }
                        }

                        idxQ = 1;
                        for (int i = x - 1; i >= 0 && y + idxQ < BOARD_ROW_COUNT && pButton.ForeColor != ButtonList[y + idxQ][i].ForeColor; i--, idxQ++)
                        {
                            ButtonList[y + idxQ][i].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[y + idxQ][i].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[y + idxQ][i].ForeColor == Color.Blue) break;
                            }
                        }

                        idxQ = 1;
                        for (int i = x + 1; i < BOARD_COLUMN_COUNT && y - idxQ >= 0 && pButton.ForeColor != ButtonList[y - idxQ][i].ForeColor; i++, idxQ++)
                        {
                            ButtonList[y - idxQ][i].FlatAppearance.BorderColor = Color.Crimson;
                            if (pButton.ForeColor == Color.Blue)
                            {
                                if (ButtonList[y - idxQ][i].ForeColor == Color.Red) break;
                            }
                            else if (pButton.ForeColor == Color.Red)
                            {
                                if (ButtonList[y - idxQ][i].ForeColor == Color.Blue) break;
                            }
                        }

                        ButtonList[y][x].ForeColor = Color.Black;
                        ChessDoing = true;
                        #endregion
                        break;

                    case "Ki":
                        #region King
                        ButtonList[y][x].Text = "";

                        if (y - 1 >= 0 && pButton.ForeColor != ButtonList[y - 1][x].ForeColor)
                        {
                            ButtonList[y - 1][x].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        if (y + 1 < BOARD_ROW_COUNT && pButton.ForeColor != ButtonList[y + 1][x].ForeColor)
                        {
                            ButtonList[y + 1][x].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        if (x - 1 >= 0 && pButton.ForeColor != ButtonList[y][x - 1].ForeColor)
                        {
                            ButtonList[y][x - 1].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        if (x + 1 < BOARD_COLUMN_COUNT && pButton.ForeColor != ButtonList[y][x + 1].ForeColor)
                        {
                            ButtonList[y][x + 1].FlatAppearance.BorderColor = Color.Crimson;
                        }


                        if (y - 1 >= 0 && x - 1 >= 0 && pButton.ForeColor != ButtonList[y - 1][x - 1].ForeColor)
                        {
                            ButtonList[y - 1][x - 1].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        if (y + 1 < BOARD_ROW_COUNT && x + 1 < BOARD_COLUMN_COUNT && pButton.ForeColor != ButtonList[y + 1][x + 1].ForeColor)
                        {
                            ButtonList[y + 1][x + 1].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        if (x - 1 >= 0 && y + 1 < BOARD_ROW_COUNT && pButton.ForeColor != ButtonList[y + 1][x - 1].ForeColor)
                        {
                            ButtonList[y + 1][x - 1].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        if (x + 1 < BOARD_COLUMN_COUNT && y - 1 >= 0 && pButton.ForeColor != ButtonList[y - 1][x + 1].ForeColor)
                        {
                            ButtonList[y - 1][x + 1].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        ButtonList[y][x].ForeColor = Color.Black;
                        ChessDoing = true;
                        #endregion
                        break;
                }
                PrevX = x;
                PrevY = y;
                #endregion

                ButtonList[y][x].FlatAppearance.BorderColor = Color.Chartreuse;
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
                    if (i == 0)
                    {
                        ChessObject a = new ChessObject(ChessObject.PAWN, Color.Blue);
                        a.SetLocation(j, 1);
                        BlueTeamList.Add(a);
                    }

                    if (i == 1)
                    {
                        ChessObject a = new ChessObject(ChessObject.PAWN, Color.Red);
                        a.SetLocation(j, 6);
                        RedTeamList.Add(a);
                    }
                }
            }

            ListSet(ref BlueTeamList, Color.Blue);
            ListSet(ref RedTeamList, Color.Red);
        }

        private void ListSet(ref List<ChessObject> pList, Color pColor)
        {
            int RowIDX = pColor == Color.Red ? 7 : 0;

            ChessObject a = new ChessObject(ChessObject.CASTLE, pColor);
            a.SetLocation(0, RowIDX);
            pList.Add(a);

            a = new ChessObject(ChessObject.KNIGHT, pColor);
            a.SetLocation(1, RowIDX);
            pList.Add(a);

            a = new ChessObject(ChessObject.VISHOP, pColor);
            a.SetLocation(2, RowIDX);
            pList.Add(a);

            a = new ChessObject(ChessObject.KING, pColor);
            a.SetLocation(3, RowIDX);
            pList.Add(a);

            a = new ChessObject(ChessObject.QUEEN, pColor);
            a.SetLocation(4, RowIDX);
            pList.Add(a);

            a = new ChessObject(ChessObject.VISHOP, pColor);
            a.SetLocation(5, RowIDX);
            pList.Add(a);

            a = new ChessObject(ChessObject.KNIGHT, pColor);
            a.SetLocation(6, RowIDX);
            pList.Add(a);

            a = new ChessObject(ChessObject.CASTLE, pColor);
            a.SetLocation(7, RowIDX);
            pList.Add(a);
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
