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
        CoordinateBase c1 = new CoordinateBase();
        PointBase p00;
        PointBase p55;
        PointBase p05;
        PointBase p50;
        LineBase l0000;
        LineBase l0550;
        LineBase l0055;


        [TestInitialize]
        public void Initialize()
        {
            p00 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            p55 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            p05 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });
            p50 = new PointBase(new System.Collections.Generic.List<ElementBase> { c1 });

            p00.Point.SetArray(0, 0, new double[] {  0 ,  0  }); //good mat initilizer
            p55.Point.SetArray(0, 0, new double[] {  5 ,  5  }); //good mat initilizer
            p05.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] {  0 ,  5 , 1 }); //good mat initilizer
            p50.Point = new Mat(3, 1, MatType.CV_64FC1, new double[] {  5 ,  0 , 1 }); //good mat initilizer


            l0000 = new LineBase(new System.Collections.Generic.List<ElementBase> { p00, p00 });
            l0550 = new LineBase(new System.Collections.Generic.List<ElementBase> { p05, p50 });
            l0055 = new LineBase(new System.Collections.Generic.List<ElementBase> { p00, p55 });

            Mat coeff0550 = l0550.Coefficient();
            Mat coeff0055 = l0055.Coefficient();
        }

        [TestMethod]
        public void TestMethodPointAndLine()
        {
 
            Assert.AreEqual(l0000.Length,0);

        }

        [TestMethod]
        public void TestMethodSolveLineCoeff()
        {

            

            Mat coeff = l0550.Coefficient();
            Trace.WriteLine(l0550.Coefficient().ToString());

            //shoule equals to zero
            Trace.WriteLine((coeff.Transpose() * p05.Point).ToMat().Norm());
            Trace.WriteLine((coeff.Transpose() * p50.Point).ToMat().Norm());

           
        }

        /// <summary>
        /// Intersected/Parralel
        /// </summary>
        [TestMethod]
        public void TestMethodSolvePointIntersect()
        {

            PointIntersection pit = new PointIntersection(new System.Collections.Generic.List<ElementBase> { l0550, l0055 });

            Trace.WriteLine(pit.Point.Get<double>(0));
        }



        [TestMethod]
        public void TestMethodLineFitted()
        {

        }
    }
}
