using AdventOfCode2023.Models.Day7;
using AdventOfCode2023.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public static class Day8
    {
        private static string _sampleInput = @"30373
25512
65332
33549
35390";

        private static Dictionary<(int, int), bool> treesTop = new Dictionary<(int, int), bool>(); //whether trees are visible from the top edge
        private static Dictionary<(int, int), bool> treesRight = new Dictionary<(int, int), bool>();
        private static Dictionary<(int, int), bool> treesBottom = new Dictionary<(int, int), bool>();
        private static Dictionary<(int, int), bool> treesLeft = new Dictionary<(int, int), bool>();

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

            ProcessTop(lines);
            ProcessBottom(lines);
            ProcessLeft(lines);
            ProcessRight(lines);

            var width = lines[0].Length;
            var height = lines.Length;
            var count = 0;

            for(int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (treesTop[(i,j)] || treesBottom[(i,j)] || treesLeft[(i,j)] || treesRight[(i, j)])
                    {
                        count++;
                    }
                }
            }

            return count.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var width = lines[0].Length;
            var height = lines.Length;
            var maxScore = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var score = CalculateScenicScore(i, j, lines);
                    //Console.WriteLine(i + " " + j + " " + score);
                    if (score > maxScore) { maxScore = score; }
                }
            }

            return maxScore.ToString();
        }

        #region Private Methods
        private static void ProcessTop(string[] lines)
        {
            var maxHeight = -1;
            var width = lines[0].Length;
            var height = lines.Length;

            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    var thisTree = Int32.Parse(lines[j][i].ToString());

                    if (maxHeight < thisTree)
                    {
                        treesTop.Add((i, j), true);
                        maxHeight = thisTree;
                    }
                    else
                    {
                        treesTop.Add((i, j), false);
                    }
                }

                maxHeight = -1;
            }
        }

        private static void ProcessBottom(string[] lines)
        {
            var maxHeight = -1;
            var width = lines[0].Length;
            var height = lines.Length;

            for (int i = 0; i < width; i++)
            {
                for (int j = height - 1; j >= 0; j--)
                {
                    var thisTree = Int32.Parse(lines[j][i].ToString());

                    if (maxHeight < thisTree)
                    {
                        treesBottom.Add((i, j), true);
                        maxHeight = thisTree;
                    }
                    else
                    {
                        treesBottom.Add((i, j), false);
                    }
                }

                maxHeight = -1;
            }
        }

        private static void ProcessLeft(string[] lines)
        {
            var maxHeight = -1;
            var width = lines[0].Length;
            var height = lines.Length;

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    var thisTree = Int32.Parse(lines[j][i].ToString());

                    if (maxHeight < thisTree)
                    {
                        treesLeft.Add((i, j), true);
                        maxHeight = thisTree;
                    }
                    else
                    {
                        treesLeft.Add((i, j), false);
                    }
                }

                maxHeight = -1;
            }
        }

        private static void ProcessRight(string[] lines)
        {
            var maxHeight = -1;
            var width = lines[0].Length;
            var height = lines.Length;

            for (int j = 0; j < height; j++)
            {
                for (int i = width - 1; i >= 0; i--)
                {
                    var thisTree = Int32.Parse(lines[j][i].ToString());

                    if (maxHeight < thisTree)
                    {
                        treesRight.Add((i, j), true);
                        maxHeight = thisTree;
                    }
                    else
                    {
                        treesRight.Add((i, j), false);
                    }
                }

                maxHeight = -1;
            }
        }

        private static int CalculateScenicScore(int i, int j, string[] lines)
        {
            var left = CalculateScenicScoreLeft(i, j, lines);
            //Console.WriteLine("left");
            var right = CalculateScenicScoreRight(i, j, lines);
            //Console.WriteLine("right");
            var top = CalculateScenicScoreUp(i, j, lines);
            //Console.WriteLine("top");
            var bottom = CalculateScenicScoreDown(i, j, lines);
            //Console.WriteLine("bottom");
            //Console.WriteLine("up: " + top + " left: " + left + " down: " + bottom + " right: " + right);

            return left * right * top * bottom;
        }

        private static int CalculateScenicScoreLeft(int i, int j, string[] lines)
        {
            var x = i - 1;
            var y = j;

            if (x < 0)
            {
                return 0; //left edge
            }

            var compareTree = Int32.Parse(lines[j][i].ToString());
            var thisTree = Int32.Parse(lines[y][x].ToString());
            var count = 0;

            while (x >= 0 && thisTree < compareTree)
            {
                count++;
                x--;
                if (x >= 0)
                {
                    thisTree = Int32.Parse(lines[y][x].ToString());
                }
            }
            if (thisTree >= compareTree)
            {
                return count + 1;
            }

            return count;
        }

        private static int CalculateScenicScoreRight(int i, int j, string[] lines)
        {
            var width = lines[0].Length;
            var x = i + 1;
            var y = j;

            if (x == width)
            {
                return 0; // right edge
            }

            var compareTree = Int32.Parse(lines[j][i].ToString());
            var thisTree = Int32.Parse(lines[y][x].ToString());
            var count = 0;

            while (x < width && thisTree < compareTree)
            {
                count++;
                x++;
                if (x < width)
                {
                    thisTree = Int32.Parse(lines[y][x].ToString());
                }
            }
            if (thisTree >= compareTree)
            {
                return count + 1;
            }

            return count;
        }

        private static int CalculateScenicScoreUp(int i, int j, string[] lines)
        {
            var x = i;
            var y = j - 1;

            if (y < 0)
            {
                return 0; //top edge
            }

            var compareTree = Int32.Parse(lines[j][i].ToString());
            var thisTree = Int32.Parse(lines[y][x].ToString());
            var count = 0;

            while (y >= 0 && thisTree < compareTree)
            {
                count++;
                y--;
                if (y >= 0)
                {
                    thisTree = Int32.Parse(lines[y][x].ToString());
                }
            }
            if (thisTree >= compareTree)
            {
                return count + 1;
            }

            return count;
        }

        private static int CalculateScenicScoreDown(int i, int j, string[] lines)
        {
            var height = lines.Length;
            var x = i;
            var y = j + 1;

            if (y == height) { return 0; }

            var compareTree = Int32.Parse(lines[j][i].ToString());
            var thisTree = Int32.Parse(lines[y][x].ToString());
            var count = 0;

            while (y < height && thisTree < compareTree)
            {
                count++;
                y++;
                if (y < height)
                {
                    thisTree = Int32.Parse(lines[y][x].ToString());
                }
            }

            if (thisTree >= compareTree)
            {
                return count + 1;
            }
            return count;
        }
        #endregion
    }
}
