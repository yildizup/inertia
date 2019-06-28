using System;
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
        float mass;
        PVector location;
        float G; //gravitational constanct

        public Planet(float x, float y)
        {
            location = new PVector(x, y);
            mass = 100;
            G = 1;
        }

        public void DrawPlanet(Graphics g)
        {
            Pen pen = Pens.Green;
            Size size = new Size(Convert.ToInt32(mass), Convert.ToInt32(mass));
            Point point = new Point(Convert.ToInt32(location.Px) - size.Width / 2, Convert.ToInt32(location.Py) - size.Height /2);
            Rectangle rect = new Rectangle(point, size);
            g.DrawEllipse(pen, rect);
        }
        public void DrawArea(Graphics g)
        {
            Pen pen = Pens.Green;
            Size size = new Size(Convert.ToInt32(mass) * 2, Convert.ToInt32(mass) * 2);
            Point point = new Point(Convert.ToInt32(location.Px) - size.Height / 2 , Convert.ToInt32(location.Py) - size.Height /2);
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
            PVector force = PVector.Subtract(location, b.BallPos);
            float distance = force.Magnitude();

            if (distance < 1 || distance > 25)
            {
                if (distance < 1)
                {
                    distance = 1;
                }
                if (distance > 10)
                {
                    distance = 10;
                }
            }

            force.Normalize();
            float strength = (G * mass * b.Mass) / (distance * distance); //gravitational force
            force.Multiplicate(strength);
            return force;
        }
    }
}
