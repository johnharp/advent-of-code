using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day16
{
    [TestClass]
    public class TestPacketValues
    {
        [TestMethod]
        public void Test1()
        {
            Packet p = Packet.PacketFromHex("C200B40A82");
            Assert.AreEqual(3, p.Value);

            p = Packet.PacketFromHex("04005AC33890");
            Assert.AreEqual(54, p.Value);

            p = Packet.PacketFromHex("880086C3E88112");
            Assert.AreEqual(7, p.Value);

            p = Packet.PacketFromHex("CE00C43D881120");
            Assert.AreEqual(9, p.Value);

            p = Packet.PacketFromHex("D8005AC2A8F0");
            Assert.AreEqual(1, p.Value);

            p = Packet.PacketFromHex("F600BC2D8F");
            Assert.AreEqual(0, p.Value);

            p = Packet.PacketFromHex("9C005AC2F8F0");
            Assert.AreEqual(0, p.Value);

            p = Packet.PacketFromHex("9C0141080250320F1802104A08");
            Assert.AreEqual(1, p.Value);
        }
    }
}
