using System;
using System.Collections.Generic;

namespace day12
{
    public class Cave
    {
        public static Dictionary<string, Cave> Caves = new Dictionary<string, Cave>();

        public string Name { get; set; }
        public List<Cave> ConnectedCaves = new List<Cave>();

        public Cave(string Name)
        {
            this.Name = Name;
            Caves[Name] = this;
        }

        public int CountPaths(List<Cave> closedCaves, Cave oneExceptionClosedCave)
        {
            int numpaths = 0;

            if (this.Name == "end")
            {
                Console.WriteLine("end");
                return 1;
            }
            else
            {
                Console.Write($"{this.Name},");
            }




            foreach(var cave in this.ConnectedCaves)
            {
                List<Cave> subClosedCaves = new List<Cave>(closedCaves);
                 
                if (Char.IsLower(this.Name[0]) &&
                    !subClosedCaves.Contains(this))
                {
                    subClosedCaves.Add(this);
                }

                if (!subClosedCaves.Contains(cave))
                {
                    numpaths += cave.CountPaths(subClosedCaves, oneExceptionClosedCave);
                }
                else if (subClosedCaves.Contains(cave) &&
                    cave.Name != "start" &&
                    cave.Name != "end" &&
                    oneExceptionClosedCave == null)
                {
                    numpaths += cave.CountPaths(subClosedCaves, cave);
                }
            }

            return numpaths;
        }

        public static void ParseInput(PuzzleInput input)
        {
            foreach (var line in input.Lines)
            {
                Cave.ParsePath(line);
            }
        }


        private static void ParsePath(string path)
        {
            string[] parts = path.Split("-");
            string name1 = parts[0];
            string name2 = parts[1];

            Cave cave1;
            Cave cave2;

            if (Caves.ContainsKey(name1)) cave1 = Caves[name1];
            else cave1 = new Cave(name1);

            if (Caves.ContainsKey(name2)) cave2 = Caves[name2];
            else cave2 = new Cave(name2);

            cave1.ConnectedCaves.Add(cave2);
            cave2.ConnectedCaves.Add(cave1);
        }

        public static void DumpAll()
        {
            foreach(string name in Caves.Keys)
            {
                Cave cave = Caves[name];
                Console.Write($"Cave {cave.Name} connected to: ");
                foreach(Cave connectedCave in cave.ConnectedCaves)
                {
                    Console.Write($"{connectedCave.Name} ");
                }
                Console.WriteLine();
            }
        }
    }
}
