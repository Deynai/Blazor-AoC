using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(15, "Rambunctious Recitation")]
    public class Day15 : Solution
    {
        private string inputString = string.Empty;
        private uint[] history = new uint[30_000_000];
        private uint count = 1;
        private uint num;

        public Day15(string inputBox)
        {
            inputString = inputBox;
        }

        public override string GetPart1()
        {
            uint[] start_sequence = inputString.Split(",").Select(n => uint.Parse(n)).ToArray();
            num = start_sequence.Last();

            for(int i = 0; i < start_sequence.Length-1; i++)
            {
                history[start_sequence[i]] = count;
                count++;
            }

            while(count < 2020)
            {
                GetNextTerm();
                count++;
            }

            return num.ToString();
        }

        public override string GetPart2()
        {
            while(count < 30_000_000)
            {
                GetNextTerm();
                count++;
            }

            return num.ToString();
        }

        private void GetNextTerm()
        {
            uint next = history[num].Equals(0) ? 0 : count - history[num];
            history[num] = count;
            num = next;
        }
    }
}
