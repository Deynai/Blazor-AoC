using AoC.Code.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 5, "Binary Boarding")]
    public class Day05 : Solution
    {
        private string inputString = string.Empty;
        private List<int> passes;

        public Day05(string inputBox)
        {
            inputString = inputBox.Replace("\r", string.Empty);
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            ParseInput();

            return passes.Max().ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            for(int i = 0; i < passes.Count-1; i++)
            {
                if(!(passes[i+1] - passes[i]).Equals(1))
                {
                    return (passes[i] + 1).ToString();
                }
            }

            return "No Empty Seat";
        }

        private void ParseInput()
        {
            passes = inputString.Split(new string[] { "\n" }, StringSplitOptions.None)
                                .Select(b => Convert.ToInt32(b.Replace('B', '1').Replace('R', '1').Replace('F', '0').Replace('L', '0'), 2))
                                .OrderBy(x => x)
                                .ToList();
        }
    }
}