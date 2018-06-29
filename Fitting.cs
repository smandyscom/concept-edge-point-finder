using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;

namespace WindowsFormsApp2.Fitting
{
    class Fitting
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

        public static Mat CircleFitting(List<OpenCvSharp.Point> pointCloud)
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
    }
}
