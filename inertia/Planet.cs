﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace inertia
{
    class Planet
    {
        int mass;
        PVector location;
        float G; //gravitational constanct
        int diameter;
        Bitmap planetBitmap;
        public Planet(int x, int y, int m)
        {
            mass = m;
            G = 0.5F;
            diameter = mass;
            location = new PVector(x - diameter / 2, y - diameter / 2);
            //planetBitmap = new Bitmap(Properties.Resources.planet1);
        }

        public void DrawPlanet(Graphics g)
        {
            Pen pen = Pens.Green;
            Size size = new Size(diameter, diameter);
            Point point = new Point(Convert.ToInt32(location.Px) - diameter / 2, Convert.ToInt32(location.Py) - diameter / 2);
            Rectangle rect = new Rectangle(point, size);
            g.DrawEllipse(pen, rect);

            //Rectangle resizedRectangleBitmap = new Rectangle(Convert.ToInt32(location.Px - size.Width / 2), Convert.ToInt32(location.Py - size.Height / 2), size.Width, size.Height);
            //g.DrawImage(planetBitmap, resizedRectangleBitmap);
        }
        public void DrawArea(Graphics g)
        {
            Pen pen = Pens.Green;
            Size size = new Size(diameter * 4, diameter * 4);
            Point point = new Point(Convert.ToInt32(location.Px) - size.Width / 2, Convert.ToInt32(location.Py) - size.Height / 2);
            Rectangle rect = new Rectangle(point, size);
            g.DrawEllipse(pen, rect);
        }


        /// <summary>
        ///Calculates the distance to the mouse and then calculates the gravitational force 
        /// </summary>
        /// <param name="b">Instance of the ball object</param>
        /// <returns>PVector which will be used as the force</returns>
        public PVector AttractBall(Ball b)
        {
            int rad = (diameter * 4) / 2;
            if (b.Location.Px > this.location.Px - rad && b.Location.Px < this.location.Px + rad)
            {
                PVector force = PVector.Subtract(location, b.Location);
                float distance = force.Magnitude();

                if (distance < 5 || distance > 20)
                {
                    if (distance < 5)
                    {
                        distance = 5;
                    }
                    if (distance > 20)
                    {
                        distance = 20;
                    }
                }

                force.Normalize();
                float strength = (G * mass * b.Mass) / (distance * distance); //gravitational force
                force.Multiplicate(strength);
                return force;
            }
            else
            {
                return new PVector(0, 0);
            }
        }

        public void DrawBallArea(Graphics g, Ball b)
        {
            Pen p = Pens.Black;
            g.DrawLine(p, b.Location.Px - 500, b.Location.Py, b.Location.Px - 500, 0);
            g.DrawLine(p, b.Location.Px + 500, b.Location.Py, b.Location.Px + 500, 0);
        }


        public string DebugInfo(Ball B)
        {
            return "Hallo";
        }

        public void DebugDraw(Graphics g)
        {
            Pen p = Pens.Black;
            int rad = diameter * 4 / 2;
            //left border
            g.DrawLine(p, location.Px - rad, location.Py, location.Px - rad, 0);

            //right border
            g.DrawLine(p, location.Px + rad, location.Py, location.Px + rad, 0);
        }
        public bool CheckCollision(Ball b)
        {
            PVector distance = PVector.Subtract(b.Location, location);
            float length = distance.Magnitude();
            float radii_sum = b.Diameter / 2 + diameter / 2;

            if (length <= radii_sum)
            {


                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// If the ball collides it should bounce off
        /// </summary>
        /// <param name="b"></param>
      
        public int Diameter
        {
            get { return diameter; }
            set { diameter = value; }
        }

        public int Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        public PVector Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
