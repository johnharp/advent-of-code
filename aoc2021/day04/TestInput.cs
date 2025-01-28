using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day04
{
    [TestClass]
    public class TestInput
    {
        [TestMethod]
        public void TestExampleInput()
        {
            PuzzleInput input = new PuzzleInput("input-example.txt");

            Assert.AreEqual(27, input.CalledNumbers.Length);
            Assert.AreEqual(3, input.Boards.Count);
        }
    }
}
