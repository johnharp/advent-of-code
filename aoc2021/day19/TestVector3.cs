using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day19
{
    [TestClass]
    public class TestVector3
    {
        [TestMethod]
        public void TestConstructors()
        {
            var v1 = new Vector3(17, 23, 71);
            var v2 = new Vector3("17,23,71");

            Assert.AreEqual(v1, v2);

            Assert.AreEqual("  17,  23,  71", v1.ToString());
            var v3 = new Vector3(v1.ToString());

            Assert.AreEqual(v1, v3);
        }

        [TestMethod]
        public void VerifyIsValueType()
        {
            var v1 = new Vector3(100, 200, 300);
            var v2 = v1;

            v2.x = 999;

            Assert.AreEqual(100, v1.x);
            Assert.AreEqual(999, v2.x);
        }

        [TestMethod]
        public void TestTimesScalar()
        {
            var v1 = new Vector3(1, 0, 0);
            var v2 = new Vector3(0, -1, 0);
            var v3 = new Vector3(0, 0, -1);
            Assert.AreEqual(new Vector3(-1, 0, 0), v1.TimesScalar(-1));
            Assert.AreEqual(new Vector3(0, 1, 0), v2.TimesScalar(-1));
            Assert.AreEqual(new Vector3(0, 0, 1), v3.TimesScalar(-1));

            Assert.AreEqual(new Vector3(89, 0, 0), v1.TimesScalar(89));
        }

        [TestMethod]
        public void TestCrossProduct()
        {
            var v1 = new Vector3(0, 0, -1);
            var v2 = new Vector3(0, 1, 0);
            var v3 = v1.CrossProduct(v2);
            Assert.AreEqual(new Vector3(1, 0, 0), v3);
        }
    }
}
