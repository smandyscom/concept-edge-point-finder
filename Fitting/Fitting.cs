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
        static Mat RightSingularVector(Mat matrix)
        {
            Mat w = new Mat(); //diagonal singular value matrix
            Mat u = new Mat();
            Mat vt = new Mat();
            Cv2.SVDecomp(matrix, w, u, vt);

            //convert w into columns array
            List<Mat> __columns = new List<Mat>();
            for (int i = 0; i < w.Cols; i++)
            {
                __columns.Add(w.Col[i]);
            }

            int smallestIndex = __columns.IndexOf( __columns.Find((__column) =>
            {
                return __column.Sum().Val0 == __columns.Min((___column) =>
                {
                    return ___column.Sum().Val0;
                });
            }));

            //find smallest sigular value
            return vt.T().Col[smallestIndex];            
        }
    }
}
