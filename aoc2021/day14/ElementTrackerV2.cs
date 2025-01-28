using System;
using System.Collections.Generic;

namespace day14
{
    public class ElementTrackerV2
    {
        private Dictionary<string, char> RulesDictionary;
        private Dictionary<string, long> Pairs;
        private char FirstElement = ' ';
        private char LastElement = ' ';


        public ElementTrackerV2()
        {
            RulesDictionary = new Dictionary<string, char>();
            Pairs = new Dictionary<string, long>();
        }

        // rule is formatted like this
        // "CH -> B"
        public void InitializeRules(List<string> rules)
        {
            foreach (string rule in rules)
            {
                string[] parts = rule.Split(" -> ");

                char value = char.Parse(parts[1]);
                RulesDictionary[parts[0]] = value;
            }
        }

        public void InitializeElements(string str)
        {
            char[] values = str.ToCharArray();
            FirstElement = values[0];
            LastElement = values[values.Length - 1];

            for (int i = 0; i < values.Length - 1; i++)
            {
                string pair = $"{values[i]}{values[i + 1]}";
                IncPairCount(pair);
            }
        }

        public long GetPairCount(string pair)
        {
            if (Pairs.ContainsKey(pair))
            {
                return Pairs[pair];
            }
            else
            {
                return 0;
            }
        }

        public void SetPairCount(string pair, long value)
        {
            Pairs[pair] = value;
        }

        public void IncPairCount(string pair)
        {
            SetPairCount(pair, GetPairCount(pair) + 1);
        }

        public void ApplyRulesToElements()
        {
            var pairCountChanges =
                new Dictionary<string, long>();

            foreach(string pair in Pairs.Keys)
            {
                char leftEl = pair[0];
                char rightEl = pair[1];

                char newel = RulesDictionary[pair];

                string leftpair = $"{leftEl}{newel}";
                string rightpair = $"{newel}{rightEl}";

                if (!pairCountChanges.ContainsKey(pair)) pairCountChanges[pair] = 0;
                if (!pairCountChanges.ContainsKey(leftpair)) pairCountChanges[leftpair] = 0;
                if (!pairCountChanges.ContainsKey(rightpair)) pairCountChanges[rightpair] = 0;

                pairCountChanges[pair] -= Pairs[pair];
                pairCountChanges[leftpair] += Pairs[pair];
                pairCountChanges[rightpair] += Pairs[pair];
            }

            foreach(var pair in pairCountChanges.Keys)
            {
                if (!Pairs.ContainsKey(pair)) Pairs[pair] = 0;
                Pairs[pair] += pairCountChanges[pair];
            }

            var pairs = new List<string>(Pairs.Keys);

            foreach(var pair in pairs)
            {
                if (Pairs[pair] == 0)
                {
                    Pairs.Remove(pair);
                }
            }
        }

        public void DumpPairCounts()
        {
            foreach(string pair in Pairs.Keys)
            {
                Console.WriteLine($"{pair} appears {Pairs[pair]} times");
            }
        }

        public void DumpStatistics()
        {
            var stats = new Dictionary<char, long>();

            char mostFrequentChar = ' ';
            long mostFrequentCount = 0;

            char leastFrequentChar = ' ';
            long leastFrequentCount = long.MaxValue;


            // Every element in the strand is going to get
            // two copies: one for the pair before, and one
            // for the pair behind.
            // *except* for the first and last element in
            // the strand.  Those don't get the extra copy.
            // So, to gather the statistics:
            // * count the element totals for all the pairs
            // * add an additional count for the first and last
            //   element
            // * divide these counts by 2 to get the real counts
            // 
            foreach (var pair in Pairs.Keys)
            {
                long paircount = Pairs[pair];

                char char1 = pair[0];
                if (!stats.ContainsKey(char1)) stats[char1] = 0;
                stats[char1] += paircount;


                char char2 = pair[1];
                if (!stats.ContainsKey(char2)) stats[char2] = 0;
                stats[char2] += paircount;
            }
            stats[FirstElement]++;
            stats[LastElement]++;
            foreach (char c in stats.Keys)
            {
                stats[c] = stats[c] / 2;
            }

            //foreach(char c in stats.Keys)
            //{
            //    Console.WriteLine($"{c} appears {stats[c]} times");
            //}

            foreach (char c in stats.Keys)
            {
                if (stats[c] > mostFrequentCount)
                {
                    mostFrequentChar = c;
                    mostFrequentCount = stats[c];
                }

                if (stats[c] < leastFrequentCount)
                {
                    leastFrequentChar = c;
                    leastFrequentCount = stats[c];
                }
            }

            Console.WriteLine(
                $"The most frequent character is {mostFrequentChar}, " +
                $"it appears {mostFrequentCount} times");
            Console.WriteLine(
                $"The least frequent character is {leastFrequentChar}, " +
                $"it appears {leastFrequentCount} times");
            Console.WriteLine(
                $"Their difference is {mostFrequentCount - leastFrequentCount}");
        }
    }
}
