using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using WindowsFormsApp2.Interface;
using System.Drawing;

namespace WindowsFormsApp2.DrawObjects
{
    public class CircleFitted :  CircleBase,  ICoeffcient
    {
        Mat __coefficient = new Mat();

        public List<SnapBase> __selectedPoints = new List<SnapBase>();

        enum CoefficientDefinition : int
        {
            A=0,
            B,
            C
        };

        public Mat Coefficient
        {
            get { return __coefficient; }
            set
            {
                //take [a b c] turns into center/radius
                __coefficient = value;

                float a, b, c;
                a = (float)__coefficient.At<double>(0, (int)CoefficientDefinition.A);
                b = (float)__coefficient.At<double>(0, (int)CoefficientDefinition.B);
                c = (float)__coefficient.At<double>(0, (int)CoefficientDefinition.C);

                __center.Location.X = (float)(-1* a)/2;
                __center.Location.Y = (float)(-1 * b) / 2;

                __radius = (float)(Math.Sqrt(Math.Pow(a/2, 2) + Math.Pow(b/2, 2) - c));
            }
        }

        public override Idraw Update(object data = null)
        {
            //
            Mat __yVectors = Mat.Zeros(__selectedPoints.Count, 1, MatType.CV_64FC1);
            PointF __point;

            for (int i = 0; i < __selectedPoints.Count; i++)
            {
                __point = __selectedPoints[i].Location;
                __yVectors.Set<double>(0, i, -1 * (Math.Pow(__point.X, 2) + Math.Pow(__point.Y, 2)));
            }
             Coefficient =
               Fitting.Fitting.DataFitting(Utils.ToCoordsCoefficients(__selectedPoints), __yVectors, Fitting.Fitting.FittingCategrory.Polynominal, 1);

            return this;
        }
    }//class
}
