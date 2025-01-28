using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day17
{
    [TestClass]
    public class TestBallistics
    {
        [TestMethod]
        public void TestWillHit()
        {
            var t = new Target("target area: x=20..30, y=-10..-5");
            var b = new Ballistics(t);

            Assert.IsTrue(b.WillHit(7, 2));
            Assert.IsTrue(b.WillHit(6, 3));
            Assert.IsTrue(b.WillHit(9, 0));
            Assert.IsFalse(b.WillHit(17, -4));
            Assert.IsTrue(b.WillHit(6, 9));
            Assert.IsTrue(b.WillHit(7, 9));
            Assert.IsFalse(b.WillHit(6, 10));
        }

        [TestMethod]
        public void TestMaxAchievedY()
        {
            var t = new Target("target area: x=20..30, y=-10..-5");
            var b = new Ballistics(t);

            Assert.AreEqual(3, b.MaxAchievedY(7, 2));
            Assert.AreEqual(6, b.MaxAchievedY(6, 3));
            Assert.AreEqual(0, b.MaxAchievedY(9, 0));
            Assert.AreEqual(0, b.MaxAchievedY(17, -4));
            Assert.AreEqual(45, b.MaxAchievedY(6, 9));
        }

        [TestMethod]
        public void TestDetermineBestInitialVelocityY()
        {
            var t = new Target("target area: x=20..30, y=-10..-5");
            var b = new Ballistics(t);

            Assert.AreEqual((6, 9), b.DetermineBestInitialVelocity());
        }
    }
}
