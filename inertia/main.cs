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

        Ball b = new Ball(5, 10, 30);
        Planet p;
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

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            Invalidate();
            b.BallPos.Px += 1;
            b.update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            b.drawBall(g);
            p.drawPlanet(g);


        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            bool bo = true;
            if (bo)
            {
                PVector pullForce = p.attract(b); //Pullforce of the Planet will be added to the ball as an acceleration
                b.applyForce(pullForce);
            }
        }
    }
}






//- removed not used code in Ball class
// - Added Planet Class for generating Planets as an attractor
