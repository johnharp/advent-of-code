using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day19
{
    [TestClass]
    public class TestCalculator
    {
        [TestMethod]
        public void TestMatch()
        {
            var calc = new Calculator();
            var l1 = new List<Vector3>()
            {
                new Vector3(-1, -1, 1),
                new Vector3(5, 6, -4),
                new Vector3(-2, -2, 2),
                new Vector3(-3, -3, 3),
                new Vector3(-2, -3, 1),
                new Vector3(8, 0, 7)
            };

            var l2 = new List<Vector3>()
            {
                new Vector3(8, 0, 7),
                new Vector3(5, 6, -4),
                new Vector3(5, 5, 1),
                new Vector3(-3, -3, 3),
                new Vector3(-2, -3, 1),
                new Vector3(8, 2, 1),
            };

            l1.Sort();
            l2.Sort();

            var matches = calc.Match(l1, l2);

            Assert.AreEqual(4, matches.Count);
        }

    }
}
