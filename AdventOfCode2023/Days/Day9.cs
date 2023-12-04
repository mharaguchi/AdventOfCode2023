using AdventOfCode2023.Models.Day7;
using AdventOfCode2023.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Days
{
    public static class Day9
    {
        //        private static string _sampleInput = @"R 4
        //U 4
        //L 3
        //D 1
        //R 4
        //D 1
        //L 5
        //R 2";

        private static string _sampleInput = @"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";

        private static readonly HashSet<(int, int)> _tailPositions = new(); // positions the tail has visited
        private static (int, int) _headPosition = (0, 0);
        private static (int, int) _tailPosition = (0, 0);

        private static List<(int, int)> _positions = new() { (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), (0, 0), };

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

            foreach(var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { " " });
                var direction = tokens[0];
                var number = Int32.Parse(tokens[1]);

                if (!_tailPositions.Contains(_tailPosition))
                {
                    _tailPositions.Add(_tailPosition);
                }

                for(int i = 0; i < number; i++)
                {
                    MoveHead(direction);
                    MoveTail(direction);

                    if (!_tailPositions.Contains(_tailPosition))
                    {
                        _tailPositions.Add(_tailPosition);
                    }
                }
            }

            return _tailPositions.Count.ToString();
        }

        internal static string RunPart2(string input)
        {
            var lines = FileInputUtils.SplitLinesIntoStringArray(input);

            foreach (var line in lines)
            {
                var tokens = StringUtils.SplitInOrder(line, new string[] { " " });
                var direction = tokens[0];
                var number = Int32.Parse(tokens[1]);

                if (!_tailPositions.Contains(_positions[9]))
                {
                    _tailPositions.Add(_positions[9]);
                }

                for (int i = 0; i < number; i++)
                {
                    MoveHeadKnot(direction);
                    MoveTailKnots(direction);

                    if (!_tailPositions.Contains(_positions[9]))
                    {
                        _tailPositions.Add(_positions[9]);
                    }
                }
            }

            return _tailPositions.Count.ToString();
        }

        #region Private Methods
        private static void MoveHead(string direction)
        {
            switch (direction)
            {
                case "R":
                    var newLocation = (_headPosition.Item1 + 1, _headPosition.Item2);
                    _headPosition = newLocation;
                    break;
                case "D":
                    newLocation = (_headPosition.Item1, _headPosition.Item2 - 1);
                    _headPosition = newLocation;
                    break;
                case "U":
                    newLocation = (_headPosition.Item1, _headPosition.Item2 + 1);
                    _headPosition = newLocation;
                    break;
                case "L":
                    newLocation = (_headPosition.Item1 - 1, _headPosition.Item2);
                    _headPosition = newLocation;
                    break;
            }
        }

        private static void MoveHeadKnot(string direction)
        {
            var _headKnotPosition = _positions[0];

            switch (direction)
            {
                case "R":
                    var newLocation = (_headKnotPosition.Item1 + 1, _headKnotPosition.Item2);
                    _positions[0] = newLocation;
                    break;
                case "D":
                    newLocation = (_headKnotPosition.Item1, _headKnotPosition.Item2 - 1);
                    _positions[0] = newLocation;
                    break;
                case "U":
                    newLocation = (_headKnotPosition.Item1, _headKnotPosition.Item2 + 1);
                    _positions[0] = newLocation;
                    break;
                case "L":
                    newLocation = (_headKnotPosition.Item1 - 1, _headKnotPosition.Item2);
                    _positions[0] = newLocation;
                    break;
            }
        }

        private static void MoveTail(string direction)
        {
            if (IsTailConnected())
            {
                return;
            }
            switch (direction)
            {
                case "R":
                    var newLocation = (_headPosition.Item1 - 1, _headPosition.Item2); // left of head
                    _tailPosition = newLocation;
                    break;
                case "D":
                    newLocation = (_headPosition.Item1, _headPosition.Item2 + 1); //above head
                    _tailPosition = newLocation;
                    break;
                case "U":
                    newLocation = (_headPosition.Item1, _headPosition.Item2 - 1); //below head 
                    _tailPosition = newLocation;
                    break;
                case "L":
                    newLocation = (_headPosition.Item1 + 1, _headPosition.Item2); // right of head
                    _tailPosition = newLocation;
                    break;
            }
        }

        private static void MoveTailKnots(string direction)
        {
            for (int i = 1; i < 10; i++)
            {
                if (IsTailKnotConnected(i))
                {
                    continue;
                }

                _positions[i] = CalculateTailKnotPosition(i);
            }
        }

        private static (int, int) CalculateTailKnotPosition(int knotNum)
        {
            var previousKnotPosition = _positions[knotNum - 1];
            var thisKnotPosition = _positions[knotNum];

            if (previousKnotPosition.Item1 == thisKnotPosition.Item1 || previousKnotPosition.Item2 == thisKnotPosition.Item2) //if same row or col
            {
                if (previousKnotPosition.Item1 == thisKnotPosition.Item1) //same column
                {
                    if (previousKnotPosition.Item2 > thisKnotPosition.Item2)
                    {
                        return (previousKnotPosition.Item1, previousKnotPosition.Item2 - 1);
                    }
                    else
                    {
                        return (previousKnotPosition.Item1, previousKnotPosition.Item2 + 1);
                    }
                }
                else //same row
                {
                    if (previousKnotPosition.Item1 > thisKnotPosition.Item1)
                    {
                        return (previousKnotPosition.Item1 - 1, previousKnotPosition.Item2);
                    }
                    else
                    {
                        return (previousKnotPosition.Item1 + 1, previousKnotPosition.Item2);
                    }
                }
            }
            
            //else diagonal movement
            var newX = 0;
            var newY = 0;

            if (previousKnotPosition.Item1 > thisKnotPosition.Item1)
            {
                newX = thisKnotPosition.Item1 + 1;
            }
            else
            {
                newX = thisKnotPosition.Item1 - 1;
            }
            if (previousKnotPosition.Item2 > thisKnotPosition.Item2)
            {
                newY = thisKnotPosition.Item2 + 1;
            }
            else
            {
                newY = thisKnotPosition.Item2 - 1;
            }

            return (newX, newY);
        }

        private static bool IsTailConnected()
        {
            if (Math.Abs(_tailPosition.Item1 - _headPosition.Item1) > 1 || Math.Abs(_tailPosition.Item2 - _headPosition.Item2) > 1)
            {
                return false;
            }
            return true;
        }

        private static bool IsTailKnotConnected(int knotNum)
        {
            if (Math.Abs(_positions[knotNum].Item1 - _positions[knotNum - 1].Item1) > 1 || Math.Abs(_positions[knotNum].Item2 - _positions[knotNum - 1].Item2) > 1)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
