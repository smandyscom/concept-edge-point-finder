using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Presentation.ViewModels
{

    public enum PointType : short
    {
        start = 0,
        end,
        mid,
        center,
        intersection,
        edge
    };

    // Equals(object obj)
    //  https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type

    public abstract class SnapBase : IDraw , INotifyPropertyChanged
	{
        public PointType Type { get; set; } = PointType.start;
        //public SnapPoint upstream = null;

        // public Idraw owner = null;

        public PointF Location { get; set; }
        protected float range = 10;

        public bool isSelected { get; set; }

		public SnapBase upstream;

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
		{
			PropertyChanged?.Invoke(this, eventArgs);
		}

		public SnapBase(PointF location, PointType type)
        {
            Location = location;
            Type = type;
        }

        public bool isHitObject(PointF p)
        {
			isSelected = false;
            double leftPoint = Location.X - range;
            double rightPoint = Location.X + range;
            if (p.X < leftPoint || p.X > rightPoint)
                return false;

            double bottomPoint = Location.Y - range;
            double topPoint = Location.Y + range;
            if (p.Y < bottomPoint || p.Y > topPoint)
                return false;

			isSelected = true;
            return isSelected;
        }

        public double Distance2(PointF p)
        {
            return Math.Pow(Location.X - p.X, 2) + Math.Pow(Location.Y - p.Y, 2);
        }

        public virtual void draw(Graphics g)
        {
            g.DrawRectangle(new Pen(Color.Cyan), Location.X - range / 2, Location.Y - range / 2, range, range);
        }

        public List<SnapBase> GetSnapPoints()
        {
            return new List<SnapBase>() { this };
        }

        public override string ToString()
        {
            return Type.ToString() + Location.ToString();
        }

        public abstract IDraw Update(object data = null);
    }

    public class SnapPoint : SnapBase
    {
        public SnapPoint(PointF location, PointType type) : base(location, type)
        {
        }
      
        public override IDraw Update(object data = null)
        {
            if (upstream != null) Location = upstream.Location;
            return this;
        }
    }

}
