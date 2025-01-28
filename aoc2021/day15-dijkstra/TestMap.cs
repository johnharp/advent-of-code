using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day15_dijkstra
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void TestNeighbors()
        {
            var input = new PuzzleInput("input-example.txt");
            Map m = new Map(input.NumCols, input.NumRows);
            m.InitializeRiskValues(input.Lines);

            var neighbors = m.NonFinalNeighbors(0, 0);
            Assert.AreEqual(2, neighbors.Count);
            Assert.IsTrue(neighbors.Contains((0, 1)));
            Assert.IsTrue(neighbors.Contains((1, 0)));

            neighbors = m.NonFinalNeighbors(1, 0);
            Assert.AreEqual(3, neighbors.Count);
            Assert.IsTrue(neighbors.Contains((0, 0)));
            Assert.IsTrue(neighbors.Contains((1, 1)));
            Assert.IsTrue(neighbors.Contains((2, 0)));
        }

        [TestMethod]
        public void TestEval()
        {
            var input = new PuzzleInput("input-example.txt");
            Map m = new Map(input.NumCols, input.NumRows);
            m.InitializeRiskValues(input.Lines);

            m.Eval(0, 0);
            Assert.AreEqual(0, m.RiskSum[0, 0]);
            Assert.AreEqual(1, m.RiskSum[1, 0]);
            Assert.AreEqual(1, m.RiskSum[0, 1]);

            m.Eval(1, 0);
            Assert.AreEqual(0, m.RiskSum[0, 0]);
            Assert.AreEqual(7, m.RiskSum[2, 0]);
            Assert.AreEqual(4, m.RiskSum[1, 1]);
        }
    }
}
