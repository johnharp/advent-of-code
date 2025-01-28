using System;
using System.Collections;

namespace day15
{
	public class Range
	{
		public int Min { get; set; }
		public int Max { get; set; }

		public int NumberContained
		{
			get
			{
				return (Max - Min) + 1;
			}
		}

		public Range(int min, int max)
		{
			Min = min;
			Max = max;
		}

        public override string ToString()
        {
            return $"[{Min},{Max}]";
        }

        public static List<Range> MergeRanges(List<Range> ranges)
		{
			if (ranges.Count() <= 1) return ranges;
			else
			{
                var sorted = ranges
                    .OrderBy(r => r.Min)
                    .ThenBy(r => r.Max)
                    .ToList();
				var merged = new Stack<Range>();

				merged.Push(sorted[0]);
				for (int i = 1; i<sorted.Count(); i++)
				{
					var top = merged.Peek();

					if (top.Max < sorted[i].Min-1)
					{
						// Not overlapping
						merged.Push(sorted[i]);
					}
					else
					{
						// Overlap
						// If not completely contained update the range end
						if (top.Max < sorted[i].Max)
						{
							top.Max = sorted[i].Max;
							merged.Pop();
							merged.Push(top);
						}
					}
				}

				return merged.ToList().OrderBy(x => x.Min)
					.ThenBy(x => x.Max).ToList();
            }
		}


    }
}

