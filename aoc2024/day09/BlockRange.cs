public class BlockRange
{
    public static int EMPTY = -1;

    public int Start { get; set; }
    public int Length { get; set; }
    public int Id { get; set; }
    public BlockRange(int start, int length, int id)
    {
        Start = start;
        Length = length;
        Id = id;
    }

    public BlockRange(BlockRange b)
    {
        Start = b.Start;
        Length = b.Length;
        Id = b.Id;
    }

    public bool IsEmpty{
        get{
            return Id == EMPTY;
        }
    }

    public bool WillFit(BlockRange other)
    {
        return (Id == -1 && Length >= other.Length);
    }

    // When called on an empty block (a block with ID -1), Fit() will
    // reduce the length of the empty block by the length of the 
    // other block (the block being relocated).
    // Also set the length of the other block to 0.
    // After calling Fit(), the caller is responsible for destroying
    // any blocks with length of 0.
    public void Fit(BlockRange other)
    {
        if (Id != -1 || other.Id == -1)
        {
            throw new Exception(
                "Invalid call to Fit" +
                " (should only be called on an empty block and passed a non-empty block)");
        }

        if (Length < other.Length)
        {
            throw new Exception(
                "Invalid call to Fit" +
                " (other block is too large)");
        }

        this.Length = this.Length - other.Length;
        this.Start = this.Start + other.Length;
        other.Length = 0;
    }
}