using System.Drawing;
using WindowsFormsApp2.Interface;


namespace WindowsFormsApp2.DrawObjects
{
    public class Line : Idraw
    {
        public Point __start;
        public Point __end;
        public Pen __penBlack = new Pen(Color.Black, 3);
        public void draw(Graphics graphics)
        {
            graphics.DrawLine(__penBlack, __start, __end);
        }

        public SnapPoint[] GetSnapPoints()
        {
            SnapPoint p1 = new SnapPoint(__start, this);
            SnapPoint p2 = new SnapPoint(__end, this);


            SnapPoint p3 = new SnapPoint(new PointF(
                (__start.X + __end.X) / 2,
                (__start.Y + __end.Y) / 2
                ),
                this);
            return new SnapPoint[] { p1, p2, p3 };
        }
    }
}
