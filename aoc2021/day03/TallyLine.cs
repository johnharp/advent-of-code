using System;
namespace day03
{
    public class TallyLine
    {
        public string str { get; set; }
        public char[] bits { get; set; }

        public int intValue {
            get
            {
                int bitValue = 1;
                int value = 0;
                for (int i = bits.Length - 1; i >= 0; i--)
                {
                    if (bits[i] == '1') value += bitValue;
                    bitValue = bitValue * 2;
                }
                return value;
            }
        }

        public TallyLine(string lineStr)
        {
            str = lineStr;
            bits = str.ToCharArray();
        }


    }
}
