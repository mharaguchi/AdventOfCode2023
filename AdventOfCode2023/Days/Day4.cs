using AdventOfCode2023.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public static class Day4
    {
        public static string _sampleInput = @"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8";

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

            var totalScore = 0;

            foreach(string line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { "-", ",", "-" });
                var ints = tokens.Select(int.Parse).ToList();
                if (ints[0] <= ints[2] && ints[1] >= ints[3] || ints[2] <= ints[0] && ints[3] >= ints[1])
                {
                    totalScore++;
                }
            }

            return totalScore.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var totalScore = 0;

            foreach (string line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { "-", ",", "-" });
                var ints = tokens.Select(int.Parse).ToList();
                if (!(ints[1] < ints[2] || ints[3] < ints[0]))
                {
                    //Console.WriteLine(line);
                    totalScore++;
                }
            }

            return totalScore.ToString();
        }

        #region Private Methods
        private static char FindCommonItem(string first, string second)
        {
            foreach(char ch in first)
            {
                if (second.Contains(ch))
                {
                    return ch;
                }
            }
            throw new Exception();
        }

        private static int GetItemScore(char item)
        {
            if (item >= 'a' && item <= 'z')
            {
                return item - 'a' + 1;
            }
            else
            {
                return item - 'A' + 27;
            }
        }

        private static HashSet<char> GetItemSet(string items)
        {
            var set = new HashSet<char>();

            foreach(char item in items) { 
                if (!set.Contains(item))
                {
                    set.Add(item);
                }
            }

            return set;
        }
        #endregion
    }
}
