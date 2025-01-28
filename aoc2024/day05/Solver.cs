namespace day05;

using System.Text.RegularExpressions;
using System.Linq;
using System.Xml.Schema;
using System.Collections.Immutable;
using System.Data;

public class Solver
{
    Input input;

    public Solver(Input suppliedInput)
    {
        input = suppliedInput;
    }

    public long part1()
    {
        long solution = 0;

        foreach (var update in input.Updates)
        {
            if (input.RulesGraph.TestPath(update))
            {
                solution += MidValue(update);
            }
        }

        return solution;
    }

    public long part2()
    {
        long solution = 0;

        Console.WriteLine($"sorted: {String.Join(",", input.SortedNodes)}");
        foreach (var update in input.Updates)
        {
            if (!input.RulesGraph.TestPath(update))
            {
                List<int> corrected = SortPages(update);

                if (input.RulesGraph.TestPath(corrected))
                {
                    solution += MidValue(corrected);
                }
            }
        }

        return solution;
    }

    public int MidValue(List<int> pages)
    {
        int midpoint = pages.Count / 2;
        int midvalue = pages[midpoint];

        return midvalue;
    }

    public List<int> SortPages(List<int> pages)
    {
        List<int> sorted = new List<int>(pages);

        sorted.Sort(new Comparison<int>(ComparePages));
        return sorted;
    }


    private int ComparePages(int x, int y)
    {
        if (input.RulesGraph.Adj(x).Contains(y)) return -1;
        if (input.RulesGraph.Adj(y).Contains(x)) return 1;
        return 0;
    }

}