using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(6, "Custom Customs")]
    public class Day6 : Solution
    {
        private string inputString = string.Empty;

        public Day6(string inputBox)
        {
            inputString = inputBox.Replace("\r", string.Empty);
        }

        public override string GetPart1()
        {
            return inputString.Split(new string[] { "\n\n" }, StringSplitOptions.None)
                            .Select(lines => lines.Replace("\n", string.Empty)
                                                .Distinct()
                                                .Count())
                            .Aggregate((sum, counts) => sum + counts)
                            .ToString();
        }

        public override string GetPart2()
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
