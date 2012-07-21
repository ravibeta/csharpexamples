using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace PhoneAppTicTacToe
{
    public partial class MainPage : PhoneApplicationPage
    {
        public char[,] Board { get; set; }
        private bool gameOver = false;

        // Constructor
        public MainPage()
        {
            Board = new char[3, 3] { { 'a', 'b', 'c' }, { 'd', 'e', 'f' }, { 'g', 'h', 'i' } };
            InitializeComponent();
            this._9.IsEnabled = false;
        }

        private void Play(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int pos = Int32.Parse(b.Name.Substring(1));
            b.IsEnabled = false;
            b.Content = "X";
            int r = pos / 3;
            int c = pos % 3;
            Board[r, c] = 'X';
            CheckLastMove(r, c, 'X');
            if (!gameOver)
            {
                var i = GetNextMove('O');
                Button t = GetButton(i);
                if (t != null && t.IsEnabled)
                {
                    t.IsEnabled = false;
                    t.Content = "O";
                    CheckLastMove(i/3, i%3, 'O');
                }
                else
                    gameOver = true;
            }
            if (gameOver)
            {
                this._0.IsEnabled = false;
                this._1.IsEnabled = false;
                this._2.IsEnabled = false;
                this._3.IsEnabled = false;
                this._4.IsEnabled = false;
                this._5.IsEnabled = false;
                this._6.IsEnabled = false;
                this._7.IsEnabled = false;
                this._8.IsEnabled = false;
                this._9.Content = "Reset";
                this._9.IsEnabled = true;
            }
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            Board = new char[3, 3] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
            this._0.IsEnabled = true;
            this._1.IsEnabled = true;
            this._2.IsEnabled = true;
            this._3.IsEnabled = true;
            this._4.IsEnabled = true;
            this._5.IsEnabled = true;
            this._6.IsEnabled = true;
            this._7.IsEnabled = true;
            this._8.IsEnabled = true;
            this._0.Content = "click me";
            this._1.Content = "click me";
            this._2.Content = "click me";
            this._3.Content = "click me";
            this._4.Content = "click me";
            this._5.Content = "click me";
            this._6.Content = "click me";
            this._7.Content = "click me";
            this._8.Content = "click me";
            this._9.Content = "Play";
            this._9.IsEnabled = false;
            gameOver = false;
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

        }
        private int GetNextMove(char c)
        {
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
                        return i * 3 + j;
                    }
                }

            return -1;
        }

        private int ThreeInARow(int i, int j, char c)
        {
            if (i - 1 >= 0 && j - 1 >= 0
                && i + 1 <= 2 && j + 1 <= 2 && Board[i - 1, j - 1] == Board[i + 1, j + 1])
            {
                if (Board[i - 1, j - 1] == c && c == Board[i + 1, j + 1]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i - 1 >= 0
                && i + 1 <= 2 && Board[i - 1, j] == Board[i + 1, j])
            {
                if (Board[i - 1, j] == c && c == Board[i + 1, j]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i - 1 >= 0 && j + 1 <= 2
                && i + 1 <= 2 && j - 1 >= 0 && Board[i - 1, j + 1] == Board[i + 1, j - 1])
            {
                if (Board[i - 1, j + 1] == c && c == Board[i + 1, j - 1]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (j - 1 >= 0
                && j + 1 <= 2 && Board[i, j - 1] == Board[i, j + 1])
            {
                if (Board[i, j - 1] == c && c == Board[i, j + 1]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }
            return -1;
        }

        private int SkipNeighbour3InARow(int i, int j, char c)
        {
            if (i - 1 >= 0 && j - 1 >= 0
                && i - 2 >= 0 && j - 2 >= 0 && Board[i - 1, j - 1] == Board[i - 2, j - 2])
            {
                if (Board[i - 1, j - 1] == c && c == Board[i - 2, j - 2]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i - 1 >= 0
                && i - 2 >= 0 && Board[i - 1, j] == Board[i - 2, j])
            {
                if (Board[i - 1, j] == c && c == Board[i - 2, j]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i - 1 >= 0 && j + 1 <= 2
                && i - 2 >= 0 && j + 2 <= 2 && Board[i - 1, j + 1] == Board[i - 2, j + 2])
            {
                if (Board[i - 1, j + 1] == c && c == Board[i - 2, j + 2]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (j + 1 <= 2
                && j + 2 <= 2 && Board[i, j + 1] == Board[i, j + 2])
            {
                if (Board[i, j + 1] == c && c == Board[i, j + 2]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && j + 1 <= 2
                && i + 2 <= 2 && j + 2 <= 2 && Board[i + 1, j + 1] == Board[i + 2, j + 2])
            {
                if (Board[i + 1, j + 1] == c && c == Board[i + 2, j + 2]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i + 1 <= 2
                && i + 2 <= 2 && Board[i + 1, j] == Board[i + 2, j])
            {
                if (Board[i + 1, j] == c && c == Board[i + 2, j]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && j - 1 >= 0
                && i + 2 <= 2 && j - 2 >= 0 && Board[i + 1, j - 1] == Board[i + 2, j - 2])
            {
                if (Board[i + 1, j - 1] == c && c == Board[i + 2, j - 2]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (j - 1 >= 0
                && j - 2 >= 0 && Board[i, j - 1] == Board[i, j - 2])
            {
                if (Board[i, j - 1] == c && c == Board[i, j - 2]) gameOver = true;
                Board[i, j] = c;
                return i * 3 + j;
            }
            return -1;
        }

        private int TwoInARow(int i, int j, char c)
        {
            if (i - 1 >= 0 && j - 1 >= 0 && Board[i - 1, j - 1] == c)
            {
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i - 1 >= 0 && Board[i - 1, j] == c)
            {
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i - 1 >= 0 && j + 1 <= 2 && Board[i - 1, j + 1] == c)
            {
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (j + 1 <= 2 && Board[i, j + 1] == c)
            {
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && j + 1 <= 2 && Board[i + 1, j + 1] == c)
            {
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && Board[i + 1, j] == c)
            {
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (i + 1 <= 2 && j - 1 >= 0 && Board[i + 1, j - 1] == c)
            {
                Board[i, j] = c;
                return i * 3 + j;
            }

            if (j - 1 >= 0 && Board[i, j - 1] == c)
            {
                Board[i, j] = c;
                return i * 3 + j;
            }
            return -1;
        }

    }
}