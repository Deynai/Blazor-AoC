using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;

using System.Diagnostics;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(19, "Monster Messages")]
    public class Day19Replacement : Solution
    {
        private string inputString = string.Empty;

        private Dictionary<string, string[][]> grammar;
        private HashSet<string> messages;

        private Regex reg_first_digit = new Regex("[\\d]+");
        private Regex reg_number = new Regex(@"\w+");

        public Day19Replacement(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        
        public override string GetPart1()
        {
            int sum = 0;
            foreach(string message in messages)
            {
                sum += RecurseRule("", "0", message) ? 1 : 0;
            }
            return sum.ToString();
        }

        public override string GetPart2()
        {
            string[][] rule1 = new string[2][]; rule1[0] = new string[] { "42" }; rule1[1] = new string[] { "42", "8" };
            string[][] rule2 = new string[2][]; rule2[0] = new string[] { "42", "31" }; rule2[1] = new string[] { "42", "11", "31" };
            grammar["8"] = rule1;
            grammar["11"] = rule2;

            int sum = 0;
            foreach (string message in messages)
            {
                sum += RecurseRule("", "0", message) ? 1 : 0;
            }
            return sum.ToString();
        }
        
        private bool RecurseRule(string curr_letters, string curr, string message)
        {
            Match firstdigit = reg_first_digit.Match(curr);
            //Match firstletters = reg_first_letters.Match(curr);

            if (curr_letters.Length > 0)
            {
                if (!Compare(curr_letters, message))
                {
                    return false;
                }
            }

            if (curr.Length.Equals(0))
            {
                return message.Equals(curr_letters);
            }

            else
            {
                string[][] rule = grammar[firstdigit.Value.ToString()];

                for (int i = 0; i < rule.Length; i++)
                {
                    string opt = "";
                    for (int j = 0; j < rule[i].Length; j++)
                    {
                        if (rule[i][j].Equals("a") || rule[i][j].Equals("b"))
                        {
                            curr_letters += rule[i][j];
                        }
                        else
                        {
                            opt += rule[i][j] + " ";
                        }
                    }

                    string currcopy = reg_first_digit.Replace(curr, opt.Trim(), 1).Trim();

                    if (RecurseRule(curr_letters, currcopy, message))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool Compare(string requirements, string message)
        {
            if (requirements.Length > message.Length) { return false; }
            for (int i = 0; i < requirements.Length; i++)
            {
                if (!requirements[i].Equals(message[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private void ParseInput()
        {
            Regex reg_number = new Regex(@"\w+");

            messages = inputString.Replace("\r", "").Split("\n\n")[1].Split("\n").ToHashSet();

            grammar = inputString.Replace("\r", "").Split("\n\n")[0].Split("\n")
                                    .Select(line =>
                                    {
                                        // we're going to format, i.e: "4: 3 8 21 | 17 5" into (4, {{3, 8, 21}, {17, 5}}), a (string, string[][]) tuple
                                        string[][] subrules = line.Substring(line.IndexOf(":") + 1).Replace("\"", "").Split("|")
                                                                        .Select(or => reg_number.Matches(or)
                                                                            .Select(m => m.Value).ToArray())
                                                                        .ToArray();

                                        return (id: line.Substring(0, line.IndexOf(":")), subrules: subrules);
                                    })
                                    .ToDictionary(l => l.id, l => l.subrules);
        }
    }
}
