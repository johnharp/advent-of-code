namespace day07;

class Program
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("input.txt");

        foreach(var line in lines)
        {
            HandleLine(line);
        }
        Node.LinkParents();

        Console.WriteLine($"Part 1: {Node.RootNodeId}");

        var all = Node.AllNodes;
        foreach(var node in all)
        {
            var ChildTotalWeights = node.ChildTotalWeights;
            if (ChildTotalWeights.Distinct().Count() > 1)
            {
                Console.WriteLine($"Node {node.Id} with weight {node.Weight} has children towers with differing weights:");
                foreach(var c in node.Children)
                {
                    Console.WriteLine($"\tchild {c.Id} has Weight {c.Weight} and TotalWeight {c.TotalWeight}");
                }
            }
        }
    }

    static void HandleLine(string line)
    {
        string[] parts = line.Split(" -> ");
        string[] leftParts = parts[0].Split(" ");

        string id = leftParts[0];
        int weight = int.Parse(leftParts[1].Replace("(", "").Replace(")", ""));
        var childIds = new List<String>();

        if (parts.Length == 2)
        {
           childIds = parts[1].Split(", ").ToList();
        }

        Node n = new Node(id, weight, childIds);
        Node.AddNode(n);
    }

}
