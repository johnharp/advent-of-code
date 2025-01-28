namespace day04;

using System.Text.RegularExpressions;
using System.Linq;
using System.Xml.Schema;

public class Solver
{
    private string[] data;
    private string pattern = "XMAS";

    public Solver(string[] lines)
    {
        data = lines;
    }

    public long part1()
    {
        int count = 0;

        for (int x = 0; x < data[0].Length; x++)
        {
            for (int y = 0; y < data.Length; y++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (match(pattern, x, y, dx, dy)) count++;
                    }
                }
            }
        }

        return count;
    }

    public long part2()
    {
        int count = 0;

        for (int x = 1; x < data[0].Length - 1; x++)
        {
            for (int y = 1; y < data.Length - 1; y++)
            {
                if (get(x, y) == 'A') {

                    if (
                        ((get(x-1, y-1) == 'M' && get(x+1, y+1) == 'S') ||
                        (get(x-1, y-1) == 'S' && get(x+1, y+1) == 'M'))
                        &&
                        ((get(x+1, y-1) == 'M' && get(x-1, y+1) == 'S') ||
                        (get(x+1, y-1) == 'S' && get(x-1, y+1) == 'M'))
                    ) {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public char get(int x, int y)
    {
        if (x < 0 || x > data[0].Length - 1 ||
            y < 0 || y > data.Length - 1)
        {
            return '\0';
        }
        else
        {
            return (char)data[y][x];
        }
    }

    public bool check(char v, int x, int y)
    {
        return v == get(x, y);
    }

    public bool match(string pattern, int x, int y, int dx, int dy)
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            int cx = x + dx * i;
            int cy = y + dy * i;
            char v = pattern[i];

            if (!check(v, cx, cy)) return false;
        }

        return true;
    }
}