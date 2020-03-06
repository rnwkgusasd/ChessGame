using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class GameForm : Form
    {
        #region Member Variables
        public List<Button[]> ButtonList = new List<Button[]>();  // It contains all of board tiles.

        public List<ChessObject> BlueTeamList   = new List<ChessObject>(); // Blue team's chess objects.
        public List<ChessObject> RedTeamList    = new List<ChessObject>(); // Red team's chess objects.

        // Board Size = 8 x 8
        public static int BOARD_ROW_COUNT       = 8; // Board Rows Count = 8
        public static int BOARD_COLUMN_COUNT    = 8; // Board Columns Count = 8

        public int RedMoveCount     = 0; // Red Team Move Count
        public int BlueMoveCount    = 0; // Blue Team Move Count

        public string IPAddress = "";

        private int ClickX = 0; // X Position of Click Button
        private int ClickY = 0; // Y Position of Click Button

        private int PrevX = 0; // X Position of Previous Click Button
        private int PrevY = 0; // Y Position of Previous Click Button

        private bool ChessDoing = false; // Doing Chess Check

        private Color PrevColor; // Color of Previous Click Button

        private ConnectServer mConnect = new ConnectServer();

        #endregion

        #region Static Variables
        public const string PAWN = "p"; // Pawn Shape
        public const string CASTLE = "r"; // Castle Shape
        public const string KNIGHT = "n"; // Knight Shape
        public const string VISHOP = "b"; // Vishop Shape
        public const string QUEEN = "q"; // Queen Shape
        public const string KING = "k"; // King Shape

        public const string PAWNF = "o"; // Pawn Shape - Full
        public const string CASTLEF = "t"; // Castle Shape - Full
        public const string KNIGHTF = "m"; // Knight Shape - Full
        public const string VISHOPF = "v"; // Vishop Shape - Full
        public const string QUEENF = "w"; // Queen Shape - Full
        public const string KINGF = "l"; // King Shape - Full
        #endregion

        public class ChessObject
        {
            private string           Name        = "";  // Object Name
            private int              MoveCount   = 0;   // Object Move Count
            private int              X           = 0;   // Object X Position
            private int              Y           = 0;   // Object Y Position

            private readonly Color TeamColor;           // Object Team Color

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
            /// Set object name.
            /// </summary>
            public void SetName(string pName)
            {
                Name = pName;
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
        }

        public GameForm(bool pIsHost)
        {
            this.BackColor = Color.Gray;
            InitializeComponent();

            InitBoard();
            ShowBoard();

            //if(!pIsHost)
            //{
            //    mConnect.Open();
            //    mConnect.Send("p|0|0|1|0;");
            //}
        }

        /// <summary>
        /// Initialize chess board. (Create ButtonList)
        /// </summary>
        private void InitBoard()
        {
            ButtonList.Clear();
            PrivateFontCollection CustomFont = new PrivateFontCollection();
            CustomFont.AddFontFile("Resources\\Chess7-DaE1.ttf");

            for (int i = 0; i < BOARD_ROW_COUNT; i++)
            {
                Button[] tButtons = new Button[BOARD_COLUMN_COUNT];

                for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                {
                    tButtons[j] = new Button
                    {
                        FlatStyle = FlatStyle.Flat,
                        Size = new Size(50, 50),
                        BackColor = (i + j) % 2 == 0 ? Color.White : Color.Black,
                        ForeColor = Color.Black,
                        Font = new Font(CustomFont.Families[0], 20f)
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

                bool MoveCheck = false;

                if(!ChessDoing)
                {
                    if (tmp.ForeColor.Name != "Black") lblBtnColor.Text = String.Format($"{tmp.ForeColor.Name}");
                    if (tmp.Text != "") lblBtnText.Text = String.Format($"{tmp.Text}");
                }

                // Red Blue Team Turn Check
                if (RedMoveCount == BlueMoveCount && ButtonList[ClickY][ClickX].ForeColor == Color.Red)     MoveCheck = true;
                if (RedMoveCount > BlueMoveCount && ButtonList[ClickY][ClickX].ForeColor == Color.Blue)     MoveCheck = true;

                // Other Conditions
                if (ButtonList[ClickY][ClickX].ForeColor == Color.Blue && PrevColor == Color.Red)
                {
                    MoveCheck = true;
                    PrevColor = Color.Black;
                }
                if (ButtonList[ClickY][ClickX].ForeColor == Color.Red && PrevColor == Color.Blue)
                {
                    MoveCheck = true;
                    PrevColor = Color.Black;
                }
                if (ButtonList[ClickY][ClickX].ForeColor == Color.Black)                                    MoveCheck = true;

                // Do
                if (MoveCheck) MoveableLocation(ClickX, ClickY, ButtonList[ClickY][ClickX]);
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
            if(pButton.FlatAppearance.BorderColor != Color.Crimson) PrevColor = pButton.ForeColor;

            if (pButton.Text == "" && pBorderColor == Color.Gray) return;

            if (ChessDoing && pButton.Text == KING)
            {
                DialogResult result = MsgForm.ShowDialog("Winner is", pButton.ForeColor == Color.Red ? "Blue" : "Red");

                if(result == DialogResult.OK)
                    Dispose();
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

                            if (BlueTeamList[i].GetLocation().Y == BOARD_ROW_COUNT - 1 && BlueTeamList[i].GetName().ToUpper() == "P")
                            {
                                SelectObject frm = new SelectObject();

                                frm.SetArray(new string[] { PAWN, KNIGHT, VISHOP, CASTLE, QUEEN });

                                DialogResult result = frm.ShowDialog();

                                if(result == DialogResult.OK)
                                {
                                    BlueTeamList[i].SetName(frm.GetString());
                                    ButtonList[y][x].Text = frm.GetString();
                                }
                            }

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

                            if (RedTeamList[i].GetLocation().Y == 0 && RedTeamList[i].GetName().ToUpper() == "P")
                            {
                                SelectObject frm = new SelectObject();

                                frm.SetArray(new string[] { PAWN, KNIGHT, VISHOP, CASTLE, QUEEN });

                                DialogResult result = frm.ShowDialog();

                                if (result == DialogResult.OK)
                                {
                                    RedTeamList[i].SetName(frm.GetString());
                                    ButtonList[y][x].Text = frm.GetString();
                                }
                            }

                            break;
                        }
                    }
                }

                RedMoveCount = RedTeamList.Sum(moveCount => moveCount.GetMoveCount());
                BlueMoveCount = BlueTeamList.Sum(moveCount => moveCount.GetMoveCount());

                lblTeamRedMove.Text = String.Format($"Red Team Move : {RedMoveCount}");
                lblTeamBlueMove.Text = String.Format($"Blue Team Move : {BlueMoveCount}");

                return;
            }

            // Show Chess Object Moveable Location
            if(!ChessDoing)
            {
                #region Chess Object Move

                switch (pButton.Text)
                {
                    case PAWN:
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

                            if (y + 1 < BOARD_ROW_COUNT && x + 1 < BOARD_COLUMN_COUNT && ButtonList[y + 1][x + 1].ForeColor == Color.Red)   ButtonList[y + 1][x + 1].FlatAppearance.BorderColor = Color.Crimson;
                            if (y + 1 < BOARD_ROW_COUNT && x - 1 >= 0 && ButtonList[y + 1][x - 1].ForeColor == Color.Red)                    ButtonList[y + 1][x - 1].FlatAppearance.BorderColor = Color.Crimson;
                        }
                        else
                        {
                            for (int i = 0; i < RedTeamList.Count; i++)
                            {
                                if (RedTeamList[i].GetLocation() == new Point(x, y))
                                    pMoveCount = RedTeamList[i].GetMoveCount();
                            } // find click locations chess object

                            if (pMoveCount == 0) CanMove = 2;

                            for (int i = 0; i <= CanMove && y - i >= 0 && ButtonList[y - i][x].Text == ""; i++)
                            {
                                ButtonList[y - i][x].FlatAppearance.BorderColor = Color.Crimson;
                            }

                            if (y - 1 >= 0 && x + 1 < BOARD_COLUMN_COUNT && ButtonList[y - 1][x + 1].ForeColor == Color.Blue)    ButtonList[y - 1][x + 1].FlatAppearance.BorderColor = Color.Crimson;
                            if (y - 1 >= 0 && x - 1 >= 0 && ButtonList[y - 1][x - 1].ForeColor == Color.Blue)                     ButtonList[y - 1][x - 1].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        ButtonList[y][x].ForeColor = Color.Black;
                        ChessDoing = true;
                        #endregion
                        break;

                    case CASTLE:
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

                    case KNIGHT:
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

                            if (y + 1 < BOARD_ROW_COUNT && x + 2 < BOARD_COLUMN_COUNT && ButtonList[y + 1][x + 2].ForeColor != pButton.ForeColor)
                                ButtonList[y + 1][x + 2].FlatAppearance.BorderColor = Color.Crimson;
                        }

                        ButtonList[y][x].ForeColor = Color.Black;
                        ChessDoing = true;
                        #endregion
                        break;

                    case VISHOP:
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
                        
                    case QUEEN:
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

                    case KING:
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

        /// <summary>
        /// Add controls - Show chess board
        /// </summary>
        private void ShowBoard()
        {
            for(int i = 0; i < BOARD_ROW_COUNT; i++)
            {
                // Columns number
                Label Alphabet = new Label
                {
                    Text = Convert.ToChar('A' + i).ToString(),
                    Size = new Size(20, 20),
                    Location = new Point(190 + (i * 55), 18)
                };
                
                // Rows number
                Label Number = new Label
                {
                    Text = (8 - i).ToString(),
                    Size = new Size(20, 20),
                    Location = new Point(150, 60 + (i * 55))
                };

                Controls.Add(Alphabet);
                Controls.Add(Number);
                
                for (int j = 0; j < BOARD_COLUMN_COUNT; j++)
                {
                    ButtonList[i][j].Location = ButtonList[i][j].PointToClient(new Point(180 + (j * 55), 70 + (i * 55)));
                    
                    Controls.Add(ButtonList[i][j]);
                }
            }
            SetBoard();
        }

        /// <summary>
        /// Make chess objects
        /// </summary>
        private void SetChess()
        {
            for (int i = 0; i < 2; i++)
            {
                // Make Pawn
                for (int j = 0; j < 8; j++)
                {
                    if (i == 0)
                    {
                        ChessObject a = new ChessObject(PAWN, Color.Blue);
                        a.SetLocation(j, 1);
                        BlueTeamList.Add(a);
                    }

                    if (i == 1)
                    {
                        ChessObject a = new ChessObject(PAWN, Color.Red);
                        a.SetLocation(j, 6);
                        RedTeamList.Add(a);
                    }
                }
            }

            ListSet(ref BlueTeamList, Color.Blue);
            ListSet(ref RedTeamList, Color.Red);
        }

        /// <summary>
        /// Make chess objects without pawn
        /// </summary>
        /// <param name="pList"></param>
        /// <param name="pColor"></param>
        private void ListSet(ref List<ChessObject> pList, Color pColor)
        {
            int RowIDX = pColor == Color.Red ? 7 : 0;

            ChessObject a = new ChessObject(CASTLE, pColor);
            a.SetLocation(0, RowIDX);
            pList.Add(a);

            a = new ChessObject(KNIGHT, pColor);
            a.SetLocation(1, RowIDX);
            pList.Add(a);

            a = new ChessObject(VISHOP, pColor);
            a.SetLocation(2, RowIDX);
            pList.Add(a);

            a = new ChessObject(KING, pColor);
            a.SetLocation(3, RowIDX);
            pList.Add(a);

            a = new ChessObject(QUEEN, pColor);
            a.SetLocation(4, RowIDX);
            pList.Add(a);

            a = new ChessObject(VISHOP, pColor);
            a.SetLocation(5, RowIDX);
            pList.Add(a);

            a = new ChessObject(KNIGHT, pColor);
            a.SetLocation(6, RowIDX);
            pList.Add(a);

            a = new ChessObject(CASTLE, pColor);
            a.SetLocation(7, RowIDX);
            pList.Add(a);
        }

        /// <summary>
        /// Set board initialize setting
        /// </summary>
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

        /// <summary>
        /// Set buttons text - Initialize
        /// </summary>
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
        
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
