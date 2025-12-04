using System.Data;

static class Day04
{
    public static void Run()
    {
        char[][] grid = File.ReadLines("./data/day04.txt").Select(line => line.ToCharArray()).ToArray();

        // PrintGrid(grid);
        Console.WriteLine(SolvePart1(grid));
        Console.WriteLine(SolvePart2(grid));
    }

    public static int SolvePart1(char[][] grid)
    {
        return grid
            .SelectMany((row, i) => row.Select((_, j) => grid.IsAccessible(i, j)))
            .Count(accessible => accessible);
    }

    public static int SolvePart2(char[][] grid)
    {
        var gridStorage = new ShrinkingGrid(grid);
        gridStorage.StartShrinking();

        return gridStorage.ClearedCount;
    }

    public static bool IsAccessible(this char[][] grid, int row, int col)
    {
        if (grid[row][col] != '@') return false;

        var neighborCount = 0;

        // Create the kernel
        var rowStart = Math.Max(0, row - 1);
        var rowEnd = Math.Min(grid.Length - 1, row + 1);

        var colStart = Math.Max(0, col - 1);
        var colEnd = Math.Min(grid[0].Length - 1, col + 1);

        for (int i = rowStart; i <= rowEnd; i++)
        {
            for (int j = colStart; j <= colEnd; j++)
            {
                if (grid[i][j] == '@' && (i != row || j != col))
                {
                    neighborCount++;
                }
            }
        }

        return neighborCount < 4;
    }

    public static void PrintGrid(char[][] grid)
    {
        foreach (var row in grid)
        {
            foreach (var item in row)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }

    public class ShrinkingGrid
    {
        public char[][] Grid { get; set; }
        public char[][] ShadowGrid { get; set; }
        public int ClearedCount { get; set; } = 0;
        public bool CanShrink { get; set; } = true;

        public ShrinkingGrid(char[][] grid)
        {
            Grid = grid;
            ShadowGrid = new char[grid.Length][];
            grid.CopyTo(ShadowGrid);
        }

        public void StartShrinking()
        {
            while (CanShrink)
            {
                ShrinkGrid();
            }
        }

        private void ShrinkGrid()
        {
            int count = 0;

            for (int row = 0; row < Grid.Length; row++)
            {
                for (int col = 0; col < Grid[0].Length; col++)
                {
                    if (Grid.IsAccessible(row, col))
                    {
                        count++;
                        ShadowGrid[row][col] = 'X';
                    }
                }
            }

            if (count > 0)
            {
                ClearedCount += count;
                ShadowGrid.CopyTo(Grid);
            }
            else
            {
                CanShrink = false;
            }
        }
    }
}