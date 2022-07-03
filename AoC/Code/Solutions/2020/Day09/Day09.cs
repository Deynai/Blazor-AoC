using AoC.Code.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 9, "Encoding Error")]
    public class Day09 : Solution
    {
        private string inputString = string.Empty;
        private List<long> input;

        private long part1number;

        public Day09(string inputBox)
        {
            inputString = inputBox.Replace("\r", string.Empty);
            ParseInput();
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            for(int i = 25; i < input.Count; i++)
            {
                List<long> range = input.Skip(i - 25)
                                        .Take(25)
                                        .OrderBy(x => x)
                                        .ToList();
                if(!FindPair(range, input[i]))
                {
                    part1number = input[i];
                    return input[i].ToString();
                }
            }

            return "Not found";
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            (int index, int length) range = FindContiguous(part1number);
            long min = input.GetRange(range.index, range.length).Min();
            long max = input.GetRange(range.index, range.length).Max();
            return (min+max).ToString();
        }

        private bool FindPair(List<long> range, long target)
        {
            int start_index = 0;
            int end_index = range.Count-1;

            while (!start_index.Equals(end_index))
            {
                long val = range[end_index] + range[start_index] - target;
                if (val.Equals(0)) { return true; }
                if(val > 0) { end_index--; }
                if(val < 0) { start_index++; }
            }

            return false;
        }

        private (int, int) FindContiguous(long target)
        {
            // we are going to sum values from a base index
            // if the sum is too high => increment the index and reduce the length, if too low => increment the range
            int base_index = 0;
            int length = 2;

            while (length > 1)
            {
                long val = -target;
                foreach(int num in input.GetRange(base_index, length))
                {
                    val += num;
                }

                if (val.Equals(0)) { return (base_index, length); }
                if (val > 0) { base_index++; length--; }
                if (val < 0) { length++; }
            }

            return (0, 1);
        }

        private void ParseInput()
        {
            input = inputString.Split("\n")
                               .Select(num => long.Parse(num))
                               .ToList();
        }
    }
}
