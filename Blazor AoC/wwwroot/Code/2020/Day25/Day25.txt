using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(25, "Combo Breaker")]
    public class Day25 : Solution
    {
        private string inputString = string.Empty;

        int pkey1;
        int pkey2;

        public Day25(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override string GetPart1()
        {
            int modulo = 20201227;
            int value = 1;
            int subject_number = 7;
            int loop_count = pkey1 > pkey2 ? pkey1 : pkey2;

            for(int i = 1; i < loop_count; i++)
            {
                value = (value * subject_number) % modulo;

                // if value is one of our keys, we have found the loop size for one of them
                if (value.Equals(pkey1) || value.Equals(pkey2))
                {
                    // transform loop size using the other public key to find the encryption key
                    int encryption_key = value.Equals(pkey1) ? (int)BigInteger.ModPow(pkey2, i, modulo) : (int)BigInteger.ModPow(pkey1, i, modulo);
                    return encryption_key.ToString();
                }
            }

            return "No Encryption Key found";
        }

        public override string GetPart2()
        {
            return "⭐";
        }

        private void ParseInput()
        {
            var nums = inputString.Replace("\r", "").Trim().Split("\n").Select(n => int.Parse(n)).ToArray();
            pkey1 = nums[0];
            pkey2 = nums[1];
        }
    }
}
