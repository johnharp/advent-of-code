using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text.Unicode;

string basedir = AppDomain.CurrentDomain.BaseDirectory;

Directory.SetCurrentDirectory(basedir);
string prefix = "../../../";

string filename = "input-sample.txt";

string[] lines = File.ReadAllLines($"{prefix}{filename}");
Debug.Assert(lines.Length == 1, "Expected exactly one line of input");
List<BlockRange> ranges;

// Part 1
ranges = HandleLine(lines[0], isPartOne: true);
Console.WriteLine($"Part 1: {Sum(Compact(ranges))}");

// Part 2
// ranges = HandleLine(lines[0], isPartOne: false);
// DumpBlockRangeList(ranges);
// Dump(ranges);

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
                
                // zero out the lenght of the successfully relocated block so
                // it will be removed
                block.Length = 0;

                // Now combine any adjacent free blocks by calling the method
                // MergeFreeBlocks() which takes a list of all blocks and
                // returns a list with the adjacent free blocks merged.
                allRanges = RemoveEmptyBlocks(allRanges);
                allRanges = allRanges.OrderBy(r => r.Start).ToList();
                //allRanges = MergeFreeBlocks(allRanges);
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

List<BlockRange> MergeFreeBlocks(List<BlockRange> ranges)
{
    List<BlockRange> merged = new List<BlockRange>();

    for (int i = 0; i < ranges.Count; i++)
    {
        var block = ranges[i];
        for (int j = i+1; j < ranges.Count; j++)
        {
            if (block.IsEmpty && ranges[j].IsEmpty)
            {
                block.Length += ranges[j].Length;
                i++;
            }
            else
            {
                break;
            }
        }
        merged.Add(block);
    }

    return merged;
}


// List<BlockRange> relocateBlocks = new List<BlockRange>(occupied);
// relocateBlocks.Sort((a, b) => b.Start.CompareTo(a.Start));

// foreach(var block in relocateBlocks)
// {
//     foreach (var freeBlock in free)
//     {
//         if (freeBlock.WillFit(block))
//         {
//             var newBlock = new BlockRange(freeBlock.Start, block.Length, block.Id);
//             occupied.Add(newBlock);
//             freeBlock.Fit(block);
//             break;
//         }
//     }
//     free = free.Where(b => b.Length > 0).ToList();
//     occupied = occupied.Where(b => b.Length > 0).ToList();
//     free.Sort((a, b) => a.Start.CompareTo(b.Start));
//     occupied.Sort((a, b) => a.Start.CompareTo(b.Start));
//     Dump(free, occupied);
// }

// List<BlockRange> Compact(List<BlockRange> blockRanges)
// {

// }

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


// void TrimEnd()
// {
//     while (free.Count > 0 && free.Last() > occupied.Last())
//     {
//         free.RemoveAt(free.Count - 1);
//     }
// }

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


BlockRange GetLast(List<BlockRange> list)
{
    if (list.Count == 0) throw new Exception("List is empty");
    return list[list.Count - 1];
}
