using System.ComponentModel.DataAnnotations;
using System.Data;

var lines = File.ReadAllLines("input.txt");
var masses = lines.Select(int.Parse).ToList();

var fuelAmounts = masses.Select(mass => CalcFuel(mass));
var part1 = fuelAmounts.Sum();
var fuelAmountsPart2 = masses.Select(mass => CalcFuelPart2(mass));
var part2 = fuelAmountsPart2.Sum();

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");


int CalcFuel(int mass)
{
    return Math.Max(mass / 3 - 2, 0);
}

int CalcFuelPart2(int mass)
{
    var fuel = CalcFuel(mass);

    int additionalFuel = CalcFuel(fuel);
    while (additionalFuel > 0)
    {
        fuel += additionalFuel;
        additionalFuel = CalcFuel(additionalFuel);
    }

    return fuel + additionalFuel;
}