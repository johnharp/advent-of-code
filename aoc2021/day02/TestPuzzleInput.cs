using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day02
{
    [TestClass]
    public class TestPuzzleInput
    {
        [TestMethod]
        public void TestExampleInput()
        {
            PuzzleInput input = new PuzzleInput();
            var steps = input.Example();

            Assert.AreEqual(6, steps.Length);
            Assert.AreEqual(NavStep.ActionValue.Forward, steps[0].Action);
            Assert.AreEqual(5, steps[0].Quantity);

            Assert.AreEqual(NavStep.ActionValue.Forward, steps[5].Action);
            Assert.AreEqual(2, steps[5].Quantity);
        }
    }
}
