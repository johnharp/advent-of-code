using System;
namespace day15
{
	public class Location
	{
        public int x { get; set; }
		public int y { get; set; }

        public Location(int _x, int _y)
		{
			x = _x;
			y = _y;
		}

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public int DistanceTo(Location other)
        {
            int distance =
                Math.Abs(x - other.x) +
                Math.Abs(y - other.y);

            return distance;
        }
    }
}

