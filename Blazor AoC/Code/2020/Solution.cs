using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020
{
    // Base Class for AoC Solutions.
    public class Solution
    {
        public virtual string GetPart1()
        {
            return "Part 1 Error";
        }

        public virtual string GetPart2()
        {
            return "Part 2 Error";
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
