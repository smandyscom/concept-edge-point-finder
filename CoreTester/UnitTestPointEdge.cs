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
        

        PointBase p100c0, p300c0;

        LineBase lc0;
        GrayImage imagec0;
        PointEdge pEdgec0;

        /// <summary>
        /// Used to establish c1
        /// </summary>
        PointBase p11;
        PointBase p22;
        PointBase p12;
        /// <summary>
        /// Refer to C0
        /// </summary>
        CoordinateBase c1;

        PointBase p100c1, p300c1;

        LineBase lc1;

        /// <summary>
        /// 
        /// </summary>
        GrayImage imagec1;
        PointEdge pEdgec1;


        [TestInitialize]
        public void Initialize()
        {
            p11 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });
            p22 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });
            p12 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });

            p11.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 1, 1, 1 });
            p22.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 2, 2, 1 });
            p12.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 1, 2, 1 });

            p100c0 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });
            p300c0 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });

            p100c0.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 100, 100, 1 });
            p300c0.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 300, 100, 1 });


            lc0 = new LineBase(new System.Collections.Generic.List<ElementBase> {p100c0,p300c0 });
            imagec0 = new GrayImage(new System.Collections.Generic.List<ElementBase> { c0 });

            Cv2.CvtColor(Cv2.ImRead(@"..\..\tester1.png"),imagec0.Image, ColorConversionCodes.BGR2GRAY);


            //establish c1
            c1 = new CoordinateBase(new System.Collections.Generic.List<ElementBase> { p11, p22, p12 });

            p100c1 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            p300c1 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });

            p100c1.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 100, 100, 1 });
            p300c1.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 300, 100, 1 });

            lc1 = new LineBase(new System.Collections.Generic.List<ElementBase> { p100c1, p300c1 });
 
        }

        /// <summary>
        /// Image/line on the root 
        /// </summary>
        [TestMethod]
        public void TestMethodBasic()
        {
            pEdgec0 = new PointEdge(new System.Collections.Generic.List<ElementBase> { lc0, imagec0 });
            //check if on expected point
            Assert.AreEqual((pEdgec0.Point - new Mat(3, 1, MatType.CV_64FC1, new double[] { 199, 100, 1 })).ToMat().Norm(), 0);

            Trace.WriteLine("");
        }

        /// <summary>
        /// Image on the root , line on the C1
        /// </summary>
        [TestMethod]
        public void TestMethodCascadeImageC0()
        {
            pEdgec1 = new PointEdge(new System.Collections.Generic.List<ElementBase> { lc1, imagec0 });
            //check if on circumference
            Assert.AreEqual((c1.Transformation * pEdgec1.Point - new Mat(3, 1, MatType.CV_64FC1, new double[] { 100, 100, 1 })).ToMat().Norm(), 100);

            Trace.WriteLine("");
        }
    }
}
