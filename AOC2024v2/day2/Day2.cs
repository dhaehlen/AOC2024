namespace AOC2024v2;

static class Day2
{
    public static void RunDay2Part1()
    {
        bool direction; //false: decreasing, true: increasing
        int safeCount = 0;

        using StreamReader file = new("./day2/input.txt");
        string? line;
        while ((line = file.ReadLine()) is not null)
        {
            line = line.Trim();
            string[] levels = line.Split(' ');

            int previousLevel = int.Parse(levels[0]);
            int nextLevel = int.Parse(levels[1]);
            direction = nextLevel > previousLevel;

            int diff = Math.Abs(nextLevel - previousLevel);
            if (diff > 3 || diff < 1) { continue; }
            previousLevel = nextLevel;

            for(int i = 2; i < levels.Length; i++)
            {
                if (direction)
                {
                    nextLevel = int.Parse(levels[i]);
                    if (nextLevel < previousLevel) { break; }
                    diff = Math.Abs(nextLevel - previousLevel);
                    if (diff > 3 || diff < 1) { break; }
                    previousLevel = nextLevel;
                }
                else
                {
                    nextLevel = int.Parse(levels[i]);
                    if (nextLevel > previousLevel) { break;}
                    diff = Math.Abs(nextLevel - previousLevel);
                    if (diff > 3 || diff < 1) { break; }
                    previousLevel = nextLevel;
                }
                if(i == levels.Length - 1){ safeCount++; }
            }
        }
        Console.WriteLine($"Safe Count is {safeCount}");
    }

/// <summary>
/// Day 2 Part 2 is significantly harder. I think one way would be to traverse
/// each route through the report. Essentially brute force it to see if any
/// route is safe. By route I mean some path from left to right in the report 
/// numbers skipping or not skipping one number. The seach can be trimmed down
/// though becuase we are only allowed to skip once so paths that require more than
/// one skip are not safe.
/// 
/// So how do we do that. We can think of the array and the paths through it as a
/// tree. example 1 3 2 4 5
///                     o
///                    / \
///                   /   \
///                  /     \
///                 /       \
///                1         3
///              /   \      / \
///             3     2    2   4
///            /\    / \   |    \
///           2  4  4   5  4     5
///          /\  |  |      |
///         4  5 5  5      5
///         |
///         5
///         
/// So for example the 1 - 2 - 5 branch is not accessabile since it requires two skips
/// in the original "array".
/// 
/// We can also stop searching a branch if it voilates a condition (either direction or
/// step size). For example going down the left side of the tree through the path 
/// o - 1 - 3 - 2 we don't need to check any branches after 2 because 2 is a change of
/// direction.
/// 
/// </summary>
/// 
    public static bool RecursivePathSearch(int[] array, int i, bool? currentDirection, bool skipped)
    {
        if(i == array.Length - 1) { return true; }
        bool result = false;
        int current = array[i];

        if(i + 1 < array.Length)
        {
            int next = array[i + 1];
            bool direction = current < next;
            int stepSizeNext = Math.Abs(next - current);
            bool stepSizeNextIsValid = stepSizeNext > 0 && stepSizeNext <= 3;
            bool directionIsValid = direction == (currentDirection ?? direction);
            
            if(!skipped && i == array.Length - 2){ return true; } 
            if(directionIsValid && stepSizeNextIsValid)
            {
                result = result || RecursivePathSearch(array, i + 1, direction, skipped);
            }
        }

        if(i + 2 < array.Length)
        {
            int nextnext = array[i + 2];
            bool directionNext = current < nextnext;;
            int stepSizeNextNext = Math.Abs(nextnext - current);
            bool stepSizeNextNextIsValid = stepSizeNextNext > 0 && stepSizeNextNext <= 3;
            bool directionNextIsValid = directionNext == (currentDirection ?? directionNext);

            if(directionNextIsValid && stepSizeNextNextIsValid && !skipped)
            {
                skipped = true;
                result = result || RecursivePathSearch(array, i + 2, directionNext, skipped);
            }
        }
        return result;
    }

    public static void RunDay2Part2()
    {
        int safeCount = 0;

        using StreamReader file = new("./day2/input.txt");
        string? line;
        while((line = file.ReadLine()) is not null)
        {
            line = line.Trim();
            string[] sLevels = line.Split(' ');
            int[] levels = new int[sLevels.Length];
            for(int i = 0; i < sLevels.Length; i++)
            {
                levels[i] = int.Parse(sLevels[i]);
            }

            bool safe = RecursivePathSearch(levels, 0, null, false);
            safe = safe || RecursivePathSearch(levels, 1, null, true);
            Console.WriteLine($"{line} is : {safe}");
            if(safe){ safeCount++; }
        }

        Console.WriteLine($"Safe Count with dampening is: {safeCount}");
    }
}