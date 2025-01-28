using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day03
{
    [TestClass]
    public class TestPuzzleInput
    {
        [TestMethod]
        public void TestExampleInput()
        {
            PuzzleInput input = new PuzzleInput();
            var lines = input.ExampleInput();

            Assert.AreEqual(12, lines.Count);
        }
    }
}
