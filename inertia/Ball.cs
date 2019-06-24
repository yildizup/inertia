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
        PVector mousePos;
        PVector ballPos;
        PVector velocity;
        PVector acceleration;
        float mass;

        public Ball(float m, float x, float y)
        {
            mass = m;
            ballPos = new PVector(x, y);
            velocity = new PVector(0, 0);
            acceleration = new PVector(0, 0);
        }

        public void update()
        {
            velocity.add(acceleration);
            ballPos.add(velocity);
            velocity.limit(60);
            acceleration.multiplicate(0);
        }

        //for now i solved the problem with this
        public void getMousePos(float x, float y)
        {
            mousePos = new PVector(x, y);
        }

        PVector ballToMouse()
        {
            PVector dir = PVector.subtract(mousePos, ballPos);
            dir.normalize();
            dir.multiplicate(0.4F);
            return dir;
        }

        public void applyForce(PVector force)
        {
            PVector f = force.get();
            f.divide(mass); //since F = m * a --> a = F/m
            acceleration.add(f);
        }

        public void drawBall(Graphics g)
        {
            Pen pen = Pens.Black;
            Size s = new Size(Convert.ToInt32(mass * 8), Convert.ToInt32(mass * 8));
            Rectangle rect = new Rectangle(Convert.ToInt32(ballPos.Px), Convert.ToInt32(ballPos.Py), s.Width, s.Height);
            g.DrawEllipse(pen, rect);
        }

        public PVector BallPos
        {
            get { return ballPos; }
            set { ballPos = value; }
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

        public float Mass
        {
            get { return mass; }
            set { mass = value; }
        }
    }
}
