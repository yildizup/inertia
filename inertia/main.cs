using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inertia
{
    public partial class main : Form
    {
        Timer mainTimer;
        Ball b = new Ball(20, 10, 10);
        bool space = false;
        public main()
        {
            InitializeComponent();

            #region Timer
            mainTimer = new Timer();
            mainTimer.Tick += new EventHandler(TimerEventProcessor);
            mainTimer.Interval = 7;
            mainTimer.Start();
            #endregion
            this.DoubleBuffered = true;

        }

        int tmpX = 4;
        //TODO: Planet draw the area of the graviation field
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            Invalidate();
            b.Location.Px += tmpX;
            b.Update();
            int aa = this.ClientSize.Height;
            int bb = this.ClientSize.Width;
            Tmp1();
            Tmp2();
        }

        public void Tmp2()
        {
            if (b.Location.Px > this.ClientSize.Width || b.Location.Px < 0)
            {
                b.Velocity.Px *= -1;
                tmpX *= -1;
            }
            if (b.Location.Py > this.ClientSize.Height || b.Location.Py < 0)
            {
                b.Velocity.Py *= -1;
                tmpX *= -1;
            }
        }

        public void Tmp1()
        {

            if (space)
            {

            }
            if (down)
            {
                b.Location.Py += 10;
            }
            if (up)
            {
                b.Location.Py -= 10;
                tmpX = 4;
            }
            if (right)
            {
                b.Location.Px += 10;
            }
            if (left)
            {

                b.Location.Px -= 10;
            }
            if (one)
            {
                b.Velocity = new PVector(0, 0);
                tmpX = 0;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            b.DrawBall(g);
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    space = true;
                    break;
                case Keys.Down:
                    down = true;
                    break;
                case Keys.Right:
                    right = true;
                    break;
                case Keys.Left:
                    left = true;
                    break;
                case Keys.Up:
                    up = true;
                    break;
                case Keys.D1:
                    one = true;
                    break;


            }
        }
        bool down;
        bool right;
        bool up;
        bool left;
        bool one;
        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            space = false;
            down = false;
            right = false;
            left = false;
            up = false;
            one = false;

        }
    }
}


