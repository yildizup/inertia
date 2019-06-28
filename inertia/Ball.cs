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
        PVector ballPos;
        PVector velocity;
        PVector acceleration;
        float mass;

        public Ball(float m, float x, float y)
        {
            mass = m;
            BallPos = new PVector(x, y);
            velocity = new PVector(0, 0);
            acceleration = new PVector(0, 0);
        }

        public void Update()
        {
            velocity.Add(acceleration);
            BallPos.Add(velocity);
            velocity.Limit(60);
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
            Size s = new Size(Convert.ToInt32(mass), Convert.ToInt32(mass));
            rect = new Rectangle(Convert.ToInt32(BallPos.Px), Convert.ToInt32(BallPos.Py), s.Width, s.Height);
            g.DrawEllipse(pen, rect);
        }


        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
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
