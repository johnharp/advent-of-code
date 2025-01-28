using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day02
{
    [TestClass]
    public class TestNav
    {
        [TestMethod]
        public void TestExample()
        {
            PuzzleInput input = new PuzzleInput();
            var steps = input.Example();

            Nav nav = new Nav();

            foreach(var step in steps)
            {
                nav.ApplyNavStep(step);
            }

            Assert.AreEqual(15, nav.HorizontalPosition);
            Assert.AreEqual(60, nav.Depth);
        }
    }
}
