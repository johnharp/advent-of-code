var lines = File.ReadAllLines("../../../input.txt");
DirectoryEntry Root = new DirectoryEntry("/", null);
DirectoryEntry? CurrentDirectory = Root;



foreach(var line in lines)
{
    handleLine(line);
}
Root.CalculateSize();
//Root.Print();


Console.WriteLine($"Part 1 Total: {DirectoryEntry.Part1Total}");

long TotalDiskSize = 70000000;
long TargetFreeSpace = 30000000;

long AmountToFree = TargetFreeSpace - (TotalDiskSize - Root.Size);
long SmallestToDelete = Root.Size;

Console.WriteLine($"Part 2 Amount to Free: {AmountToFree}");

void SearchDirectoriesForTarget(DirectoryEntry d)
{
    if (d.Size >= AmountToFree && d.Size < SmallestToDelete)
    {
        SmallestToDelete = d.Size;
    }
    foreach(var childd in d.Directories)
    {
        SearchDirectoriesForTarget(childd);
    }
}

SearchDirectoriesForTarget(Root);
Console.WriteLine($"Part 2 Smallest to Delete and reach target: {SmallestToDelete}");

void handleLine(string line)
{
    line.Trim();

    if (line == "$ cd .." && CurrentDirectory != null)
    {
        CurrentDirectory = CurrentDirectory.Parent;
    }
    else if (line == "$ cd /")
    {
        CurrentDirectory = Root;
    }
    else if (line == "$ ls")
    {
        // can ignore these -- if we do a listing, the following
        // lines won't match a command and will be handled as files or
        // directoryies
    }
    else if (line.StartsWith("$ cd ") && CurrentDirectory != null)
    {
        string[] parts = line.Split(" ");
        string name = parts[2];

        DirectoryEntry? dir = CurrentDirectory.ContainsDirectory(name);
        if (dir == null)
        {
            dir = new DirectoryEntry(name, CurrentDirectory);
        }
        CurrentDirectory = dir;
    }
    else if (line.StartsWith("dir ") && CurrentDirectory != null)
    {
        string[] parts = line.Split(" ");
        string name = parts[1];
        DirectoryEntry? dir = CurrentDirectory.ContainsDirectory(name);
        if (dir == null)
        {
            dir = new DirectoryEntry(name, CurrentDirectory);
        }
    }
    else  // <size> <filename>
    {
        string[] parts = line.Split();
        long size = long.Parse(parts[0]);
        string name = parts[1];

        if (CurrentDirectory!= null)
        {
            new FileEntry(name, size, CurrentDirectory);
        }
    }
}


public class DirectoryEntry
{
    public static long Part1TargetSize = 100000;
    public static long Part1Total = 0;



    public string Name { get; set; }
    public int Level { get; set; }
    public DirectoryEntry? Parent { get; set; }
    public List<DirectoryEntry> Directories = new List<DirectoryEntry>();
    public List<FileEntry> FileEntries = new List<FileEntry>();

    public long Size = -1;

    public DirectoryEntry(string name, DirectoryEntry? parent)
    {
        Name = name;
        Parent = parent;

        if (parent == null)
        {
            Level = 0;
        }
        else
        {
            Level = parent.Level + 1;
            parent.Directories.Add(this);
        }
    }

    public DirectoryEntry? ContainsDirectory(string name)
    {
        foreach(var dir in Directories)
        {
            if (dir.Name == name)
            {
                return dir;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < Level; i++)
        {
            Console.Write("  ");
        }
        Console.WriteLine($"- {Name} (dir, size={Size})");
        foreach(var dir in Directories)
        {
            dir.Print();
        }
        foreach(var file in FileEntries)
        {
            file.Print();
        }
    }

    public long CalculateSize()
    {
        if (Size == -1)
        {
            long s = 0;
            foreach(var d in Directories)
            {
                s += d.CalculateSize();
            }

            foreach(var f in FileEntries)
            {
                s += f.Size;
            }

            Size = s;

            // if this is the first time the directory has
            // been asked to calculate size, consider it for the
            // target sum
            if (Size <= Part1TargetSize)
            {
                Part1Total += Size;
            }
        }

        return Size;
    }
}

public class FileEntry
{
    public String Name { get; set; }
    public long Size { get; set; }
    public DirectoryEntry Parent { get; set; }
    public int Level { get; set; }

    public FileEntry(string name, long size, DirectoryEntry parent)
    {
        Name = name;
        Size = size;
        Parent = parent;

        if (parent == null)
        {
            Level = 0;
        }
        else
        {
            Level = parent.Level + 1;
            parent.FileEntries.Add(this);
        }
    }

    public void Print()
    {
        for (int i = 0; i < Level; i++)
        {
            Console.Write("  ");
        }
        Console.WriteLine($"- {Name} (file, size={Size})");
    }
}