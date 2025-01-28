using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day04
{
    [TestClass]
    public class TestBoard
    {
        [TestMethod]
        public void TestBoardConstructor()
        {
            var c = new Board(
                "14 21 17 24  4",
                "10 16 15  9 19",
                "18  8 23 26 20",
                "22 11 13  6  5",
                 "2  0 12  3  7");

            Assert.AreEqual("21", c.values[0][1]);
            Assert.AreEqual("12", c.values[4][2]);
        }

        [TestMethod]
        public void TestWin()
        {
            var c = new Board(
                "14 21 17 24  4",
                "10 16 15  9 19",
                "18  8 23 26 20",
                "22 11 13  6  5",
                 "2  0 12  3  7");

            c.marks[1][0] = true;
            c.marks[1][1] = true;
            c.marks[1][2] = true;
            c.marks[1][3] = true;


            Assert.AreEqual(false, c.IsWin());
            c.marks[1][4] = true;

            Assert.AreEqual(true, c.IsWin());
            c.marks[1][4] = false;

            c.marks[0][3] = true;
            c.marks[2][3] = true;
            c.marks[3][3] = true;
            Assert.AreEqual(false, c.IsWin());

            c.marks[4][3] = true;
            Assert.AreEqual(true, c.IsWin());

        }
    }
}
