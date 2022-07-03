using AoC.Code.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 6, "Custom Customs")]
    public class Day06 : Solution
    {
        private string inputString = string.Empty;

        public Day06(string inputBox)
        {
            inputString = inputBox.Replace("\r", string.Empty);
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            return inputString.Split(new string[] { "\n\n" }, StringSplitOptions.None)
                            .Select(lines => lines.Replace("\n", string.Empty)
                                                .Distinct()
                                                .Count())
                            .Aggregate((sum, counts) => sum + counts)
                            .ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            return inputString.Split(new string[] { "\n\n" }, StringSplitOptions.None)
                            .Select(lines => lines.Split(new string[] { "\n" }, StringSplitOptions.None))
                            .Select(str => str.Skip(1)
                                        .Aggregate(new HashSet<char>(str.First()),
                                                    (set, c) => { set.IntersectWith(c); return set; })
                                        .Count()
                                   )
                            .Aggregate((sum, counts) => sum + counts)
                            .ToString();
        }
    }
}
