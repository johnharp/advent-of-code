using System;
using System.Linq;
using System.Collections.Generic;

namespace day08
{
    class Program
    {
        private static string[] ALLSIGNALS = { "a", "b", "c", "d", "e", "f", "g" };
        private static string ALLSEGMENTS = "abcdefg";

        static void Main(string[] args)
        {
            PuzzleInput input = new PuzzleInput("input.txt");

            Part1(input);
            Part2(input);
        }

        private static void Part1(PuzzleInput input)
        {
            int oneFourSevenEightCount = 0;

            foreach (var line in input.Lines)
            {
                string[] patterns;
                string[] outputs;

                (patterns, outputs) = SplitLine(line);

                // 1: 2 segments
                // 4: 4 segments
                // 7: 3 segments
                // 8: 7 segments

                foreach (var output in outputs)
                {
                    int length = output.Length;

                    if (length == 2 || length == 4 || length == 3 || length == 7)
                    {
                        oneFourSevenEightCount++;
                    }
                }
            }

            Console.Out.WriteLine($"Part 1 - count of 1, 4, 7, 8: {oneFourSevenEightCount}");
        }

        private static void Part2(PuzzleInput input)
        {
            Console.Out.WriteLine("Part 2 -");
            long v = 0;
            foreach (var line in input.Lines)
            {
                string[] patterns;
                string[] outputs;

                (patterns, outputs) = SplitLine(line);
                v += Part2Line(patterns, outputs);
            }

            Console.Out.WriteLine($"SUM: {v}");
        }

        private static long Part2Line(string[] patterns, string[] outputs)
        {
            // facts dictionary has:
            //   key = a signal letter (a, b, c, d, e, f, or g)
            //   value = a string listing all the possible segments that
            //           signal could drive.  If we know nothing it would
            //           be like this "abcdefg".  If we know the exact mapping
            //           it would have a single segment letter, ex. "b"
            Dictionary<string, string> facts = InitialFacts();

            Dictionary<string, string> segmentTrans = SegmentTrans();

            //WriteLine(patterns, outputs);

            // "1" is the only number displayed using 2 segments
            string signalsForOne = FindPatternOfLength(2, patterns);
            OnlyTheseSignalsCouldBeTheseSegments(signalsForOne, "cf", facts);

            // "7" is the only number displayed using 3 segments
            string signalsForSeven = FindPatternOfLength(3, patterns);
            OnlyTheseSignalsCouldBeTheseSegments(signalsForSeven, "acf", facts);

            // "4" is the only number displayed using 4 segments
            string signalsForFour = FindPatternOfLength(4, patterns);
            OnlyTheseSignalsCouldBeTheseSegments(signalsForFour, "bcdf", facts);

            // by this point we have already have determined the 2 signals
            // that could be the "c" and "f" segments.  We just don't know
            // which is which.
            //
            // "0", "6", and "9" are all displayed using 6 segments
            // The "c" segment is lit in both "0" and "9", but is missing in
            // "6"
            // 
            string[] patternsOfLengthSix = FindPatternsOfLength(6, patterns);
            if (patternsOfLengthSix.Length != 3) throw new Exception(
                "There should be three patterns of length six.");

            string[] cfSignals = FindSignalsThatCouldBeOneOfTwoSegments("cf", facts);
            if (cfSignals.Length != 2) throw new Exception(
                "There should only be 2 signals that could be the c or f segment");
            string sig1 = cfSignals[0];
            string sig2 = cfSignals[1];

            int sig1Count = CountNumTimesSignalInPatterns(sig1, patternsOfLengthSix);
            int sig2Count = CountNumTimesSignalInPatterns(sig2, patternsOfLengthSix);

            // whichever appears 2 times is the "c"
            // whichever appears 3 times if the "f"

            if (sig1Count == 2 && sig2Count == 3)
            {
                SolveASignal(sig1, "c", facts);
                SolveASignal(sig2, "f", facts);
            }
            else if (sig1Count == 3 && sig2Count == 2)
            {
                SolveASignal(sig2, "c", facts);
                SolveASignal(sig1, "f", facts);
            }
            else
            {
                throw new Exception("algorithm failed for cf determination");
            }

            // Similarly in the 6-length group, "g" is in all three of them
            // but "e" is only in two
            string[] egSignals = FindSignalsThatCouldBeOneOfTwoSegments("eg", facts);
            if (cfSignals.Length != 2) throw new Exception(
                "There should only be 2 signals that could be the e or g segment");
            sig1 = egSignals[0];
            sig2 = egSignals[1];

            sig1Count = CountNumTimesSignalInPatterns(sig1, patternsOfLengthSix);
            sig2Count = CountNumTimesSignalInPatterns(sig2, patternsOfLengthSix);

            // whichever appears 2 times is the "e"
            // whichever appears 3 times if the "g"

            if (sig1Count == 2 && sig2Count == 3)
            {
                SolveASignal(sig1, "e", facts);
                SolveASignal(sig2, "g", facts);
            }
            else if (sig1Count == 3 && sig2Count == 2)
            {
                SolveASignal(sig2, "e", facts);
                SolveASignal(sig1, "g", facts);
            }
            else
            {
                throw new Exception("algorithm failed for eg determination");
            }

            // Similarly in the 6-length group, "b" is in all three of them
            // but "d" is only in two
            string[] bdSignals = FindSignalsThatCouldBeOneOfTwoSegments("bd", facts);
            if (bdSignals.Length != 2) throw new Exception(
                "There should only be 2 signals that could be the b or d segment");
            sig1 = bdSignals[0];
            sig2 = bdSignals[1];

            sig1Count = CountNumTimesSignalInPatterns(sig1, patternsOfLengthSix);
            sig2Count = CountNumTimesSignalInPatterns(sig2, patternsOfLengthSix);

            // whichever appears 2 times is the "d"
            // whichever appears 3 times if the "b"

            if (sig1Count == 2 && sig2Count == 3)
            {
                SolveASignal(sig1, "d", facts);
                SolveASignal(sig2, "b", facts);
            }
            else if (sig1Count == 3 && sig2Count == 2)
            {
                SolveASignal(sig2, "d", facts);
                SolveASignal(sig1, "b", facts);
            }
            else
            {
                throw new Exception("algorithm failed for bd determination");
            }


            //WriteFacts(facts);

            long value = Output(outputs, facts, segmentTrans);
            Console.Out.WriteLine(value);
            return value;

        }

