using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(12, "Rain Risk")]
    public class Day12 : Solution
    {
        private string inputString = string.Empty;

        private List<string> instructions;
        private (int x, int y)[] deltas = { (0, 1), (1, 0), (0, -1), (-1, 0) };
        private (int x, int y) pos = (0, 0);
        private (int x, int y) wp = (10, 1);
        private int facing = 1; // (N = 0, E = 1, S = 2, W = 3)

        public Day12(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            foreach(string line in instructions)
            {
                RunOperation1(line);
            }

            return (Math.Abs(pos.x) + Math.Abs(pos.y)).ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            pos = (0, 0);

            foreach (string line in instructions)
            {
                RunOperation2(line);
            }

            return (Math.Abs(pos.x) + Math.Abs(pos.y)).ToString();
        }

        private void RunOperation1(string line)
        {
            int val = int.Parse(line.Substring(1));

            switch (line[0])
            {
                case 'N': pos.y += val;
                    break;
                case 'S': pos.y -= val;
                    break;
                case 'E': pos.x += val;
                    break;
                case 'W': pos.x -= val;
                    break;
                case 'L': facing = (4 + facing - (val / 90)) % 4;
                    break;
                case 'R': facing = (facing + (val / 90)) % 4;
                    break;
                case 'F': pos.x += deltas[facing].x * val;
                          pos.y += deltas[facing].y * val;
                    break;
                default:
                    break;
            }
        }

        private void RunOperation2(string line)
        {
            int val = int.Parse(line.Substring(1));

            switch (line[0])
            {
                case 'N':
                    wp.y += val;
                    break;
                case 'S':
                    wp.y -= val;
                    break;
                case 'E':
                    wp.x += val;
                    break;
                case 'W':
                    wp.x -= val;
                    break;
                case 'L':
                    RotateWP(4 - val / 90);
                    break;
                case 'R':
                    RotateWP(val / 90);
                    break;
                case 'F':
                    pos.x += wp.x * val;
                    pos.y += wp.y * val;
                    break;
                default:
                    break;
            }
        }

        private void RotateWP(int val)
        {
            switch (val)
            {
                case 1: wp = (wp.y, -wp.x);
                    break;
                case 2: wp = (-wp.x, -wp.y);
                    break;
                case 3: wp = (-wp.y, wp.x);
                    break;
                default:
                    break;
            }
        }

        private void ParseInput()
        {
            instructions = inputString.Replace("\r", "").Split("\n").ToList();            
        }
    }
}
