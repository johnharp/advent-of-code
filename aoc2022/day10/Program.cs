string filename;

filename = "../../../input.txt";
//filename = "../../../input-example.txt";
string[] lines = File.ReadAllLines(filename);

// xvalues contains the value of the X register
// throughout the given clock cycle
List<long> xvalues = new List<long>();
// initial value of X is 1
xvalues.Add(1); // cycle 0

long nextx = 1;

// start of cycle 2
foreach (var line in lines)
{
    string[] parts = line.Split();
    if (parts[0] == "noop")
    {
        xvalues.Add(nextx);
        // nextx retains its value
    }
    else if (parts[0] == "addx")
    {
        long v = long.Parse(parts[1]);
        xvalues.Add(nextx);
        xvalues.Add(xvalues.Last());
        nextx = v + xvalues.Last();
    }
}

xvalues.Add(nextx);

//for(int clock=0; clock<xvalues.Count; clock++)
//{
//    Console.WriteLine($"clock {clock}: x = {xvalues[clock]}");
//}

long totalStrength = 0;
for (int i = 20; i < xvalues.Count; i += 40)
{
    long strength = i * xvalues[i];
    totalStrength += strength;
}

Console.WriteLine($"Total signal strength =" +
    $" {totalStrength}");
Console.WriteLine();

for(int row = 0; row<=5; row++)
{
    for (int col=0; col<=39; col++)
    {
        int clock = col + (row * 40) + 1;

        int spritePos = (int) xvalues[clock];
        int minPixel = spritePos - 1;
        int maxPixel = spritePos + 1;
        if (minPixel<=col && maxPixel>=col)
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }
    Console.WriteLine();
}