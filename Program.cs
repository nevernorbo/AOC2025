Action[] problemSolutions =
[
    Day01.Run,
    Day02.Run,
    Day03.Run,
    Day04.Run,
    Day05.Run,
];

string input = args.Length != 0 ? args[0] : "";

if (int.TryParse(input, out int number) && number >= 1 && number <= problemSolutions.Length)
{
    Console.WriteLine($"Running day number [{number}]...{Environment.NewLine}");
    problemSolutions[number - 1]();
}
else
{
    Console.WriteLine($"{Environment.NewLine}Enter the day number as a command line argument [1-{problemSolutions.Length}]");
}