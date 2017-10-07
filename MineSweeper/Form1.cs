using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        Square[,] grid;
        List<Square> mineList;
        public List<Square> unknownList = new List<Square>();

        public Form1()
        {
            InitializeComponent();
            StartNewGame(6, 6, 3);
        }

        private void NewGameEasy(object sender, EventArgs e)
        {
            StartNewGame(6,6,3);
        }

        private void NewGameMedium(object sender, EventArgs e)
        {
            StartNewGame(15, 15, 30);
        }

        private void NewGameHard(object sender, EventArgs e)
        {
            StartNewGame(20, 50, 100);
        }

        private void ClearPreviousGame()
        {
            if (grid == null)
                return;

            unknownList.Clear();

            for (int r = 0; r < grid.GetLength(0); ++r)
            {
                for (int c = 0; c < grid.GetLength(1); ++c)
                {
                    if (Controls.Contains(grid[r, c].btn))
                    {
                        Controls.Remove(grid[r, c].btn);
                    }
                }
            }
        }

        private void StartNewGame(int row, int col, int mines)
        {
            ClearPreviousGame();

            grid = new Square[row,col];

            this.Width = 22 * (col+1) + 10;
            this.Height = 22 * row + 100;

            for (int r = 0; r < row; ++r)
            {
                for (int c = 0; c < col; ++c)
                {
                    grid[r,c] = CreateSquare(r, c);
                    unknownList.Add(grid[r, c]);
                }
            }

            //place mines
            int minesLeft = mines;
            mineList = new List<Square>();
            while (true)
            {
                Random rand = new Random();
                int r = rand.Next(0, row);
                int c = rand.Next(0, col);

                if (!grid[r, c].isMine)
                {
                    minesLeft--;
                    grid[r, c].PlaceMine();
                    mineList.Add(grid[r,c]);
                }

                if (minesLeft <= 0)
                    break;
            }
        }

        private Square CreateSquare(int r, int c)
        {
            Square sqr = new Square(this,r,c);
            Controls.AddRange(new System.Windows.Forms.Control[] { sqr.btn, });

            return sqr;
        }

        public bool IsMine(int r, int c)
        {
            if (r >= 0 && r < grid.GetLength(0))
            {
                if (c >= 0 && c < grid.GetLength(1))
                {
                    return grid[r, c].isMine;
                }
            }
            return false;
        }

        public bool IsMarkedMine(int r, int c)
        {
            if (r >= 0 && r < grid.GetLength(0))
            {
                if (c >= 0 && c < grid.GetLength(1))
                {
                    return grid[r, c].state == Square.markAsMine;
                }
            }
            return false;
        }

        public void OpenLocation(int r, int c)
        {
            if (r >= 0 && r < grid.GetLength(0))
            {
                if (c >= 0 && c < grid.GetLength(1))
                {
                    grid[r, c].OpenLocation();
                }
            }
        }

        public void ShowAllMines()
        {
            for (int i = 0; i < mineList.Count; ++i)
            {
                mineList[i].btn.Text = "M";
            }
        }

        private void Solve(object sender, EventArgs e)
        {
        //    if (unknownList.Count > 0)
        //    {
                
        //    }
        }


        //public bool IsThisMine(int r, int c)
        //{
        //    int loopCounter = 0;
        //}
    }
}
