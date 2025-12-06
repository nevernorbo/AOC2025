using System.Text;

static class Day05
{
    struct Range
    {
        public long Start { get; set; }
        public long End { get; set; }

        public long CountBetween()
        {
            return End - Start + 1;
        }

        public override string ToString()
        {
            return $"{Start}-{End}";
        }
    }

    public static void Run()
    {
        Console.WriteLine("Part 1: " + SolvePart1());
        Console.WriteLine("Part 2: " + SolvePart2());
    }

    private static int SolvePart1()
    {
        using var reader = new StreamReader("./data/day05.txt", Encoding.UTF8);
        var freshIngredients = GetRanges(reader);

        string? line;
        var freshCount = 0;

        while ((line = reader.ReadLine()) != null)
        {
            var ingredient = long.Parse(line);
            foreach (var item in freshIngredients)
            {
                if (ingredient >= item.Start && ingredient <= item.End)
                {
                    freshCount++;
                    break;
                }
            }
        }

        return freshCount;
    }

    private static long SolvePart2()
    {
        using var reader = new StreamReader("./data/day05.txt", Encoding.UTF8);

        var ranges = GetRanges(reader)
            .OrderBy(r => r.Start)
            .ToArray();

        var count = 0L;
        var prevRange = ranges[0];

        for (int i = 0; i < ranges.Length - 1; i++)
        {
            Console.WriteLine(ranges[i]);

            if (prevRange.End >= ranges[i + 1].Start)
            {
                if (prevRange.End < ranges[i + 1].End)
                {
                    prevRange.End = ranges[i + 1].End;
                }
            }
            else
            {
                count += prevRange.CountBetween();
                prevRange = ranges[i + 1];
            }
        }

        count += prevRange.CountBetween();

        return count;
    }

    private static IEnumerable<Range> GetRanges(StreamReader reader)
    {
        string? line;
        while ((line = reader.ReadLine()) != "")
        {
            var split = line.Split('-').Select(long.Parse).ToArray();
            var range = new Range { Start = split[0], End = split[1] };

            yield return range;
        }
    }
}