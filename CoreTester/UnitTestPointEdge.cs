using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Arch;
using Core.Derived;
using OpenCvSharp;
using System.Diagnostics;

namespace CoreTester
{
    [TestClass]
    public class UnitTestPointEdge
    {
        /// <summary>
        /// Root coordinate
        /// </summary>
        CoordinateBase c0 = new CoordinateBase();
        PointBase p11;
        PointBase p22;
        PointBase p12;
        LineBase lc0;
        GrayImage imagec0;
        /// <summary>
        /// Refer to C0
        /// </summary>
        CoordinateBase c1;

        LineBase lc1;
        GrayImage imagec1;


        [TestInitialize]
        public void Initialize()
        {
            p11 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });
            p22 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });
            p12 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });

            p11.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 1, 1, 1 });
            p22.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 2, 2, 1 });
            p12.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 1, 2, 1 });

            lc0 = new LineBase(new System.Collections.Generic.List<ElementBase> {p11,p22 });
            imagec0 = new GrayImage(new System.Collections.Generic.List<ElementBase> { c0 });

            Cv2.CvtColor(Cv2.ImRead(@"..\..\tester1.png"),imagec0.Image, ColorConversionCodes.BGR2GRAY);


            //establish c1
            c1 = new CoordinateBase(new System.Collections.Generic.List<ElementBase> { p11, p22, p12 });

        }

        /// <summary>
        /// Image/line on the root 
        /// </summary>
        [TestMethod]
        public void TestMethodBasic()
        {
            Trace.WriteLine("");
        }

        /// <summary>
        /// Image on the root , line on the C1
        /// </summary>
        [TestMethod]
        public void TestMethodCascade()
        {

        }
    }
}
