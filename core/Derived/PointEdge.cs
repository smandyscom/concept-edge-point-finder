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
    /// Dependecies
    /// Line + image
    /// </summary>
    public class PointEdge : PointBase
    {

        //Graphic reference
        internal LineBase m_line = null;
        internal GrayImage m_image = null;

        /// <summary>
        /// Dependencies : line and image
        /// Coordiante follows line
        /// </summary>
        /// <param name="list"></param>
        public PointEdge(List<ElementBase> dependencies) : base(dependencies)
        {
            m_line = dependencies.Find(x => x.GetType() == typeof(LineBase)) as LineBase;
            m_image = dependencies.Find(x => x.GetType() == typeof(GrayImage)) as GrayImage;

            OnValueChanged(this, null);//updated
        }

        /// <summary>
        /// Method to find out edge
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public override void OnValueChanged(object sender, EventArgs args)
        {
            //TODO vector project to image's coordinate , then find

            Mat currentPoint = m_line.m_end1.Point;
            Mat unit = m_line.Vector / m_line.Length;
            double accumulatedLength = 0;

            Dictionary<Mat, byte> grayValueTable = new Dictionary<Mat, byte>();


            while (accumulatedLength <= m_line.Length)
            {
                currentPoint += unit;
                grayValueTable.Add(currentPoint,
                    m_image.Image.At<byte>(
                        currentPoint.At<int>(0),
                        currentPoint.At<int>(1)
                        )
                        );

                //unit length add one
                accumulatedLength++;
            }

            //diff and find maximum
            List<byte> grayValues = grayValueTable.Values.ToList();
            List<int> diffValues = new List<int>();
            for (int i = 0; i < grayValueTable.Count-1; i++)
            {
                diffValues.Add(Math.Abs(grayValues[i + 1] - grayValues[i]));
            }
            int edgeIndex = diffValues.IndexOf(diffValues.Max());

            //edge point calculated
            m_point = grayValueTable.Keys.ElementAt(edgeIndex);

            base.OnValueChanged(sender, args);
        }
    }
}
