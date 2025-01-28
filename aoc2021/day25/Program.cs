using System;

namespace day25
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
        }

        static void Part1()
        {
            var input = new PuzzleInput("input-example.txt");
            Grid g = new Grid();
            g.Init(input.Lines);
            Grid k = new Grid(g);


            bool somethingMoved = true;

            int numSteps = 1;
            int maxSteps = 1_000_000;

            g.Dump();
            while (somethingMoved && numSteps < maxSteps)
            {
                somethingMoved = false;
                Console.WriteLine($"After step {numSteps} =================================");

                bool movedRight = TryMoveRight(g, k);
                k.Dump();
                Console.WriteLine();
                bool movedDown = TryMoveDown(k, g);
                g.Dump();
                numSteps++;
                somethingMoved = movedRight || movedDown;
            }

            Console.WriteLine($"Steps taken: {numSteps}");
        }

        static bool TryMoveRight(Grid g, Grid k)
        {
            bool moved = false;

            for (int y = 0; y<g.Height; y++)
            {
                for (int x = 0; x<g.Width; x++)
                {
                    if (g.Get(x, y) == '.' && g.Get(g.LeftOf(x, y)) == '>')
                    {
                        moved = true;
                        k.Set(x, y, '>');
                        k.Set(g.LeftOf(x, y), '.');
                    }
                    else
                    {
                        k.Set(x, y, g.Get(x, y));
                    }
                }
            }
            return moved;
        }

        static bool TryMoveDown(Grid g, Grid k)
        {
            bool moved = false;

            for (int y = 0; y < g.Height; y++)
            {
                for (int x = 0; x < g.Width; x++)
                {
                    if (g.Get(x, y) == '.' && g.Get(g.Above(x, y)) == 'v')
                    {
                        moved = true;
                        k.Set(x, y, 'v');
                        k.Set(g.Above(x, y), '.');
                    }
                    else
                    {
                        k.Set(x, y, g.Get(x, y));
                    }
                }
            }
            return moved;
        }
    }
}
