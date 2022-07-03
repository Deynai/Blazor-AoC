using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 1, "Report Repair")]
    public class Day01 : Solution
    {
        string inputString = string.Empty;
        private int[] input;

        public Day01(string inputBox)
        {
            inputString = inputBox;
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            input = inputString.Split('\n').Select(str => int.Parse(str)).OrderBy(x => x).ToArray();

            (int, int) pair = FindPair(input, 2020);
            return (pair.Item1 * pair.Item2).ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            for(int i = 0; i < input.Length; i++)
            {
                (int, int) pair = FindPair(input, 2020 - input[i], i);
                if (!pair.Item1.Equals(-1))
                {
                    return (pair.Item1 * pair.Item2 * input[i]).ToString();
                }
            }

            return "Error";
        }

        public (int, int) FindPair(int[] input, int target, int exception = -1)
        {
            int lower = 0;
            int upper = input.Length - 1;

            while (!upper.Equals(lower))
            {
                int val = input[lower] + input[upper];

                if (val.Equals(target))
                {
                    return (input[lower], input[upper]);
                }

                if (val < target)
                {
                    lower++;
                    if (lower.Equals(exception))
                    {
                        lower++;
                    }
                }
                else
                {
                    upper--;
                    if (upper.Equals(exception))
                    {
                        upper--;
                    }
                }
            }

            return (-1, -1);
        }
    }
}