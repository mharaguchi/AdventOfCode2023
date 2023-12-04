using AdventOfCode2023.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public static class Day6
    {
        public static string _sampleInput = @"zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";

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
            var tracker = 3;

            while (true)
            {
                var dupeFound = false;

                for (int i = tracker; i >= tracker - 2; i--)
                {
                    var thisChar = input[i];
                    for (int j = i - 1; j >= tracker - 3; j--)
                    {
                        if (thisChar == input[j])
                        {
                            dupeFound = true;
                            break;
                        }
                    }
                    if (dupeFound)
                    {
                        break;
                    }
                }
                if (dupeFound)
                {
                    tracker++;
                    continue;
                }
                return (tracker + 1).ToString(); //Account for 0-based vs 1-based
            }

            return "";
        }

        internal static string RunPart2(string input)
        {
            var tracker = 13;

            while (true)
            {
                var dupeFound = false;

                for (int i = tracker; i >= tracker - 12; i--)
                {
                    var thisChar = input[i];
                    for (int j = i - 1; j >= tracker - 13; j--)
                    {
                        if (thisChar == input[j])
                        {
                            dupeFound = true;
                            break;
                        }
                    }
                    if (dupeFound)
                    {
                        break;
                    }
                }
                if (dupeFound)
                {
                    tracker++;
                    continue;
                }
                return (tracker + 1).ToString(); //Account for 0-based vs 1-based
            }

            return "";
        }

        #region Private Methods

        #endregion
    }
}
