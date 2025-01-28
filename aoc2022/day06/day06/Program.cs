// See https://aka.ms/new-console-template for more information

using System.Text;

var s = new StreamReader("../../../input.txt");
var packetBuffer = new Buffer(4);
var messageBuffer = new Buffer(14);

bool packetDetected = false;
bool messageDetected = false;

while(!s.EndOfStream &&
      (packetDetected == false || messageDetected == false))
{
    char c = (char) s.Read();

    packetBuffer.Add(c);
    messageBuffer.Add(c);

    if (!packetDetected && packetBuffer.IsMarker())
    {
        Console.WriteLine($"Packet marker: {packetBuffer} at " +
            $"location {packetBuffer.TotalCharsRead}");
        packetDetected = true;
    }

    if (!messageDetected && messageBuffer.IsMarker())
    {
        Console.WriteLine($"Message marker: {messageBuffer} at " +
            $"location {messageBuffer.TotalCharsRead}");
        messageDetected = true;
    }
}



public class Buffer
{
    private int BufferSize = 0;
    private int TotalCharactersRead = 0;
    private List<Char> Chars;

    private Dictionary<Char, int> CharCounts;

    public Buffer(int size)
    {
        BufferSize = size;
        Chars = new List<Char>(size);
        CharCounts = new Dictionary<char, int>();
    }

    public int TotalCharsRead
    {
        get { return TotalCharactersRead;  }
    }

    public void Add(char c)
    {
        if (TotalCharactersRead >= BufferSize)
        {
            char toRemove = Chars[0];
            CharCounts[toRemove] -= 1;
            if (CharCounts[toRemove] == 0)
            {
                CharCounts.Remove(toRemove);
            }
            Chars.RemoveAt(0);
        }

        Chars.Add(c);
        TotalCharactersRead += 1;

        if (CharCounts.ContainsKey(c))
        {
            CharCounts[c] += 1;
        }
        else
        {
            CharCounts[c] = 1;
        }
    }

    public void Print()
    {
        for (int i = 0; i < BufferSize && i < TotalCharactersRead; i++)
        {
            Console.Write(Chars[i]);
        }
        Console.WriteLine();
    }

    public override string ToString()
    {
        StringBuilder b = new StringBuilder();
        for (int i = 0; i < BufferSize && i < TotalCharactersRead; i++)
        {
            b.Append(Chars[i]);
        }
        return b.ToString();
    }

    public bool IsMarker()
    {
        if (TotalCharactersRead >= BufferSize &&
            CharCounts.Keys.Count == BufferSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}