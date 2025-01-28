using System;
using System.Text;
using System.Collections.Generic;

namespace day16
{
    public class Bits
    {

        public string HexToBinary(string hex)
        {
            var sb = new StringBuilder();
            foreach (var hexchar in hex.ToCharArray())
            {
                sb.Append(HexDigitToBinary(hexchar));
            }

            return sb.ToString();
        }

        public string HexDigitToBinary(char hexchar)
        {
            string binary = "";
            string hexdigit = new string(hexchar, 1);
            int intValue = Convert.ToInt32(hexdigit, 16);

            binary = Convert.ToString(intValue, 2);
            binary = binary.PadLeft(4, '0');
            return binary;
        }

        public long BinaryToDecimal(string binary)
        {
            long v = 0;
            char[] characters = binary.ToCharArray();

            long positionValue = 1;

            for (long i = characters.Length-1; i>=0; i--)
            {
                if (characters[i] == '1')
                {
                    v += positionValue;
                }

                positionValue *= 2;
            }

            return v;
        }

        public Bits()
        {
        }
    }
}
