using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace WindowsFormsApp2.Fitting
{
    public class Fitting
    {
        /// <summary>
        /// SVD solve minimum error solution
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Mat RightSingularVector(Mat matrix)
        {
            Mat w = new Mat(); //diagonal singular value matrix
            Mat u = new Mat();
            Mat vt = new Mat();
            Cv2.SVDecomp(matrix, w, u, vt);

            Point __minLocation = new Point();
            Point __maxLocation = new Point();
            Cv2.MinMaxLoc(w,out __minLocation,out __maxLocation);

            //find smallest sigular value
            return vt.T().Col[__minLocation.Y];            
        }

        /// <summary>
        /// Form : ax^2 + by^2 + cx +dy + e = 0;
        /// </summary>
        /// <param name="pointCloud"></param>
        /// <returns></returns>
        internal static Mat EllipseFitting(List<OpenCvSharp.Point> pointCloud)
        {
            //turns(stack pointClund into vertical stacked matrix
            Mat concatedCoords = new Mat();
            Mat __point = OpenCvSharp.Mat.Ones(1, 3, MatType.CV_32FC1); // row vector
            foreach (OpenCvSharp.Point var in pointCloud)
            {
                __point.Set<double>(0, 0, var.X);
                __point.Set<double>(0, 1, var.Y);

                OpenCvSharp.Cv2.VConcat(__point,concatedCoords, concatedCoords); //vertical concate
            }

            // solve right singular vecotr at first stage
            Mat __uvw =  RightSingularVector(concatedCoords);
            Mat concatedUVWs = new Mat();
            //horizon concate uvw
            for (int i = 0; i < pointCloud.Count; i++)
            {
                OpenCvSharp.Cv2.HConcat(__uvw, concatedUVWs, concatedUVWs);
            }

            Mat coeefs =  concatedUVWs * concatedCoords.T().Inv(DecompTypes.SVD);
            //would be
            //[a, 0, c
            //[0, b, d
            //[0, 0, e

            return null;
        }

        public enum FittingCategrory
        {
            Polynominal,
            Ellipse,
        }


        // Flows
        // 1. turns each xVectors to coeff matrix
        // 2. cascaded them into coeff-matrix
        // 3. get Right singular vector of coeff-matrix
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xVectors"> should be nxm , row vector as eneity</param>
        /// <param name="yVectors"> should be nx1</param>
        /// <param name="categrory"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Mat DataFitting(Mat xVectors, Mat yVectors, FittingCategrory categrory, params int[] args)
        {
            Mat xCoeffMatrix = new Mat();
            Mat __eachCoeff = new Mat();
            Mat __eachX = new Mat();

            for (int i = 0; i < xVectors.Rows; i++)
            {
                __eachX = xVectors[i, i+1, 0, xVectors.Cols];

                switch (categrory)
                {
                    case FittingCategrory.Polynominal:
                        __eachCoeff = GeneratePolynomialCoeff(__eachX, args[0]);
                        break;
                    case FittingCategrory.Ellipse:
                        __eachCoeff = GenerateEllipseCoeff(__eachX);
                        break;
                    default:
                        break;
                }

                if (i == 0)
                    xCoeffMatrix = __eachCoeff;
                else
                    Cv2.VConcat(xCoeffMatrix, __eachCoeff, xCoeffMatrix);
            }

            //General solution (yVecotr:=zero vector) + Particular solution (yVector:=non zero vector

            return RightSingularVector(xCoeffMatrix) + xCoeffMatrix.Inv(DecompTypes.SVD) * yVectors;
        }

        /// <summary>
        /// [x^n  x^(n-1) ..... x y 1 ]
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        internal static Mat GeneratePolynomialCoeff(Mat xyCoord,int order = 1)
        {
            Mat coeffVector = new Mat(1, order + 2, MatType.CV_64FC1);

            //
            coeffVector.Set<double>(0, coeffVector.Cols - 1,1);
            coeffVector.Set<double>(0, coeffVector.Cols - 2, xyCoord.At<double>(0,1)); //y
            //
            for (int i = 0; i < coeffVector.Cols-2; i++)
            {
                coeffVector.Set<double>(0, i, Math.Pow(xyCoord.At <double>(0,0), order - i));
            }

            return coeffVector;
        }

        /// <summary>
        /// [x^2 y^2 x y 1]
        /// </summary>
        /// <returns></returns>
        internal static Mat GenerateEllipseCoeff(Mat xyCoord)
        {
            Mat coeffVector = GeneratePolynomialCoeff(xyCoord);
            Mat coeffVectorQua = new Mat(1, 2, MatType.CV_64FC1);
            //
            coeffVectorQua.Set<double>(0, 0, Math.Pow(xyCoord.At<double>(0, 0), 2));
            coeffVectorQua.Set<double>(0, 1, Math.Pow(xyCoord.At<double>(0, 1), 2));
            Cv2.HConcat(coeffVectorQua, coeffVector, coeffVector);

            return coeffVector;
        }
    }
}
