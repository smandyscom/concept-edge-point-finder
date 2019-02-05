using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Interface;
using System.Drawing;
using System.ComponentModel;

namespace WindowsFormsApp2.DrawObjects
{
  public abstract  class LineBase : Idraw , INotifyPropertyChanged
    {
        protected static Pen __pen = new Pen(Color.Black, 3);
        public bool isSelected { get; set; } = false;
        public SnapPoint __start { get; set; }
        public SnapPoint __end { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
		{
			PropertyChanged?.Invoke(this, eventArgs);
		}

		public virtual void draw(Graphics graphics)
        {
            if (isSelected)
                __pen.Color = Color.Purple;
            else
                __pen.Color = Color.Black;

            graphics.DrawLine(__pen, __start.Location, __end.Location);
        }

        public LineBase()
        {
            __start = new SnapPoint(PointF.Empty, PointType.start);
            __end = new SnapPoint(PointF.Empty, PointType.end);
        }

        public virtual List<SnapBase> GetSnapPoints()
        {
            return new List<SnapBase>() { __start,__end};
        }

        public virtual bool isHitObject(PointF hit)
        {
            isSelected = false;

            PointF p1 = __start.Location;
            PointF p2 = __end.Location;
            float halflinewidth = 1.0f;


            // check bounding rect, this is faster than creating a new rectangle and call r.Contains
            double lineLeftPoint = Math.Min(p1.X, p2.X) - halflinewidth;
            double lineRightPoint = Math.Max(p1.X, p2.X) + halflinewidth;
            if (hit.X < lineLeftPoint || hit.X > lineRightPoint)
                return false;

            double lineBottomPoint = Math.Min(p1.Y, p2.Y) - halflinewidth;
            double lineTopPoint = Math.Max(p1.Y, p2.Y) + halflinewidth;
            if (hit.Y < lineBottomPoint || hit.Y > lineTopPoint)
                return false;



            if (p1.Y == p2.Y) // line is horizontal
            {
                double min = Math.Min(p1.X, p2.X) - halflinewidth;
                double max = Math.Max(p1.X, p2.X) + halflinewidth;
                if (hit.X >= min && hit.X <= max)
                    return true;
                return false;
            }
            if (p1.X == p2.X) // line is vertical
            {
                double min = Math.Min(p1.Y, p2.Y) - halflinewidth;
                double max = Math.Max(p1.Y, p2.Y) + halflinewidth;
                if (hit.Y >= min && hit.Y <= max)
                    return true;
                return false;
            }

            // using COS law
            // a^2 = b^2 + c^2 - 2bc COS A
            // A = ACOS ((a^2 - b^2 - c^2) / (-2bc))
            double xdiff = Math.Abs(p2.X - hit.X);
            double ydiff = Math.Abs(p2.Y - hit.Y);
            double aSquare = Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2);
            double a = Math.Sqrt(aSquare);

            xdiff = Math.Abs(p1.X - p2.X);
            ydiff = Math.Abs(p1.Y - p2.Y);
            double bSquare = Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2);
            double b = Math.Sqrt(bSquare);

            xdiff = Math.Abs(p1.X - hit.X);
            ydiff = Math.Abs(p1.Y - hit.Y);
            double cSquare = Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2);
            double c = Math.Sqrt(cSquare);
            double A = Math.Acos(((aSquare - bSquare - cSquare) / (-2 * b * c)));

            // once we have A we can find the height (distance from the line)
            // SIN(A) = (h / c)
            // h = SIN(A) * c;
            double h = Math.Sin(A) * c;

            // now if height is smaller than half linewidth, the hitpoint is within the line
            isSelected = (h <= halflinewidth);
            return isSelected;
        }

        public abstract Idraw Update(object data = null);
       
    }
}
