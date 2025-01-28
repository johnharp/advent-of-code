namespace day08;
class Program
{
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines("../../../input.txt");
        TreeMap m = new TreeMap(lines);

        //Console.WriteLine($"# Cols: {m.NumCols}");
        //Console.WriteLine($"# Rows: {m.NumRows}");

        int numberTreesVisible = m.CalculateVisMap();
        int maxScenicScore = m.CalculateScenicMap();

        //Console.WriteLine("Map:");
        //m.PrintMap();

        //Console.WriteLine();
        //Console.WriteLine("VisMap ");
        //m.PrintVisMap();

        //Console.WriteLine();
        //Console.WriteLine("ScenicMap ");
        //m.PrintScenicMap();

        Console.WriteLine();
        Console.WriteLine($"Total Trees Visible: {numberTreesVisible}");

        Console.WriteLine();
        Console.WriteLine($"Maximum Scenic Score: {maxScenicScore}");

    }
}

