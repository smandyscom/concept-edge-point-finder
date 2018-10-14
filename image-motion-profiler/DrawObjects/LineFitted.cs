using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenCvSharp;
using WindowsFormsApp2.Interface;


namespace WindowsFormsApp2.DrawObjects
{
    public class LineFitted : LineBase, ICoeffcient
    {
        Mat __coefficient = new Mat();

        public List<SnapBase> __selectedPoints = new List<SnapBase>();

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


        public override bool isHitObject(PointF hit)
        {
            return (isSelected = false);
        }

        public override Idraw Update(object data = null)
        {
            __selectedPoints.ForEach(p => p.Update());
            //turns selection snap point into coeff array
            Mat __yVectors = Mat.Zeros(__selectedPoints.Count, 1, MatType.CV_64FC1);
            Coefficient =
               Fitting.Fitting.DataFitting(Utils.ToCoordsCoefficients(__selectedPoints), __yVectors, Fitting.Fitting.FittingCategrory.Polynominal, 1);
            return this;
        }
    }
}
