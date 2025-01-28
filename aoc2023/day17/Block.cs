using System.Data;

class Block
{
    private int loss;
    private int row;
    private int col;

    private static int width = 0;
    private static int height = 0;

    // AllBlocks indexes all the blocks with a key of <row, col>
    private static Dictionary<Tuple<int, int>, Block> AllBlocks = new Dictionary<Tuple<int, int>, Block>();

    public static int Height()
    {
        return height;
    }

    public static int Width()
    {
        return width;
    }

    public static void HandleInput(string[] lines)
    {
        width = lines[0].Length;
        height = lines.Length;

        int row = 0;
        int col = 0;
        foreach (string line in lines)
        {
            col = 0;
            foreach (char c in line)
            {
                int loss = (int) Char.GetNumericValue(c);
                Block b = new Block(loss, row, col);
                AllBlocks.Add(Key(row, col), b);
                col++;
            }

            row++;
        }
    }

    public static Tuple<int, int> Key(int row, int col)
    {
        return new Tuple<int, int>(row, col);
    }

    public static Block Get(int row, int col)
    {
        Block b = AllBlocks[Key(row, col)];
        return b;
    }

    override public string ToString()
    {
        return $"Block row: {this.row} col: {this.col} loss: {this.loss}";
    }

    public Block(int loss, int row, int col)
    {
        this.loss = loss;
        this.row = row;
        this.col = col;
    }
}