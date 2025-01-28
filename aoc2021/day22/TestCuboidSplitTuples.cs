using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day22
{
    [TestClass]
    public class TestCuboidSplitTuples
    {
        [TestMethod]
        public void Test1()
        {
            var result = Cuboid.SplitTuples(p:(4,6), s:(6,8));
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual((4, 5), result[0]);
            Assert.AreEqual((6, 6), result[1]);

            result = Cuboid.SplitTuples(p: (4, 12), s: (6, 8));
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual((4, 5), result[0]);
            Assert.AreEqual((6, 8), result[1]);
            Assert.AreEqual((9, 12), result[2]);

            result = Cuboid.SplitTuples(p: (4, 12), s: (1, 8));
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual((4, 8), result[0]);
            Assert.AreEqual((9, 12), result[1]);

            result = Cuboid.SplitTuples(p: (4, 12), s: (9, 12));
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual((4, 8), result[0]);
            Assert.AreEqual((9, 12), result[1]);


            result = Cuboid.SplitTuples(p: (4, 12), s: (4, 12));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual((4, 12), result[0]);

            result = Cuboid.SplitTuples(p: (-83_015, -9_461), s: (-93_533, -4_276));
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual((-83_015, -9_461), result[0]);

        }
    }
}
