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
        Ball b;
        Planet[] planets;
        Speedometer speed;
        bool space = false;
        Random random;

        int sumXPlanets; // to have a specific distance between to planets
        int sumYPlanets;

        int score = 0;
        bool aboveOrUnder = false; //only give changing points
        int timeCounter = 0; //only check every 1 second

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
            random = new Random();
            this.BackColor = Color.Black;
            speed = new Speedometer(7, new Point(this.ClientSize.Width / 2, 90), new Size(20, 20));

            matchFieldTop = this.ClientSize.Height / 5;
            matchFieldBottom = this.ClientSize.Height - this.ClientSize.Height / 5;

            this.Icon = Properties.Resources.icon;
            b = new Ball(0, matchFieldTop + 10, 20, this.ClientSize.Width);
            //firstPlanet = new Planet(120, this.ClientSize.Height / 2, 100);

            planets = new Planet[7];

            GeneratePlanets();

            sumXPlanets = 0;
            sumYPlanets = 0;

        }

        void GeneratePlanets()
        {
            sumXPlanets = 0;
            sumYPlanets = 0;
            for (int i = 0; i < planets.Length; i++)
            {
                int mass = random.Next(100, 180);
                sumXPlanets += mass + 120; //Planets have a minimum distance of their diameter + 80
                int x_position = random.Next(sumXPlanets, sumXPlanets + 20);
                int y_position = random.Next(matchFieldTop + mass * 2, matchFieldBottom - mass / 2);
                planets[i] = new Planet(x_position, y_position, mass);
            }
        }
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            Invalidate();
            b.Update();
            Tmp1();


            for (int i = 0; i < planets.Length; i++)
            {
                if (planets[i].CheckCollision(b))
                {
                    b.Location = new PVector(0, matchFieldTop + 20);
                }

                #region Score Counter
                //calculate the two points if ball is above add 1 to score and then the other way around

                timeCounter += 7;
                if (timeCounter >= 1000) //Every second one point
                {
                    if (i > 0)
                    {
                        label2.Text = score.ToString();

                        if (b.Location.Py < planets[i].Location.Py && aboveOrUnder == false)
                        {
                            score++;
                            aboveOrUnder = true;
                        }
                        if (b.Location.Py > planets[i].Location.Py && aboveOrUnder == true)
                        {
                            score++;
                            aboveOrUnder = false;
                        }


                    }
                    timeCounter = 0;
                }
                #endregion
            }

            b.ChangeHorizontalVelocity(speed.Value);
            b.BounceOfBorder(matchFieldTop, matchFieldBottom);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            DrawCanvas(g);
            b.DrawBall(g);
            //firstPlanet.DrawPlanet(g);
            //firstPlanet.DrawArea(g);
            //firstPlanet.DebugDraw(g);
            speed.FillCircles(g);

            for (int i = 0; i < planets.Length; i++)
            {
                //TODO: Planets come closer to each other each round.
                planets[i].DrawPlanet(g);
                planets[i].Location.Px -= 2;

                if (planets[i].Location.Px < 0 - 100)
                {
                    planets[i].Location.Px = this.ClientSize.Width + 100;
                }
            }

            g.DrawLine(Pens.White, new Point(this.ClientSize.Width / 2, 100), new Point(this.ClientSize.Width / 2, 0));

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

        public void Tmp1()
        {

            if (space)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    PVector force = planets[i].AttractBall(b);
                    b.ApplyForce(force);
                }
            }
            if (down)
            {
                //b.Location.Py += 10;
                speed.NumberDown();
                label1.Text = speed.Value.ToString();
            }
            if (up)
            {
                //b.Location.Py -= 10;
                speed.NumberUp();
                label1.Text = speed.Value.ToString();
            }
            if (right)
            {
                b.Location.Px += 10;
                GeneratePlanets();
            }
            if (left)
            {
                b.Location.Px -= 10;
            }
            if (one)
            {
                b.Velocity = new PVector(0, 0);
                b.Location = new PVector(20, matchFieldTop + 20);
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

            //Draw Lines
            g.DrawLine(Pens.White, new Point(0, matchFieldTop), new Point(this.ClientSize.Width, matchFieldTop));

            g.DrawLine(Pens.White, new Point(0, matchFieldBottom), new Point(this.ClientSize.Width, matchFieldBottom));

        }
    }
}