        private static long Output(string[] outputs, Dictionary<String, String> facts,
            Dictionary<string, string> tran)
        {
            long v = 0;
            string digits = "";
            foreach(var output in outputs)
            {
                string digit = TranslateOutput(output, facts, tran);
                digits += digit;
            }

            v = long.Parse(digits);

            return v;
        }

        private static string TranslateOutput(string output, Dictionary<String, String> facts,
            Dictionary<string, string> tran)
        {
            string translatedPattern = "";
            foreach(var c in output)
            {
                string key = new string(c, 1);
                string translatedSignal = facts[key];
                translatedPattern += translatedSignal;
            }
            translatedPattern = String.Concat(translatedPattern.OrderBy(c => c));
            string displayedDigit = tran[translatedPattern];
            return displayedDigit;
        }

        // split apart an input line
        // the left side before the "|" has 10 examples of the
        // signal patterns for 0, 1, 2, ... 10 (in a random order)
        // the right side after the "|" has a 4-digit number "output"
        // displayed using these patterns
        private static (string[], string[]) SplitLine(string line)
        {
            string[] parts = line.Split(" | ");
            string[] patterns = parts[0].Split(" ");
            string[] outputs = parts[1].Split(" ");

            return (patterns, outputs);
        }

        private static void WriteLine(string[] patterns, string[] outputs)
        {
            foreach (string pattern in patterns)
            {
                Console.Out.Write(pattern + " ");
            }
            Console.Out.Write("===> ");
            foreach (string output in outputs)
            {
                Console.Out.Write(output + " ");
            }
            Console.Out.WriteLine();
        }

        private static string Subtract(string s1, string remove)
        {
            string v = "";

            foreach(var c in s1)
            {
                if (!remove.Contains(c))
                {
                    v += c;
                }
            }

            return v;
        }

        private static string Intersect(string s1, string s2)
        {
            string v = "";

            foreach(var c in s1)
            {
                if (s2.Contains(c))
                {
                    v += c;
                }
            }

            foreach(var c in s2)
            {
                if (s1.Contains(c) && !v.Contains(c))
                {
                    v += c;
                }
            }

            return v;
        }

