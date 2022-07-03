using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(10, "Adapter Array")]
    public class Day10 : Solution
    {
        private string inputString = string.Empty;
        private List<int> jolts;

        private Dictionary<int, int> permsMemo = new Dictionary<int, int>();

        public Day10(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            int[] differences = new int[] { 0, 0, 0, 0 };
            for(int i = 0; i < jolts.Count-1; i++)
            {
                differences[jolts[i + 1] - jolts[i]]++;
            }

            return (differences[1] * differences[3]).ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            // Since no 2-gaps exist, the set of adaptors can be broken into individual sets separated by 3-gaps
            // e.g (1,1,1,1) -> 3 -> (1,1,1) -> 3 -> 3 -> (1,1,1,1,1), etc
            // then the total number of valid permutations is equal to the product of valid permutations of each set of 1-gaps
            
            List<int> sets = new List<int>();
            int count = 0;
            for(int i = 0; i < jolts.Count-1; i++)
            {
                if((jolts[i+1] - jolts[i]).Equals(1))
                {
                    count++;
                }
                else if(!count.Equals(0))
                {
                    sets.Add(count);
                    count = 0;
                }
            }

            // but how many permutations are valid in a set of N 1-gaps?
            // inductively, P(N) = P(N-1) + P(N-2) + P(N-3) for N > 3, and P(1) = 1, P(2) = 2, P(3) = 4.

            permsMemo.Add(1, 1); permsMemo.Add(2, 2); permsMemo.Add(3, 4);

            long total_permutations = 1;
            foreach(int set_count in sets)
            {
                total_permutations *= FindPerms(set_count);
            }

            return total_permutations.ToString();
        }

        private int FindPerms(int n)
        {
            if (permsMemo.ContainsKey(n)) { return permsMemo[n]; }

            int val = (FindPerms(n - 1) + FindPerms(n - 2) + FindPerms(n - 3));
            permsMemo.Add(n, val);
            return val;
        }

        private void ParseInput()
        {
            jolts = inputString.Replace("\r", "")
                                 .Split("\n")
                                 .Select(line => int.Parse(line))
                                 .OrderBy(x => x)
                                 .ToList();

            // add the wall socket and device as 0 and max+3
            jolts.Insert(0, 0);
            jolts.Add(jolts.Last()+3);
        }
    }
}
