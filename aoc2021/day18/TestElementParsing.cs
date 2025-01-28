using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day18
{
    [TestClass]
    public class TestElementParsing
    {
        [TestMethod]
        public void Test1()
        {
            var el = Element.CreateElementFromString("[[1,2],3]");
            Assert.IsTrue(el.IsPair);
            Assert.IsTrue(el.Left.IsPair);
            Assert.IsTrue(el.Right.IsScalar);
            Assert.AreEqual(3, el.Right.Value);
            Assert.AreEqual(1, el.Left.Left.Value);
            Assert.AreEqual(2, el.Left.Right.Value);

            el = Element.CreateElementFromString("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            Assert.IsTrue(el.IsPair);
            Assert.AreEqual(0, el.NestedLevel);
            Assert.IsTrue(el.Left.IsPair);
            Assert.AreEqual(1, el.Left.NestedLevel);
            Assert.AreEqual(3, el.Left.Right.Right.NestedLevel);
            Assert.AreEqual(4, el.Left.Right.Right.Left.NestedLevel);
            Assert.AreEqual(8, el.Left.Right.Right.Left.Value);

            Assert.AreEqual(4,
                el.Right.Right.Right.Right.NestedLevel);

        }

        public void TestElementToString()
        {
            
        }
    }
}
