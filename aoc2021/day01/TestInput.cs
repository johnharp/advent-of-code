using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day01
{
    [TestClass]
    public class TestInput
    {
        private Input input = new Input();

        [TestMethod]
        public void TestExampleInput()
        {
            int[] ex = input.Example();


            Assert.AreEqual(199, ex[0]);
            Assert.AreEqual(210, ex[3]);
            Assert.AreEqual(263, ex[9]);
        }

        [TestMethod]
        public void TestPartAInput()
        {
            int[] ex = input.PartA();

            Assert.AreEqual(2000, ex.Length);
            Assert.AreEqual(174, ex[0]);
            Assert.AreEqual(4618, ex[1999]);
        }
    }
}
