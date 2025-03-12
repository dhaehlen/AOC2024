namespace AOC2024v2;

public static class Day4
{
    public static void RunDay4Part1()
    {
        using StreamReader file = new("./AOC2024v2/day4/input.txt");
        List<string> fileLines = [];
        string? line;
        while((line = file.ReadLine()) is not null)
        {
            fileLines.Add(line.Trim());
        }

        int countOfXmas = CountXmas(fileLines);
        Console.WriteLine($"Count of XMAS: {countOfXmas}");
    }

    public static int CountXmas(List<string> input)
    {
        int width = input[0].Length;
        int height = input.Count;

        char[,] charField = new char[height, width];

        int row = 0;
        foreach(string puzzleLine in input)
        {   
            int column = 0;
            foreach(char c in puzzleLine)
            {
                charField[row, column] = c; 
                column++;
            }
            row++;
        }
        int countOfXmas = 0;

        //Search for X then look at the surrounding 8 squares for M. Intially we were going
        //to look for S or X but this would result in double counting.
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                char c = charField[i, j];
                if(c == 'X')
                {
                    //Up Left
                    if(GetUpLeftChar(charField, i, j) == 'M')
                    {
                        if(GetUpLeftChar(charField, i - 1, j - 1) == 'A')
                        {
                            if(GetUpLeftChar(charField, i - 2, j - 2) == 'S')
                            {
                                countOfXmas++;
                            }
                        }
                    }

                    //Up
                    if(GetUpChar(charField, i, j) == 'M')
                    {
                        if(GetUpChar(charField, i - 1, j) == 'A')
                        {
                            if(GetUpChar(charField, i - 2, j) == 'S')
                            {
                                countOfXmas++;
                            }
                        }
                    }

                    //Up Right
                    if(GetUpRightChar(charField, i, j) == 'M')
                    {
                        if(GetUpRightChar(charField, i - 1, j + 1) == 'A')
                        {
                            if(GetUpRightChar(charField, i - 2, j + 2) == 'S')
                            {
                                countOfXmas++;
                            }
                        }
                    }

                    //Right
                    if(GetRightChar(charField, i, j) == 'M')
                    {
                        if(GetRightChar(charField, i, j + 1) == 'A')
                        {
                            if(GetRightChar(charField, i, j + 2) == 'S')
                            {
                                countOfXmas++;
                            }
                        }
                    }

                    //Down Right
                    if(GetDownRightChar(charField, i, j) == 'M')
                    {
                        if(GetDownRightChar(charField, i + 1, j + 1) == 'A')
                        {
                            if(GetDownRightChar(charField, i + 2, j + 2) == 'S')
                            {
                                countOfXmas++;
                            }
                        }

                    }

                    //Down
                    if(GetDownChar(charField, i, j) == 'M')
                    {
                        if(GetDownChar(charField, i + 1, j) == 'A')
                        {
                            if(GetDownChar(charField, i + 2, j) == 'S')
                            {
                                countOfXmas++;
                            }
                        }
                    }

                    //Down Left
                    if(GetDownLeftChar(charField, i, j) == 'M')
                    {
                        if(GetDownLeftChar(charField, i + 1, j - 1) == 'A')
                        {
                            if(GetDownLeftChar(charField, i + 2, j - 2) == 'S')
                            {
                                countOfXmas++;
                            }
                        }
                    }

                    //Left
                    if(GetLeftChar(charField, i, j) == 'M')
                    {
                        if(GetLeftChar(charField, i, j - 1) == 'A')
                        {
                            if(GetLeftChar(charField, i, j - 2) == 'S')
                            {
                                countOfXmas++;
                            }
                        }
                    }
                }
            }
        }
        return countOfXmas;
    }

    private static char GetUpLeftChar(char[,] charField, int i, int j)
    {
        if(i - 1 >= 0 && j - 1 >= 0)
        {
            return charField[i - 1, j - 1];
        }
        return ' ';
    }

    private static char GetUpChar(char[,] charField, int i, int j)
    {
        if(i - 1 >= 0)
        {
            return charField[i - 1, j];
        }
        return ' ';
    }

    private static char GetUpRightChar(char[,] charField, int i, int j)
    {
        if(i - 1 >= 0 && j + 1 < charField.GetLength(1))
        {
            return charField[i - 1, j + 1];
        }
        return ' ';
    }

    private static char GetRightChar(char[,] charField, int i, int j)
    {
        if(j + 1 < charField.GetLength(1))
        {
            return charField[i, j + 1];
        }
        return ' ';
    }
    
    private static char GetDownRightChar(char[,] charField, int i, int j)
    {
        if(i + 1 < charField.GetLength(0) && j + 1 < charField.GetLength(1))
        {
            return charField[i + 1, j + 1];
        }
        return ' ';
    }

    private static char GetDownChar(char[,] charField, int i, int j)
    {
        if(i + 1 < charField.GetLength(0))
        {
            return charField[i + 1, j];
        }
        return ' ';
    }

    private static char GetDownLeftChar(char[,] charField, int i, int j)
    {
        if(i + 1 < charField.GetLength(0) && j - 1 >= 0)
        {
            return charField[i + 1, j - 1];
        }
        return ' ';
    }

    private static char GetLeftChar(char[,] charField, int i, int j)
    {
        if(j - 1 >= 0)
        {
            return charField[i, j - 1];
        }
        return ' ';
    }
}


public class Day4Tests
{
    public List<List<string>> testCases;

    public Day4Tests()
    {
        testCases =
        [
            //XMAS
            ["XMAS"],
            //SMAX
            ["SAMX"],
            //X
            //M
            //A
            //S
            ["X", "M", "A", "S"],
            //S
            //A
            //M
            //X
            ["S", "A", "M", "X"],
            //X
            //oM
            //ooA
            //oooS
            //["Xooo", "oMoo", "ooAo", "oooS"],
            //oooX
            //ooM
            //oA
            //S
            ["oooX", "ooMo", "oAoo", "Sooo"],
            //XMAS
            //XMAM
            //XMAS
            //XXAS
            ["XMAS", "XMAM", "XMAS", "XXAS"],

            //from question
            ["MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"],

            ["SOOOOOS",
             "OAOOOAO",
             "OOMOMOO",
             "OOOXOOO"],

            ["..X...",
             ".SAMX.",
             ".A..A.",
             "XMAS.S",
             ".X...."],
        ];
    }
    public void RunDay4Part1Tests()
    {
        foreach (List<string> testCase in testCases)
        {
            int countOfXmas = Day4.CountXmas(testCase);
            Console.WriteLine($"Count of XMAS: {countOfXmas}");
        }
    }
}