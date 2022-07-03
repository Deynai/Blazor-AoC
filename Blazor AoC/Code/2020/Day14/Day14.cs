using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Numerics;
using System.Threading;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(14, "Docking Data")]
    public class Day14 : Solution
    {
        private string inputString = string.Empty;
        private List<string> input;

        private Regex mask_regex = new Regex("[10X]*$");
        private Regex mem_regex = new Regex("\\d+");

        private class Mask
        {
            // the mask will have a 1's component and an X's component
            public long ones;
            public long xs;
            public List<long> xs_list = new List<long>();

            public Mask(string mask)
            {
                long power = 1;
                for (int i = 0; i < mask.Length; i++)
                {
                    if (mask[mask.Length - i - 1].Equals('1')) { ones += power; }
                    if (mask[mask.Length - i - 1].Equals('X')) { xs += power; xs_list.Add(power); }
                    power *= 2;
                }
            }
        }

        public Day14(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            Mask mask = new Mask(input[0]);
            Dictionary<long, long> memory = new Dictionary<long, long>();

            foreach(string line in input.Skip(1))
            {
                switch (line[1])
                {
                    case 'a': mask = new Mask(mask_regex.Match(line).Value);
                        break;
                    case 'e':
                        MatchCollection mem_matches = mem_regex.Matches(line);
                        (long address, long value) mem_numbers = (long.Parse(mem_matches[0].Value), long.Parse(mem_matches[1].Value));
                        memory.Remove(mem_numbers.address); // overwrite any existing values
                        memory.Add(mem_numbers.address, ApplyMask1(mask, mem_numbers.value));
                        break;
                    default:
                        break;
                }
            }

            return memory.Values.Sum().ToString();
        }

        private long ApplyMask1(Mask mask, long value)
        {
            value = value & mask.xs; // bitwise AND the value and the X's (as 1's)
            value = value | mask.ones; // bitwise "OR" the value and the ones - the same as adding both
            return value;
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            Mask mask = new Mask(input[0]);
            Dictionary<long, long> memory = new Dictionary<long, long>();

            foreach (string line in input.Skip(1))
            {
                switch (line[1])
                {
                    case 'a':
                        mask = new Mask(mask_regex.Match(line).Value);
                        break;
                    case 'e':
                        MatchCollection mem_matches = mem_regex.Matches(line);
                        (long address, long value) mem_numbers = (long.Parse(mem_matches[0].Value), long.Parse(mem_matches[1].Value));
                        WriteToMemory(memory, mask.xs_list, ApplyMask2(mask, mem_numbers.address), mem_numbers.value);
                        break;
                    default:
                        break;
                }
            }

            return memory.Values.Sum().ToString();
        }
        private long ApplyMask2(Mask mask, long address)
        {
            address = address | mask.ones;
            address = address & ~mask.xs;
            return address;
        }

        private void WriteToMemory(Dictionary<long, long> memory, List<long> xs_list, long base_address, long value)
        {
            List<long> addresses = new List<long>();
            addresses.Add(base_address);

            foreach(long x_pow in xs_list)
            {
                List<long> copyAddresses = new List<long>(addresses);
                foreach(long address in copyAddresses)
                {
                    addresses.Add(x_pow + address);
                };
            }

            foreach(long address in addresses)
            {
                memory.Remove(address);
                memory.Add(address, value);
            }
        }

        private void ParseInput()
        {
            input = inputString.Replace("\r", "").Split("\n").ToList();
        }
    }
}
