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

        PVector location;
        PVector velocity;
        PVector acceleration;
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
            ballBitmap = new Bitmap(Properties.Resources.moving_object);
            clientSizeWidth = sizeWidth;
        }

        public void Update()
        {
            velocity.Add(acceleration);
            Location.Add(velocity);
            BounceOfHorizontalBorder();
            velocity.Limit(20);

            if (this.Location.Px >= clientSizeWidth / 1.4)
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
            Pen pen = Pens.White;
            Size s = new Size((int)diameter, (int)diameter);

            //Resizes according to the size of the ball
            Rectangle resizedRectangleBitmap = new Rectangle(Convert.ToInt32(location.Px - s.Width / 2), Convert.ToInt32(location.Py - s.Height / 2), s.Width, s.Height);
            g.DrawImage(RotateImage(ballBitmap, rotatingAngle), resizedRectangleBitmap);
            if (rotatingAngle < 360)
            {
                rotatingAngle += 5;
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
                    this.velocity.Px = -1F;
                    break;

                case -2:

                    this.velocity.Px = -0.8F;
                    break;
                case -1:
                    this.velocity.Px = -0.5F;
                    break;
                case 0:

                    this.velocity.Px = 0.5F;
                    break;

                case 1:

                    this.velocity.Px = 1F;
                    break;
                case 2:

                    this.velocity.Px = 1.5F;
                    break;

                case 3:

                    this.velocity.Px = 2F;
                    break;

            }

        }

        public void BounceOfVerticalBorder(int topborder, int bottomborder)
        {
            if (location.Py - diameter / 2 < topborder + 10)
            {
                this.Velocity.Multiplicate(-0.4F);
                this.ApplyForce(new PVector(0, 4));
                location.Py += 1;
            }
            if (location.Py + diameter / 2 > bottomborder)
            {
                this.Velocity.Multiplicate(-0.3F);
                location.Py -= 1;
            }
        }

         void BounceOfHorizontalBorder()
        {
            if (this.location.Px <= this.diameter)
            {
                this.Location.Px = this.diameter +1 ;
                this.Velocity.Px = 1;


            }
        }

        #region Properties
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

        #endregion
    }
}
