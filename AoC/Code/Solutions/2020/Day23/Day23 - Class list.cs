using AoC.Code.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    //[Puzzle(2020, 23, "Crab Cups")]
    public class Day23class : Solution
    {
        private string inputString = string.Empty;

        Dictionary<int, Cup> cups;
        List<int> input;

        public Day23class(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            RunGame(0, 100);

            List<int> order = new List<int>();
            Cup iterCup = cups[1].next;

            while (!iterCup.label.Equals(1))
            {
                order.Add(iterCup.label);
                iterCup = iterCup.next;
            }

            return string.Join("", order);
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            RunGame(1_000_000, 10_000_000);
            long cup1 = cups[1].next.label;
            long cup2 = cups[1].next.next.label;
            return (cup1*cup2).ToString();
        }

        private void RunGame(int number_of_cups, int turns)
        {
            cups = new Dictionary<int, Cup>();
            InitialiseCups(input, number_of_cups);
            Cup cup = cups[input[0]];

            for(int i = 0; i < turns; i++)
            {
                MakeTurn(cup);
                cup = cup.next;
            }
        }

        private void MakeTurn(Cup cup)
        {
            Cup threeCupHead = cup.next;
            Cup threeCupTail = cup.next.next.next;

            // snip out the 3 cups
            cup.next = threeCupTail.next;

            // find next suitable selected cup

            int next_label = cup.label - 1;
            if (next_label < 1) { next_label += cups.Count; };
            while (threeCupHead.label.Equals(next_label) || threeCupHead.next.label.Equals(next_label) || threeCupHead.next.next.label.Equals(next_label))
            {
                next_label -= 1;
                if (next_label < 1) { next_label += cups.Count; };
            }
            cup = cups[next_label];

            // connect set of 3 cups to the right of the new selected cup
            threeCupTail.next = cup.next;
            cup.next = threeCupHead;
        }

        private void InitialiseCups(List<int> input, int extras)
        {
            Cup cup = new Cup(input[0]);
            cups.Add(cup.label, cup);
            
            // start with the input
            for (int i = 1; i < input.Count; i++)
            {
                Cup nextCup = new Cup(input[i]);
                cup.next = nextCup;
                cups.Add(nextCup.label, nextCup);
                cup = nextCup;
            }

            // pad extras (for part 2)
            for(int j = input.Count+1; j < extras+1; j++)
            {
                Cup nextCup = new Cup(j);
                cup.next = nextCup;
                cups.Add(nextCup.label, nextCup);
                cup = nextCup;
            }

            cup.next = cups[input[0]];
        }

        private void ParseInput()
        {
            input = inputString.Trim().ToCharArray().Select(n => int.Parse(n.ToString())).ToList();
        }

        // helper class for a doubly linked list of cups
        private class Cup
        {
            public int label;
            public Cup next;
            public Cup(int l)
            {
                label = l;
            }
        }
    }
}
