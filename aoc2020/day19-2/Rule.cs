using System;
using System.Collections.Generic;

namespace day19_2
{
    public class Rule
    {
        public int Num { get; set; }
        public string Char { get; set; }
        public int[] Rules1 = null;
        public int[] Rules2 = null;

        public static Dictionary<int, Rule> RuleIndex = new Dictionary<int, Rule>();


        private string Txt { get; set; }


        public Rule(string txt)
        {
            string[] parts = txt.Split(": ");
            Num = Int32.Parse(parts[0]);
            Txt = parts[1];

            if (Txt.StartsWith("\""))
            {
                Char = Txt.Trim('"');
            }
            else
            {
                string[] subrules = Txt.Split(" | ");
                if (subrules[0] != "")
                {
                    Rules1 = ParseRulePart(subrules[0]);
                }

                if (subrules[1] != "")
                {
                    Rules2 = ParseRulePart(subrules[1]);
                }
            }

            RuleIndex[Num] = this;
        }

        private int[] ParseRulePart(string t)
        {
            string[] nums = t.Split(" ");
            int[] r = new int[nums.Length];

            for(int i = 0; i < nums.Length; i++)
            {
                int n = Int32.Parse(nums[i]);
                r[i] = n;
            }

            return r;
        }

        public override string ToString()
        {
            string rulenum = $"rule #{Num}";
            string ruledesc;

            if (Char != "") ruledesc = $"matches '{Char}'";
            else ruledesc = $"matches subrules: ";

            return $"{rulenum} {ruledesc}";
        }
    }
}
