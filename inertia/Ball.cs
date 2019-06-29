using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace inertia
{
    class Ball
    {
        Graphics g;
        PVector location;
        PVector velocity;
        PVector acceleration;
        int mass;
        int diameter;

        public Ball(int m, int x, int y)
        {
            mass = m;
            Location = new PVector(x, y);
            velocity = new PVector(0, 0);
            acceleration = new PVector(0, 0);
            diameter = m;
        }

        public void Update()
        {
            velocity.Add(acceleration);
            Location.Add(velocity);
            velocity.Limit(30);
            if (velocity.Px > 5)
            {
                velocity.Px = 5;
            }
            acceleration.Multiplicate(0);
        }

        public void ApplyForce(PVector force)
        {
            PVector f = force.Get();
            f.Divide(mass); //since F = m * a --> a = F/m
            acceleration.Add(f);
        }
        Rectangle rect;
        public void DrawBall(Graphics g)
        {
            Pen pen = Pens.Black;
            Size s = new Size(diameter, diameter);
            g.DrawEllipse(pen, location.Px, location.Py, s.Width, s.Height);
        }


        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
        public PVector Location
        {
            get { return location; }
            set { location = value; }
        }
        public PVector Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        public PVector Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        public int Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        public int Diameter
        {
            
            get { return diameter; }
            set { diameter = value; }
        } 
    }
}
