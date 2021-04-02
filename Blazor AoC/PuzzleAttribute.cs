using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_AoC
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct)
        ]
    public class PuzzleAttribute : System.Attribute
    {
        public int dayid;
        public string title;

        public PuzzleAttribute(int id, string title)
        {
            dayid = id;
            this.title = title;
        }
    }
}
