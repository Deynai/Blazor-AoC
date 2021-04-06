using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(5, "Binary Boarding")]
    public class Day5 : Solution
    {
        private string inputString = string.Empty;
        List<int> passes;

        public Day5(string inputBox)
        {
            inputString = inputBox;
        }

        public override string GetPart1()
        {
            ParseInput();

            return passes.Max().ToString();
        }

        public override string GetPart2()
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
            passes = inputString.Split(new string[] { "\r\n" }, StringSplitOptions.None)
                                .Select(b => Convert.ToInt32(b.Replace('B', '1').Replace('R', '1').Replace('F', '0').Replace('L', '0'), 2))
                                .OrderBy(x => x)
                                .ToList();
        }
    }
}