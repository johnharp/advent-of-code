using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace day02
{
    [TestClass]
    public class TestNavStep
    {
        [TestMethod]
        public void TestParseInput()
        {
            NavStep s1 = new NavStep("forward 5");

            Assert.AreEqual(NavStep.ActionValue.Forward, s1.Action);
            Assert.AreEqual(5, s1.Quantity);
        }
    }
}
