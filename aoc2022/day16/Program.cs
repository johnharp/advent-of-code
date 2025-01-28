namespace day16;
class Program
{
    static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("../../../input-example.txt");

        // Create all the valves
        foreach (var line in lines)
        {
            var v = new Valve(line);
        }

        // Link the connected valves so each valve has a list of its neighbors
        Valve.LinkAllConnectedValves();

        // "Compact" all the valves so that the only zero-flow rate valve remaining is AA
        Valve.CompactAll();


        foreach(var v in Valve.ValveDictionary.Values)
        {
            Console.WriteLine(v);
        }
    }
}

