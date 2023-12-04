using AdventOfCode2023.Days;
using AdventOfCode2023.Utils;

namespace AdventOfCode2023
{
    public static class DayRunner
    {
        public static string GetAnswer(int day, int part, bool useSampleData)
        {
            var input = FileInputUtils.GetInput(day);
            return RunDay(input, day, part, useSampleData).ToString();
        }

        private static string RunDay(string input, int day, int part, bool useSampleData)
        {
            return day switch
            {
                1 => Day1.Run(input, part, useSampleData),
                2 => Day2.Run(input, part, useSampleData),
                3 => Day3.Run(input, part, useSampleData),
                4 => Day4.Run(input, part, useSampleData),
                5 => Day5.Run(input, part, useSampleData),
                6 => Day6.Run(input, part, useSampleData),
                7 => Day7.Run(input, part, useSampleData),
                8 => Day8.Run(input, part, useSampleData),
                9 => Day9.Run(input, part, useSampleData),
                _ => "Error selecting day",
            }; ;
        }
    }
}
