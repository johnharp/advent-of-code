using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata;

string basedir = AppDomain.CurrentDomain.BaseDirectory;

Directory.SetCurrentDirectory(basedir);
string prefix = "../../../";

string filename = "input.txt";

string[] lines = File.ReadAllLines($"{prefix}{filename}");
Debug.Assert(lines.Length == 1, "Expected exactly one line of input");

List<int?> data = new List<int?>();
int freePtr = -1;
int occupiedPtr = -1;

HandleLine(lines[0]);

while (freePtr < occupiedPtr)
{
    int blockIdToMove = data[occupiedPtr].Value;
    data[freePtr] = blockIdToMove;
    data[occupiedPtr] = null;

    freePtr++;
    while (data[freePtr] != null && freePtr < data.Count)
    {
        freePtr++;
    }

    occupiedPtr--;
    while (data[occupiedPtr] == null && occupiedPtr >= 0)
    {
        occupiedPtr--;
    }
}

long part1 = CheckSum();
Console.WriteLine($"Part 1: {part1}");




void DumpData()
{
    foreach(int? block in data)
    {
        Console.Write(block == null ? "." : block);
    }
    Console.WriteLine();
}

long CheckSum()
{
    long sum = 0;
    for (int i = 0; i < data.Count; i++)
    {
        if (data[i] != null)
        {
            sum += data[i].Value * i;
        }
    }
    return sum;
}

// void TrimEnd()
// {
//     while (free.Count > 0 && free.Last() > occupied.Last())
//     {
//         free.RemoveAt(free.Count - 1);
//     }
// }

void HandleLine(string line)
{
    bool isFile = true;
    int id = 0;
    int index = 0;
    foreach(char c in line)
    {
        int value = int.Parse(c.ToString());

        if (isFile)
        {
            for (int i = 0; i < value; i++)
            {
                data.Add(id);

                // the occupied pointer will point to the last occupied
                // block, so during initialization we'll keep moving it
                // forward for any occupied block
                occupiedPtr = index;
                index++;
            }
            id++;
        }
        else
        {
            // the free pointer starts out at the lowest position where
            // a free block resides and doesn't move from there
            // until we start compacting
            if (freePtr == -1) freePtr = index;

            for (int i = 0; i < value; i++)
            {
                data.Add(null);
                index++;
            }
        }
        isFile = !isFile;
    }
}
