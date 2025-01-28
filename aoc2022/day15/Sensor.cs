using System;
namespace day15
{
	public class Sensor
	{
        public Location Location { get; set; }
        public Beacon ClosestBeacon { get; set; }
        public int ClosestBeaconDistance { get; }

		public Sensor(int x, int y, Beacon b)
		{
            Location = new Location(x, y);
            ClosestBeacon = b;

            ClosestBeaconDistance = Location.DistanceTo(b.Location);
		}

        public Range? XValueIntersections(int atY)
        {
            var xvalues = new List<int>();

            var loc = new Location(Location.x, atY);
            var dist = Location.DistanceTo(loc);

            // if the distance to the nearest point on the atY row
            // is <= ClosestBeaconDistance then we know that we have
            // at least one XValueIntersection
            if (dist <= ClosestBeaconDistance)
            {
                int delta = ClosestBeaconDistance - dist;
                int min = Location.x - delta;
                int max = Location.x + delta;

                return new Range(min, max);
            }

            return null;
        }

        public override string ToString()
        {
            return $"Sensor at {Location.ToString()} with closest {ClosestBeacon} distance: {ClosestBeaconDistance}";
        }
    }
}

