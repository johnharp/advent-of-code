using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text.Unicode;

string basedir = AppDomain.CurrentDomain.BaseDirectory;

Directory.SetCurrentDirectory(basedir);
string prefix = "../../../";

string filename = "input.txt";

string[] lines = File.ReadAllLines($"{prefix}{filename}");
Debug.Assert(lines.Length == 1, "Expected exactly one line of input");
List<BlockRange> ranges;

// Part 1
ranges = HandleLine(lines[0], isPartOne: true);
Console.WriteLine($"Part 1: {Sum(Compact(ranges))}");

// Part 2
ranges = HandleLine(lines[0], isPartOne: false);
Console.WriteLine($"Part 2: {Sum(Compact(ranges))}");


long Sum(IEnumerable<BlockRange> ranges)
{
    long sum = 0;
    foreach (var range in ranges.Where(r => !r.IsEmpty))
    {
        for (int i = 0; i < range.Length; i++)
        {
            sum += range.Id * (range.Start+i);
        }
    }
    return sum;
}

List<BlockRange> Compact(List<BlockRange> ranges)
{
    // Make a shallow copy of the initial range list
    var allRanges = new List<BlockRange>(ranges);

    // We'll be modifying the collection "allRanges", so make a copy of all
    // the blocks occupied at start and iterate through them one at a time
    // to attempt reloction.  Each block only gets one chance to relocate.
    var relocateBlocks = new List<BlockRange>(allRanges.Where(r => !r.IsEmpty))
        .OrderByDescending(r => r.Start)
        .ToList();

    for (int i = 0; i < relocateBlocks.Count; i++)
    {
        var block = relocateBlocks[i];
        var free = new List<BlockRange>(allRanges.Where(r => r.IsEmpty && r.Start < block.Start));

        // attempt to relocate the block into each free block
        for (int j = 0; j < free.Count; j++)
        {
            var freeBlock = free[j];
            if (freeBlock.WillFit(block))
            {
                // Create the new block copy for the relocated block at the
                // start of the free block where it will fit
                var newOccupiedBlock = new BlockRange(freeBlock.Start, block.Length, block.Id);
                allRanges.Add(newOccupiedBlock);

                // The freeblock where we relocated the block will reduce in size
                freeBlock.Length = freeBlock.Length - block.Length;
                freeBlock.Start = freeBlock.Start + block.Length;
                
                // zero out the lenght of the successfully relocated block so
                // it will be removed
                block.Length = 0;

                // Now combine any adjacent free blocks by calling the method
                // MergeFreeBlocks() which takes a list of all blocks and
                // returns a list with the adjacent free blocks merged.
                allRanges = RemoveEmptyBlocks(allRanges);
                allRanges = allRanges.OrderBy(r => r.Start).ToList();

                break;
            }
        }
    }

    return allRanges;
}

List<BlockRange> RemoveEmptyBlocks(List<BlockRange> ranges)
{
    return ranges.Where(r => r.Length > 0).ToList();
}


#pragma warning disable CS8321 // Local function is declared but never used
void DumpBlockRangeList(IEnumerable<BlockRange> ranges)
{
    foreach (var range in ranges)
    {
        Console.WriteLine($"Start: {range.Start}, Length: {range.Length}, Id: {range.Id}");
    }
}

void Dump(IEnumerable<BlockRange> ranges)
{
    foreach (var range in ranges)
    {
        char c = range.Id == BlockRange.EMPTY ? '.' : range.Id.ToString()[0];
        for (int i = 0; i < range.Length; i++)
        {
            Console.Write(c);
        }
    }
    Console.WriteLine();
}
#pragma warning restore CS8321 // Local function is declared but never used



List<BlockRange> HandleLine(string line, bool isPartOne)
{
    List<BlockRange> blockRanges = new List<BlockRange>();

    bool isFile = true;
    int id = 0;
    int index = 0;
    line = line.Trim();

    foreach (char c in line)
    {
        int value = int.Parse(c.ToString());

        if (value > 0)
        {
            if (isFile)
            {
                if (isPartOne)
                {
                    for (int i = 0; i < value; i++)
                    {
                        blockRanges.Add(new BlockRange(index, 1, id));
                        index++;
                    }
                }
                else
                {
                    blockRanges.Add(new BlockRange(index, value, id));
                    index += value;
                }
                id++;
            }
            else
            {
                if (isPartOne)
                {
                    for (int i = 0; i < value; i++)
                    {
                        blockRanges.Add(new BlockRange(index, 1, BlockRange.EMPTY));
                        index++;
                    }
                }
                else
                {
                    blockRanges.Add(new BlockRange(index, value, BlockRange.EMPTY));
                    index += value;
                }

            }
        }

        isFile = !isFile;
    }

    return blockRanges;
}
