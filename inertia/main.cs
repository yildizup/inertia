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
        Planet p = new Planet(500, 500, 100);
        bool space = false;


        readonly int matchFieldTop;
        readonly int matchFieldBottom;
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

            matchFieldTop = this.ClientSize.Height / 5;
            matchFieldBottom = this.ClientSize.Height - this.ClientSize.Height / 5; 

        }

        //TODO: Planet draw the area of the graviation field
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            Invalidate();
            b.Update();
            int aa = this.ClientSize.Height;
            int bb = this.ClientSize.Width;
            Tmp1();
            b.Velocity.Px += 0.01F;

            if (p.CheckCollision(b))
            {
                this.BackColor = Color.Green;
            }
            else
            {

                this.BackColor = Color.White;
            }

        }

        public void Tmp1()
        {

            if (space)
            {

                PVector force = p.AttractBall(b);
                b.ApplyForce(force);

            }
            if (down)
            {
                b.Location.Py += 10;
            }
            if (up)
            {
                b.Location.Py -= 10;
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
            }
        }



        private void DrawCanvas(Graphics g)
        {
            Brush brush = Brushes.Black;

            Point pointTop = new Point(0, 0);
            Size size = new Size(this.ClientSize.Width, matchFieldTop);
            Rectangle rectangleTop = new Rectangle(pointTop, size);
            g.FillRectangle(brush, rectangleTop);

            Point pointBottom = new Point(0, matchFieldBottom);
            Rectangle rectangleBottom = new Rectangle(pointBottom, size);
            g.FillRectangle(brush, rectangleBottom);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            DrawCanvas(g);
            b.DrawBall(g);
            p.DrawPlanet(g);
            p.DrawArea(g);
            p.DebugDraw(g);
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


