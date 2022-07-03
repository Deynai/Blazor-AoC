using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020
{
    // Base Class for AoC Solutions.
    public class Solution
    {
        public virtual async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            return "Not Implemented";
        }

        public virtual async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            return "Not Implemented";
        }
    }

    public class DayTitle : System.Attribute
    {
        private string name;

        public DayTitle(string name)
        {
            this.name = name;
        }
    }
}
