using System;
using System.Collections.Generic;
using System.Text;

namespace day14
{
    public class ElementTracker
    {
        private Dictionary<string, char> RulesDictionary = new Dictionary<string, char>();
        public Element Head = null;

        // rule is formatted like this
        // "CH -> B"
        public void InitializeRules(List<string> rules)
        {
            foreach(string rule in rules)
            {
                string[] parts = rule.Split(" -> ");

                char value = char.Parse(parts[1]);
                RulesDictionary[parts[0]] = value;
            }
        }

        public void InitializeElements(string str)
        {
            char[] values = str.ToCharArray();

            Head = new Element(values[0]);
            Element p = Head;
            for (int i = 1; i < values.Length; i++)
            {
                p = p.NewElementAfter(values[i]);
            }
        }

        public List<Element> CurrentElements()
        {
            List<Element> elements = new List<Element>();

            Element p = Head;

            while (p != null)
            {
                elements.Add(p);
                p = p.Next;
            }

            return elements;
        }

        public string CurrentElementsStr()
        {
            StringBuilder sb = new StringBuilder();
            
            Element p = Head;
            while (p != null)
            {
                sb.Append(p.Value);
                p = p.Next;
            }

            return sb.ToString();
        }

        public void DumpCurrentElements()
        {
            Console.WriteLine(CurrentElementsStr());
        }

        public void ApplyRulesToElements()
        {
            var elements = CurrentElements();

            foreach(var element in elements)
            {
                string pairval = element.PairValue();
                if (pairval != "")
                {
                    if (RulesDictionary.ContainsKey(pairval))
                    {
                        char v = RulesDictionary[pairval];
                        element.NewElementAfter(v);
                    }
                }
            }
        }

        public void DumpStatistics()
        {
            var stats = new Dictionary<char, long>();
            List<Element> elements = CurrentElements();

            foreach(Element el in elements)
            {
                if (!stats.ContainsKey(el.Value))
                {
                    stats[el.Value] = 1;
                }
                else
                {
                    stats[el.Value]++;
                }
            }

            char mostFrequentChar = ' ';
            long mostFrequentCount = 0;

            char leastFrequentChar = ' ';
            long leastFrequentCount = long.MaxValue;

            foreach(char c in stats.Keys)
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

        public ElementTracker()
        {
        }
    }
}
