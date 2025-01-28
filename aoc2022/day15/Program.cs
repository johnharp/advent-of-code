namespace day15;

class Program
{
    static void Main(string[] args)
    {
        World world = new World();
        string[] lines = File.ReadAllLines("../../../input.txt");

        foreach(var line in lines)
        {
            world.HandleInputLine(line);
        }

        for (int y = 0; y <= 4000000; y++)
        {
            var ranges = world.RangesWithoutBeacon(y, 0, 4000000);

            if (ranges.Count > 1)
            {
                Console.Out.Write($"Y={y}\t");

                foreach (var range in ranges)
                {
                    Console.Out.Write(range);
                    Console.Out.Write(" ");
                }
                Console.Out.WriteLine();
            }

        }

        // Output from above code:
        // Y=2855041       [0,2911362] [2911364,4000000]

        // OK, ugly but I'm tired and it's late!
        // from above output we know the only place the
        // beacon can be is X=2911363, Y=2855041

        int X = 2911363;
        int Y = 2855041;

        // So using those values, the answer is:
        long v = ((long)X * (long)4000000) + (long)Y;
        Console.Out.WriteLine($"Value = {v}");


        //// Remove locations on Y=YValueToConsider where a beacon exists
        //foreach (var b in world.Beacons)
        //{
        //    if (b.Location.y == YValueToConsider)
        //    {
        //        allintersections.Remove(b.Location.x);
        //    }
        //}

        //int numIntersections = allintersections.Count();
        //Console.Out.WriteLine($"There are {numIntersections} spots that can not have a beacon");
    }
}

