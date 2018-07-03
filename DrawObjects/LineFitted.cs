﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenCvSharp;
using WindowsFormsApp2.Interface;


namespace WindowsFormsApp2.DrawObjects
{
    class LineFitted : LineBase, ICoeffcient
    {
        Mat __coefficient = new Mat();

        public List<SnapPoint> __selectedPoints = new List<SnapPoint>();

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


        public void Fit()
        {
            //turns selection snap point into coeff array
            Mat __xVectors = new Mat();
            Mat __yVectors = new Mat(__selectedPoints.Count, 1, MatType.CV_64FC1);
            List<Mat> __coords = new List<Mat>();

            __selectedPoints.ForEach(__snap =>
            {
                Mat __each = new Mat(1, 2, MatType.CV_64FC1);
                __each.Set<double>(0, 0, __snap.Location.X);
                __each.Set<double>(0, 1, __snap.Location.Y);

                __coords.Add(__each);
            });

            Cv2.VConcat(__coords.ToArray(), __xVectors);
            Coefficient =
               Fitting.Fitting.DataFitting(__xVectors, __yVectors, Fitting.Fitting.FittingCategrory.Polynominal, 1);
        }
    }
}
