﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using AoC.Code.Solutions;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 7, "Handy Haversacks")]
    public class Day07 : Solution
    {
        private string inputString = string.Empty;
        private Dictionary<string, List<(string bag, int amount)>> bagsGraph;

        // Memoisation dicts to cut down on recursion steps
        private Dictionary<string, bool> containsMemo = new Dictionary<string, bool>();
        private Dictionary<string, int> childMemo = new Dictionary<string, int>();

        public Day07(string inputBox)
        {
            inputString = inputBox.Replace("\r", string.Empty);
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            ParseInput();

            return bagsGraph.Keys.Select(k => ContainsGold(k) ? 1 : 0)
                                .Aggregate((s, c) => s + c)
                                .ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            return ChildAmount("shiny gold").ToString();
        }

        private bool ContainsGold(string bag)
        {
            if (containsMemo.ContainsKey(bag))
            {
                return containsMemo[bag];
            }

            if (bagsGraph[bag].Count.Equals(0))
            {
                return false;
            }

            foreach ((string bag, int count) subBag in bagsGraph[bag])
            {
                if (subBag.bag.Equals("shiny gold"))
                {
                    containsMemo.Add(bag, true);
                    return true;
                }
                
                if (ContainsGold(subBag.bag))
                {
                    containsMemo.Add(bag, true);
                    return true;
                }
            }

            containsMemo.Add(bag, false);
            return false;
        }

        private int ChildAmount(string bag)
        {
            if (childMemo.ContainsKey(bag))
            {
                return childMemo[bag];
            }

            if (bagsGraph[bag].Count.Equals(0))
            {
                return 0;
            }

            int amount = 0;
            foreach ((string bag, int count) subBag in bagsGraph[bag])
            {
                amount += (1 + ChildAmount(subBag.bag)) * subBag.count;
            }

            childMemo.Add(bag, amount);
            return amount;
        }

        private void ParseInput()
        {
            bagsGraph = inputString.Split(new string[] { "\n" }, StringSplitOptions.None)
                                .Select(line => { return Regex.Replace(line, @" bags contain | bag(s)?(, )?(\.)?|no other bags\.", ""); }) // remove unnecessary language
                                .Select(line2 =>
                                {
                                    return Regex.Replace(line2, @"(\d) ", ",$1-") // this will format as, e.g: "posh orange,5-muted green,3-striped violet"
                                                .Split(',');
                                })
                                .Select(line3 => (
                                    mainBag: line3.First(),
                                    subBags: line3.Skip(1).Select(b => { var split = b.Split('-'); return (split[1], int.Parse(split[0])); }).ToList()
                                ))
                                .ToDictionary(d => d.mainBag, d => d.subBags);
        }
    }
}
