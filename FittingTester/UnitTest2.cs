using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WindowsFormsApp2;

namespace FittingTester
{
    public struct Data : 
        IEquatable<Data>
    {
         public int generation;
         public int order;

        bool IEquatable<Data>.Equals(Data other)
        {
            return other.generation == generation &&
                other.order == order;
        }
    }

    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethodHierarchyNode1()
        {
            // Root(0,0)
            // |
            // |-------Branch10------Branch20-------------------Leaf30
            // |                 |---Leaf21
            // |                 |---Leaf22
            // L-------Branch20------Leaf23
            //                   |--Leaf24

            Data __root00 = new Data(){ generation=0, order=0 };

        }
    }
}
