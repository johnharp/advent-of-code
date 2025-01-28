using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day18
{
    [TestClass]
    public class TestCalculator
    {
        [TestMethod]
        public void TestAdd()
        {
            Calculator c = new Calculator();

            Element a = Element.CreateElementFromString("[1,2]");
            Element b = Element.CreateElementFromString("[[3,4],5]");

            // sum should be [[1,2],[[3,4],5]]

            Element s = c.Add(a, b);

            Assert.IsTrue(s.IsPair);
            Assert.IsFalse(s.IsScalar);
            Assert.AreEqual(0, s.NestedLevel);

            Element t1 = s.Left;
            Assert.IsTrue(t1.IsPair);
            Assert.IsFalse(t1.IsScalar);
            Assert.AreEqual(1, t1.NestedLevel);
            Assert.AreEqual(s, t1.Parent);

            Element t2 = s.Left.Left;
            Assert.IsTrue(t2.IsScalar);
            Assert.IsFalse(t2.IsPair);
            Assert.AreEqual(1, t2.Value);
            Assert.AreEqual(2, t2.NestedLevel);
            Assert.AreEqual(t1, t2.Parent);

            Element t3 = s.Left.Right;
            Assert.IsTrue(t3.IsScalar);
            Assert.IsFalse(t3.IsPair);
            Assert.AreEqual(2, t3.Value);
            Assert.AreEqual(2, t3.NestedLevel);
            Assert.AreEqual(t1, t3.Parent);

            Element t4 = s.Right.Left;
            Assert.IsTrue(t4.IsPair);
            Assert.IsFalse(t4.IsScalar);
            Assert.AreEqual(2, t4.NestedLevel);
            Assert.AreEqual(s.Right, t4.Parent);

            Assert.AreEqual(3, s.Right.Left.Left.Value);
            Assert.AreEqual(4, s.Right.Left.Right.Value);
            Assert.AreEqual(5, s.Right.Right.Value);
        }

        [TestMethod]
        public void MakeSureLeftmostIsFound()
        {
            Calculator c = new Calculator();
            var e = Element.CreateElementFromString(
                "[[1,[2,[3,[4,4]]]],[[[[10,9],8],7],6]]");
            var s = c.FindFirstPairToExplode(e);
            Assert.AreEqual("[4,4]", s.ToString());
        }

        [TestMethod]
        public void TestFindFirstElementToExplode()
        {
            Calculator c = new Calculator();

            var e = Element.CreateElementFromString("[[[[[9,8],1],2],3],4]");
            var splodee = c.FindFirstPairToExplode(e);

            Assert.AreEqual("[9,8]", splodee.ToString());

            e = Element.CreateElementFromString("[7,[6,[5,[4,[3,2]]]]]");
            splodee = c.FindFirstPairToExplode(e);

            Assert.AreEqual("[3,2]", splodee.ToString());

            e = Element.CreateElementFromString("[[6,[5,[4,[3,2]]]],1]");
            splodee = c.FindFirstPairToExplode(e);

            Assert.AreEqual("[3,2]", splodee.ToString());

            e = Element.CreateElementFromString("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");
            splodee = c.FindFirstPairToExplode(e);

            Assert.AreEqual("[7,3]", splodee.ToString());

            e = Element.CreateElementFromString("[[3,[2,[8,0]]],[9,[5,[4,[3,2]]]]]");
            splodee = c.FindFirstPairToExplode(e);

            Assert.AreEqual("[3,2]", splodee.ToString());

        }

        [TestMethod]
        public void TestExplodeReturnValue()
        {
            Calculator c = new Calculator();
            var e = Element.CreateElementFromString("[[[[0,7],4],[15,[0,13]]],[1,1]]");

            bool didExplode = c.Explode(e);
            Assert.IsFalse(didExplode);

            e = Element.CreateElementFromString("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]");
            didExplode = c.Explode(e);
            Assert.IsTrue(didExplode);
        }

        [TestMethod]
        public void TestAddToFirstNumberLeftOf()
        {
            Calculator c = new Calculator();
            var e = Element.CreateElementFromString("[[[[[9,8],1],2],3],4]");
            var p = e.Left.Left.Left.Left;
            Assert.AreEqual("[9,8]", p.ToString());
            c.AddToFirstNumberLeftOf(p, 100);
            // no number to the left, so should not modify the original
            // element
            Assert.AreEqual("[[[[[9,8],1],2],3],4]", e.ToString());

            e = Element.CreateElementFromString("[7,[6,[5,[4,[3,2]]]]]");
            p = e.Right.Right.Right.Right;
            Assert.AreEqual("[3,2]", p.ToString());
            c.AddToFirstNumberLeftOf(p, 100);
            Assert.AreEqual("[7,[6,[5,[104,[3,2]]]]]", e.ToString());

            e = Element.CreateElementFromString("[[6,[5,[4,[3,2]]]],1]");
            p = e.Left.Right.Right.Right;
            Assert.AreEqual("[3,2]", p.ToString());
            c.AddToFirstNumberLeftOf(p, 200);
            Assert.AreEqual("[[6,[5,[204,[3,2]]]],1]", e.ToString());

            e = Element.CreateElementFromString("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");
            p = e.Left.Right.Right.Right;
            Assert.AreEqual("[7,3]", p.ToString());
            c.AddToFirstNumberLeftOf(p, 300);
            Assert.AreEqual("[[3,[2,[301,[7,3]]]],[6,[5,[4,[3,2]]]]]", e.ToString());

            e = Element.CreateElementFromString("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]");
            p = e.Left.Right.Right.Left;
            Assert.AreEqual("[8,4]", p.ToString());
            c.AddToFirstNumberLeftOf(p, 400);
            Assert.AreEqual("[[[[0,7],4],[407,[[8,4],9]]],[1,1]]", e.ToString());
        }

        [TestMethod]
        public void TestAddToFirstNumberRightOf()
        {
            Calculator c = new Calculator();
            var e = Element.CreateElementFromString("[[[[[9,8],1],2],3],4]");
            var p = e.Left.Left.Left.Left;
            Assert.AreEqual("[9,8]", p.ToString());
            c.AddToFirstNumberRightOf(p, 100);
            // no number to the left, so should not modify the original
            // element
            Assert.AreEqual("[[[[[9,8],101],2],3],4]", e.ToString());

            e = Element.CreateElementFromString("[7,[6,[5,[4,[3,2]]]]]");
            p = e.Right.Right.Right.Right;
            Assert.AreEqual("[3,2]", p.ToString());
            c.AddToFirstNumberRightOf(p, 100);
            Assert.AreEqual("[7,[6,[5,[4,[3,2]]]]]", e.ToString());

            e = Element.CreateElementFromString("[[6,[5,[4,[3,2]]]],1]");
            p = e.Left.Right.Right.Right;
            Assert.AreEqual("[3,2]", p.ToString());
            c.AddToFirstNumberRightOf(p, 200);
            Assert.AreEqual("[[6,[5,[4,[3,2]]]],201]", e.ToString());

            e = Element.CreateElementFromString("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");
            p = e.Left.Right.Right.Right;
            Assert.AreEqual("[7,3]", p.ToString());
            c.AddToFirstNumberRightOf(p, 300);
            Assert.AreEqual("[[3,[2,[1,[7,3]]]],[306,[5,[4,[3,2]]]]]", e.ToString());

            e = Element.CreateElementFromString("[[[[0,7],4],[7,[[8,4],9]]],[1,1]]");
            p = e.Left.Right.Right.Left;
            Assert.AreEqual("[8,4]", p.ToString());
            c.AddToFirstNumberRightOf(p, 400);
            Assert.AreEqual("[[[[0,7],4],[7,[[8,4],409]]],[1,1]]", e.ToString());
        }
    }
}
