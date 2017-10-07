using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public class Square
    {
        //const
        public const int markAsQM = -4;
        public const int markAsMine = -3;
        public const int unknown = -2;

        public bool isMine;
        public int mineCount;

        public int state;
        public Button btn;

        public int r;
        public int c;

        private Form1 game;

        
        public Square(Form1 game, int r, int c)
        {
            this.game = game;
            this.r = r;
            this.c = c;
            state = unknown;
            btn = new Button();
            btn.Text = " ";
            btn.Name = r.ToString() + "," + c.ToString();
            btn.Size = new System.Drawing.Size(22, 22);
            btn.Location = new System.Drawing.Point(10 + c * 22, 40 + r * 22);
            btn.MouseUp += Btn_MouseUp;
        }

        private void Btn_MouseUp(object sender, MouseEventArgs me)
        {
            Button btnClick = sender as Button;

            if (btnClick == null)
            {
                return;
            }

            string[] split = btnClick.Name.Split(new Char[] { ',' });

            int r = System.Convert.ToInt32(split[0]);
            int c = System.Convert.ToInt32(split[1]);

            if (me.Button == MouseButtons.Left)
            {
                //Console.WriteLine(btnClick.Name + " left clicked !");
                OpenLocation();
            }
            else if (me.Button == MouseButtons.Right)
            {
                //Console.WriteLine(btnClick.Name + " right clicked !");

                if (state == unknown)
                    MarkAsMine();
                else if (state == markAsMine)
                    MarkAsQM();
                else if (state == markAsQM)
                    ClearMark();
            }

            game.Refresh();
        }

        public void OpenLocation()
        {
            if (state == markAsMine || state == markAsQM || state >= 0)
                return;

            game.unknownList.Remove(this);

            

            if (isMine)
            {
                MessageBox.Show("Game Over !");

                game.ShowAllMines();

                return;
            }

            int mineNum = 0;

            if (game.IsMine(r - 1, c - 1))
                ++mineNum;
            if (game.IsMine(r - 1, c))
                ++mineNum;
            if (game.IsMine(r - 1, c + 1))
                ++mineNum;
            if (game.IsMine(r + 1, c - 1))
                ++mineNum;
            if (game.IsMine(r + 1, c))
                ++mineNum;
            if (game.IsMine(r + 1, c + 1))
                ++mineNum;
            if (game.IsMine(r, c - 1))
                ++mineNum;
            if (game.IsMine(r, c + 1))
                ++mineNum;

            state = mineNum;
            mineCount = mineNum;

            if (mineNum > 0)
            {
                //set the lbl
                btn.Text = mineNum.ToString();
            }
            else
            {
                game.OpenLocation(r - 1, c - 1);
                game.OpenLocation(r - 1, c);
                game.OpenLocation(r - 1, c + 1);
                game.OpenLocation(r + 1, c - 1);
                game.OpenLocation(r + 1, c);
                game.OpenLocation(r + 1, c + 1);
                game.OpenLocation(r, c - 1);
                game.OpenLocation(r, c + 1);

                btn.Visible = false;
            }

            bool moreSafeLocToFind = false;
            for (int i = 0; i < game.unknownList.Count; ++i)
            {
                if (!game.unknownList[i].isMine)
                {
                    moreSafeLocToFind = true;
                    break;
                }
            }

            if (!moreSafeLocToFind)
                MessageBox.Show("You Win !");
        }

        public void PlaceMine()
        {
            isMine = true;
            //btn.Text = "M";
        }

        public void MarkAsQM()
        {
            state = markAsQM;
            btn.Text = "?";
        }

        public void MarkAsMine()
        {
            state = markAsMine;
            btn.Text = "m";
            //game.unknownList.Remove(this);
        }

        public void ClearMark()
        {
            state = unknown;
            btn.Text = " ";
            //game.unknownList.Add(this);
        }

        public int KnownMines()
        {
            int mineNum = 0;

            if (game.IsMarkedMine(r - 1, c - 1))
                ++mineNum;
            if (game.IsMarkedMine(r - 1, c))
                ++mineNum;
            if (game.IsMarkedMine(r - 1, c + 1))
                ++mineNum;
            if (game.IsMarkedMine(r + 1, c - 1))
                ++mineNum;
            if (game.IsMarkedMine(r + 1, c))
                ++mineNum;
            if (game.IsMarkedMine(r + 1, c + 1))
                ++mineNum;
            if (game.IsMarkedMine(r, c - 1))
                ++mineNum;
            if (game.IsMarkedMine(r, c + 1))
                ++mineNum;

            return mineNum;
        }
    }
}
