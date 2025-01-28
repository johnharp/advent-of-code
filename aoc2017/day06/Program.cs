public class MemoryBank
{
    public int bankNum;
    public int blockCount;
}

public class Program
{
    public static void Main(string[] args)
    {
        var banks = File.ReadAllLines("input.txt")[0]
            .Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Select((v, i) => new MemoryBank() { bankNum = i, blockCount = int.Parse(v) })
            .ToList();

        HashSet<string> states = new HashSet<string>();

        bool duplicateStateDetected = false;
        string dupState = "";
        int numRedistributions = 0;
        while (!duplicateStateDetected)
        {
            numRedistributions++;

            var bank = Choose(banks);
            Distribute(bank, banks);
            string state = BankState(banks);
            if (states.Contains(state))
            {
                duplicateStateDetected = true;
                dupState = state;
            }
            else
            {
                states.Add(state);
            }
        }

        Console.WriteLine($"Part 1: {numRedistributions}");

        int part2 = 0;
        while (true)
        {
            part2++;

            var bank = Choose(banks);
            Distribute(bank, banks);
            string state = BankState(banks);
            if (state == dupState) break;
        }

        Console.WriteLine($"Part 2: {part2}");

    }


    private static MemoryBank Choose(List<MemoryBank> banks)
    {
        MemoryBank bank = banks
            .OrderByDescending(b => b.blockCount)
            .ThenBy(b => b.bankNum)
            .Take(1)
            .Single();

        return bank;
    }

    private static void Distribute(MemoryBank bank, List<MemoryBank> banks)
    {
        int blocksToDistribute = bank.blockCount;
        bank.blockCount = 0;
        int bankNum = bank.bankNum;

        while (blocksToDistribute > 0)
        {
            bankNum = (bankNum + 1) % banks.Count;
            banks[bankNum].blockCount++;
            blocksToDistribute--;
        }
    }

    private static string BankState(List<MemoryBank> banks)
    {
        string s = 
            String.Join("|", banks.Select(b => b.blockCount));

        return s;
    }

    private static void Dump(List<MemoryBank> banks)
    {
        foreach (var bank in banks.OrderBy(b => b.bankNum))
        {
            Console.Write($"{bank.blockCount}  ");
        }
        Console.WriteLine();
    }
}

