using System.Globalization;

namespace day05;

public class Input
{
    public Digraph RulesGraph = new Digraph();
    public List<List<int>> Updates = new List<List<int>>();
    public List<int> SortedNodes = new List<int>();

    public Input(string filename)
    {
        IEnumerable<string> lines = File.ReadLines(filename);

        bool sectionSeparatorFound = false;

        foreach (string line in lines)
        {
            if (line.Trim() == "")
            {
                sectionSeparatorFound = true;
                continue;
            }

            if (!sectionSeparatorFound) {
                // Handle first section of input
                var parts = line.Split("|");

                int v = int.Parse(parts[0]);
                int w = int.Parse(parts[1]);

                RulesGraph.AddEdge(v, w);
            }
            else
            {
                // Handle second section of input
                var parts = line.Split(",");
                List<int> numbers = parts.Select(x => int.Parse(x)).ToList();
                Updates.Add(numbers);
            }
        }

        SortedNodes = RulesGraph.TopologicalSort();
    }

}