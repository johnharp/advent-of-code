using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace day01
{
    [TestClass]
    public class TestSonarHelper
    {
        [TestMethod]
        public void TestCountDepthIncreases()
        {
            Input input = new Input();
            int[] values = input.Example();

            SonarHelper helper = new SonarHelper();
            int numIncreases = helper.CountDepthIncreases(values);

            Assert.AreEqual(7, numIncreases);
        }

        [TestMethod]
        public void TestCountDepthIncreasesSlidingWindow()
        {
            Input input = new Input();
            int[] values = input.Example();

            SonarHelper helper = new SonarHelper();
            int num = helper.CountDepthIncreasesSlidingWindow(values);

            Assert.AreEqual(5, num);
        }
    }
}
