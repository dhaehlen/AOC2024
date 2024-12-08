using System.Text.RegularExpressions;

namespace AOC2024v2;

public static class Day3
{

    public static void RunDay3Part1()
    {
        string pattern = @"mul\(([0-9]+),([0-9]+)\)";
        Regex rx = new(pattern);

        using StreamReader file = new("./day3/input.txt");
        string text = file.ReadToEnd();
        MatchCollection results = rx.Matches(text);

        ulong runningTotal = 0;
        foreach (Match match in results)
        {
            Console.WriteLine($"match: {match.Value} -> {match.Groups[1].Value}, {match.Groups[2].Value}");

            //assuming we got uncorrupted results
            //mul(#.....,#....)
            ulong first = ulong.Parse(match.Groups[1].Value);
            ulong second = ulong.Parse(match.Groups[2].Value);

            runningTotal += first * second;
        }

        Console.WriteLine($"Matches Found: {results.Count} Total Value: {runningTotal}");
    }

    public static void RunDay3Part2()
    {
        string pattern = @"don't\(\)|do\(\)|(mul)\(([0-9]+),([0-9]+)\)";
        Regex rx = new(pattern);

        using StreamReader file = new("./day3/input.txt");
        string text = file.ReadToEnd();
        MatchCollection matches = rx.Matches(text);

        ulong runningTotal = 0;
        bool executeState = true;
        foreach(Match match in matches)
        {
            switch(match.Value)
            {
                case "do()":
                    executeState = true;
                    Console.WriteLine("EXECUTE ON");
                    break;
                case "don't()":
                    executeState = false;
                    Console.WriteLine("EXECUTE OFF");
                    break;
                default:
                    if(executeState)
                    {
                        ulong first = ulong.Parse(match.Groups[2].Value);
                        ulong second = ulong.Parse(match.Groups[3].Value);
                        Console.WriteLine($"Number1: {first}, Number2: {second}");
                        runningTotal += first * second;
                    }
                    break;
            }
        }
        Console.WriteLine($"Matches Found: {matches.Count} Total Value: {runningTotal}");
    }  
}