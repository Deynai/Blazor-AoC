using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020.Day1
{
    public class Day1 : Solution
    {
        string input_filename = string.Empty;

        private string part1 = string.Empty;
        private string part2 = string.Empty;

        public Day1(string path)
        {
            input_filename = path;
        }

        public override string GetPart1()
        {
            string[] input = System.IO.File.ReadAllLines(input_filename);

            int num;

            
            if(int.TryParse(input[0], out num))
            {
                part1 = AddTwenty(num).ToString();
                return part1;
            }
            else
            {
                return "Invalid Input";
            }
            
        }

        public override string GetPart2()
        {
            int num;
            if (!part1.Equals(string.Empty))
            {
                num = int.Parse(part1);
            }
            else
            {
                if(!int.TryParse(GetPart1(), out num))
                {
                    return "Invalid Input";
                }
            }

            part2 = Remainder7(num).ToString();
            return part2;
        }

        private int AddTwenty(int num)
        {
            return num + 20;
        }
        private int Remainder7(int num)
        {
            return num % 7;
        }
    }
}