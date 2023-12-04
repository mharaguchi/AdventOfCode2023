using AdventOfCode2023.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public static class Day5
    {
        public static string _sampleInput = @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

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
            var numCols = lines[0].Length / 4 + 1; //Add 1 because the last column won't have a space after
            var reverseStacks = new List<List<char>>();
            var stacks = new List<Stack<char>>();
            var instructions = new List<List<string>>();

            for (int i = 0; i < numCols; i++)
            {
                reverseStacks.Add(new List<char>());
            }

            var firstCrate = lines[0][1];
            var colTracker = 0;
            var lineTracker = 0;

            // Read in beginning values
            SetReverseStacks(lines, numCols, reverseStacks, ref firstCrate, ref colTracker, ref lineTracker);

            // Set up non-reversed stacks
            for (int i = 0; i < numCols; i++)
            {
                reverseStacks[i].Reverse();
                stacks.Add(new Stack<char>(reverseStacks[i]));
            }

            // Parse instructions
            lineTracker = ParseInstructions(lines, instructions, lineTracker);

            // Run instructions
            foreach (var instruction in instructions)
            {
                var numCrates = Int32.Parse(instruction[0]);
                var sourceStackNum = Int32.Parse(instruction[1]) - 1; // 0-based lists, 1-based column numbers in instructions
                var targetStackNum = Int32.Parse(instruction[2]) - 1;

                // Move crates
                for (int i = 0; i < numCrates; i++)
                {
                    char popped = stacks[sourceStackNum].Pop();
                    stacks[targetStackNum].Push(popped);
                }
            }

            // Create answer
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numCols; i++)
            {
                if (stacks[i].Count > 0)
                {
                    sb.Append(stacks[i].Pop());
                }
            }

            return sb.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            var numCols = lines[0].Length / 4 + 1; //Add 1 because the last column won't have a space after
            var reverseStacks = new List<List<char>>();
            var stacks = new List<Stack<char>>();
            var instructions = new List<List<string>>();

            for (int i = 0; i < numCols; i++)
            {
                reverseStacks.Add(new List<char>());
            }

            var firstCrate = lines[0][1];
            var colTracker = 0;
            var lineTracker = 0;

            // Read in beginning values
            SetReverseStacks(lines, numCols, reverseStacks, ref firstCrate, ref colTracker, ref lineTracker);

            // Set up non-reversed stacks
            for (int i = 0; i < numCols; i++)
            {
                reverseStacks[i].Reverse();
                stacks.Add(new Stack<char>(reverseStacks[i]));
            }

            // Parse instructions
            lineTracker = ParseInstructions(lines, instructions, lineTracker);

            // Run instructions
            foreach (var instruction in instructions)
            {
                var numCrates = Int32.Parse(instruction[0]);
                var sourceStackNum = Int32.Parse(instruction[1]) - 1; // 0-based lists, 1-based column numbers in instructions
                var targetStackNum = Int32.Parse(instruction[2]) - 1;
                var popped = new List<char>();

                // Pop crates
                for (int i = 0; i < numCrates; i++)
                {
                    popped.Add(stacks[sourceStackNum].Pop());
                }

                // Reverse order
                popped.Reverse();

                // Push crates
                foreach(var crate in popped)
                {
                    stacks[targetStackNum].Push(crate);
                }
            }

            // Create answer
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numCols; i++)
            {
                if (stacks[i].Count > 0)
                {
                    sb.Append(stacks[i].Pop());
                }
            }

            return sb.ToString();
        }

        #region Private Methods
        private static void SetReverseStacks(string[] lines, int numCols, List<List<char>> reverseStacks, ref char firstCrate, ref int colTracker, ref int lineTracker)
        {
            while (firstCrate != '1')
            {
                while (colTracker < numCols)
                {
                    var thisCrate = lines[lineTracker][1 + colTracker * 4];
                    if (thisCrate != ' ')
                    {
                        reverseStacks[colTracker].Add(thisCrate);
                    }
                    colTracker++;
                }
                colTracker = 0;
                lineTracker++;
                firstCrate = lines[lineTracker][1];
            }
        }

        private static int ParseInstructions(string[] lines, List<List<string>> instructions, int lineTracker)
        {
            lineTracker += 1; // current line is column labels, so skip that line
            while (lineTracker < lines.Length)
            {
                var thisLine = lines[lineTracker];
                var tokens = StringUtils.SplitInOrder(thisLine, new string[] { "move ", " from ", " to " });
                instructions.Add(tokens);
                lineTracker++;
            }

            return lineTracker;
        }
        #endregion
    }
}
