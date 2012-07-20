using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TicTacToeWeb
{
    public partial class _Default : Page
    {
        
        protected void Page_Init(object sender, EventArgs e)
        {
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Play(object sender, EventArgs e)
        {
            Button b = sender as Button;
            char[,] Board = Session["board"] as char[,];
            bool gameOver = Convert.ToBoolean(Session["gameOver"]);
            int pos = Int32.Parse(b.ID.Substring(1));
            b.Enabled = false;
            b.Text = "X";
            int r = pos / 3;
            int c = pos % 3;
            Board[r, c] = 'X';
            Session["board"] = Board;
            CheckLastMove(r, c, 'X');
            gameOver = Convert.ToBoolean(Session["gameOver"]);
            if (!gameOver)
            {
                var i = GetNextMove('O');
                Button t = GetButton(i);
                if (t != null && t.Enabled)
                {
                    t.Enabled = false;
                    t.Text = "O";
                    CheckLastMove(i/3, i%3, 'O');
                    gameOver = Convert.ToBoolean(Session["gameOver"]);
                }
                else
                    gameOver = true;
            }
            if (gameOver)
            {
                this._0.Enabled = false;
                this._1.Enabled = false;
                this._2.Enabled = false;
                this._3.Enabled = false;
                this._4.Enabled = false;
                this._5.Enabled = false;
                this._6.Enabled = false;
                this._7.Enabled = false;
                this._8.Enabled = false;
                this._9.Text = "Reset";
                this._9.Enabled = true;
            }

            Session["board"] = Board;
            Session["gameOver"] = gameOver;
        }
        protected void Reset(object sender, EventArgs e)
        {
            var Board = new char[3, 3] { { 'a', 'b', 'c' }, { 'd', 'e', 'f' }, { 'g', 'h', 'i' } };
            bool gameOver = false;
            this._0.Enabled = true;
            this._1.Enabled = true;
            this._2.Enabled = true;
            this._3.Enabled = true;
            this._4.Enabled = true;
            this._5.Enabled = true;
            this._6.Enabled = true;
            this._7.Enabled = true;
            this._8.Enabled = true;
            this._0.Text = "click me";
            this._1.Text = "click me";
            this._2.Text = "click me";
            this._3.Text = "click me";
            this._4.Text = "click me";
            this._5.Text = "click me";
            this._6.Text = "click me";
            this._7.Text = "click me";
            this._8.Text = "click me";
            this._9.Text = "Play";
            this._9.Enabled = false;
            Session["board"] = Board;
            Session["gameOver"] = gameOver;
        }
        private Button GetButton(int i)
        {
            switch (i)
            {
                case 0:
                    return this._0;
                case 1:
                    return this._1;
                case 2:
                    return this._2;
                case 3:
                    return this._3;
                case 4:
                    return this._4;
                case 5:
                    return this._5;
                case 6:
                    return this._6;
                case 7:
                    return this._7;
                case 8:
                    return this._8;
                default:
                    return null;
            }
        }
        private void CheckLastMove(int i, int j, char c)
        {
            var Board = Session["board"] as char[,];
            var gameOver = Convert.ToBoolean(Session["gameOver"]);
            if (i - 1 >= 0 && j - 1 >= 0 && i + 1 <= 2 && j + 1 <= 2 && Board[i - 1, j - 1] == c && c == Board[i + 1, j + 1]) gameOver = true;
            if (i - 1 >= 0 && i + 1 <= 2 && Board[i - 1, j] == c && c == Board[i + 1, j]) gameOver = true;
            if (i - 1 >= 0 && j + 1 <= 2 && i + 1 <= 2 && j - 1 >= 0 && Board[i - 1, j + 1] == c && c == Board[i + 1, j - 1]) gameOver = true;
            if (j - 1 >= 0 && j + 1 <= 2 && Board[i, j - 1] == c && c == Board[i, j + 1]) gameOver = true;
            if (i - 1 >= 0 && j - 1 >= 0 && i - 2 >= 0 && j - 2 >= 0 && Board[i - 1, j - 1] == c && c == Board[i - 2, j - 2]) gameOver = true;
            if (i - 1 >= 0 && i - 2 >= 0 && Board[i - 1, j] == c && c == Board[i - 2, j]) gameOver = true;
            if (i - 1 >= 0 && j + 1 <= 2 && i - 2 >= 0 && j + 2 <= 2 && Board[i - 1, j + 1] == c && c == Board[i - 2, j + 2]) gameOver = true;
            if (j + 1 <= 2 && j + 2 <= 2 && Board[i, j + 1] == c && c == Board[i, j + 2]) gameOver = true;
            if (i + 1 <= 2 && j + 1 <= 2 && i + 2 <= 2 && j + 2 <= 2 && Board[i + 1, j + 1] == c && c == Board[i + 2, j + 2]) gameOver = true;
            if (i + 1 <= 2 && i + 2 <= 2 && Board[i + 1, j] == c && c == Board[i + 2, j]) gameOver = true;
            if (i + 1 <= 2 && j - 1 >= 0 && i + 2 <= 2 && j - 2 >= 0 && Board[i + 1, j - 1] == c && c == Board[i + 2, j - 2]) gameOver = true;
            if (j - 1 >= 0 && j - 2 >= 0 && Board[i, j - 1] == c && c == Board[i, j - 2]) gameOver = true;

            Session["board"] = Board;
            Session["gameOver"] = gameOver;
        }
        private int GetNextMove(char c)
        {
            var Board = Session["board"] as char[,];
            var gameOver = Convert.ToBoolean(Session["gameOver"]);
            
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] != 'X' && Board[i, j] != 'O')
                    {
                        // immediate neighbours forming 3 in a row
                        int res = ThreeInARow(i, j, c);
                        if (res != -1)
                            return res;
                    }
                }

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] != 'X' && Board[i, j] != 'O')
                    {
                        // skip neighbours forming 3 in a row
                        int res = SkipNeighbour3InARow(i, j, c);
                        if (res != -1)
                            return res;

                    }
                }


            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] != 'X' && Board[i, j] != 'O')
                    {
                        // neighbours forming a 2 in a row
                        int res = TwoInARow(i, j, c);
                        if (res != -1)
                            return res;
                    }
                }

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] != 'X' && Board[i, j] != 'O')
                    {
                        Board[i, j] = c;
                        Session["board"] = Board;
                        Session["gameOver"] = gameOver;
                        return i * 3 + j;
                    }
                }

            return -1;
        }

        private int ThreeInARow(int i, int j, char c)
        {
            var Board = Session["board"] as char[,];
            var gameOver = Convert.ToBoolean(Session["gameOver"]);
            
            if (i - 1 >= 0 && j - 1 >= 0
                && i + 1 <= 2 && j + 1 <= 2 && Board[i - 1, j - 1] == Board[i + 1, j + 1])
            {
                if (Board[i - 1, j - 1] == c && c == Board[i + 1, j + 1]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i - 1 >= 0
                && i + 1 <= 2 && Board[i - 1, j] == Board[i + 1, j])
            {
                if (Board[i - 1, j] == c && c == Board[i + 1, j]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i - 1 >= 0 && j + 1 <= 2
                && i + 1 <= 2 && j - 1 >= 0 && Board[i - 1, j + 1] == Board[i + 1, j - 1])
            {
                if (Board[i - 1, j + 1] == c && c == Board[i + 1, j - 1]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (j - 1 >= 0
                && j + 1 <= 2 && Board[i, j - 1] == Board[i, j + 1])
            {
                if (Board[i, j - 1] == c && c == Board[i, j + 1]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }
            return -1;
        }

        private int SkipNeighbour3InARow(int i, int j, char c)
        {
            var Board = Session["board"] as char[,];
            var gameOver = Convert.ToBoolean(Session["gameOver"]);
            
            if (i - 1 >= 0 && j - 1 >= 0
                && i - 2 >= 0 && j - 2 >= 0 && Board[i - 1, j - 1] == Board[i - 2, j - 2])
            {
                if (Board[i - 1, j - 1] == c && c == Board[i - 2, j - 2]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i - 1 >= 0
                && i - 2 >= 0 && Board[i - 1, j] == Board[i - 2, j])
            {
                if (Board[i - 1, j] == c && c == Board[i - 2, j]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i - 1 >= 0 && j + 1 <= 2
                && i - 2 >= 0 && j + 2 <= 2 && Board[i - 1, j + 1] == Board[i - 2, j + 2])
            {
                if (Board[i - 1, j + 1] == c && c == Board[i - 2, j + 2]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (j + 1 <= 2
                && j + 2 <= 2 && Board[i, j + 1] == Board[i, j + 2])
            {
                if (Board[i, j + 1] == c && c == Board[i, j + 2]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && j + 1 <= 2
                && i + 2 <= 2 && j + 2 <= 2 && Board[i + 1, j + 1] == Board[i + 2, j + 2])
            {
                if (Board[i + 1, j + 1] == c && c == Board[i + 2, j + 2]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i + 1 <= 2
                && i + 2 <= 2 && Board[i + 1, j] == Board[i + 2, j])
            {
                if (Board[i + 1, j] == c && c == Board[i + 2, j]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && j - 1 >= 0
                && i + 2 <= 2 && j - 2 >= 0 && Board[i + 1, j - 1] == Board[i + 2, j - 2])
            {
                if (Board[i + 1, j - 1] == c && c == Board[i + 2, j - 2]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (j - 1 >= 0
                && j - 2 >= 0 && Board[i, j - 1] == Board[i, j - 2])
            {
                if (Board[i, j - 1] == c && c == Board[i, j - 2]) gameOver = true;
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }
            return -1;
        }

        private int TwoInARow(int i, int j, char c)
        {
            var Board = Session["board"] as char[,];
            var gameOver = Convert.ToBoolean(Session["gameOver"]);
            
            if (i - 1 >= 0 && j - 1 >= 0 && Board[i - 1, j - 1] == c)
            {
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i - 1 >= 0 && Board[i - 1, j] == c)
            {
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i - 1 >= 0 && j + 1 <= 2 && Board[i - 1, j + 1] == c)
            {
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (j + 1 <= 2 && Board[i, j + 1] == c)
            {
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && j + 1 <= 2 && Board[i + 1, j + 1] == c)
            {
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && Board[i + 1, j] == c)
            {
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && j - 1 >= 0 && Board[i + 1, j - 1] == c)
            {
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }

            if (j - 1 >= 0 && Board[i, j - 1] == c)
            {
                Board[i, j] = c;
                Session["board"] = Board;
                Session["gameOver"] = gameOver;
                return i * 3 + j;
            }
            return -1;
        }
    }
}