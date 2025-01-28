using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day19
{
    [TestClass]
    public class TestOrientationRotation
    {
        [TestMethod]
        public void TestRotate()
        {
            Vector3 v1 = new Vector3(10, 3, 1);

            // Verify normal orientation doesn't modify
            // the vector  (if the sensor is already aligned
            // with the world x, y, z, it's points don't need
            // rotation.
            Orientation o1 = new Orientation(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1));

            Assert.AreEqual(new Vector3(10, 3, 1), o1.Rotate(v1));

            // sensor x is pointed along -1,
            // y is upside down,
            // z is normal
            // equivalent to rotating -180 deg around z axis
            Orientation o2 = new Orientation(
                new Vector3(-1, 0, 0),
                new Vector3(0, -1, 0),
                new Vector3(0, 0, 1));

            Assert.AreEqual(new Vector3(-10, -3, 1), o2.Rotate(v1));


            // rotate 90 deg around y axis
            Orientation o3 = new Orientation(
                new Vector3(0, 0, -1),
                new Vector3(0, 1, 0),
                new Vector3(1, 0, 0));

            Assert.AreEqual(new Vector3(1, 3, -10), o3.Rotate(v1));

            Orientation o4 = new Orientation(
                new Vector3(0, 0, -1),
                new Vector3(0, -1, 0),
                new Vector3(-1, 0, 0));

            Assert.AreEqual(new Vector3(-1, -3, -10), o4.Rotate(v1));
        }
    }
}
