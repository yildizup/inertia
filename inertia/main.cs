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
        Ball b = new Ball(20, 10, 30);
        Planet p;
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

            p = new Planet(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
        }

        //TODO: Planet draw the area of the graviation field
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            Invalidate();
            b.BallPos.Px += 1;
            b.Update();

            if (space)
            {
                PVector pullForce = p.AttractBall(b); //Pullforce of the Planet will be added to the ball as an acceleration
                b.ApplyForce(pullForce);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            b.DrawBall(g);
            p.DrawPlanet(g);
            p.DrawArea(g);
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            space = true; 
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            space = false;
        }
    }
}


