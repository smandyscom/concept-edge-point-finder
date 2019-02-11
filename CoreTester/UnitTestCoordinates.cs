using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Arch;
using OpenCvSharp;
namespace CoreTester
{
    [TestClass]
    public class UnitTestCoordinates
    {
        CoordinateBase c0 = new CoordinateBase();
        PointBase p11;
        PointBase p22;
        PointBase p12;
        /// <summary>
        /// Refer to C0
        /// </summary>
        CoordinateBase c1;

        [TestInitialize]
        public void Initialize()
        {
            p11 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });
            p22 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });
            p12 = new PointBase(new System.Collections.Generic.List<ElementBase> { c0 });

            p11.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] {1 , 1 , 1});
            p22.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] {2 , 2 , 1 });
            p12.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] {1 , 2 , 1 });

        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
