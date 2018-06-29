using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WindowsFormsApp2.Interface;
using WindowsFormsApp2.DrawObjects;
using OpenCvSharp;
namespace WindowsFormsApp2
{
    class Utils
    {

        public static PointF GetIntersectPoint(Idraw shape1, Idraw shape2)
        {
            Type type1 = shape1.GetType();
            Type type2 = shape2.GetType();
            if (type1 == typeof(Line) && type2 == typeof(Line))
            {
                Line line1 = shape1 as Line;
                Line line2 = shape2 as Line;
                return LinesIntersect(line1.__start, line1.__end, line2.__start, line2.__end, false, false);
            }

            return PointF.Empty;


        }

        public static PointF LinesIntersect(System.Drawing.Point lp1, System.Drawing.Point lp2, System.Drawing.Point lp3, System.Drawing.Point lp4, bool extendA, bool extendB)
        {
            double x1 = lp1.X;
            double x2 = lp2.X;
            double x3 = lp3.X;
            double x4 = lp4.X;
            double y1 = lp1.Y;
            double y2 = lp2.Y;
            double y3 = lp3.Y;
            double y4 = lp4.Y;
            PointF intersection = PointF.Empty;
            double denominator = ((y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1));
            if (denominator == 0) // lines are parallel
                return PointF.Empty;
            double numerator_ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3));
            double numerator_ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3));
            double ua = numerator_ua / denominator;
            double ub = numerator_ub / denominator;
            // if a line is not extended then ua (or ub) must be between 0 and 1
            if (extendA == false)
            {
                if (ua < 0 || ua > 1)
                    return PointF.Empty;
            }
            if (extendB == false)
            {
                if (ub < 0 || ub > 1)
                    return PointF.Empty;
            }
            if (extendA || extendB) // no need to chck range of ua and ub if check is one on lines 
            {
                intersection.X = Convert.ToSingle(x1 + ua * (x2 - x1));
                intersection.Y = Convert.ToSingle(y1 + ua * (y2 - y1));
                return intersection;
            }
            if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
            {
                intersection.X = Convert.ToSingle(x1 + ua * (x2 - x1));
                intersection.Y = Convert.ToSingle(y1 + ua * (y2 - y1));
                return intersection;
            }

            return PointF.Empty;
        }





        public static PointF GetEdgePoint(ref Mat imgGray, ref Line lineEngaged)
        {

            Dictionary<PointF, MatType> __mattypeTable = new Dictionary<PointF, MatType>();
            Dictionary<PointF, byte> __grayValueTable = new Dictionary<PointF, byte>();

            // clear table
            // interpolate start-end , load into table
            // access each point's gray value
            // post handling
            __mattypeTable.Clear();
            __grayValueTable.Clear();

            PointF __vector = lineEngaged.__end - new System.Drawing.Size(lineEngaged.__start);

            float __distance = (float)Math.Sqrt(Math.Pow(__vector.X, 2) + Math.Pow(__vector.Y, 2));
            ///turns into unit vector
            __vector.X = __vector.X / __distance;
            __vector.Y = __vector.Y / __distance;

            PointF __accumulation = lineEngaged.__start;

            double __acuumulatedLength = 0;
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

    }   //Utils
}   //namespace
