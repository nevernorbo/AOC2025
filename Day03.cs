using System.Data;

static class Day03
{
    public static void Run()
    {
        var banks = File.ReadAllLines("./data/day03.txt");

        Console.WriteLine($"Part 1: {CalculateJoltage(banks, 2)}");
        Console.WriteLine($"Part 2: {CalculateJoltage(banks, 12)}");
    }

    public static long CalculateJoltage(string[] banks, int nrOfBatteries)
    {
        long totalJoltage = 0L;
        foreach (var bankStr in banks)
        {
            var bank = bankStr.Select(c => long.Parse(c.ToString())).ToList();
            string joltage = "";
            int maxIndex = 0;

            for (int i = 1; i <= nrOfBatteries; i++)
            {
                int endIndex = (nrOfBatteries - i) != 0 ? -(nrOfBatteries - i) : bank.Count;

                int windowSize = endIndex < 0 ? bank.Count + endIndex - maxIndex : endIndex - maxIndex;
                var window = bank.Skip(maxIndex).Take(windowSize);

                long maxBattery = window.Max();
                maxIndex = window.ToList().IndexOf(maxBattery) + maxIndex + 1;

                joltage += maxBattery.ToString();
            }

            totalJoltage += long.Parse(joltage);
        }

        return totalJoltage;
    }
}