using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day16
{
    [TestClass]
    public class TestPacket
    {
        [TestMethod]
        public void TestVersionAndTypeIdParsing()
        {
            var p1 = Packet.PacketFromHex("D2FE28");
            Assert.AreEqual(6, p1.Version);
            Assert.AreEqual(4, p1.TypeId);

            var p2 = Packet.PacketFromHex("EE00D40C823060");
            Assert.AreEqual(7, p2.Version);
            Assert.AreEqual(3, p2.TypeId);

            var p3 = Packet.PacketFromHex("8A004A801A8002F478");
            Assert.AreEqual(4, p3.Version);
        }

        [TestMethod]
        public void TestLiteralValuePacket()
        {
            var p1 = Packet.PacketFromHex("D2FE28");
            Assert.AreEqual(6, p1.Version);
            Assert.AreEqual(4, p1.TypeId);
            Assert.AreEqual(2021, p1.LiteralValue);
            Assert.AreEqual("000", p1.Remainder);
            Assert.AreEqual(3 + 3 + 5*3, p1.PacketBitLength);
        }

        [TestMethod]
        public void TestOperaterPacketLengthType0()
        {
            var p1 = Packet.PacketFromHex("38006F45291200");
            Assert.AreEqual(1, p1.Version);
            Assert.AreEqual(6, p1.TypeId);
            Assert.AreEqual('0', p1.SubPacketsLengthType);
            Assert.AreEqual(27, p1.SubPacketDataLength);


            Assert.AreEqual(2, p1.Packets.Count);
            Assert.AreEqual(10, p1.Packets[0].LiteralValue);
            Assert.AreEqual(20, p1.Packets[1].LiteralValue);

            Assert.AreEqual(11, p1.Packets[0].PacketBitLength);
            Assert.AreEqual(16, p1.Packets[1].PacketBitLength);
        }

        [TestMethod]
        public void TestOperaterPacketLengthType1()
        {
            var p1 = Packet.PacketFromHex("EE00D40C823060");
            Assert.AreEqual(7, p1.Version);
            Assert.AreEqual(3, p1.TypeId);
            Assert.AreEqual('1', p1.SubPacketsLengthType);
            Assert.AreEqual(3, p1.NumSubPackets);

            Assert.AreEqual(3, p1.Packets.Count);
            Assert.AreEqual(1, p1.Packets[0].LiteralValue);
            Assert.AreEqual(2, p1.Packets[1].LiteralValue);
            Assert.AreEqual(3, p1.Packets[2].LiteralValue);
        }
    }
}
