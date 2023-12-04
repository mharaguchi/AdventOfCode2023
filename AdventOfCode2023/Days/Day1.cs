using AdventOfCode2023.Utils;

namespace AdventOfCode2023.Days
{
    public static class Day1
    {
        public static string _sampleInput = @"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";

        public static string _sampleInputPart2 = @"two1nine
eighttwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";

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
                    return RunPart2(_sampleInputPart2);
                }
                return RunPart2(input);
            }
        }

        internal static string RunPart1(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var total = 0;

            foreach(var line in lines)
            {
                var firstDigit = "";
                var lastDigit = "";
                for(int i = 0; i < line.Length; i++)
                {
                    var thisChar = line[i];
                    if (thisChar >= '0' && thisChar <= '9')
                    {
                        firstDigit = thisChar.ToString();
                        break;
                    }
                }
                for (int j = line.Length - 1; j >= 0; j--)
                {
                    var thisChar = line[j];
                    if (thisChar >= '0' && thisChar <= '9')
                    {
                        lastDigit = thisChar.ToString();
                        break;
                    }
                }
                var calibrationValue = firstDigit + lastDigit;
                Console.WriteLine(calibrationValue);

                total += Int32.Parse(calibrationValue);
            }

            return total.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            var total = 0;

            foreach (var line in lines)
            {
                var firstDigit = "";
                var lastDigit = "";

                for (int i = 0; i < line.Length; i++)
                {
                    var thisChar = line[i];
                    if (thisChar >= '0' && thisChar <= '9')
                    {
                        firstDigit = thisChar.ToString();
                        break;
                    }

                    var subStr = line.Substring(i, line.Length - i);
                    if (subStr.StartsWith("one"))
                    {
                        firstDigit = "1";
                        break;
                    }
                    if (subStr.StartsWith("two"))
                    {
                        firstDigit = "2";
                        break;
                    }
                    if (subStr.StartsWith("three"))
                    {
                        firstDigit = "3";
                        break;
                    }
                    if (subStr.StartsWith("four"))
                    {
                        firstDigit = "4";
                        break;
                    }
                    if (subStr.StartsWith("five"))
                    {
                        firstDigit = "5";
                        break;
                    }
                    if (subStr.StartsWith("six"))
                    {
                        firstDigit = "6";
                        break;
                    }
                    if (subStr.StartsWith("seven"))
                    {
                        firstDigit = "7";
                        break;
                    }
                    if (subStr.StartsWith("eight"))
                    {
                        firstDigit = "8";
                        break;
                    }
                    if (subStr.StartsWith("nine"))
                    {
                        firstDigit = "9";
                        break;
                    }
                }
                for (int j = line.Length - 1; j >= 0; j--)
                {
                    var thisChar = line[j];
                    if (thisChar >= '0' && thisChar <= '9')
                    {
                        lastDigit = thisChar.ToString();
                        break;
                    }

                    var subStr = line.Substring(j, line.Length - j);
                    if (subStr.StartsWith("one"))
                    {
                        lastDigit = "1";
                        break;
                    }
                    if (subStr.StartsWith("two"))
                    {
                        lastDigit = "2";
                        break;
                    }
                    if (subStr.StartsWith("three"))
                    {
                        lastDigit = "3";
                        break;
                    }
                    if (subStr.StartsWith("four"))
                    {
                        lastDigit = "4";
                        break;
                    }
                    if (subStr.StartsWith("five"))
                    {
                        lastDigit = "5";
                        break;
                    }
                    if (subStr.StartsWith("six"))
                    {
                        lastDigit = "6";
                        break;
                    }
                    if (subStr.StartsWith("seven"))
                    {
                        lastDigit = "7";
                        break;
                    }
                    if (subStr.StartsWith("eight"))
                    {
                        lastDigit = "8";
                        break;
                    }
                    if (subStr.StartsWith("nine"))
                    {
                        lastDigit = "9";
                        break;
                    }
                }
                var calibrationValue = firstDigit + lastDigit;
                Console.WriteLine(calibrationValue);

                total += Int32.Parse(calibrationValue);
            }

            return total.ToString();
        }
    }
}
