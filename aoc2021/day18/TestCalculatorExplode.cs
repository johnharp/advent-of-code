using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day18
{
    [TestClass]
    public class TestCalculatorExplode
    {
        [TestMethod]
        public void Test1()
        {
            var c = new Calculator();
            var e = Element.CreateElementFromString("[[[[[9,8],1],2],3],4]");
            c.Explode(e);
            Assert.AreEqual("[[[[0,9],2],3],4]", e.ToString());

            e = Element.CreateElementFromString("[7,[6,[5,[4,[3,2]]]]]");
            c.Explode(e);
            Assert.AreEqual("[7,[6,[5,[7,0]]]]", e.ToString());

            e = Element.CreateElementFromString("[[6,[5,[4,[3,2]]]],1]");
            c.Explode(e);
            Assert.AreEqual("[[6,[5,[7,0]]],3]", e.ToString());

            e = Element.CreateElementFromString("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");
            c.Explode(e);
            Assert.AreEqual("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]", e.ToString());

            e = Element.CreateElementFromString("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            c.Explode(e);
            Assert.AreEqual("[[3,[2,[8,0]]],[9,[5,[7,0]]]]", e.ToString());
        }
    }
}
