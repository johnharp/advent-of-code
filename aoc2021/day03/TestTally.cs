using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day03
{
    [TestClass]
    public class TestTally
    {
        [TestMethod]
        public void TestTallyClass()
        {
            PuzzleInput input = new PuzzleInput();
            var lines = input.ExampleInput();

            Tally t = new Tally(lines);

            Assert.AreEqual(5, t.TallyBitPositions[0].zerosCount);
            Assert.AreEqual(7, t.TallyBitPositions[0].onesCount);

            Assert.AreEqual(7, t.TallyBitPositions[4].zerosCount);
            Assert.AreEqual(5, t.TallyBitPositions[4].onesCount);

            Assert.AreEqual(22, t.gamma);
            Assert.AreEqual(9, t.epsilon);

            Assert.AreEqual(12, t.numLines);

            t.RemoveLine(t.TallyLines[1]); // line removed looks like: 11110

            Assert.AreEqual(5, t.TallyBitPositions[0].zerosCount);
            Assert.AreEqual(6, t.TallyBitPositions[0].onesCount);
            Assert.AreEqual(11, t.numLines);


        }
    }
}
