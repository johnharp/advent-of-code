using System;
using System.Collections.Generic;

namespace day09
{
    public class Basin
    {
        public long row;
        public long col;

        public long size;

        public static List<Basin> Basins = new List<Basin>();

        public Basin(long row, long col)
        {
            this.row = row;
            this.col = col;

            size = 1;

            Basins.Add(this);
        }

        public static Basin Get(long row, long col)
        {
            foreach(var basin in Basins)
            {
                if (basin.row == row && basin.col == col)
                {
                    return basin;
                }
            }

            return null;
        }

        public static void DumpAll()
        {
            foreach(var basin in Basins)
            {
                Console.Out.WriteLine($"Basin row,col: {basin.row}, {basin.col} size: {basin.size}");
            }
        }
    }
}
