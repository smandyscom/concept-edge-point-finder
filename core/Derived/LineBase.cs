using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Arch;
using OpenCvSharp;

namespace Core.Derived
{
    /// <summary>
    /// Dependencies
    /// Two points
    /// </summary>
    class LineBase : ElementBase
    {
        public Mat Vector
        {
            get
            {
                return m_end2.Point - m_end1.Point;
            }
        }
        public double Length
        {
            get { return Vector.Norm(); }
        }

        public PointBase End1
        {
            get { return m_end1; }
        }
        public PointBase End2
        {
            get { return m_end2; }
        }


        internal PointBase m_end1 = null;
        internal PointBase m_end2 = null;

        public LineBase(List<ElementBase> dependencies) : base(dependencies)
        {
            //perform downcasting , lack of exception handling
            m_end1 = dependencies.First() as PointBase;
            m_end2 = dependencies.Last() as PointBase;
        }
    }
}
