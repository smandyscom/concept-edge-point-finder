using System.Drawing;
using WindowsFormsApp2.Interface;
using OpenCvSharp;
using System.Collections.Generic;
using System;
using System.Linq;

namespace WindowsFormsApp2.DrawObjects
{
    public class Line :
        Idraw,
        ICoeffcient
    {
        public SnapPoint __start;
        public SnapPoint __end;

        SnapPoint __mid;
        SnapPoint __edge;

        static Pen __penBlack = new Pen(Color.Black, 3);
        static Pen __penGreen = new Pen(Color.Green, 3);

        Mat __coefficient = new Mat();

        public bool isSelected { get; set; }

        /// <summary>
        /// ax+by+c=0
        /// input : [a b c]
        /// </summary>
        public Mat Coefficient
        {
            get { return __coefficient; }
            set
            {
                //take [a b c] turns into start/end point
                __coefficient = value;

                __start.Location.X = 0;
                __start.Location.Y = (float)((-1 * __coefficient.At<double>(0, 2)) / __coefficient.At<double>(0, 1)); // y = -c/b

                __end.Location.Y = 0;
                __end.Location.X = (float)((-1 * __coefficient.At<double>(0, 2)) / __coefficient.At<double>(0, 0));// x = -c/a
            }
        }


        public void draw(Graphics graphics, Mat gray = null)
        {

            if (gray != null)
                __edge.Location = GetEdgePoint(gray);

            __mid.Location.X = (__start.Location.X + __end.Location.X) / 2;
            __mid.Location.Y = (__start.Location.Y + __end.Location.Y) / 2;

            draw(graphics);
        }

        public void draw(Graphics graphics)
        {
            if (isSelected)
                __penBlack.Color = Color.Purple;
            else
                __penBlack.Color = Color.Black;

            graphics.DrawLine(__penBlack, __start.Location, __end.Location);

            float width = 10;
            graphics.DrawEllipse(__penGreen, __edge.Location.X - width / 2, __edge.Location.Y - width / 2, width, width);
        }


        public Line()
        {
            __start = new SnapPoint(this, PointType.start);
            __end = new SnapPoint(this, PointType.end);
            __mid = new SnapPoint(this, PointType.mid);
            __edge = new SnapPoint(this, PointType.edge);
        }

        public SnapPoint[] GetSnapPoints()
        {
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

        public bool isHitObject(PointF p3)
        {
            isSelected = false;

            PointF p1 = __start.Location;
            PointF p2 = __end.Location;
            float halflinewidth = 1.0f;


            // check bounding rect, this is faster than creating a new rectangle and call r.Contains
            double lineLeftPoint = Math.Min(p1.X, p2.X) - halflinewidth;
            double lineRightPoint = Math.Max(p1.X, p2.X) + halflinewidth;
            if (p3.X < lineLeftPoint || p3.X > lineRightPoint)
                return false;

            double lineBottomPoint = Math.Min(p1.Y, p2.Y) - halflinewidth;
            double lineTopPoint = Math.Max(p1.Y, p2.Y) + halflinewidth;
            if (p3.Y < lineBottomPoint || p3.Y > lineTopPoint)
                return false;



            if (p1.Y == p2.Y) // line is horizontal
            {
                double min = Math.Min(p1.X, p2.X) - halflinewidth;
                double max = Math.Max(p1.X, p2.X) + halflinewidth;
                if (p3.X >= min && p3.X <= max)
                    return true;
                return false;
            }
            if (p1.X == p2.X) // line is vertical
            {
                double min = Math.Min(p1.Y, p2.Y) - halflinewidth;
                double max = Math.Max(p1.Y, p2.Y) + halflinewidth;
                if (p3.Y >= min && p3.Y <= max)
                    return true;
                return false;
            }

            // using COS law
            // a^2 = b^2 + c^2 - 2bc COS A
            // A = ACOS ((a^2 - b^2 - c^2) / (-2bc))
            double xdiff = Math.Abs(p2.X - p3.X);
            double ydiff = Math.Abs(p2.Y - p3.Y);
            double aSquare = Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2);
            double a = Math.Sqrt(aSquare);

            xdiff = Math.Abs(p1.X - p2.X);
            ydiff = Math.Abs(p1.Y - p2.Y);
            double bSquare = Math.Pow(xdiff, 2) + Math.Pow(ydiff, 2);
            double b = Math.Sqrt(bSquare);

            xdiff = Math.Abs(p1.X - p3.X);
            ydiff = Math.Abs(p1.Y - p3.Y);
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
    }
}
