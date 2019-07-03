using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace inertia
{
    class Speedometer
    {
        int number;
        int numberCenterCircle; // The the center (neutral) ellipse
        int distance; //distance between two circles in y direction
        int movingFromTheCenterCircle;

        Point locationLowest;
        Rectangle[] rectangles;
        Size size;
        Graphics g;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Number of Circles (odd number)</param>
        /// <param name="point">Position of Circles (starting from lowest one)</param>
        /// <param name="s">Size of circle</param>

        public Speedometer(int n, Point point, Size s)
        {
            distance = 3;
            //Number of circles
            number = n;
            //Location of the lowest. The Circles are drawn from 0 to [number] and from bottom to top
            locationLowest = point;
            //Number of circles
            rectangles = new Rectangle[number];

            numberCenterCircle = number / 2;
            movingFromTheCenterCircle = number / 2;
            size = s;

            //Set position of each rectangle
            for (int i = 0; i < rectangles.Length; i++)
            {
                rectangles[i] = new Rectangle(point.X, point.Y - (size.Height + distance) * i, size.Width, size.Height);
            }

        }


        public void FillCircles(Graphics g)
        {
            Brush brush = Brushes.Yellow;
            Pen pen = Pens.Yellow;


            for (int i = 0; i < rectangles.Length; i++)
            {
                g.DrawEllipse(pen, rectangles[i]);
            }

            g.FillEllipse(brush, rectangles[numberCenterCircle]); //neutral circle

            //If the circle to be colored isnt the center circle
            if (movingFromTheCenterCircle != numberCenterCircle)
            {
                //If its the bottom area
                if (movingFromTheCenterCircle > numberCenterCircle)
                {
                    for (int i = numberCenterCircle + 1; i <= movingFromTheCenterCircle; i++)
                    {
                        g.FillEllipse(Brushes.Blue, rectangles[i]);
                    }
                }
                if (movingFromTheCenterCircle < numberCenterCircle)
                {
                    for (int i = number / 2- 1; i >= movingFromTheCenterCircle; i--)
                    {
                        g.FillEllipse(Brushes.Red, rectangles[i]);
                    }
                }

            }

        }


        public void NumberUp()
        {
            if (movingFromTheCenterCircle < number - 1)
            {
                movingFromTheCenterCircle += 1;
            }
        }
        public void NumberDown()
        {
            if (movingFromTheCenterCircle > 0)
            {
                movingFromTheCenterCircle -= 1;
            }
        }




    }
}
