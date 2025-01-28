using System;
using System.Collections.Generic;
using System.Text;

namespace day16
{
    public class Packet
    {
        public long Version;
        public long TypeId;
        public long LiteralValue;

        public long PacketBitLength = 0;

        public char SubPacketsLengthType = ' ';

        public List<Packet> Packets = new List<Packet>();
        public long NumSubPackets;
        public long SubPacketDataLength;

        public string Remainder;    // All bits left over after parsing


        public long Value
        {
            get
            {
                switch (TypeId)
                {
                    case 0:
                        return CalculateSumPacketValue();
                    case 1:
                        return CalculateProductPacketValue();
                    case 2:
                        return CalculateMinimumPacketValue();
                    case 3:
                        return CalculateMaximumPacketValue();
                    case 4:
                        return LiteralValue;
                    case 5:
                        return CalculateGreaterThanPacketValue();
                    case 6:
                        return CalculateLessThanPacketValue();
                    case 7:
                        return CalculateEqualPacketValue();
                    default:
                        throw new Exception("unknown type id");
                }
                           
            }
        }


        public long CalculateSumPacketValue()
        {
            long v = 0;
            foreach(var p in Packets)
            {
                v += p.Value;
            }

            return v;
        }

        public long CalculateProductPacketValue()
        {
            long v = 1;
            foreach (var p in Packets)
            {
                v *= p.Value;
            }

            return v;
        }

        public long CalculateMinimumPacketValue()
        {
            long min = long.MaxValue;
            if (Packets.Count == 0) throw new Exception("CalculateMinimumPacketValue: no subpackets");
            foreach (var p in Packets)
            {
                if (p.Value < min) min = p.Value;
            }

            return min;
        }

        public long CalculateMaximumPacketValue()
        {
            long max = long.MinValue;
            if (Packets.Count == 0) throw new Exception("CalculateMaximumPacketValue: no subpackets");
            foreach (var p in Packets)
            {
                if (p.Value > max) max = p.Value;
            }

            return max;
        }


        public long CalculateGreaterThanPacketValue()
        {
            if (Packets.Count != 2) throw new Exception("expected 2 subpackets");

            if (Packets[0].Value > Packets[1].Value) return 1;
            else return 0;
        }

        public long CalculateLessThanPacketValue()
        {
            if (Packets.Count != 2) throw new Exception("expected 2 subpackets");

            if (Packets[0].Value < Packets[1].Value) return 1;
            else return 0;
        }

        public long CalculateEqualPacketValue()
        {
            if (Packets.Count != 2) throw new Exception("expected 2 subpackets");

            if (Packets[0].Value == Packets[1].Value) return 1;
            else return 0;
        }

        public long VersionSum
        {
            get
            {
                long sum = Version;
                foreach(Packet subpacket in Packets)
                {
                    sum += subpacket.VersionSum;
                }

                return sum;
            }
        }
        public Packet(string binary)
        {
            var b = new Bits();
            if (binary.Length < 6) throw new Exception("Invalid packet -- missing version and type ID");

            Version = b.BinaryToDecimal(binary.Substring(0, 3));
            TypeId = b.BinaryToDecimal(binary.Substring(3, 3));

            // all packets are at least 6 bits long (3 bits for version and type)
            PacketBitLength = 6;

            Remainder = binary.Substring(6);
            
            if (TypeId == 4)
            {
                // Literal Value Packet
                ParseLiteral();
            }
            else
            {
                // Operator Packet
                ParseOperator();
            }
        }

        public static Packet PacketFromHex(string hex)
        {
            var b = new Bits();
            string binary = b.HexToBinary(hex);
            return new Packet(binary);
        }

        // Given the r
        private void ParseLiteral()
        {
            var b = new Bits();
            var sb = new StringBuilder();

            int i = 0;
            bool handledLastGroup = false;

            while ((i<Remainder.Length) && (!handledLastGroup))
            {
                char groupEndBit = Remainder[i];
                if (groupEndBit == '0') handledLastGroup = true;

                sb.Append(Remainder.Substring(i + 1, 4));
                i += 5;
                PacketBitLength += 5;
            }

            Remainder = Remainder.Substring(i);

            LiteralValue = b.BinaryToDecimal(sb.ToString()); 
        }

        private void ParseOperator()
        {
            Bits b = new Bits();
            SubPacketsLengthType = Remainder[0];
            Remainder = Remainder.Substring(1);

            PacketBitLength += 1;

            if (SubPacketsLengthType == '0')
            {
                string subpacketsNumBits = Remainder.Substring(0, 15);
                PacketBitLength += 15;

                SubPacketDataLength = b.BinaryToDecimal(subpacketsNumBits);
                PacketBitLength += SubPacketDataLength;

                Remainder = Remainder.Substring(15);

                long numBitsSubpacketsCreated = 0;

                while (numBitsSubpacketsCreated < SubPacketDataLength)
                {
                    Packet p = new Packet(Remainder.Substring((int)numBitsSubpacketsCreated));
                    numBitsSubpacketsCreated += p.PacketBitLength;
                    Packets.Add(p);
                }
            }
            else if (SubPacketsLengthType == '1')
            {
                string numSubPacketsBits = Remainder.Substring(0, 11);
                NumSubPackets = b.BinaryToDecimal(numSubPacketsBits);
                Remainder = Remainder.Substring(11);
                PacketBitLength += 11;

                for (int n = 0; n<NumSubPackets; n++)
                {
                    Packet p = new Packet(Remainder);
                    PacketBitLength += p.PacketBitLength;

                    Remainder = Remainder.Substring((int)p.PacketBitLength);
                    Packets.Add(p);
                }
            }
            else
            {
                throw new Exception($"Invalid packet length type: {SubPacketsLengthType}");
            }
        }
    }
}
