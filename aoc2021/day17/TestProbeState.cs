using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day17
{
    [TestClass]
    public class TestProbeState
    {
        [TestMethod]
        public void TestStep()
        {
            ProbeState s;

            s = new ProbeState(initialXvel: 7, initialYvel: 2);
            Assert.AreEqual((0, 0), (s.x, s.y));

            s.Step();
            Assert.AreEqual((7, 2), (s.x, s.y));

            s.Step();
            Assert.AreEqual((13, 3), (s.x, s.y));

            s.Step();
            Assert.AreEqual((18, 3), (s.x, s.y));

            s.Step();
            Assert.AreEqual((22, 2), (s.x, s.y));

            s.Step();
            Assert.AreEqual((25, 0), (s.x, s.y));

            s.Step();
            Assert.AreEqual((27, -3), (s.x, s.y));

            s.Step();
            Assert.AreEqual((28, -7), (s.x, s.y));

            s = new ProbeState(initialXvel: 6, initialYvel: 3);
            Assert.AreEqual((0, 0), (s.x, s.y));

            s.Step();
            Assert.AreEqual((6, 3), (s.x, s.y));

            s.Step();
            Assert.AreEqual((11, 5), (s.x, s.y));

            s.Step();
            Assert.AreEqual((15, 6), (s.x, s.y));

            s.Step();
            Assert.AreEqual((18, 6), (s.x, s.y));

            s.Step();
            Assert.AreEqual((20, 5), (s.x, s.y));

            s.Step();
            Assert.AreEqual((21, 3), (s.x, s.y));

            s.Step();
            Assert.AreEqual((21, 0), (s.x, s.y));

            s.Step();
            Assert.AreEqual((21, -4), (s.x, s.y));

            s.Step();
            Assert.AreEqual((21, -9), (s.x, s.y));

        }
    }
}
