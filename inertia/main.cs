﻿using System;
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
        Planet firstPlanet;
        Planet[] planets;
        Speedometer speed;
        bool space = false;

        int sumXPlanets; // to have a specific distance between to planets
        int sumYPlanets;


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
            speed = new Speedometer(7, new Point(this.ClientSize.Width - 20, 90), new Size(10, 10));

            matchFieldTop = this.ClientSize.Height / 5;
            matchFieldBottom = this.ClientSize.Height - this.ClientSize.Height / 5;

            b = new Ball(0, matchFieldTop + 10, 20);
            //firstPlanet = new Planet(120, this.ClientSize.Height / 2, 100);

            planets = new Planet[8];

            GeneratePlanets();

            sumXPlanets = 0;
            sumYPlanets = 0;
        }
        void GeneratePlanets()
        {
            Random random = new Random();
            sumXPlanets = 0;
            sumYPlanets = 0;
            for (int i = 0; i < planets.Length; i++)
            {
                int mass = random.Next(100, 180);
                sumXPlanets += mass + 80;
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
                planets[i].GetBack(b);
            }

            b.ChangeHorizontalVelocity(speed.Value);
            b.BounceOfBorder(matchFieldTop,matchFieldBottom);
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
                planets[i].DrawPlanet(g);
                planets[i].Location.Px -= 1;

                if (planets[i].Location.Px < 0 - planets[i].Mass / 2)
                {
                    planets[i].Location.Px = this.ClientSize.Width + planets[i].Mass / 2;
                }
            }

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


