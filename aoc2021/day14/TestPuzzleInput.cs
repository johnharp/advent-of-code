using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day14
{
    [TestClass]
    public class TestPuzzleInput
    {
        [TestMethod]
        public void TestExampleInput()
        {
            PuzzleInput input = new PuzzleInput("input-example.txt");
            Assert.AreEqual("NNCB", input.InitialElements);

            Assert.AreEqual("CH -> B", input.InsertionRules[0]);
            Assert.AreEqual("CN -> C", input.InsertionRules[15]);

        }
    }
}
