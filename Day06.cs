static class Day06
{
    public static void Run()
    {
        // Console.WriteLine(SolvePart1());
        Console.WriteLine(SolvePart2());
    }

    private static long SolvePart1()
    {
        var data = File.ReadLines("./data/day06.txt")
            .Select(line => line
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim()).ToArray())
            .ToArray();

        var totalSum = 0L;

        for (int col = 0; col < data[0].Length; col++)
        {
            var isMultiplication = data[^1][col] == "*";

            long columnValue = isMultiplication ? 1 : 0; // Last row

            for (int row = 0; row < data.Length - 1; row++)
            {
                if (isMultiplication)
                {
                    columnValue *= long.Parse(data[row][col]);
                }
                else
                {
                    columnValue += long.Parse(data[row][col]);
                }
            }

            totalSum += columnValue;
        }

        return totalSum;
    }

    private static long SolvePart2()
    {
        var lines = File.ReadLines("./data/day06.txt").ToArray();

        var operators = lines[^1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        var transposedNumbers = lines[..^1]
            .Select(line => line.ToCharArray())
            .ToArray()
            .Transposed()
            .Select(s => new string(s).Trim()); // Join char[] back into strings

        var blockIndex = 0; // Keeps track of the index of a block
        var totalSum = 0L;
        var localAmount = operators[blockIndex] == "*" ? 1L : 0L;

        foreach (string number in transposedNumbers)
        {
            if (number == string.Empty)
            {
                blockIndex++;
                totalSum += localAmount;
                localAmount = operators[blockIndex] == "*" ? 1L : 0L;
                continue;
            }

            if (operators[blockIndex] == "*")
            {
                localAmount *= long.Parse(number);
            }
            else
            {
                localAmount += long.Parse(number);
            }
        }

        totalSum += localAmount; // The last localAmount won't get added in the loop

        return totalSum;
    }

    public static char[][] Transposed(this char[][] charMatrix)
    {
        int rows = charMatrix.Length;
        int cols = charMatrix[0].Length;

        var transposed = new char[cols][];

        // Initialize inner arrays
        for (int j = 0; j < cols; j++)
        {
            transposed[j] = new char[rows];
        }

        // Copy values
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                transposed[j][i] = charMatrix[i][j];
            }
        }

        return transposed;
    }

}
