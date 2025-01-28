using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day16
{
    [TestClass]
    public class TestVersionSum
    {
        [TestMethod]
        public void Test1()
        {
            var p1 = Packet.PacketFromHex("8A004A801A8002F478");
            Assert.AreEqual(4, p1.Version);
            Assert.AreEqual(1, p1.Packets.Count);
            Assert.AreEqual(1, p1.Packets[0].Version);
            Assert.AreEqual(1, p1.Packets[0].Packets.Count);
            Assert.AreEqual(5, p1.Packets[0].Packets[0].Version);
            Assert.AreEqual(16, p1.VersionSum);

            var p2 = Packet.PacketFromHex("620080001611562C8802118E34");
            /*
            011 Version 3
            000 Operator
            1   Type 1, next 11 bits = num subpackets
            00000000010  expect 2 subpackets

            Sub 1
            000  Version 0
            000  Operator
            0    Type 0, next 15 bits = num bits of subpackets
            000000000010110  22 bits of subpackets

            SubSub1
            000     version 0
            100     literal value
            01010  Last group

            SubSub2
            101     version 5
            100     literal value
            01011


            Sub2
            001  version 1
            000  operator
            1    type 1, next 11 bits = num subpackets
            00000000010  2 subpackets

            000   version 0
            100   literal
            01100 

            011   version 3
            100   literal
            01101
            00  leftovers...
            */
            Assert.AreEqual(12, p2.VersionSum);

            var p3 = Packet.PacketFromHex("C0015000016115A2E0802F182340");
            Assert.AreEqual(23, p3.VersionSum);

            var p4 = Packet.PacketFromHex("A0016C880162017C3686B18A3D4780");
            Assert.AreEqual(31, p4.VersionSum);
        }
    }
}
