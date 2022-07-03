using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(3,"Toboggan Trajectory")]
    public class Day3 : Solution
    {
        private string inputString = string.Empty;
        private int[][] trees;
        private int part1;

        public Day3(string inputBox)
        {
            inputString = inputBox;
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            trees = inputString.Split('\n').Select(x => x.Trim().ToCharArray().Select(c => c.Equals('#') ? 1 : 0).ToArray()).ToArray();

            part1 = RideSled(3, 1);
            return part1.ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            return ((long)RideSled(1, 1) * part1 * RideSled(5, 1) * RideSled(7, 1) * RideSled(1, 2)).ToString();
        }

        private int RideSled(int horiz, int vert)
        {
            (int x, int y) pos = (0, 0);
            int collisions = 0;

            while (pos.y < trees.Length)
            {
                if (trees[pos.y][pos.x].Equals(1))
                {
                    collisions++;
                }

                pos.x = (pos.x + horiz) % trees[0].Length;
                pos.y += vert;
            }

            return collisions;
        }
    }
}
