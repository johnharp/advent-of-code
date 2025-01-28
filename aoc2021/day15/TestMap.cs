using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day15
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void TestMapIndexing()
        {
            var input = new PuzzleInput("input-example.txt");
            var map = new Map(input.Lines);

            Assert.AreEqual(1, map.Values[0][0]);
            Assert.AreEqual(2, map.Values[9][0]);
            Assert.AreEqual(3, map.Values[0][7]);
            Assert.AreEqual(9, map.Values[2][8]);
            Assert.AreEqual(3, map.Values[1][9]);

        }
    }
}
