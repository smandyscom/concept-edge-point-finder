using System.Drawing;
using WindowsFormsApp2.Interface;
using OpenCvSharp;
using System.Collections.Generic;
using System;
using System.Linq;

namespace WindowsFormsApp2.DrawObjects
{
    public class Line : Idraw
    {
        public SnapPoint __start;
        public SnapPoint __end;

        SnapPoint __mid;
        SnapPoint __edge;

        static Pen __penBlack = new Pen(Color.Black, 3);
        static Pen __penGreen = new Pen(Color.Green, 3);
        public bool isSelected = false;

        public void draw(Graphics graphics, Mat gray = null)
        {
            graphics.DrawLine(__penBlack, __start.Location, __end.Location);
            if (gray != null)
                __edge.Location = GetEdgePoint(gray);
            float width = 10;
            graphics.DrawEllipse(__penGreen, __edge.Location.X - width / 2, __edge.Location.Y - width / 2, width, width);
        }

        public bool isHitOnObject(PointF hit)
        {
            return (isSelected = false);
        }

        public Line()
        {
            __start = new SnapPoint(this);
            __end = new SnapPoint(this);
            __mid = new SnapPoint(this);
            __edge = new SnapPoint(this);
        }

        public SnapPoint[] GetSnapPoints()
        {
            __mid.Location.X = (__start.Location.X + __end.Location.X) / 2;
            __mid.Location.Y = (__start.Location.Y + __end.Location.Y) / 2;
            return new SnapPoint[] { __start, __end, __mid, __edge };
        }


        PointF GetEdgePoint(Mat imgGray)
        {

            PointF __vector = __end.Location - new System.Drawing.Size(System.Drawing.Point.Round(__start.Location));

            float __distance = (float)Math.Sqrt(Math.Pow(__vector.X, 2) + Math.Pow(__vector.Y, 2));
            ///turns into unit vector
            __vector.X = __vector.X / __distance;
            __vector.Y = __vector.Y / __distance;
            if (__distance <= 0)
                return PointF.Empty;
            PointF __accumulation = __start.Location;

            double __acuumulatedLength = 0;

            // interpolate start-end , load into table
            // access each point's gray value
            // post handling
            Dictionary<PointF, MatType> __mattypeTable = new Dictionary<PointF, MatType>();
            Dictionary<PointF, byte> __grayValueTable = new Dictionary<PointF, byte>();


            while (__acuumulatedLength <= __distance)
            {
                __accumulation += new SizeF(__vector);

                //establish pair of coordinate , gray value
                __mattypeTable.Add(__accumulation,
                    imgGray.At<MatType>(
                        Convert.ToInt32(__accumulation.Y),
                        Convert.ToInt32(__accumulation.X)
                        )
                        );
                __grayValueTable.Add(__accumulation,
                    imgGray.At<byte>(
                        Convert.ToInt32(__accumulation.Y),
                        Convert.ToInt32(__accumulation.X)
                        )
                        );

                //unit length added one
                __acuumulatedLength++;
            }

            //diff and find maximum
            List<byte> __grayValueList = __grayValueTable.Values.ToList();
            List<int> __diffValueList = new List<int>();
            for (int i = 0; i < __grayValueList.Count - 1; i++)
            {
                __diffValueList.Add(Math.Abs(__grayValueList[i + 1] - __grayValueList[i]));
            }
            int edgeIndex = __diffValueList.IndexOf(__diffValueList.Max());

            return __grayValueTable.Keys.ElementAt(edgeIndex);
        }


    }
}
