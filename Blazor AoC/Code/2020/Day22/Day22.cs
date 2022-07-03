using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(22, "Crab Combat")]
    public class Day22 : Solution
    {
        private string inputString = string.Empty;

        List<int> p1; // player 1's deck
        List<int> p2; // player 2's deck

        public Day22(string inputBox)
        {
            inputString = inputBox;
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            ParseInput();

            (int, int) winner = PlayCards(p1, p2);

            return winner.Item2.ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            ParseInput();

            int winner_score = PlayRecursion(p1, p2, 0);

            return winner_score.ToString();
        }

        private (int, int) PlayCards(List<int> p1, List<int> p2)
        {
            int bp = 10000;
            while (!p1.Count.Equals(0) && !p2.Count.Equals(0))
            {
                if (bp--.Equals(0)) { throw new Exception("Error in PlayCards: Game did not end in a timely manner"); }

                // take the top card of each deck (and remove them)
                int card1 = p1[0]; int card2 = p2[0];
                p1.RemoveAt(0); p2.RemoveAt(0);

                // place the cards into the deck of whoever won
                if (card1 > card2)
                {
                    p1.Add(card1);
                    p1.Add(card2);
                }
                else
                {
                    p2.Add(card2);
                    p2.Add(card1);
                }
            }

            return p1.Count.Equals(0) ? (2, Score(p2)) : (1, Score(p1));
        }

        private int Score(List<int> deck)
        {
            int sum = 0;
            for (int i = 0; i < deck.Count; i++)
            {
                sum += (deck.Count - i) * deck[i];
            }
            return sum;
        }

        private int PlayRecursion(List<int> p1, List<int> p2, int depth)
        {
            int bp = 10000;
            HashSet<int> ht = new HashSet<int>();

            while (!p1.Count.Equals(0) && !p2.Count.Equals(0))
            {
                if (bp--.Equals(0)) { throw new Exception("Error in PlayRecursion: Game did not end in a timely manner"); }

                // if we have seen this game already in the hashset, player 1 wins immediately
                int hash = Hash(p1, p2);
                if (ht.Contains(hash)) { return 1; }
                else { ht.Add(hash); }

                // take the top cards of each deck
                int card1 = p1[0]; int card2 = p2[0];
                p1.RemoveAt(0); p2.RemoveAt(0);
                int winner = 0;

                // if we can play a sub game
                if (p1.Count >= card1 && p2.Count >= card2)
                {
                    List<int> new_p1 = p1.GetRange(0, card1);
                    List<int> new_p2 = p2.GetRange(0, card2);

                    // if p1 has the highest card, p1 must end up winning
                    if(new_p1.Max() > new_p2.Max())
                    {
                        winner = 1;
                    }
                    // if p1 doesn't have the highest card, they may still win through repetition
                    else
                    { 
                        winner = PlayRecursion(new_p1, new_p2, depth + 1);
                    }
                }
                // default to normal rules
                else
                {
                    winner = (card1 > card2) ? 1 : 2;
                }

                // add cards to the winning deck
                if (winner.Equals(1))
                {
                    p1.Add(card1);
                    p1.Add(card2);
                }
                else
                {
                    p2.Add(card2);
                    p2.Add(card1);
                }
            }

            // if finished, return the scores rather than the winner
            if (depth.Equals(0))
            {
                return p1.Count.Equals(0) ? Score(p2) : Score(p1);
            }

            return p1.Count.Equals(0) ? 2 : 1;
        }

        private int Hash(List<int> p1, List<int> p2)
        {
            int h = p1.Count;
            for (int i = 0; i < p1.Count; i++)
            {
                h = unchecked(h * 101 + p1[i]);
            }
            for (int i = 0; i < p2.Count; i++)
            {
                h = unchecked(h * 113 + p2[i]);
            }
            return h;
        }

        private void ParseInput()
        {
            var player_split = inputString.Replace("\r", "").Trim().Split("\n\n");
            p1 = player_split[0].Split("\n").Skip(1).Select(n => int.Parse(n)).ToList();
            p2 = player_split[1].Split("\n").Skip(1).Select(n => int.Parse(n)).ToList();
        }
    }
}
