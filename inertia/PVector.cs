using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inertia
{
    class PVector
    {

        float px, py;
        public PVector(float x, float y)
        {
            px = x;
            py = y;
        }

        #region static methods
        //static, because the class doesn't have to be initialized before using this method
        public static PVector add(PVector v1, PVector v2)
        {
            PVector v3 = new PVector(v1.px + v2.px, v1.py + v2.py);
            return v3;
        }
        public static PVector subtract(PVector v1, PVector v2)
        {
            PVector v3 = new PVector(v1.px - v2.px, v1.py - v2.py);
            return v3;
        }
        public static PVector divide(PVector v1, float num)
        {
            PVector v2 = new PVector(v1.px / num, v1.py / num);
            return v2;
        }
        public static PVector multiplicate(PVector v1, float num)
        {
            PVector v2 = new PVector(v1.px * num, v1.py * num);
            return v2;
        }

        #endregion

        public void divide(float num)
        {
            px /= num;
            py /= num;
        }

        public void multiplicate(float num)
        {
            px *= num;
            py *= num;
        }

        public void limit(float max)
        {
            float CX = px;
            float CY = py;
            float C = py / px;
            if (Math.Sqrt(px * px + py * py) > max)
            {
                px = (float)Math.Sqrt((max * max) / (1 + C * C));
                py = px * C; //since the numbers are in relation. It takes less effort to calculate the second coordinate. The same works for the third dimension.
            }

        }

        public void add(PVector vector)
        {
            px += vector.px;
            py += vector.py;
        }

        public void normalize()
        {
            multiplicate((float)Math.Sqrt(1 / (px * px + py * py)));
        }

        public float magnitude()
        {
            float mag = (float)Math.Sqrt(px * px + py * py);
            return mag;

        }

        public PVector get()
        {
            //copy the vector
            return this;
        }


        public float Px
        {
            get { return px; }
            set { px = value; }
        }
        public float Py
        {
            get { return py; }
            set { py = value; }
        }
    }
}
