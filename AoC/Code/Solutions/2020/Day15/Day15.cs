using AoC.Code.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 15, "Rambunctious Recitation")]
    public class Day15 : Solution
    {
        private readonly uint PART1_COUNT = 2020;
        private readonly uint PART2_COUNT = 30_000_000;
        private readonly uint PARTITION = 100_000;

        private string inputString = string.Empty;
        private uint[] history;
        private uint count = 1;
        private uint num;

        public Day15(string inputBox)
        {
            inputString = inputBox;
            history = new uint[PART2_COUNT];
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            uint[] start_sequence = inputString.Split(",").Select(n => uint.Parse(n)).ToArray();
            num = start_sequence.Last();

            for (int i = 0; i < start_sequence.Length - 1; i++)
            {
                history[start_sequence[i]] = count;
                count++;
            }

            for (uint i = count; i < PART1_COUNT; i++)
            {
                GetNextTerm(i);
            }

            count = PART1_COUNT;

            return num.ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            while (count < PART2_COUNT)
            {
                uint amountRemaining = PART2_COUNT - count;
                uint nextBatchAmount = amountRemaining < PARTITION ? amountRemaining : PARTITION;
                GetNextXTerms(count, nextBatchAmount);
                count += nextBatchAmount;
                await Task.Delay(1, cancellationToken);
            }

            count = PART2_COUNT;

            return num.ToString();
        }

        private void GetNextXTerms(uint currCount, uint amount)
        {
            for (uint i = currCount; i < currCount + amount; i++)
            {
                uint next = history[num].Equals(0) ? 0 : i - history[num];
                history[num] = i;
                num = next;
            }
        }

        private void GetNextTerm(uint currCount)
        {
            uint next = history[num].Equals(0) ? 0 : currCount - history[num];
            history[num] = currCount;
            num = next;
        }
    }
}
