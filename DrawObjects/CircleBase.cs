using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp2.Interface;

namespace WindowsFormsApp2.DrawObjects
{
    public abstract class CircleBase : Idraw
    {
        protected static Pen __pen = new Pen(Color.Yellow, 3);

        internal SnapPoint __center;
        internal float __radius;


        public CircleBase()
        {
            __center = new SnapPoint(this, PointType.center);
        }

        public bool isSelected
        {
            get;
            set;
        } = false;

        /// <summary>
        /// Offseted
        /// https://stackoverflow.com/questions/1835062/drawing-circles-with-system-drawing
        /// </summary>
        /// <param name="graphics"></param>
        public void draw(Graphics graphics)
        {
            graphics.DrawEllipse(__pen,
                __center.Location.X - __radius,
                __center.Location.Y - __radius,
                __radius*2,
                __radius*2);
        }

        public List<SnapPoint> GetSnapPoints()
        {
            return new List<SnapPoint>() {__center};
        }

        public bool isHitObject(PointF hit)
        {
            //throw new NotImplementedException();
            return false;
        }

        public abstract Idraw Update(object data = null);
      
    }
}
