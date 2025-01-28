using System;
namespace day12
{
	public class Map
	{
		public List<List<MapLocation>> Locations { get; }
		public (int, int) StartRowCol;
		public (int, int) EndRowCol;

		public int TotalNumLocations
		{
			get { return NumCols * NumRows;  }
		}

		public MapLocation StartLoc
		{
			get { return MapAt(StartRowCol); }
		}

		public MapLocation EndLoc
		{
			get { return MapAt(EndRowCol); }
		}

		public Map(string[] lines)
		{
			Locations = new List<List<MapLocation>>();
			int rownum = 0;
			foreach(var line in lines)
			{
				int colnum = 0;
				List<MapLocation> mapline = new List<MapLocation>();
				foreach(var c in line)
				{
					char height;
					if (c == 'S') // Start location
					{
						height = 'a';
						StartRowCol = (rownum, colnum);
					}
					else if (c == 'E')
					{
						height = 'z';
						EndRowCol = (rownum, colnum);
					}
					else
					{
						height = c;
					}
					var loc = new MapLocation(height, rownum, colnum);
					mapline.Add(loc);
					colnum++;
				}
				Locations.Add(mapline);
				rownum++;
			}
		}

		public void ComputeNeighbors()
		{
			foreach(var row in Locations)
			{
				foreach(var loc in row)
				{
					loc.Neighbors = Neighbors(loc);
                }
			}
		}

		public void SetDistancesToMax()
		{
            foreach (var row in Locations)
            {
                foreach (var loc in row)
                {
                    loc.Distance = int.MaxValue;
                }
            }
        }

		public List<MapLocation> AllLocations()
		{
			var list = new List<MapLocation>();
			foreach (var row in Locations)
			{
				foreach (var loc in row)
				{
					list.Add(loc);
				}
			}
			return list;
		}

		public List<MapLocation> PossibleStartLocations()
		{
            var list = new List<MapLocation>();
            foreach (var row in Locations)
            {
                foreach (var loc in row)
                {
					if (loc.Height == 'a')
					{
                        list.Add(loc);
                    }
                }
            }
            return list;
        }

		public void Print()
		{
			Console.Out.WriteLine($"Start: {StartRowCol}");
			Console.Out.WriteLine($"End  : {EndRowCol}");
			foreach(var mapline in Locations)
			{
				foreach(var location in mapline)
				{
					Console.Out.Write(location);
				}
				Console.Out.WriteLine();
			}
		}

		public MapLocation MapAt((int, int) RowCol)
		{
			return Locations[RowCol.Item1][RowCol.Item2];
		}

		public int NumRows
		{
			get
			{
				return Locations.Count();
			}
		}

		public int NumCols
		{
			get
			{
				return Locations[0].Count();
			}
		}

		public MapLocation? LocationDelta(MapLocation from, (int, int) delta)
		{
			(int, int) loc = (from.Row + delta.Item1, from.Col + delta.Item2);
			if (loc.Item1 < 0 || loc.Item1 > NumRows-1 ||
				loc.Item2 < 0 || loc.Item2 > NumCols-1)
			{
				return null;
			}
			else
			{
				return MapAt(loc);
			}
		}

		// return  all the neighboring (row, col) locations
		// Since we're solving backwards (looking for paths from
		// the highest point to the lowest point) only allow neighbors
		// that are up to one step lower, same height, or any number of
		// steps higher
		public List<MapLocation> Neighbors(MapLocation from)
		{
			var neighbors = new List<MapLocation>();
			var deltas = new List<(int, int)>
			{
				(-1, 0),
				(0, 1),
				(1, 0),
				(0, -1)
			};
			foreach(var delta in deltas)
			{
				MapLocation? l = LocationDelta(from, delta);
				if (l != null && l.Height -  from.Height >= -1)
				{
					neighbors.Add(l);
				}
			}

			return neighbors;
		}
	}
}

