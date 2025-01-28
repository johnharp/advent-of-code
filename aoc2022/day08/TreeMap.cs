using System;
namespace day08
{
	public class TreeMap
	{
		public int NumCols { get; set; }
		public int NumRows { get; set; }


		// The Map is the original puzzle input where each
		// value is the height of the tree at that spot from 0
		// (smallest tree) to 9 (tallest tree)
		// Note: 0 does *not* mean no tree.  There is a tree in
		// every grid location
		private int[][] Map { get; set; }

		// The VisMap shows at each position is the tree at that
		// spot visible (and if so, from how many directions --
		// 1-4). 
		private int[][] VisMap { get; set; }

		private int[][] ScenicMap { get; set; }

		public TreeMap(string[] lines)
		{
			NumCols = lines[0].Length;
			NumRows = 0;

			// Disregard blank rows when calculating the size
			// of the map.  
			for (int i=0; i<lines.Length; i++)
			{
				if (lines[i].Trim().Length > 0)
				{
					NumRows = i + 1;
				}
			}

			Map = new int[NumRows][];
			VisMap = new int[NumRows][];
			ScenicMap = new int[NumRows][];

			int row = 0;
			foreach(var line in  lines)
			{
				if (line.Trim().Length > 0)
				{
                    Map[row] = new int[NumCols];
					VisMap[row] = new int[NumCols];
					ScenicMap[row] = new int[NumCols];
                    for (int col = 0; col < NumCols; col++)
                    {
						string v = line.Substring(col, 1);
						Map[row][col] = int.Parse(v);
						VisMap[row][col] = 0;
						ScenicMap[row][col] = 0;
                    }
                }
				row++;
            }
		}

		public int ValueAt(int row, int col)
		{
			return Map[row][col];
		}

		public void PrintMap()
		{
			Print(Map);
		}

		public void PrintVisMap()
		{
			Print(VisMap);
		}

		public void PrintScenicMap()
		{
			Print(ScenicMap);
		}

		public void Print(int[][] m)
		{
			for (int row=0; row<NumRows; row++)
			{
				for (int col=0; col<NumCols; col++)
				{
					Console.Write(m[row][col]);
				}
				Console.WriteLine();
			}
		}

		public int CalculateVisMap()
		{
			int NumTreesVisible = 0;

			for (int row = 0; row<NumRows; row++)
			{
				int maxFromLeft = -1;

				for (int col=0; col<NumCols; col++)
				{
					if (Map[row][col] > maxFromLeft)
					{
                        maxFromLeft = Map[row][col];
						if (VisMap[row][col] == 0)
						{
							NumTreesVisible++;
						}
						VisMap[row][col]++;
					}
				}

                int maxFromRight = -1;

                for (int col = NumCols-1; col >= 0; col--)
                {
                    if (Map[row][col] > maxFromRight)
                    {
                        maxFromRight = Map[row][col];
                        if (VisMap[row][col] == 0)
                        {
                            NumTreesVisible++;
                        }
                        VisMap[row][col]++;
                    }
                }
            }

            for (int col = 0; col < NumCols; col++)
            {
                int maxFromTop = -1;

                for (int row = 0; row < NumRows; row++)
                {
                    if (Map[row][col] > maxFromTop)
                    {
                        maxFromTop = Map[row][col];
                        if (VisMap[row][col] == 0)
                        {
                            NumTreesVisible++;
                        }
                        VisMap[row][col]++;
                    }
                }

                int maxFromBottom = -1;

                for (int row = NumRows-1; row >= 0; row--)
                {
                    if (Map[row][col] > maxFromBottom)
                    {
                        maxFromBottom = Map[row][col];
                        if (VisMap[row][col] == 0)
                        {
                            NumTreesVisible++;
                        }
                        VisMap[row][col]++;
                    }
                }
            }

			return NumTreesVisible;
        }

		public int CalculateScenicMap()
		{
			int maxScenic = 0;

			for (int row=0;row<NumRows; row++)
			{
				for (int col=0; col<NumCols; col++)
				{
					int leftNumTrees = 0;
					// scan left
					for (int c = col-1; c >= 0; c-- )
					{
						leftNumTrees++;
						if (Map[row][c] >= Map[row][col]) break;
					}

					int rightNumTrees = 0;
					// scan right
					for (int c = col+1; c < NumCols; c++)
					{
						rightNumTrees++;
						if (Map[row][c] >= Map[row][col]) break;
					}

					int upNumTrees = 0;
					// scan up
					for (int r = row-1; r >= 0; r--)
					{
						upNumTrees++;
						if (Map[r][col] >= Map[row][col]) break;
					}

					int downNumTrees = 0;
					// scan down
					for (int r = row+1; r < NumRows; r++)
					{
						downNumTrees++;
						if (Map[r][col] >= Map[row][col]) break;
					}

					ScenicMap[row][col] = leftNumTrees * rightNumTrees * upNumTrees * downNumTrees;
					if (ScenicMap[row][col] > maxScenic)
					{
						maxScenic = ScenicMap[row][col];
					}

				}
			}

			return maxScenic;
		}

    }
}

