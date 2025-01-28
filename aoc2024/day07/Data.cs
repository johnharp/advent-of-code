namespace day07;

using System.Text.RegularExpressions;
using System.Numerics;

public class Data
{
    private static string pattern = @"(\d+):\s+(\d+(?:\s+\d+)+)";

    public List<BigInteger> Results = new List<BigInteger>();
    public List<List<BigInteger>> Operands = new List<List<BigInteger>>();

    public Data(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        foreach(string line in lines)
        {
            Match match = Regex.Match(line, pattern);
            if (match.Success)
            {
                string value1 = match.Groups[1].Value;
                string value2 = match.Groups[2].Value;
                BigInteger result = BigInteger.Parse(value1);

                Results.Add(result);

                string[] operandStrings = value2.Split(' ');
                List<BigInteger> operands = operandStrings.Select(x => BigInteger.Parse(x)).ToList();
                Operands.Add(operands);
            }
            else
            {
                throw new Exception($"Malformed input line: {line}");
            }
        }
    }
}