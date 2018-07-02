using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2
{

   public enum PointType
    {
        start,
        end,
        mid,
        center,
        intersection,
        edge
    };


    public class SnapPoint
    {
        PointType Type = PointType.start;
        public SnapPoint upstream = null;

        Idraw owner = null;


        public PointF Location;
        float range = 10;

        public SnapPoint(PointF location, Idraw Owner)
        {
            Location = location;
            owner = Owner;
        }

        public SnapPoint(Idraw Owner,PointType type)
        {
            owner = Owner;
            Type = type;
        }

        public bool IsNearBy(PointF p)
        {

            double leftPoint = Location.X - range;
            double rightPoint = Location.X + range;
            if (p.X < leftPoint || p.X > rightPoint)
                return false;

            double bottomPoint = Location.Y - range;
            double topPoint = Location.Y + range;
            if (p.Y < bottomPoint || p.Y > topPoint)
                return false;

            return true;
        }

        public double Distance2(PointF p)
        {
            return Math.Pow(Location.X - p.X, 2) + Math.Pow(Location.Y - p.Y, 2);
        }


        public void draw(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Cyan), Location.X - range / 2, Location.Y - range / 2, range, range);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;

            SnapPoint right = (SnapPoint)obj;
            return (Location.X == right.Location.X) && (Location.Y == right.Location.Y);
        }

        public override int GetHashCode()
        {
            return (int)Math.Pow(Location.X, Location.Y);
        }
    }
}
