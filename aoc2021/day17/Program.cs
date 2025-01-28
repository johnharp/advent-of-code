using System;

namespace day17
{
    class Program
    {
        static void Main(string[] args)
        {
            //var t = new Target("target area: x=20..30, y=-10..-5");
            var t = new Target("target area: x=128..160, y=-142..-88");
            var b = new Ballistics(t);
            var bestvel = b.DetermineBestInitialVelocity();

            Console.WriteLine(
                "The best initial velocity is " +
                $"({bestvel.Item1}, {bestvel.Item2})");

            var maxy = b.MaxAchievedY(bestvel.Item1, bestvel.Item2);

            Console.WriteLine(
                $"With this velocity, the probe reaches a max y of {maxy}");

            Console.WriteLine();

            var velocities = b.DetermineAllSuccessfulVelocities();

            Console.WriteLine(
                $"There are {velocities.Count} initial velocities that work");
        }
    }
}
