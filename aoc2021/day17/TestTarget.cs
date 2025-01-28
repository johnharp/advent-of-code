using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day17
{
    [TestClass]
    public class TestTarget
    {

        [TestMethod]
        public void TestTargetConstructor()
        {
            Target t;

            t = new Target("target area: x=20..30, y=-10..-5");
            Assert.AreEqual(20, t.mintargetx);
            Assert.AreEqual(30, t.maxtargetx);
            Assert.AreEqual(-10, t.mintargety);
            Assert.AreEqual(-5, t.maxtargety);

            t = new Target("target area: x=128..160, y=-142..-88");
            Assert.AreEqual(128, t.mintargetx);
            Assert.AreEqual(160, t.maxtargetx);
            Assert.AreEqual(-142, t.mintargety);
            Assert.AreEqual(-88, t.maxtargety);
        }

        [TestMethod]
        public void TestIsHit()
        {
            var s = new ProbeState(7, 2);
            var target = new Target("target area: x=20..30, y=-10..-5");

            Assert.IsFalse(target.IsHitX(s));
            Assert.IsFalse(target.IsHitY(s));
            Assert.IsFalse(target.IsHit(s));

            // Take 4 steps
            for (int i = 0; i<4; i++) { s.Step();  }
            Assert.IsTrue(target.IsHitX(s));
            Assert.IsFalse(target.IsHitY(s));
            Assert.IsFalse(target.IsHit(s));

            // Take 3 steps
            for (int i = 0; i < 3; i++) { s.Step(); }
            Assert.IsTrue(target.IsHitX(s));
            Assert.IsTrue(target.IsHitY(s));
            Assert.IsTrue(target.IsHit(s));

            // One more step takes us below the target
            s.Step();
            Assert.IsTrue(target.IsHitX(s));
            Assert.IsFalse(target.IsHitY(s));
            Assert.IsFalse(target.IsHit(s));
        }
    }
}