        private static string Difference(string s1, string s2)
        {
            string v = "";

            foreach (var c in s1)
            {
                if (!s2.Contains(c))
                {
                    v += c;
                }
            }

            foreach (var c in s2)
            {
                if (!s1.Contains(c))
                {
                    v += c;
                }
            }

            return v;
        }

        private static string[] FindSignalsThatCouldBeOneOfTwoSegments(string pair, Dictionary<string, string> facts)
        {
            List<string> keys = new List<string>();

            foreach(string key in ALLSIGNALS)
            {
                if (facts[key] == pair) keys.Add(key);
            }

            return keys.ToArray();
        }

        private static void OnlyTheseSignalsCouldBeTheseSegments(string signals, string segments, Dictionary<string, string> facts)
        {
            foreach (string key in ALLSIGNALS)
            {
                if (signals.Contains(key))
                {
                    facts[key] = Intersect(facts[key], segments);
                }
                else
                {
                    facts[key] = Subtract(facts[key], segments);
                }
            }
            CheckForOneLeft(facts);
        }

        private static void CheckForOneLeft(Dictionary<string, string> facts)
        {
            foreach (string key1 in ALLSIGNALS)
            {
                if (facts[key1].Length == 1)
                {
                    foreach (string key2 in ALLSIGNALS)
                    {
                        if (key2 != key1) facts[key2] = Subtract(facts[key2], facts[key1]);
                    }
                }
            }
        }

        private static int CountNumTimesSignalInPatterns(string signal, string[] patterns)
        {
            int count = 0;
            foreach(var pattern in patterns)
            {
                if (pattern.Contains(signal)) count++;
            }
            return count;
        }

        private static void ApplyRuleFor1And7(string[] patterns, Dictionary<string, string> facts)
        {
            // find the patterns of length 2 (this will display "1") and length
            // 3 (this will display "7")
            string patternFor1 = FindPatternOfLength(2, patterns);
            string patternFor7 = FindPatternOfLength(3, patterns);
            string segmentASignal = Difference(patternFor1, patternFor7);
            SolveASignal(segmentASignal, "a", facts);
        }

        private static string FindPatternOfLength(int l, string[] patterns)
        {
            string[] matches = FindPatternsOfLength(l, patterns);
            if (matches.Length == 1)
            {
                return matches[0];
            }
            else
            {
                throw new Exception(
                    $"Error: there should be exactly one pattern of length {l}\n" +
                    $"Found {matches.Length} matches instead.  This shouldn't happen");
            }
        }

        private static string[] FindPatternsOfLength(int l, string[] patterns)
        {
            List<string> matches = new List<string>();
            foreach(var pattern in patterns)
            {
                if (pattern.Length == l) matches.Add(pattern);
            }
            return matches.ToArray();
        }

        private static void SolveASignal(string sig, string seg, Dictionary<string, string> facts)
        {
            // We've discovered that signal letter "sig" represents the segment
            // letter "seg".

            facts[sig] = seg;

            // This also means seg can be removed from the possible facts for all other
            // signals
            string[] keys = facts.Keys.ToArray();
            foreach(string key in keys)
            {
                // for each fact that is not about this solved signal
                if (key != sig)
                {
                    facts[key] = facts[key].Replace(seg, "");
                }
            }
        }

        private static void WriteFacts(Dictionary<string, string> facts)
        {
            foreach(string key in facts.Keys)
            {
                Console.Out.WriteLine($"\t{key} could be \"{facts[key]}\"");
            }
        }
        private static Dictionary<string, string> InitialFacts()
        {
            var facts = new Dictionary<string, string>();

            // initially we know nothing
            // each of the signal names could be driving any
            // of the segments
            string[] signalNames = "abcdefg"
                .Select(x => new String(x, 1)).ToArray();
            foreach (string signalName in signalNames)
            {
                facts[signalName] = "abcdefg";
            }
            return facts;
        }

        private static Dictionary<string, string> SegmentTrans()
        {
            var d = new Dictionary<string, string>();

            d["abcefg"]     = "0";
            d["cf"]         = "1";
            d["acdeg"]      = "2";
            d["acdfg"]      = "3";
            d["bcdf"]       = "4";
            d["abdfg"]      = "5";
            d["abdefg"]     = "6";
            d["acf"]        = "7";
            d["abcdefg"]    = "8";
            d["abcdfg"]     = "9";


            return d;
        }
    }
}
