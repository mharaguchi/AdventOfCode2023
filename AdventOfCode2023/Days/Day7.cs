using AdventOfCode2023.Models.Day7;
using AdventOfCode2023.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public static class Day7
    {
        private static string _sampleInput = @"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k";

        private static Dictionary<string, FilesystemNode> nodes = new Dictionary<string, FilesystemNode>();
        private static int _currentLineNum = 0;
        private static string _currentPath = "";

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
            _currentPath = "/";
            var rootNode = new FilesystemNode(null, _currentPath);
            nodes.Add(_currentPath, rootNode);

            while(_currentLineNum < lines.Length)
            {
                var currentLine = lines[_currentLineNum];
                ProcessCommand(currentLine, lines);
            }

            var sum = nodes.Select(x => x.Value.Size).Where(x => x < 100000).Sum();

            return sum.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);
            _currentPath = "/";
            var rootNode = new FilesystemNode(null, _currentPath);
            nodes.Add(_currentPath, rootNode);

            while (_currentLineNum < lines.Length)
            {
                var currentLine = lines[_currentLineNum];
                ProcessCommand(currentLine, lines);
            }

            var totalDiskSpace = 70000000;
            var currentUnused = totalDiskSpace - nodes["/"].Size;
            var additionalNeeded = 30000000 - currentUnused;
            
            var min = nodes.Select(x => x.Value.Size).Where(x => x > additionalNeeded).Min();

            return min.ToString();
        }

        #region Private Methods
        private static void ProcessCommand(string command, string[] lines)
        {
            if (command[2] == 'c')
            {
                ProcessCd(command);
            }
            else
            {
                ProcessLs(command, lines);
            }
        }

        private static void ProcessLs(string command, string[] lines)
        {
            _currentLineNum++;
            var currentNode = nodes[_currentPath];
            var thisLine = lines[_currentLineNum];
            long thisFolderSize = 0;

            while (!thisLine.StartsWith("$"))
            {
                var tokens = StringUtils.SplitInOrder(thisLine, new string[] { " " });
                if (tokens[0] == "dir")
                {
                    var childPath = _currentPath + tokens[1] + "/";
                    if (!nodes.ContainsKey(childPath))
                    {
                        nodes.Add(childPath, new FilesystemNode(nodes[_currentPath], childPath));
                    }
                    currentNode.ChildFolders.Add(childPath);
                }
                else
                {
                    thisFolderSize += long.Parse(tokens[0]);
                }
                _currentLineNum++;
                if (_currentLineNum == lines.Length)
                {
                    break;
                }
                thisLine = lines[_currentLineNum];
            }
            currentNode.Size = thisFolderSize;
            if (currentNode.ChildFolders.Count == 0)
            {
                currentNode.Parent?.UpdateSizeFromChild(_currentPath, currentNode.Size);
            }
        }

        private static void ProcessCd(string command)
        {
            var tokens = StringUtils.SplitInOrder(command, new string[] { " ", " " });
            var target = tokens[2];
            if (target == "..")
            {
                var newPath = _currentPath.Substring(0, _currentPath.LastIndexOf('/'));
                newPath = newPath.Substring(0, newPath.LastIndexOf('/') + 1);
                _currentPath = newPath;
            }
            else if (target == "/")
            {
                _currentPath = "/";
            }
            else
            {
                _currentPath += target + "/";
            }
            _currentLineNum++;
        }
        #endregion
    }
}
