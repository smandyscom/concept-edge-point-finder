using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Arch;
using OpenCvSharp;
namespace CoreTester
{
    [TestClass]
    public class UnitTestCoordinates
    {
        /// <summary>
        /// Root coordinate
        /// </summary>
        CoordinateBase c0 = new CoordinateBase();
        PointBase p11;
        PointBase p22;
        PointBase p12;
        /// <summary>
        /// Refer to C0
        /// </summary>
        CoordinateBase c1;

        PointBase p11c1;
        PointBase p22c1;
        PointBase p12c1;
        /// <summary>
        /// 
        /// </summary>
        CoordinateBase c2;
        PointBase p00c2;

        CoordinateComposed cc;

        [TestInitialize]
        public void Initialize()
        {
            p11 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });
            p22 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });
            p12 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });

            p11.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] {1 , 1 , 1});
            p22.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] {2 , 2 , 1 });
            p12.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] {1 , 2 , 1 });

            c1 = new CoordinateBase(new System.Collections.Generic.List<ElementBase> { p11, p22, p12 });
            

            p11c1 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            p22c1 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            p12c1 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });

            p11c1.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 1, 1, 1 });
            p22c1.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 2, 2, 1 });
            p12c1.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 1, 2, 1 });

            c2 = new CoordinateBase(new System.Collections.Generic.List<ElementBase> { p11c1, p22c1, p12c1 });

            p00c2 = new PointBase(new System.Collections.Generic.List<ElementBase> { c2 });
            p00c2.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] { 0, 0, 1 });
        }

        [TestMethod]
        public void TestMethodCoordinateBase()
        {
            Assert.IsNotNull(c1.m_coordinateReference);
            //X-Axis
            Assert.IsTrue(Math.Abs(c1.m_transformation.Col[0].Norm() - 1) < 0.00001);
            //Y-Axis
            var value = c1.m_transformation.Col[1].Norm(); //round-off error
            Assert.IsTrue(Math.Abs(c1.m_transformation.Col[1].Norm() - 1) < 0.00001);
            //Should be perpendicular (nearly zero
            value = (c1.m_transformation.Col[0].Transpose() * c1.m_transformation.Col[1]).ToMat().Norm();
            Assert.IsTrue((c1.m_transformation.Col[0].Transpose() * c1.m_transformation.Col[1]).ToMat().Norm() < 0.00001);
                                 
        }

        [TestMethod]
        public void TestMethodCoordinateComposed()
        {
            cc = c2.Generate() as CoordinateComposed;

            var fromComposed = cc.Transformation* p00c2.Point;
            var fromCascade = c1.Transformation * c2.Transformation * p00c2.Point;

            var diff = fromComposed - fromCascade;

            Assert.AreEqual(diff.ToMat().Norm() , 0);

            Trace.WriteLine("");
        }
    }
}
