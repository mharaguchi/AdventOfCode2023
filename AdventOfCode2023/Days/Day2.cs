using AdventOfCode2023.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public static class Day2
    {
        public static string _sampleInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

        private static Dictionary<string, int> limits = new Dictionary<string, int>() { { "red", 12 }, { "blue", 14 }, { "green", 13 } };

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

            var total = 0;

            foreach (string line in lines)
            {
                var impossible = false;
                var lineSplits = StringUtils.SplitInOrder(line, new string[] { "Game ", ": "});
                var id = Int32.Parse(lineSplits[0]);
                var setsStr = lineSplits[1];
                var sets = setsStr.Split("; ");
                foreach(var set in sets)
                {
                    var colorSets = set.Split(", ");
                    foreach(var colorSet in colorSets)
                    {
                        var colorSetTokens = colorSet.Split(" ");
                        var number = Int32.Parse(colorSetTokens[0].ToString());
                        var color = colorSetTokens[1];

                        if (number > limits[color])
                        {
                            impossible = true;
                            continue;
                        }
                    }
                    if (impossible)
                    {
                        break;
                    }
                }
                if (!impossible)
                {
                    total += id;
                }
            }

            return total.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var total = 0;

            foreach (string line in lines)
            {
                var lineSplits = StringUtils.SplitInOrder(line, new string[] { "Game ", ": " });
                var setsStr = lineSplits[1];
                var sets = setsStr.Split("; ");

                var mins = new Dictionary<string, int>() { { "red", 0 }, { "green", 0 }, { "blue", 0 } };

                foreach (var set in sets)
                {
                    var colorSets = set.Split(", ");
                    foreach (var colorSet in colorSets)
                    {
                        var colorSetTokens = colorSet.Split(" ");
                        var number = Int32.Parse(colorSetTokens[0].ToString());
                        var color = colorSetTokens[1];

                        if (number > mins[color])
                        {
                            mins[color] = number;
                        }
                    }
                }
                total += mins["red"] * mins["green"] * mins["blue"];
            }

            return total.ToString();
        }

        #region Private Methods

        #endregion
    }
}
