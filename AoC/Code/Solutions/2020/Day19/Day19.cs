using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using AoC.Code.Solutions;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 19, "Monster Messages")]
    public class Day19 : Solution
    {
        private string inputString = string.Empty;
       
        private Dictionary<string, string[]> grammar;
        private HashSet<string> messages;

        public Day19(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }
        
        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            // We're going to recursively build one big regex string with plenty of |'s for all the nested rules,
            // then let the hopefully well optimised regex parser handle matching each message
            Regex reg_parser = new Regex(BuildRegexString());
            return CountValidMessages(reg_parser).ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            // Obviously a huge fudge but perhaps what the puzzle had been intending with the sentence:
            // "Remember, you only need to handle the rules you have"
            // The Earley Parser for general case took significantly longer to compute, and .NET doesn't seem to support recursive regex anyway
            grammar["8"] = new string[]{ "42", "42 42", "42 42 42", "42 42 42 42", "42 42 42 42 42", "42 42 42 42 42 42" };
            grammar["11"] = new string[]{ "42 31", "42 42 31 31", "42 42 42 31 31 31", "42 42 42 42 31 31 31 31", "42 42 42 42 42 31 31 31 31 31" };
            Regex reg_parser = new Regex(BuildRegexString());
            return CountValidMessages(reg_parser).ToString();
        }

        private int CountValidMessages(Regex reg_parser)
        {
            int count = 0;
            foreach (string message in messages)
            {
                if (reg_parser.Match(message).Success)
                {
                    count++;
                }
            }
            return count;
        }

        private string BuildRegexString()
        {
            Regex reg_number = new Regex(@"(?<n>\d+)");
            string regex_parser = "^";
            
            foreach(string s in grammar["0"])
            {
                regex_parser += s;
            }

            Match number_match = reg_number.Match(regex_parser);
            while(number_match.Success)
            {
                regex_parser = reg_number.Replace(regex_parser, (n) => GetRegexStringForRule(n.Value));
                number_match = reg_number.Match(regex_parser);
            }

            return regex_parser.Replace(" ", "") + "$";
        }

        private string GetRegexStringForRule(string rule)
        {
            string new_reg_rule = "(?:";
            foreach(string s in grammar[rule])
            {
                new_reg_rule += s + "|";
            }
            return new_reg_rule.Trim('|') + ")";
        }

        private void ParseInput()
        {
            messages = inputString.Replace("\r", "").Split("\n\n")[1].Split("\n").ToHashSet();

            grammar = inputString.Replace("\r", "").Split("\n\n")[0].Split("\n")
                                    .Select(line =>
                                    {
                                        // we're going to format, i.e: "4: 3 8 21 | 17 5" into ("4", {"3 8 21", "17 5"}), a (string, string[]) tuple
                                        string[] subrules = line.Substring(line.IndexOf(":") + 1).Replace("\"", "").Split("|").Select(l => l.Trim()).ToArray();

                                        return (id: line.Substring(0, line.IndexOf(":")), subrules: subrules);
                                    })
                                    .ToDictionary(l => l.id, l => l.subrules);
        }
    }
}
