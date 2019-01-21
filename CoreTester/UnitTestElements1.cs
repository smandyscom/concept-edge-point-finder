using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Arch;
using Core.Derived;
using OpenCvSharp;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
namespace CoreTester
{
    [TestClass]
    public class UnitTestElements1
    {

        [TestMethod]
        public void TestMethodPointAndLine()
        {
            CoordinateBase c1 = new CoordinateBase();
            PointBase p1 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            PointBase p2 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            LineBase l1 = new LineBase(new System.Collections.Generic.List<ElementBase> { p1, p2 });

            Assert.AreEqual(l1.Length,0);

        }

        [TestMethod]
        public void TestMethodSolveLineCoeff()
        {
            CoordinateBase c1 = new CoordinateBase();
            PointBase p1 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            PointBase p2 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            LineBase l1 = new LineBase(new System.Collections.Generic.List<ElementBase> { p1, p2 });

            Mat point = Mat.Ones((int)DefinitionDimension.DIM_2D, 1, MatType.CV_64FC1);
            point.Set<double>(0, 0); //x1 
            point.Set<double>(1, 5); //y1
            p1.Point = point;

            point.Set<double>(0, 5); //x2
            point.Set<double>(1, 0); //y2
            p2.Point = point;

            Mat coeff = l1.Coefficient();
            Trace.WriteLine(l1.Coefficient().ToString());

            //shoule equals to zero
            Trace.WriteLine((coeff.Transpose() * p1.Point).ToMat().Norm());
            Trace.WriteLine((coeff.Transpose() * p2.Point).ToMat().Norm());

        }

        [TestMethod]
        public void TestMethodLineFitted()
        {
        }
    }
}
