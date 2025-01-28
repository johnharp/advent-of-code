using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day05
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void TestMapMaximums()
        {
            PuzzleInput input = new PuzzleInput("input-example.txt");
            Map map = new Map(input.MaxX, input.MaxY);

            Assert.AreEqual(10, map.Values.Length);
            Assert.AreEqual(10, map.Values[0].Length);
        }
    }
}
