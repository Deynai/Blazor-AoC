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
                case "Day3":
                    soln = new Code._2020.Day3(input);
                    break;
                case "Day4":
                    soln = new Code._2020.Day4(input);
                    break;
                case "Day5":
                    soln = new Code._2020.Day5(input);
                    break;
                case "Day6":
                    soln = new Code._2020.Day6(input);
                    break;
                case "Day7":
                    soln = new Code._2020.Day7(input);
                    break;
                case "Day8":
                    soln = new Code._2020.Day8(input);
                    break;
                case "Day9":
                    soln = new Code._2020.Day9(input);
                    break;
                case "Day10":
                    soln = new Code._2020.Day10(input);
                    break;
                case "Day11":
                    soln = new Code._2020.Day11(input);
                    break;
                case "Day12":
                    soln = new Code._2020.Day12(input);
                    break;
                case "Day13":
                    soln = new Code._2020.Day13(input);
                    break;
                case "Day14":
                    soln = new Code._2020.Day14(input);
                    break;
                case "Day15":
                    soln = new Code._2020.Day15(input);
                    break;
                case "Day16":
                    soln = new Code._2020.Day16(input);
                    break;
                case "Day17":
                    soln = new Code._2020.Day17(input);
                    break;
                case "Day18":
                    soln = new Code._2020.Day18(input);
                    break;
                case "Day19":
                    soln = new Code._2020.Day19(input);
                    break;
                case "Day20":
                    soln = new Code._2020.Day20(input);
                    break;
                case "Day21":
                    soln = new Code._2020.Day21(input);
                    break;
                case "Day22":
                    soln = new Code._2020.Day22(input);
                    break;
                case "Day23":
                    soln = new Code._2020.Day23(input);
                    break;
                case "Day24":
                    soln = new Code._2020.Day24(input);
                    break;
                case "Day25":
                    soln = new Code._2020.Day25(input);
                    break;
                default: soln = new Code._2020.Solution();
                    break;
            }
            return soln;
        }
    }
}
