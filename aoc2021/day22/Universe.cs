using System;
using System.Collections.Generic;

namespace day22
{
    public class Universe
    {
        // Cuboids initially contains all cuboids
        //
        // After calling "Reduce":
        //
        // * any completely contained cuboids that can't affect
        //   the final value are removed.
        //
        // * any cuboids that are non-intersecting with any other
        //   item are moved to NonIntersectingCuboids
        //
        public List<Cuboid> Cuboids;
        public List<Cuboid> NonIntersectingCuboids;

        public Universe()
        {
            Cuboids = new List<Cuboid>();
            NonIntersectingCuboids = new List<Cuboid>();
        }

        public bool Value(long x, long y, long z)
        {
            for (int i = Cuboids.Count-1; i>=0; i--)
            {
                Cuboid c = Cuboids[i];
                if (c.Contains(x, y, z)) return c.value;
            }

            // if not contained by any cuboid, default to off
            // the universe is a dark place
            return false;
        }

        public void DumpAllCuboids()
        {
            foreach(var c in Cuboids)
            {
                Console.WriteLine(c);
            }
        }

        public void Reduce()
        {
            //RemoveContainedCuboids();
            //CountIntersections();
            //DetermineNonIntersectingCuboids();
        }

        public bool Decimate()
        {
            bool performedSplit = false;

            Cuboid splitter = null;
            Cuboid splitee = null;

            List<Cuboid> splitResult = new List<Cuboid>();

            for (int i = Cuboids.Count-1; !performedSplit && i > 0; i--)
            {
                splitter = Cuboids[i];
                if (splitter.Finalized) continue;
                for (int j = i - 1; !performedSplit && j>=0; j--)
                {
                    splitee = Cuboids[j];

                    if (splitter.Intersects(splitee))
                    {
                        performedSplit = true;
                        splitResult = splitee.Split(splitter);
                    }
                }
                if (!performedSplit) splitter.Finalized = true;
            }

            if (performedSplit && splitResult.Count > 0)
            {
                int index = Cuboids.IndexOf(splitee);

                Cuboids.RemoveAt(index);
                Cuboids.InsertRange(index, splitResult);
            }

            return performedSplit;
        }

        // whenever a larger cuboid completly contains a cuboid
        // higher up on (closer to the start of) the list
        // then the upper cuboid can be removed.
        public void RemoveContainedCuboids()
        {
            var containedCuboidsToRemove = new List<Cuboid>();

            // no need to consider the first item in the list
            // it has nothing further up the list to contain
            for (int i = Cuboids.Count-1; i > 0; i--)
            {
                Cuboid potentialContainer = Cuboids[i];

                // similarly, the potentially smaller item
                // above will never be the last item in the list
                for (int j = i-1; j >= 0; j--)
                {
                    Cuboid potentialContainee = Cuboids[j];

                    if (potentialContainer.Contains(potentialContainee) &&
                        !containedCuboidsToRemove.Contains(potentialContainee))
                    {
                        containedCuboidsToRemove.Add(potentialContainee);
                    }
                }
            }

            foreach (var c in containedCuboidsToRemove)
            {
                Cuboids.Remove(c);
            }
        }

        public void CountIntersections()
        {
            for (int i = 0; i < Cuboids.Count; i++)
            {
                Cuboid c1 = Cuboids[i];
                int numIntersections = 0;

                for (int j = 0; j < Cuboids.Count; j++)
                {
                    if (i == j) continue;
                    Cuboid c2 = Cuboids[j];

                    if (c1.Intersects(c2))
                    {
                        numIntersections++;
                    }
                }
                c1.NumIntersectingCuboids = numIntersections;
            }
        }

        public void DetermineNonIntersectingCuboids()
        {
            var nonIntersectingCuboidsToMove = new List<Cuboid>();

            for (int i = 0; i < Cuboids.Count; i++)
            {
                bool intersects = false;
                Cuboid c1 = Cuboids[i];

                for (int j = 0; j < Cuboids.Count; j++)
                {
                    if (i == j) continue;
                    Cuboid c2 = Cuboids[j];

                    if (c1.Intersects(c2))
                    {
                        intersects = true;
                        break;
                    }
                }

                if (!intersects) nonIntersectingCuboidsToMove.Add(c1);
            }

            foreach (var c in nonIntersectingCuboidsToMove)
            {
                NonIntersectingCuboids.Add(c);
                Cuboids.Remove(c);
            }
        }
    }
}
