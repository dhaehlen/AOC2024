namespace AOC2024v2.Day2;

static class Day2
{
    public static void Day2Part1(){
        bool direction; //false: decreasing, true: increasing
        int safeCount = 0;

        using StreamReader file = new("./input.txt");
        string? line;
        while ((line = file.ReadLine()) is not null)
        {
            line = line.Trim();
            string[] levels = line.Split(' ');

            int previousValue = int.Parse(levels[0]);
            int nextValue = int.Parse(levels[1]);
            direction = nextValue > previousValue;

            int diff = Math.Abs(nextValue - previousValue);

            if (diff > 3 || diff < 1) { continue; }

            previousValue = nextValue;
            bool fail = false;
            for(int i = 2; i < levels.Length; i++)
            {
                if (direction)
                {
                    nextValue = int.Parse(levels[i]);
                    if (nextValue < previousValue) { fail = true; }
                    diff = Math.Abs(nextValue - previousValue);
                    if (diff > 3 || diff < 1) { fail = true; }
                    previousValue = nextValue;
                }
                else
                {
                    nextValue = int.Parse(levels[i]);
                    if (nextValue > previousValue) { fail = true; }
                    diff = Math.Abs(nextValue - previousValue);
                    if (diff > 3 || diff < 1) { fail = true; }
                    previousValue = nextValue;
                }
            }
        }
        
    }
}