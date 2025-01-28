using System;
namespace day15
{
	public class Beacon
	{
        public Location Location { get; set; }

        public Beacon(int x, int y)
		{
            Location = new Location(x, y);
        }

        public override string ToString()
        {
            return $"Beacon at {Location.ToString()}"; ;
        }
    }
}

