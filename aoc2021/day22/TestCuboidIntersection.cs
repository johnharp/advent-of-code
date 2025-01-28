using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day22
{
    [TestClass]
    public class TestCuboidIntersection
    {
        [TestMethod]
        public void AdjacentIsNotIntersectingInX()
        {
            var c1 = new Cuboid("on x=-20..26,y=-36..17,z=-47..7");
            var c2 = new Cuboid("on x=27..30,y=-36..17,z=-47..7");

            Assert.IsFalse(c1.Intersects(c2));
            Assert.IsFalse(c2.Intersects(c1));
        }

        [TestMethod]
        public void NotIntersectingInY()
        {
            var c1 = new Cuboid("on x=-20..26,y=-36..17,z=-47..7");
            var c2 = new Cuboid("on x=-20..26,y=19..22,z=-47..7");

            Assert.IsFalse(c1.Intersects(c2));
            Assert.IsFalse(c2.Intersects(c1));
        }

        [TestMethod]
        public void NotIntersectingInZ()
        {
            var c1 = new Cuboid("on x=-20..26,y=-36..17,z=-47..7");
            var c2 = new Cuboid("on x=-20..26,y=-36..17,z=-49..-48");

            Assert.IsFalse(c1.Intersects(c2));
            Assert.IsFalse(c2.Intersects(c1));
        }

        [TestMethod]
        public void SharingAnEdgeIsIntersecting()
        {
            var c1 = new Cuboid("on x=-20..26,y=-36..17,z=-47..7");
            var c2 = new Cuboid("on x=-30..-20,y=-36..17,z=-47..7");

            Assert.IsTrue(c1.Intersects(c2));
            Assert.IsTrue(c2.Intersects(c1));
        }

        [TestMethod]
        public void CompletelyOverlappingIsIntersecting()
        {
            var c1 = new Cuboid("on x=-20..26,y=-36..17,z=-47..7");
            var c2 = new Cuboid("on x=-20..26,y=-36..17,z=-47..7");

            Assert.IsTrue(c1.Intersects(c2));
            Assert.IsTrue(c2.Intersects(c1));
        }
    }
}
