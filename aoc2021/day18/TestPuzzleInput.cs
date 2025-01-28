using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day18
{
    [TestClass]
    public class TestPuzzleInput
    {
        [TestMethod]
        public void TestExampleInput()
        {
            var input = new PuzzleInput("input-example.txt");
            Assert.AreEqual(7, input.Lines.Length);
        }
    }
}
