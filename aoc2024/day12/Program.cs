namespace day12;

class Program
{
  static string[] lines = new string[0];
  static int WIDTH = -1;
  static int HEIGHT = -1;

  static void Main(string[] args)
  {
    lines = File.ReadAllLines("input-sample.txt");
    WIDTH = lines[0].Length;
    HEIGHT = lines.Length;

  }

  static char get((int, int) pos)
  {
    if (pos.Item1 < 0 || pos.Item1 >= WIDTH || pos.Item2 < 0 || pos.Item2 >= HEIGHT)
    {
      return ' ';
    }
    return lines[pos.Item2][pos.Item1];
  } 
}
