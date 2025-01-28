using System;
using System.Text.RegularExpressions;

namespace day16
{
	public class Valve
	{
		public string Name { get; set; }
		public long Rate { get; set; }

		public List<String> ConnectedValveNames { get; set; }
		public List<NeighborValve> ConnectedValves { get; set; }

		public static Dictionary<string, Valve> ValveDictionary = new Dictionary<string, Valve>();
		public static List<Valve> GetAllValves()
		{
			var all = ValveDictionary.Values;

			return all.ToList();
		}

		public Valve(string line)
		{
            Regex rx = new Regex(
                @"^Valve (?<Name>\w\w) has flow rate=(?<Rate>\d+); tunnel[s]? lead[s]? to valve[s]? (?<Valves>.+)$",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rx.Matches(line);
            var match = matches.First();

			Name = match.Groups["Name"].Value;
			Rate = long.Parse(match.Groups["Rate"].Value);
			string valvesString = match.Groups["Valves"].Value;

			ConnectedValveNames = new List<string>();

			var names = valvesString.Split(", ");
			foreach(var name in names)
			{
				ConnectedValveNames.Add(name);
			}

			ConnectedValves = new List<NeighborValve>();

			ValveDictionary[Name] = this;
        }

		/// <summary>
		/// Checks all the connected valves and verifies only "significant" valves are
		/// connected.  (Those with a flow of 0 will never be opened so they just serve as connections
		/// and should be traversed.)
		/// </summary>
		/// <returns>
		///   True - all connected valves have a non-zero flow
		///   False - one or more connected valves have a zero flow
		/// </returns>
		public bool IsFullyCompacted()
		{
			return !ConnectedValves.Any(x => x.Valve.Rate == 0);
		}

		public NeighborValve ConnectedTo(Valve v)
		{
			foreach(NeighborValve n in ConnectedValves)
			{
				if (n.Valve == v) return n;
			}

			return null;
		}

		public static void CompactAll()
		{
			var all = Valve.GetAllValves().Where(v => v.Name == "AA" || v.Rate > 0);

			foreach (var v in all)
			{
				while (!v.IsFullyCompacted())
				{
					v.Compact();
					Console.Out.WriteLine(v);
				}
			}

			foreach (var v in all)
			{
				if (v.Rate == 0 && v.Name != "AA") ValveDictionary.Remove(v.Name);
			}
		} 

		public void Compact()
		{
			// copy the list since we'll be modifying it as we traverse
			var neighbors = new List<NeighborValve>(ConnectedValves);

			foreach(var neighbor in neighbors)
			{
				if (neighbor.Valve.Rate == 0)
				{
					ConnectedValves.Remove(neighbor);
					foreach (var neighborsNeighber in neighbor.Valve.ConnectedValves)
					{
						if (neighborsNeighber.Valve != this)
						{
							NeighborValve alreadyConnectedNeighber = ConnectedTo(neighborsNeighber.Valve);

							if (alreadyConnectedNeighber == null)
							{
                                ConnectedValves.Add(new NeighborValve(
									neighborsNeighber.Valve,
									neighbor.TraversalCost + neighborsNeighber.TraversalCost));
                            }
                            else // already connected -- just update
							{
								//alreadyConnectedNeighber.TraversalCost = int.Min(alreadyConnectedNeighber.TraversalCost, neighbor.TraversalCost + neighborsNeighber.TraversalCost);
							}
                        }
					}
				}
			}
		}



		public static void LinkAllConnectedValves()
		{
			foreach(var valve in GetAllValves())
			{
				valve.LinkConnectedValves();
			}
		}


		private void LinkConnectedValves()
		{
			foreach(string valveName in ConnectedValveNames)
			{
				Valve v = ValveDictionary[valveName];
				ConnectedValves.Add(new NeighborValve(v, 1));
			}
		}

        public override string ToString()
        {
			string connected =
				string.Join(" | ", ConnectedValves.Select(x => x.Valve.Name + "[" + x.TraversalCost + "]"));
            return $"{Name} rate={Rate} => {connected}";
        }
    }
}

