using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_AoC
{
    public class SolutionConstructor
    {
        public static Code._2020.Solution SetSolution(string day, string input)
        {
            Code._2020.Solution soln;

            switch (day)
            {
                case "Day1":
                    soln = new Code._2020.Day1(input);
                    break;
                case "Day2":
                    soln = new Code._2020.Day2(input);
                    break;
                //case "Day3":
                //    soln = new Day3();
                //    break;
                //case "Day4":
                //    soln = new Day4();
                //    break;
                default: soln = new Code._2020.Solution();
                    break;
            }

            return soln;
        }
    }
}
