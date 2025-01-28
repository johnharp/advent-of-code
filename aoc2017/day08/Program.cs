using System.Text.RegularExpressions;

Dictionary<string, long> registers = new Dictionary<string, long>();
long highestEver = long.MinValue;

string[] lines = File.ReadAllLines("input.txt");
Regex instructionRegex = new Regex(@"^([a-z]+) (inc|dec) ([0-9-]+) if (\S+) (\S+) ([0-9-]+$)");

foreach(var line in lines)
{
    HandleLine(line);
}

long part1 = registers
    .Select(r => r.Value)
    .OrderByDescending(v => v)
    .Take(1)
    .Single();

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {highestEver}");




void HandleLine(string line)
{
    Match match = instructionRegex.Match(line);
    if (!match.Success)
    {
        throw new Exception($"Failed to parse line: {line}");
    }

    string targetRegister = match.Groups[1].Value; // name of register to modify
    string operation = match.Groups[2].Value;  // inc or dec
    long amount = long.Parse(match.Groups[3].Value); // amount to inc or dec
    string conditionRegister = match.Groups[4].Value; // 
    string condition = match.Groups[5].Value; // comparison >, <, >=, <=, ==, or !=
    long conditionValue = long.Parse(match.Groups[6].Value);

    if (Condition(conditionRegister, condition, conditionValue))
    {
        Operation(targetRegister, operation, amount);
    }

}

void Operation(string targetReg, string op, long amount)
{
    switch(op)
    {
        case "inc":
            Set(targetReg, Get(targetReg) + amount);
            break;
        case "dec":
            Set(targetReg, Get(targetReg) - amount);
            break;
        default:
            throw new Exception($"Unknown operation: {Operation}");
    }
}

bool Condition(string reg, string cond, long value)
{
    long regValue = Get(reg);
    switch(cond)
    {
        case "==":
            return regValue == value;
        case "!=":
            return regValue != value;
        case "<":
            return regValue < value;
        case ">":
            return regValue > value;
        case "<=":
            return regValue <= value;
        case ">=":
            return regValue >= value;
        default:
            throw new Exception($"Unknown condition: {cond}");
    }
}

long Get(string name)
{
    Init(name);
    return registers[name];
}

void Set(string name, long value)
{
    if (value > highestEver) highestEver = value;
    registers[name] = value;
}


void Init(string name)
{
    if (!registers.ContainsKey(name))
    {
        registers[name] = 0;
    }
}


void Dump()
{
    foreach(var reg in registers)
    {
        Console.WriteLine($"{reg.Key}: {reg.Value}");
    }
}