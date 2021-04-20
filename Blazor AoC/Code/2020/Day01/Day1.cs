using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(1, "Report Repair")]
    public class Day1 : Solution
    {
        string inputString = string.Empty;
        private int[] input;

        public Day1(string inputBox)
        {
            inputString = inputBox;
        }

        public override string GetPart1()
        {
            input = inputString.Split('\n').Select(str => int.Parse(str)).OrderBy(x => x).ToArray();

            (int, int) pair = FindPair(input, 2020);
            return (pair.Item1 * pair.Item2).ToString();
        }

        public override string GetPart2()
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