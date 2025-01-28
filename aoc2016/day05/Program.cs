using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace day05;

class Program
{
    private static MD5 md5Hasher = MD5.Create();

    static void Main(string[] args)
    {
        string basedir = AppDomain.CurrentDomain.BaseDirectory;
        Directory.SetCurrentDirectory(basedir);
        string prefix = "../../../";
        string filename = "input.txt";

        string[] lines = File.ReadAllLines($"{prefix}{filename}");
        Debug.Assert(lines.Length == 1, "Expected only one line of input");
        string puzzleInput = lines[0];
        
        long i = -1;
        bool solvedPart1 = false;
        bool solvedPart2 = false;

        string password = "";
        string part2Password = "--------";

        while (!solvedPart1 || !solvedPart2)
        {
            i++;
            string startOfHash = doHash(puzzleInput, i);

            if (startOfHash.StartsWith("00000"))
            {
                if (!solvedPart1)
                {
                    password += startOfHash.Substring(5, 1);
                }

                if (!solvedPart2)
                {
                    string pos = startOfHash.Substring(5, 1);
                    string character = startOfHash.Substring(6, 1);

                    string validPos = "01234567";
                    if (validPos.Contains(pos))
                    {
                        int index = int.Parse(pos);
                        if (part2Password[index] == '-')
                        {
                            char[] p = part2Password.ToArray();
                            p[index] = character[0];
                            part2Password = new string(p);

                            Console.WriteLine(part2Password);
                        }
                    }
                }
            }

            if (password.Length == 8) solvedPart1 = true;
            if (!part2Password.Contains("-")) solvedPart2 = true;

        }

        Console.WriteLine($"Part 1: {password}");
        Console.WriteLine($"Part 2: {part2Password}");
    }

        private static string doHash(string puzzleInput, long suffixNumber)
    {
        string toHash = $"{puzzleInput}{suffixNumber}";
        byte[] result = md5Hasher.ComputeHash(Encoding.Default.GetBytes($"{puzzleInput}{suffixNumber}"));
        string startOfString = 
            result[0].ToString("x2") +
            result[1].ToString("x2") +
            result[2].ToString("x2") +
            result[3].ToString("x2");
        
        return startOfString;
    }
}
