using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2
{

    public enum PointType : short
    {
        start = 1,
        end,
        mid,
        center,
        intersection,
        edge
    };


    public abstract class SnapBase : Idraw
    {
        public PointType Type = PointType.start;
        //public SnapPoint upstream = null;

        // public Idraw owner = null;


        public PointF Location;
        float range = 10;

        public bool isSelected { get; set; }



        public SnapBase(PointF location, PointType type)
        {
            Location = location;
            Type = type;
        }

        public bool isHitObject(PointF p)
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

            SnapBase right = (SnapBase)obj;

            if (Type != right.Type)
                return false;

            return (Location.X == right.Location.X) && (Location.Y == right.Location.Y);
        }

        public override int GetHashCode()
        {
            return (int)Math.Pow(Location.X, Location.Y) * (int)Type;
        }

        public List<SnapBase> GetSnapPoints()
        {
            return new List<SnapBase>() { this };
        }

        public override string ToString()
        {
            return Type.ToString() + Location.ToString();
        }

        public abstract Idraw Update(object data = null);
    }

    public class SnapPoint : SnapBase
    {
        public SnapBase upstream;

        public SnapPoint(PointF location, PointType type) : base(location, type)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SnapPoint right = obj as SnapPoint;
                return upstream == right.upstream;
            }
            return false;
        }
        public override Idraw Update(object data = null)
        {
            if (upstream != null) Location = upstream.Location;
            return this;
        }
    }

    public class InterSectPoint : SnapBase
    {

        private Idraw owner1;
        private Idraw owner2;
        public InterSectPoint( PointF location,Idraw owner1, Idraw owner2) : base(location, PointType.intersection)
        {
            this.owner1 = owner1;
            this.owner2 = owner2;
        }

        public override Idraw Update(object data = null)
        {
            Location = Utils.GetIntersectPoint(owner1, owner2);
            return this;
        }
    }

}
