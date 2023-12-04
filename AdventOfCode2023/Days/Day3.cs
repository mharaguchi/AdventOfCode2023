using AdventOfCode2023.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public static class Day3
    {
        public static string _sampleInput = @"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598..";

        public static string Run(string input, int part, bool useSampleData)
        {
            if (part == 1)
            {
                if (useSampleData)
                {
                    return RunPart1(_sampleInput);
                }
                return RunPart1(input);
            }
            else
            {
                if (useSampleData)
                {
                    return RunPart2(_sampleInput);
                }
                return RunPart2(input);
            }
        }

        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var sum = 0;

            for (int y = 0; y < lines.Length; y++)
            {
                var numStart = -1;
                var inNum = false;
                var numLen = 0;
                var nearSymbol = false;
                var line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    if (!inNum)
                    {
                        if (line[x] >= '0' && line[x] <= '9')
                        {
                            inNum = true;
                            numStart = x;
                            numLen++;
                            if (!nearSymbol && IsNearSymbol(lines, x, y))
                            {
                                nearSymbol = true;
                            }
                        }
                    }
                    else
                    {
                        if (line[x] >= '0' && line[x] <= '9')
                        {
                            numLen++;
                            if (!nearSymbol && IsNearSymbol(lines, x, y))
                            {
                                nearSymbol = true;
                            }
                        }
                        else
                        {
                            if (nearSymbol)
                            {
                                var thisNum = Int32.Parse(line.Substring(numStart, numLen));
                                Console.WriteLine(thisNum.ToString());
                                sum += thisNum;
                                nearSymbol = false;
                            }
                            inNum = false;
                            numStart = -1;
                            numLen = 0;
                        }
                    }
                }
                if (inNum)
                {
                    if (!nearSymbol && IsNearSymbol(lines, line.Length - 1, y))
                    {
                        nearSymbol = true;
                    }
                    if (nearSymbol)
                    {
                        var thisNum = Int32.Parse(line.Substring(numStart, numLen));
                        Console.WriteLine(thisNum.ToString());
                        sum += thisNum;
                    }
                }
            }

            return sum.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            long sum = 0;

            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];

                for (int x = 0; x < line.Length; x++)
                {
                    if (line[x] == '*')
                    {
                        sum += GetGearScore(lines, x, y);
                    }
                }
            }

            return sum.ToString();
        }

        #region Private Methods
        private static bool IsNearSymbol(string[] lines, int x, int y)
        {
            var leftX = x - 1;
            var rightX = x + 1;
            var upY = y - 1;
            var downY = y + 1;

            if (leftX >= 0 && !char.IsNumber(lines[y][leftX]) && lines[y][leftX] != '.') //left
            {
                return true;
            }
            if (leftX >= 0 && upY >= 0 && !char.IsNumber(lines[upY][leftX]) && lines[upY][leftX] != '.') //up left
            {
                return true;
            }
            if (leftX >= 0 && downY < lines.Length && !char.IsNumber(lines[downY][leftX]) && lines[downY][leftX] != '.') //down left
            {
                return true;
            }
            if (upY >= 0 && !char.IsNumber(lines[upY][x]) && lines[upY][x] != '.') //up
            {
                return true;
            }
            if (downY < lines.Length && !char.IsNumber(lines[downY][x]) && lines[downY][x] != '.') //down
            {
                return true;
            }
            if (rightX < lines[0].Length && !char.IsNumber(lines[y][rightX]) && lines[y][rightX] != '.') //right
            {
                return true;
            }
            if (rightX < lines[0].Length && upY >= 0 && !char.IsNumber(lines[upY][rightX]) && lines[upY][rightX] != '.') //up right
            {
                return true;
            }
            if (rightX < lines[0].Length && downY < lines.Length && !char.IsNumber(lines[downY][rightX]) && lines[downY][rightX] != '.') //down right
            {
                return true;
            }
            return false;
        }

        private static long GetGearScore(string[] lines, int x, int y)
        {
            var leftX = x - 1;
            var rightX = x + 1;
            var upY = y - 1;
            var downY = y + 1;

            var touchingNums = 0;

            bool upLeft, up, upRight;
            upLeft = up = upRight = false;

            bool downLeft, down, downRight;
            downLeft = down = downRight = false;

            if (leftX >= 0 && char.IsNumber(lines[y][leftX])) //left
            {
                touchingNums++;
            }
            if (leftX >= 0 && upY >= 0 && char.IsNumber(lines[upY][leftX])) //up left
            {
                upLeft = true;
            }
            if (leftX >= 0 && downY < lines.Length && char.IsNumber(lines[downY][leftX])) //down left
            {
                downLeft = true;
            }
            if (upY >= 0 && char.IsNumber(lines[upY][x])) //up
            {
                up = true;
            }
            if (downY < lines.Length && char.IsNumber(lines[downY][x])) //down
            {
                down = true;
            }
            if (rightX < lines[0].Length && char.IsNumber(lines[y][rightX])) //right
            {
                touchingNums++;
            }
            if (rightX < lines[0].Length && upY >= 0 && char.IsNumber(lines[upY][rightX])) //up right
            {
                upRight = true;
            }
            if (rightX < lines[0].Length && downY < lines.Length && char.IsNumber(lines[downY][rightX])) //down right
            {
                downRight = true;
            }

            //calculate top row
            if (upLeft && !char.IsNumber(lines[upY][x]) && upRight)
            {
                touchingNums += 2;
            }
            else if (!upLeft && !up && !upRight)
            {
                touchingNums += 0; //unneeded, but useful for the else case
            }
            else
            {
                touchingNums += 1;
            }

            //calculate bottom row 
            if (downLeft && !char.IsNumber(lines[downY][x]) && downRight)
            {
                touchingNums += 2;
            }
            else if (!downLeft && !down && !downRight)
            {
                touchingNums += 0; //unneeded, but useful for the else case
            }
            else
            {
                touchingNums += 1;
            }

            if (touchingNums == 2)
            {
                return CalculateGearScore(lines, x, y);
            }
            else
            {
                return 0;
            }
        }

        private static long CalculateGearScore(string[] lines, int x, int y)
        {
            var leftX = x - 1;
            var rightX = x + 1;
            var upY = y - 1;
            var downY = y + 1;

            var touchingNums = 0;

            bool upLeft, up, upRight;
            upLeft = up = upRight = false;

            bool downLeft, down, downRight;
            downLeft = down = downRight = false;

            var nums = new List<long>();

            if (leftX >= 0 && char.IsNumber(lines[y][leftX])) //left
            {
                nums.Add(GetLeftNumber(lines, x, y));
            }
            if (leftX >= 0 && upY >= 0 && char.IsNumber(lines[upY][leftX])) //up left
            {
                upLeft = true;
            }
            if (leftX >= 0 && downY < lines.Length && char.IsNumber(lines[downY][leftX])) //down left
            {
                downLeft = true;
            }
            if (upY >= 0 && char.IsNumber(lines[upY][x])) //up
            {
                up = true;
            }
            if (downY < lines.Length && char.IsNumber(lines[downY][x])) //down
            {
                down = true;
            }
            if (rightX < lines[0].Length && char.IsNumber(lines[y][rightX])) //right
            {
                nums.Add(GetRightNumber(lines, x, y));
            }
            if (rightX < lines[0].Length && upY >= 0 && char.IsNumber(lines[upY][rightX])) //up right
            {
                upRight = true;
            }
            if (rightX < lines[0].Length && downY < lines.Length && char.IsNumber(lines[downY][rightX])) //down right
            {
                downRight = true;
            }

            //calculate top row
            if (upLeft && !char.IsNumber(lines[upY][x]) && upRight)
            {
                nums.Add(GetUpperLeftNumber(lines, x, y));
                nums.Add(GetUpperRightNumber(lines, x, y));
            }
            else if (!upLeft && !up && !upRight)
            {
                touchingNums += 0; //unneeded, but useful for the else case
            }
            else
            {
                nums.Add(GetUpNumber(lines, x, y));
            }

            //calculate bottom row 
            if (downLeft && !char.IsNumber(lines[downY][x]) && downRight)
            {
                nums.Add(GetDownRightNumber(lines, x, y));
                nums.Add(GetDownLeftNumber(lines, x, y));
            }
            else if (!downLeft && !down && !downRight)
            {
                touchingNums += 0; //unneeded, but useful for the else case
            }
            else
            {
                nums.Add(GetDownNumber(lines, x, y));
            }

            return nums.Aggregate((a, x) => a * x);
        }

        private static long GetLeftNumber(string[] lines, int x, int y)
        {
            var trackerX = x - 1;
            var numStart = x;
            var len = 0;

            while (trackerX > 0 && char.IsNumber(lines[y][trackerX]))
            {
                numStart = trackerX;
                len++;
                trackerX--;
            }

            return long.Parse(lines[y].Substring(numStart, len));
        }

        private static long GetRightNumber(string[] lines, int x, int y)
        {
            var trackerX = x + 1;
            var numStart = trackerX;
            var len = 0;

            while (trackerX < lines[0].Length && char.IsNumber(lines[y][trackerX]))
            {
                len++;
                trackerX++;
            }

            return long.Parse(lines[y].Substring(numStart, len));
        }

        private static long GetUpperLeftNumber(string[] lines, int x, int y)
        {
            var trackerX = x - 1;
            var upY = y - 1;
            var numStart = x;
            var len = 0;

            while (trackerX > 0 && char.IsNumber(lines[upY][trackerX]))
            {
                numStart = trackerX;
                len++;
                trackerX--;
            }

            return long.Parse(lines[upY].Substring(numStart, len));
        }

        private static long GetDownLeftNumber(string[] lines, int x, int y)
        {
            var trackerX = x - 1;
            var downY = y + 1;
            var numStart = x;
            var len = 0;

            while (trackerX > 0 && char.IsNumber(lines[downY][trackerX]))
            {
                numStart = trackerX;
                len++;
                trackerX--;
            }

            return long.Parse(lines[downY].Substring(numStart, len));
        }

        private static long GetUpperRightNumber(string[] lines, int x, int y)
        {
            var trackerX = x + 1;
            var upY = y - 1;
            var numStart = trackerX;
            var len = 0;

            while (trackerX < lines[0].Length && char.IsNumber(lines[upY][trackerX]))
            {
                len++;
                trackerX++;
            }

            return long.Parse(lines[upY].Substring(numStart, len));
        }

        private static long GetDownRightNumber(string[] lines, int x, int y)
        {
            var trackerX = x + 1;
            var downY = y + 1;
            var numStart = trackerX;
            var len = 0;

            while (trackerX < lines[0].Length && char.IsNumber(lines[downY][trackerX]))
            {
                len++;
                trackerX++;
            }

            return long.Parse(lines[downY].Substring(numStart, len));
        }

        private static long GetUpNumber(string[] lines, int x, int y)
        {
            var upY = y - 1;
            var startX = x - 1;
            if (startX < 0 || !char.IsNumber(lines[upY][startX])){
                startX++;
                if (!char.IsNumber(lines[upY][startX]))
                {
                    startX++;
                }
            }
            var trackerX = startX;
            var numStart = x;
            var len = 0;

            while (trackerX >= 0 && char.IsNumber(lines[upY][trackerX]))
            {
                numStart = trackerX;
                len++;
                trackerX--;
            }
            trackerX = startX + 1;
            while (trackerX < lines[0].Length && char.IsNumber(lines[upY][trackerX])){
                len++;
                trackerX++;
            }

            return long.Parse(lines[upY].Substring(numStart, len));

        }

        private static long GetDownNumber(string[] lines, int x, int y)
        {
            var downY = y + 1;
            var startX = x - 1;
            if (startX < 0 || !char.IsNumber(lines[downY][startX]))
            {
                startX++;
                if (!char.IsNumber(lines[downY][startX]))
                {
                    startX++;
                }
            }
            var trackerX = startX;
            var numStart = x;
            var len = 0;

            while (trackerX >= 0 && char.IsNumber(lines[downY][trackerX]))
            {
                numStart = trackerX;
                len++;
                trackerX--;
            }
            trackerX = startX + 1;
            while (trackerX < lines[0].Length && char.IsNumber(lines[downY][trackerX])){
                len++;
                trackerX++;
            }

            var numStr = lines[downY].Substring(numStart, len);
            return long.Parse(numStr);
        }
        #endregion
    }
}
