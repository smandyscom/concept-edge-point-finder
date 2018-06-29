using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2
{
    public class SnapPoint
    {
        Idraw owner = null;
        PointF location;
        public PointF Location { get { return location; } }
        float range = 10;

        public SnapPoint(PointF Location, Idraw Owner)
        {
            location = Location;
            owner = Owner;
        }

        public bool IsNearBy(PointF p)
        {

            double leftPoint = location.X - range;
            double rightPoint = location.X + range;
            if (p.X < leftPoint || p.X > rightPoint)
                return false;

            double bottomPoint = location.Y - range;
            double topPoint = location.Y + range;
            if (p.Y < bottomPoint || p.Y > topPoint)
                return false;

            return true;
        }

        public double Distance2(PointF p)
        {
            return Math.Pow(location.X - p.X, 2) + Math.Pow(location.Y - p.Y, 2);
        }


        public void draw(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Cyan), location.X - range / 2, location.Y - range / 2, range, range);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;

            SnapPoint right = (SnapPoint)obj;
            return (location.X == right.location.X) && (location.Y == right.location.Y);
        }

        public override int GetHashCode()
        {
            return (int)Math.Pow(location.X, location.Y);
        }
    }
}
