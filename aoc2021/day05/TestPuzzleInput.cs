using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day05
{
    [TestClass]
    public class TestPuzzleInput
    {
        [TestMethod]
        public void TestExampleInput()
        {
            PuzzleInput input = new PuzzleInput("input-example.txt");

            Segment s0 = input.Segments[0];
            Segment s9 = input.Segments[9];

            Assert.AreEqual(0, s0.P1.x);
            Assert.AreEqual(9, s0.P1.y);
            Assert.AreEqual(8, s9.P2.x);
            Assert.AreEqual(2, s9.P2.y);

            Assert.AreEqual(9, input.MaxX);
            Assert.AreEqual(9, input.MaxY);

            Map m = new Map(input.MaxX, input.MaxY);
            foreach(var seg in input.Segments)
            {
                m.AddSegment(seg);
            }

            Console.Out.WriteLine(
                $"Number greater than 2 = {m.CountValuesGreaterThan(2)}");
        }
    }
}
