static class Day02
{
    struct Range
    {
        public long Start { get; set; }
        public long End { get; set; }
    }

    public static void Run()
    {
        // Read input
        var input = File.ReadAllText("./data/day02.txt");

        // Parse into ranges
        var ranges = input.Split(',').Select(range =>
        {
            var split = range.Split('-');
            return new Range { Start = long.Parse(split[0]), End = long.Parse(split[1]) };
        }).ToList();

        SolvePart1(ranges);
        SolvePart2(ranges);
    }

    private static void SolvePart1(List<Range> ranges)
    {
        // Initialize result variable
        var invalidIdSum = 0L;

        // Iterate through the ranges
        foreach (var range in ranges)
        {
            // Iterate through each number in the range
            foreach (var number in range.Enumerate())
            {
                var numberString = number.ToString();
                var n = numberString.Length;

                var (firstPart, secondPart) = (numberString[..(n / 2)], numberString[(n / 2)..]);
                if (firstPart == secondPart)
                {
                    invalidIdSum += number;
                }
            }
        }

        Console.WriteLine(invalidIdSum);
    }

    private static void SolvePart2(List<Range> ranges)
    {
        // Initialize result variable
        var invalidIdSum = 0L;

        // Iterate through the ranges
        foreach (var range in ranges)
        {
            // Iterate through each number in the range
            foreach (var number in range.Enumerate())
            {
                var numberString = number.ToString();
                var numberStringLength = numberString.Length;

                // Split the number into chunks starting from the biggest chunks (when the number is split in two)
                for (int divisor = 2; divisor <= numberStringLength; divisor++)
                {
                    if (numberStringLength % divisor != 0) continue; // If the number is not divisible by the divisor there wouldn't be a repeat

                    var chunkSize = numberStringLength / divisor;

                    if (HasRepeat(numberString, chunkSize))
                    {
                        invalidIdSum += number;
                        break; // Break at longest chunk repeat
                    }
                }
            }
        }

        Console.WriteLine(invalidIdSum);
    }

    private static IEnumerable<long> Enumerate(this Range range)
    {
        for (var number = range.Start; number <= range.End; number++)
            yield return number;
    }

    private static bool HasRepeat(string numberString, int chunkSize)
    {
        var firstChunk = numberString[..chunkSize];

        for (int i = chunkSize; i < numberString.Length; i += chunkSize)
        {
            if (firstChunk != numberString[i..(i + chunkSize)])
            {
                return false;
            }
        }

        return true;
    }
}