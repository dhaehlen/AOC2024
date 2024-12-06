namespace AOC2024v2.day1;

static class Day1
{
    public static void RunDay1Part1()
    {
        Console.WriteLine("Day 1 Part 1");

        int[] leftNums = new int[1000];
        int[] rightNums = new int[1000];

        int index = 0;
        using(StreamReader file = new("./day1/input.txt"))
        {
            string? line;
            while((line = file.ReadLine()) is not null)
            {
                line = line.Trim();
                string[] words = line.Split("  ");
                if(words.Length == 2)
                {
                    leftNums[index] = int.Parse(words[0].Trim());
                    rightNums[index] = int.Parse(words[1].Trim());
                }
                else{
                    Console.WriteLine($"line# {index}: {line} does not contain 2 numbers");
                }
                index++;
            }
        }
        Array.Sort(leftNums);
        Array.Sort(rightNums);

        ulong runningTotal = 0;

        for (int i = 0; i < leftNums.Length; i++)
        {
            runningTotal += (ulong) Math.Abs(leftNums[i] - rightNums[i]);
        }

        Console.WriteLine($"The distance is {runningTotal}");
    }

    public static void RunDay1Part2()
    {
        Console.WriteLine("Day 1 Part 2");
        HashSet<int> leftNums = [];
        Dictionary<int, int> rightNums = [];

        using StreamReader file = new("./day1/input.txt");
        string? line;
        while((line = file.ReadLine()) is not null)
        {
            line = line.Trim();
            string[] words = line.Split("  ");
            if(words.Length == 2)
            {
                leftNums.Add(int.Parse(words[0].Trim()));
                bool exists = rightNums.TryGetValue(int.Parse(words[1]), out int currentCount);
                if(exists)
                {
                    rightNums[int.Parse(words[1])] = currentCount + 1;
                }
                else
                {
                    rightNums.Add(int.Parse(words[1]), 1);
                }
            }
            else
            {
                Console.WriteLine($"{line} does not contain 2 numbers");
            }
        }

        ulong runningTotal = 0; 
        foreach(int i in leftNums){
            runningTotal += (ulong) (i * rightNums.GetValueOrDefault(i, 0));
        }

        Console.WriteLine($"The similarity is: {runningTotal}");
    }
}