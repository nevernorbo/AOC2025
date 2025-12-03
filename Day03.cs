using System.Data;

static class Day03
{
    public static void Run()
    {
        var banks = File.ReadAllLines("./data/day03_small.txt");

        Console.WriteLine($"Part 1: {CalculateJoltage(banks, 2)}");
        Console.WriteLine($"Part 2: {CalculateJoltage(banks, 12)}");
    }

    public static long CalculateJoltage(string[] lines, int batteryCount)
    {
        long totalJoltage = 0L;

        foreach (var line in lines)
        {
            string joltage = "";
            int startIndex = 0;

            for (int pickedCount = 0; pickedCount < batteryCount; pickedCount++)
            {
                char maxValue = '\0';
                for (int i = startIndex; i <= line.Length - (batteryCount - pickedCount); i++)
                {
                    if (line[i] > maxValue)
                    {
                        maxValue = line[i];
                        startIndex = i + 1;
                    }
                }
                joltage += maxValue;
            }

            totalJoltage += long.Parse(joltage);
        }

        return totalJoltage;
    }
}