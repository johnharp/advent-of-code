using System;
namespace day03
{
    public class TallyBitPosition
    {
        public int onesCount { get; set; }
        public int zerosCount { get; set; }
        public char mostCommon { get; set; }
        public char leastCommon { get; set; }

        public TallyBitPosition()
        {
            mostCommon = '?';
        }
    }
}
