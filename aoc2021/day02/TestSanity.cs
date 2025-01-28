using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day02
{
    [TestClass]
    public class TestSanity
    {
        [TestMethod]
        public void TestMathStillWorks()
        {
            Assert.AreEqual(true, 1 + 1 == 2);
        }
    }
}
