using System;
using System.Collections.Generic;

namespace day03
{
    public class Tally
    {
        public int lineLength = -1;
        public int numLines = -1;
        public List<TallyLine> TallyLines;
        public TallyBitPosition[] TallyBitPositions;

        public int gamma
        {
            get
            {
                char[] gammaBits = new char[lineLength];

                for (int i = 0; i < lineLength; i++)
                {
                    gammaBits[i] = TallyBitPositions[i].mostCommon;
                }

                TallyLine gammaTallyLine = new TallyLine(new String(gammaBits));
                return gammaTallyLine.intValue;
            }
        }

        public int epsilon
        {
            get
            {
                char[] epsilonBits = new char[lineLength];

                for (int i = 0; i < lineLength; i++)
                {
                    epsilonBits[i] = TallyBitPositions[i].leastCommon;
                }

                TallyLine epsilonTallyLine = new TallyLine(new String(epsilonBits));
                return epsilonTallyLine.intValue;
            }
        }


        public Tally(List<String> lines)
        {
            HandleLines(lines);
            ComputeBitPositions();
        }

        public void RemoveLine(TallyLine lineToRemove)
        {
            for (int i = 0; i < lineLength; i++)
            {
                TallyBitPosition bitPos = TallyBitPositions[i];

                if (lineToRemove.bits[i] == '1') bitPos.onesCount--;
                if (lineToRemove.bits[i] == '0') bitPos.zerosCount--;

                ComputeMostAndLeastCommonForBitPosition(i);
            }
            numLines--;
            TallyLines.Remove(lineToRemove);
        }


        private void HandleLines(List<String> lines)
        {
            TallyLines = new List<TallyLine>();

            bool firstLine = true;
            foreach (string line in lines)
            {
                TallyLine tallyLine = new TallyLine(line);
                TallyLines.Add(tallyLine);

                if (firstLine)
                {
                    lineLength = tallyLine.bits.Length;
                    firstLine = false;
                }
            }

            numLines = TallyLines.Count;
        }

        private void ComputeMostAndLeastCommonForBitPosition(int i)
        {
            TallyBitPosition bitPos = TallyBitPositions[i];
            if (bitPos.onesCount > bitPos.zerosCount)
            {
                bitPos.mostCommon = '1';
                bitPos.leastCommon = '0';
            }
            else if (bitPos.zerosCount > bitPos.onesCount)
            {
                bitPos.mostCommon = '0';
                bitPos.leastCommon = '1';
            }
            else
            {
                bitPos.mostCommon = '=';
                bitPos.leastCommon = '=';
            }
        }

        private void ComputeBitPositions()
        {
            TallyBitPositions = new TallyBitPosition[lineLength];
            for (int i = 0; i < lineLength; i++)
            {
                TallyBitPositions[i] = new TallyBitPosition();
            }

            foreach (var TallyLine in TallyLines)
            {
                for (int i = 0; i < lineLength; i++)
                {
                    char c = TallyLine.bits[i];

                    if (c == '0') TallyBitPositions[i].zerosCount++;
                    else if (c == '1') TallyBitPositions[i].onesCount++;
                    else throw new Exception("Invalid character in input");

                }
            }

            for (int i = 0; i < lineLength; i++)
            {
                ComputeMostAndLeastCommonForBitPosition(i);
            }
        }

        public void RemoveInvalidOxGenRatingLinesAtBitPos(int n)
        {
            var invalidItems = DetermineInvalidOxGenRatingItemsAtBitPos(n);
            foreach(var invalidItem in invalidItems)
            {
                RemoveLine(invalidItem);
            }
        }

        public void RemoveInvalidCo2ScrubRatingLinesAtBitPos(int n)
        {
            var invalidItems = DetermineInvalidCo2ScrubRatingItemsAtBitPos(n);
            foreach (var invalidItem in invalidItems)
            {
                RemoveLine(invalidItem);
            }
        }

        private bool OxGenRatingValidAtBitPos(TallyLine l, int n)
        {
            return ((l.bits[n] == TallyBitPositions[n].mostCommon) ||
                (l.bits[n] == '1' && TallyBitPositions[n].mostCommon == '='));
        }

        private bool Co2ScrubRatingValidAtBitPos(TallyLine l, int n)
        {
            return ((l.bits[n] == TallyBitPositions[n].leastCommon) ||
                (l.bits[n] == '0' && TallyBitPositions[n].mostCommon == '='));
        }

        private List<TallyLine> DetermineInvalidOxGenRatingItemsAtBitPos(int n)
        {
            var invalidItems = new List<TallyLine>();

            foreach(TallyLine l in TallyLines)
            {
                if (!OxGenRatingValidAtBitPos(l, n)) invalidItems.Add(l);
            }

            return invalidItems;
        }

        private List<TallyLine> DetermineInvalidCo2ScrubRatingItemsAtBitPos(int n)
        {
            var invalidItems = new List<TallyLine>();

            foreach (TallyLine l in TallyLines)
            {
                if (!Co2ScrubRatingValidAtBitPos(l, n)) invalidItems.Add(l);
            }

            return invalidItems;
        }
    }
}
