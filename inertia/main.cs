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
        Ball ball;
        Planet[] planets;
        Speedometer speedometer;
        bool space = false;
        Random random;
        bool startGame;

        int sumXPlanets; // to have a specific distance between to planets

        int score = 0;
        bool aboveOrUnder = true; //only give changing points
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

            #region Form properties
            this.Icon = Properties.Resources.icon;
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.None;
            #endregion

            matchFieldTop = this.ClientSize.Height / 5;
            matchFieldBottom = this.ClientSize.Height - this.ClientSize.Height / 5;

            random = new Random();

            #region instances
            speedometer = new Speedometer(7, new Point(this.ClientSize.Width / 2, 90), new Size(20, 20));
            ball = new Ball(20, matchFieldTop + 10, 20, this.ClientSize.Width);
            planets = new Planet[7];
            #endregion

            GeneratePlanets();


            startGame = false;
            sumXPlanets = 0;

        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            if (startGame)
            {
                Invalidate();
                ball.Update();
                EventOnKeyPress();

                for (int i = 0; i < planets.Length; i++)
                {
                    if (planets[i].CheckCollision(ball))
                    {
                        ball.Location = new PVector(ball.Diameter, matchFieldTop + 20);
                        GeneratePlanets();
                        score = 0;
                        startGame = false;
                        speedometer.Reset();
                    }

                    #region Score Counter
                    //calculate the two points. Change boolean according to their position change

                    timeCounter += 7;
                    if (timeCounter >= 1000) //Every second one point
                    {
                        if (i > 0)
                        {
                            if (ball.Location.Py < planets[i].Location.Py && aboveOrUnder == false)
                            {
                                score++;
                                aboveOrUnder = true;
                            }
                            if (ball.Location.Py > planets[i].Location.Py && aboveOrUnder == true)
                            {
                                score++;
                                aboveOrUnder = false;
                            }


                        }
                        timeCounter = 0;
                    }
                    #endregion
                }

                ball.ChangeHorizontalVelocity(speedometer.Value);
                ball.BounceOfVerticalBorder(matchFieldTop, matchFieldBottom);
            }
            if (!startGame)
            {
                Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            DrawCanvas(g);

            ball.DrawBall(g);
            speedometer.FillCircles(g);

            for (int i = 0; i < planets.Length; i++)
            {
                //TODO: Planets come closer to each other each round. Fix
                planets[i].DrawArea(g);
                planets[i].DrawPlanet(g);
                planets[i].Location.Px -= 2;

                if (planets[i].Location.Px < 0 - 100)
                {
                    planets[i].Location.Px = this.ClientSize.Width + 100;
                }
            }

            WriteScore(g);

            if (!startGame)
            {
                StartMenu(g);
            }
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    space = true;
                    break;
                case Keys.Right:
                    if (startGame)
                    {
                        speedometer.NumberUp();
                    }
                    break;
                case Keys.Left:
                    if (startGame)
                    {
                        speedometer.NumberDown();
                    }
                    break;
                case Keys.Up:
                    if (!startGame)
                    {
                        startGame = true;
                        GeneratePlanets();
                    }
                    break;
            }
        }

        private void Main_KeyUp(object sender, KeyEventArgs e)
        {
            space = false;

        }
        /// <summary>
        /// determines what should happen, when a key is pressed
        /// </summary>
        public void EventOnKeyPress()
        {

            if (space)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    PVector force = planets[i].AttractBall(ball);
                    ball.ApplyForce(force);

                    if (planets[i].Transparency < 75)
                    {
                        planets[i].Transparency += 2;
                    }
                }
            }

            if (!space)
            {

                for (int i = 0; i < planets.Length; i++)
                {
                    if (planets[i].Transparency > 40)
                    {
                        planets[i].Transparency -= 2;
                    }
                }
            }
        }


        #region Form UI
        void WriteScore(Graphics g)
        {
            Font font = new Font("MS Comic Sans", 24);
            string sScore = String.Format("{0}", score);
            SizeF stringValues = g.MeasureString(sScore, font);
            g.DrawString(sScore, font, Brushes.White, new Point(Convert.ToInt32(this.ClientSize.Width / 2 - stringValues.Width / 2), Convert.ToInt32(matchFieldTop - stringValues.Height)));
        }

        void StartMenu(Graphics g)
        {
            Font font = new Font("MS Comic Sans", 24);
            string sScore = "Press [UP] arrow to start";
            SizeF stringValues = g.MeasureString(sScore, font);
            g.DrawString(sScore, font, Brushes.White, new Point(Convert.ToInt32(this.ClientSize.Width / 2 - stringValues.Width / 2), Convert.ToInt32(this.ClientSize.Height / 2 - stringValues.Height)));
        }

        /// <summary>
        /// draw the upper and lower canvas
        /// </summary>
        /// <param name="g"></param>
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
        #endregion

        void GeneratePlanets()
        {
            sumXPlanets = 0;
            for (int i = 0; i < planets.Length; i++)
            {
                int mass = random.Next(100, 150);
                sumXPlanets += mass + 120; //Planets have a minimum distance of their diameter + 80
                int x_position = random.Next(sumXPlanets, sumXPlanets + 20);
                int y_position = random.Next(matchFieldTop + mass + (int)mass / 2, matchFieldBottom - mass / 2);
                planets[i] = new Planet(x_position, y_position, mass);
            }
        }

    }
}


