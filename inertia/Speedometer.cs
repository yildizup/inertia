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
        int distance;
        Point locationLowest;
        Rectangle[] rectangles;
        Size size;
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
            size = s;

            //Set position of each rectangle
            for (int i = 0; i < rectangles.Length; i++)
            {
                rectangles[i] = new Rectangle(point.X, point.Y - (size.Height + distance) * i, size.Width, size.Height);
            }

        }




    }
}
