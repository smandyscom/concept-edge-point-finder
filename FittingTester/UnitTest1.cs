using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;
using WindowsFormsApp2;
using WindowsFormsApp2.Fitting;

namespace FittingTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEllipseCoeficients()
        {
            Mat __coords = Mat.Ones(1, 2, MatType.CV_64FC1);
            __coords.Set<double>(0, 0, 2);
            __coords.Set<double>(0, 1, 3);

            Mat __output = Fitting.GenerateEllipseCoeff(__coords);
        }

        [TestMethod]
        public void TestPolynominalCoefficients()
        {
            Mat __coords = Mat.Ones(1, 2, MatType.CV_64FC1);
            __coords.Set<double>(0, 0, 2);
            __coords.Set<double>(0, 1, 3);

            Mat __output = Fitting.GeneratePolynomialCoeff(__coords,1);

            __output = Fitting.GeneratePolynomialCoeff(__coords, 2);
            __output = Fitting.GeneratePolynomialCoeff(__coords, 3);
        }

        [TestMethod]
        public void TestDataFitting1()
        {
            Mat __xVector = Mat.Ones(5, 2, MatType.CV_64FC1);
            Mat __yVector = Mat.Zeros(5, 1, MatType.CV_64FC1);

            Cv2.Randn(__xVector, 1, 1);

            Mat __output = Fitting.DataFitting(__xVector, __yVector, Fitting.FittingCategrory.Polynominal, 1);
        }
    }
}
