string[] lines = File.ReadAllLines("input.txt");

int part1 = 0;
int part2 = 0;

foreach(var line in lines)
{
    handleLine(line);
}

Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

void handleLine(string line)
{
    int brace = 0;

    int abbaInBrace = 0;
    int abbaOutOfBrace = 0;

    HashSet<string> abas = new HashSet<string>();
    HashSet<string> babs = new HashSet<string>();

    for(int i = 0; i < line.Length; i++)
    {
        char c = line[i];
        if (c == '[')
        {
            brace++;
        }
        if (c == ']')
        {
            brace--;
            if (brace < 0)
            {
                throw new Exception($"Unmatched braces in line: {line}");
            }
        }

        if (isAbba(line, i))
        {
            if (brace > 0) abbaInBrace++;
            else abbaOutOfBrace++;
        }

        if (isAba(line, i))
        {
            string aba = $"{line[i]}{line[i-1]}{line[i]}";
            string bab = $"{line[i-1]}{line[i]}{line[i-1]}";

            if (brace > 0) {
                babs.Add(bab);
            }
            else
            {
                abas.Add(aba);
            }
        }
    }

    if (abbaInBrace == 0 && abbaOutOfBrace > 0) part1++;
    foreach(string aba in abas)
    {
        if (babs.Contains(aba))
        {
            part2++;
            break;
        }
    }

}

bool isAbba(string line, int i)
{
    return
        i >= 3 &&
        i < line.Length &&
        line[i] == line[i-3] && line[i] != '[' && line[i] != ']' &&
        line[i-1] != line[i] && line[i-1] != '[' && line[i-1] != ']' &&
        line[i-1] == line[i-2];
}

bool isAba(string line, int i)
{
    return
        i >= 2 &&
        i < line.Length &&
        line[i] == line[i-2] && line[i] != '[' && line[i] != ']' &&
        line[i-1] != line[i] && line[i-1] != '[' && line[i] != ']';
}

