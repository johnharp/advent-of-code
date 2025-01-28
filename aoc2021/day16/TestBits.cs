using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace day16
{
    [TestClass]
    public class TestBits
    {
        [TestMethod]
        public void HexDigitToBinary()
        {
            Bits b = new Bits();

            Assert.AreEqual("0000", b.HexDigitToBinary('0'));
            Assert.AreEqual("0001", b.HexDigitToBinary('1'));
            Assert.AreEqual("0010", b.HexDigitToBinary('2'));
            Assert.AreEqual("0011", b.HexDigitToBinary('3'));
            Assert.AreEqual("0100", b.HexDigitToBinary('4'));
            Assert.AreEqual("0101", b.HexDigitToBinary('5'));
            Assert.AreEqual("0110", b.HexDigitToBinary('6'));
            Assert.AreEqual("0111", b.HexDigitToBinary('7'));
            Assert.AreEqual("1000", b.HexDigitToBinary('8'));
            Assert.AreEqual("1001", b.HexDigitToBinary('9'));
            Assert.AreEqual("1010", b.HexDigitToBinary('A'));
            Assert.AreEqual("1011", b.HexDigitToBinary('B'));
            Assert.AreEqual("1100", b.HexDigitToBinary('C'));
            Assert.AreEqual("1101", b.HexDigitToBinary('D'));
            Assert.AreEqual("1110", b.HexDigitToBinary('E'));
            Assert.AreEqual("1111", b.HexDigitToBinary('F'));
        }

        [TestMethod]
        public void HexToBinary()
        {
            Bits b = new Bits();
            Assert.AreEqual(
                "110100101111111000101000",
                b.HexToBinary("D2FE28"));

            Assert.AreEqual(
                "00111000000000000110111101000101001010010001001000000000",
                b.HexToBinary("38006F45291200"));

            Assert.AreEqual(
                "11101110000000001101010000001100100000100011000001100000",
                b.HexToBinary("EE00D40C823060"));
        }

        [TestMethod]
        public void TestBinaryToDecimal()
        {
            Bits b = new Bits();
            Assert.AreEqual(7, b.BinaryToDecimal("111"));
            Assert.AreEqual(6, b.BinaryToDecimal("110"));
            Assert.AreEqual(3, b.BinaryToDecimal("011"));
            Assert.AreEqual(1, b.BinaryToDecimal("001"));
        }
    }
}
