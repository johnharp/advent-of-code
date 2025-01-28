using System.Globalization;
using Display = System.Collections.Generic.HashSet<(int, int)>;

// int WIDTH = 7;
// int HEIGHT = 3;

int WIDTH = 50;
int HEIGHT = 6;

Display display = new Display();

string[] lines = File.ReadAllLines("input.txt");
foreach (var line in lines)
{
    if (line.StartsWith("rect "))
    {
        string rest = line.Replace("rect ", "");
        string[] parts = rest.Split("x");
        display = Rect(display, int.Parse(parts[0]), int.Parse(parts[1]));
    }
    else if (line.StartsWith("rotate column x="))
    {
        string rest = line.Replace("rotate column x=", "");
        string[] parts = rest.Split(" by ");
        display = RotateColumn(display, int.Parse(parts[0]), int.Parse(parts[1]));
    }
    else if (line.StartsWith("rotate row y="))
    {
        string rest = line.Replace("rotate row y=", "");
        string[] parts = rest.Split(" by ");
        display = RotateRow(display, int.Parse(parts[0]), int.Parse(parts[1]));
    }

    //DumpDisplay(display);
    //Console.WriteLine();
}

Console.WriteLine($"Part 1: {display.Count}");
DumpDisplay(display);


Display Rect(Display d, int w, int h)
{
    Display newDisplay = new Display(d);
    for (int y = 0; y < h; y++)
    {
        for (int x = 0; x < w; x++)
        {
            newDisplay.Add((x, y));
        }
    }
    return newDisplay;
}

Display RotateColumn(Display d, int x, int n)
{
    Display newDisplay = new Display();
    foreach(var point in d)
    {
        if (point.Item1 == x)
        {
            int newy = (point.Item2 + n) % HEIGHT;
            newDisplay.Add((point.Item1, newy));
        }
        else
        {
            newDisplay.Add(point);
        }
    }
    return newDisplay;
}

Display RotateRow(Display d, int y, int n)
{
    Display newDisplay = new Display();
    foreach(var point in d)
    {
        if (point.Item2 == y)
        {
            int newx = (point.Item1 + n) % WIDTH;
            newDisplay.Add((newx, point.Item2));
        }
        else
        {
            newDisplay.Add(point);
        }
    }
    return newDisplay;
}

void DumpDisplay(Display d)
{
    for (int y = 0; y < HEIGHT; y++)
    {
        for (int x = 0; x < WIDTH; x++)
        {
            if (display.Contains((x, y))) Console.Write("#");
            else Console.Write(".");
        }
        Console.WriteLine();
    }
}

