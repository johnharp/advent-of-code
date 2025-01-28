using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day01
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMathStillWorks()
        {
            Assert.AreEqual(true, 1 + 1 == 2);
        }
    }
}
