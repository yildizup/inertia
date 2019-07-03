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
        int rotatingAngle = 0;

        public Ball(int x, int y, int m)
        {
            mass = m;
            Location = new PVector(x, y);
            velocity = new PVector(0, 0);
            acceleration = new PVector(0, 0);
            diameter = m * 2;
        }

        public void Update()
        {
            velocity.Add(acceleration);
            Velocity.Px += 0.01F;
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
        public void DrawBall(Graphics g)
        {
            Pen pen = Pens.Black;
            Size s = new Size(diameter, diameter);
            //g.DrawArc(pen, location.Px - s.Width / 2, location.Py - s.Height / 2, s.Width, s.Height, 0, 340);

            Bitmap myBitmap = new Bitmap(@"..\..\..\Pictures\ufo.png");
            Rectangle testRect = new Rectangle(Convert.ToInt32(location.Px - s.Width / 2), Convert.ToInt32(location.Py - s.Height / 2), s.Width, s.Height);
            g.DrawImage(RotateImage(myBitmap, rotatingAngle), testRect);
            if (rotatingAngle < 360)
            {
                rotatingAngle += 2;
            }
            else
            {
                rotatingAngle = 0;
            }

        }

        Bitmap RotateImage(Bitmap b, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                //move rotation point to center of image
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                //rotate
                g.RotateTransform(angle);
                //move image back
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                //draw passed in image onto graphics object
                g.DrawImage(b, new Point(0, 0));
            }
            return returnBitmap;
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
