using System;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace day15
{
	public class World
	{
		public List<Sensor> Sensors { get; }
		public List<Beacon> Beacons { get; }

		public World()
		{
			Sensors = new List<Sensor>();
			Beacons = new List<Beacon>();
		}

		public void HandleInputLine(string l)
		{
            Regex rx = new Regex(
				@"^Sensor at x=(?<SensorX>[\+-]?\d+), y=(?<SensorY>[\+-]?\d+): closest beacon is at x=(?<BeaconX>[\+-]?\d+), y=(?<BeaconY>[\+-]?\d+)$",
				RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches = rx.Matches(l);

			var match = matches.First();
			int sx = GetMatch(match, "SensorX");
			int sy = GetMatch(match, "SensorY");
			int bx = GetMatch(match, "BeaconX");
			int by = GetMatch(match, "BeaconY");

			Beacon? b = Beacons.Find(be => be.Location.x == bx &&
								be.Location.y == by);
			if (b == null)
			{
				b = new Beacon(bx, by);
				Beacons.Add(b);
            }
            Sensor s = new Sensor(sx, sy, b);
			Sensors.Add(s);
        }

		public List<Range> RangesWithoutBeacon(int atY, int lowestAllowed, int highestAllowed)
		{
            var allintersections = new List<Range>();
            foreach (var s in Sensors)
            {
                Range? intersections = s.XValueIntersections(atY);
                if (intersections != null)
                {
					if (intersections.Min < lowestAllowed) intersections.Min = lowestAllowed;
					if (intersections.Max > highestAllowed) intersections.Max = highestAllowed;
                    allintersections.Add(intersections);
                }
            }

            allintersections = Range.MergeRanges(allintersections);

			return allintersections;

			//int sum = allintersections.Sum(i => i.NumberContained);
			//foreach(var b in Beacons)
			//{
			//	if (b.Location.x >= lowestAllowed &&
			//		b.Location.x <= highestAllowed)
			//	{
			//		if (b.Location.y == atY)
			//		{
			//			sum--;
			//		}
			//	}
			//}
			//return sum;
        }

        private int GetMatch(Match? match, string key)
		{
			if (match == null) throw new FormatException("Bad input line");

			int v = int.Parse(match.Groups[key].Value);
			return v;
		}
    }
}

