using System.Drawing;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2.DrawObjects
{
    public class Ellipse : Idraw
    {
        public Pen __pen = new Pen(Color.Green, 3);
        public PointF __center;
        public float width = 10;
        public float height = 10;

        public void draw(Graphics graphics)
        {
            graphics.DrawEllipse(__pen, __center.X - width / 2, __center.Y - height / 2, width, height);
        }

        public SnapPoint[] GetSnapPoints()
        {
            SnapPoint p1 = new SnapPoint(__center, this);
            return new SnapPoint[] { p1 };
        }
    }
}
