using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace inertia
{
    class Coin
    {
        Point point;
        Size size;
        Rectangle rectangle;




        public Coin(int posX,int posY)
        {
            point = new Point(posX, posY);
            size = new Size(5, 5);
            rectangle = new Rectangle(point, size);
        }

        public void DrawCoin(Graphics g)
        {
            g.DrawEllipse(Pens.White, rectangle);
        }



    }
}
