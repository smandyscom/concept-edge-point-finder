using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Arch;
using Core.Derived;


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
        public void TestMethodIntersection()
        {
        }
    }
}
