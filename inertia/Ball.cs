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
        float HorizontalVelocity;
        int mass;
        float diameter;
        int rotatingAngle = 0;
        Bitmap ballBitmap;
        int clientSizeWidth;

        public Ball(int x, int y, int m, int sizeWidth)
        {
            mass = m;
            Location = new PVector(x, y);
            velocity = new PVector(0, 0);
            acceleration = new PVector(0, 0);
            diameter = m * 1.75F;
            HorizontalVelocity = 0;
            ballBitmap = new Bitmap(Properties.Resources.moving_object);
            clientSizeWidth = sizeWidth;
        }

        public void Update()
        {
            velocity.Add(acceleration);
            Velocity.Px += HorizontalVelocity;
            Location.Add(velocity);
            velocity.Limit(20);
            if (velocity.Px > 5)
            {
                velocity.Px = 5;
            }

            if (this.Location.Px >= clientSizeWidth / 2)
            {
                this.Velocity.Px = -0.3F;
            }
            if (this.Location.Px < clientSizeWidth / 7)
            {
                this.Velocity.Px = 0.3F;
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
            Size s = new Size((int)diameter, (int)diameter);
            //g.DrawArc(pen, location.Px - s.Width / 2, location.Py - s.Height / 2, s.Width, s.Height, 0, 340);

            //Resizes according to the size of the ball
            Rectangle resizedRectangleBitmap = new Rectangle(Convert.ToInt32(location.Px - s.Width / 2), Convert.ToInt32(location.Py - s.Height / 2), s.Width, s.Height);
            g.DrawImage(RotateImage(ballBitmap, rotatingAngle), resizedRectangleBitmap);
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


        /// <summary>
        /// Changes the horizontal velocity according to the speedometer
        /// </summary>
        /// <param name="value">value of the speedometer</param>
        public void ChangeHorizontalVelocity(int value)
        {
            switch (value)
            {
                case -3:
                    HorizontalVelocity = -0.03F;
                    break;

                case -2:

                    HorizontalVelocity = -0.02F;
                    break;
                case -1:

                    HorizontalVelocity = -0.01F;
                    break;
                case 0:

                    HorizontalVelocity = 0.01F;
                    break;

                case 1:

                    HorizontalVelocity = 0.01F;
                    break;
                case 2:

                    HorizontalVelocity = 0.02F;
                    break;

                case 3:

                    HorizontalVelocity = 0.03F;
                    break;

            }

        }

        public void BounceOfBorder(int topborder, int bottomborder)
        {
            if (location.Py - diameter / 2 < topborder + 10)
            {
                this.Velocity.Multiplicate(-0.3F);
                location.Py += 1;
            }
            if (location.Py + diameter / 2 > bottomborder)
            {
                this.Velocity.Multiplicate(-0.3F);
                location.Py -= 1;
            }


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
        public float Diameter
        {

            get { return diameter; }
            set { diameter = value; }
        }
    }
}
