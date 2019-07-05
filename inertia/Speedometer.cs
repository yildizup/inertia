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
        Point point;
        Size size;
        Rectangle[] rectangles;

        int number;
        int centerCircle;
        int drawedCircle;


        public Speedometer(int n, Point p, Size s)
        {
            number = n; //amount of circles (odd number)
            centerCircle = number / 2;
            rectangles = new Rectangle[number];
            drawedCircle = number / 2; //it begins from the middle
            point = p;
            size = s;
            int distanceX = 15;
            point.X -= (number + 1) * distanceX - 3;
            int distanceY = -5;

            for (int i = 0; i < rectangles.Length; i++)
            {
                if (i < centerCircle)
                {
                    rectangles[i] = new Rectangle(point.X + (size.Width + distanceX) * i, point.Y - (size.Height + distanceY) * i, size.Width, size.Height);
                }
                if (i == centerCircle)
                {
                    rectangles[i] = new Rectangle(point.X + (size.Width + distanceX) * i, point.Y - (size.Height + distanceY) * i, size.Width, size.Height);
                }

                if (i > centerCircle)
                {

                    //"(i-(number -1)) subtracts i with the whole amount of circles to begin by zero again
                    rectangles[i] = new Rectangle(point.X + (size.Width + distanceX) * (i), point.Y + (size.Height + distanceY) * (i - (number - 1)), size.Width, size.Height);
                }
            }

        }


        public void FillCircles(Graphics g)
        {


            g.FillEllipse(Brushes.Black, rectangles[centerCircle]);


            for (int i = 0; i < rectangles.Length; i++)
            {
                g.DrawEllipse(Pens.DarkOliveGreen, rectangles[i]);
            }

            if (drawedCircle > centerCircle)
            {
                for (int i = centerCircle + 1; i <= drawedCircle; i++)
                {
                    g.FillEllipse(Brushes.Red, rectangles[i]);
                }
            }

            if (drawedCircle < centerCircle)
            {
                for (int i = centerCircle - 1; i >= drawedCircle; i--)
                {
                    g.FillEllipse(Brushes.Red, rectangles[i]);
                }
            }
        }

        public void NumberUp()
        {
            if (drawedCircle < rectangles.Length - 1)
            {
                drawedCircle++;
            }
        }

        public void NumberDown()
        {
            if (drawedCircle > 0)
            {
                drawedCircle--;
            }
        }


    }
}
