using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day15_dijkstra
{
    [TestClass]
    public class TestNode
    {
        [TestMethod]
        public void TestLoadedNodes()
        {
            var input = new PuzzleInput("input-example.txt");
            input.CreateNodes(5);

            Node n = Node.Get(0, 0);
            Assert.AreEqual(1, n.Risk);
            n = Node.Get(2, 6);
            Assert.AreEqual(5, n.Risk);

            n = Node.Get(1, 49);
            Assert.AreEqual(7, n.Risk);

            var ns = n.NonFinalNeighbors();
            Assert.AreEqual(3, ns.Count);
            Assert.IsTrue(ns.Contains(Node.Get(0, 49)));
            Assert.IsTrue(ns.Contains(Node.Get(2, 49)));
            Assert.IsTrue(ns.Contains(Node.Get(1, 48)));
        }
    }
}
