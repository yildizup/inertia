using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


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
            mass = 20;
            G = 1;
        }

        public void drawPlanet(Graphics g)
        {
            Pen pen = Pens.Green;
            Point point = new Point(Convert.ToInt32(location.Px), Convert.ToInt32(location.Py));
            Size size = new Size(Convert.ToInt32(mass * 3), Convert.ToInt32(mass * 3));
            Rectangle rect = new Rectangle(point, size);
            g.DrawEllipse(pen, rect);
        }

        public PVector attract(Ball b)
        {
            PVector force = PVector.subtract(location, b.BallPos);
            float distance = force.magnitude();

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

            force.normalize();
            float strength = (G * mass * b.Mass) / (distance * distance); //gravitational force
            force.multiplicate(strength);
            return force;

        }
    }
}
