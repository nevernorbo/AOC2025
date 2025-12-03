static class Day01
{
    public static void Run()
    {
        SolvePart1();
        SolvePart2();
    }

    private static void SolvePart1()
    {
        var dial = 50;
        long password = 0;

        foreach (var line in File.ReadLines("./data/day01.txt"))
        {
            var (direction, rotation) = (line[0], int.Parse(line[1..]));

            if (direction == 'L') rotation *= -1;

            dial = (dial + rotation) % 100;

            if (dial == 0) password++;
        }

        Console.WriteLine(password);
    }

    private static void SolvePart2()
    {
        var dial = 50;
        long password = 0;

        foreach (var line in File.ReadLines("./data/day01.txt"))
        {
            var (direction, rotation) = (line[0], int.Parse(line[1..]));

            var circles = rotation / 100;
            password += circles;
            rotation -= circles * 100;
            if (rotation % 100 == 0)
            {
                dial = 0;
                continue;
            }

            int newValue = 0;

            if (direction == 'L')
            {
                newValue = dial - rotation;
                if (dial != 0 && newValue < 0) password++;
            }
            else if (direction == 'R')
            {
                newValue = dial + rotation;
                if (dial != 0 && newValue > 100) password++;
            }

            dial = ((newValue % 100) + 100) % 100;

            if (dial == 0) password++;
        }

        Console.WriteLine(password);
    }
}
