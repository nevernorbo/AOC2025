static class Day07
{
    public static void Run()
    {
        // var lines = File.ReadLines("./data/day07_small.txt").Select(line => line.ToCharArray()).ToArray();
        var lines = File.ReadLines("./data/day07.txt").Select(line => line.ToCharArray()).ToArray();

        Console.WriteLine(SolvePart1(lines));
        Console.WriteLine("Part2: " + SolvePart2(lines));
    }

    private static int SolvePart1(char[][] lines)
    {
        var count = 0;
        var beams = lines[0].Select(c => c.Equals('S') ? true : false).ToArray();

        for (int i = 1; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                if (lines[i][j] == '^' && beams[j])
                {
                    beams[j] = false;
                    beams[Math.Min(j + 1, lines[0].Length)] = true;
                    beams[Math.Max(j - 1, 0)] = true;
                    count++;
                }
            }
        }

        return count;
    }

    private static long SolvePart2(char[][] lines)
    {
        var beams = lines[0].Select(c => c.Equals('S') ? 1L : 0L).ToArray();

        for (int i = 1; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                if (lines[i][j] == '^' && beams[j] > 0)
                {
                    beams[Math.Min(j + 1, lines[0].Length)] += beams[j];
                    beams[Math.Max(j - 1, 0)] += beams[j];
                    beams[j] = 0;
                }
            }
        }

        return beams.Sum();
    }
}
