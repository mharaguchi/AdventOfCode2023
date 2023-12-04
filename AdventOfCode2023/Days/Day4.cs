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
        public static string _sampleInput = @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";

        public static Dictionary<int, int> cardInstances = new Dictionary<int, int>();

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
                var thisCardMatches = -1;
                var tokens = StringUtils.SplitInOrder(line, new string[] { ": ", " | "});
                var winningNumbers = StringUtils.SpaceSeparatedIntStringToHashSet(tokens[1]);
                var myNumbers = tokens[2].Split(" ").Where(s => s.Trim().Length > 0).Select(s => s.Trim()).Select(int.Parse);
                foreach (var myNum in myNumbers)
                {
                    if (winningNumbers.Contains(myNum))
                    {
                        thisCardMatches++;
                    }
                }
                if (thisCardMatches >= 0)
                {
                    totalScore += (int)Math.Pow(2, thisCardMatches);
                }
            }

            return totalScore.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            for(int i = 1; i <= lines.Length; i++)
            {
                cardInstances.Add(i, 1);
            }

            foreach (string line in lines)
            {
                var thisCardMatches = 0;
                var tokens = StringUtils.SplitInOrder(line, new string[] { " ", ": ", " | " });
                var cardId = Int32.Parse(tokens[1]);
                var winningNumbers = StringUtils.SpaceSeparatedIntStringToHashSet(tokens[2]);
                var myNumbers = tokens[3].Split(" ").Where(s => s.Trim().Length > 0).Select(s => s.Trim()).Select(int.Parse);
                foreach (var myNum in myNumbers)
                {
                    if (winningNumbers.Contains(myNum))
                    {
                        thisCardMatches++;
                    }
                }
                if (thisCardMatches > 0)
                {
                    for (int i = 1; i <= thisCardMatches; i++)
                    {
                        cardInstances[cardId + i] += cardInstances[cardId];
                    }
                }
            }

            return cardInstances.Sum(x => x.Value).ToString();
        }

        #region Private Methods
        #endregion
    }
}
