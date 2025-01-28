public class Model
{
    public Dictionary<char, List<(int, int)>> AntennaDictionary = new Dictionary<char, List<(int, int)>>();
    public int Width = 0;
    public int Height = 0;

    public Model(string[] lines)
    {
        if (lines.Length > 0) Width = lines[0].Length;
        Height = lines.Length;
        
        for (int y = 0; y < lines.Length; y++)
        {
            string line = lines[y];
            for (int x = 0; x < line.Length; x ++)
            {
                char c = line[x];

                if (Char.IsLetterOrDigit(c))
                {
                    if (!AntennaDictionary.Keys.Contains(c))
                    {
                        AntennaDictionary[c] = new List<(int, int)>();
                    }

                    AntennaDictionary[c].Add((x, y));
                }
            }
        }
    }
}