using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(4, "Passport Processing")]
    public class Day4 : Solution
    {
        private string inputString = string.Empty;

        private List<Dictionary<string, string>> passports = new List<Dictionary<string, string>>();
        
        public Day4(string inputBox)
        {
            inputString = inputBox.Replace("\r", string.Empty);
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            ParseInput();

            return passports.FindAll(pp => (pp.Count == 8) || (!pp.ContainsKey("cid") && pp.Count == 7))
                            .Count.ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            Regex hcl_regex = new Regex("^#[0-9a-f]{6}$"); // # followed by exactly 6 of digits 0-9 and letters a-f
            Regex pid_regex = new Regex("^[0-9]{9}$"); // exactly 9 digits of 0-9
            Regex ecl_regex = new Regex("^amb$|^blu$|^brn$|^gry$|^grn$|^hzl$|^oth$"); // exactly a 3 letter string equal to one of 7 options
            Regex hgt_cm_regex = new Regex("^[0-9]{3}cm$"); // exactly 3 digits 0-9 followed by letters "cm"
            Regex hgt_in_regex = new Regex("^[0-9]{2}in$"); // exactly 2 digits 0-9 followed by letters "in"

            return passports.FindAll(pp => (pp.Count == 8) || (!pp.ContainsKey("cid") && pp.Count == 7)) // cid
                            .FindAll(pp => int.Parse(pp["byr"]) >= 1920 && int.Parse(pp["byr"]) <= 2002) // byr
                            .FindAll(pp => int.Parse(pp["iyr"]) >= 2010 && int.Parse(pp["iyr"]) <= 2020) // iyr 
                            .FindAll(pp => int.Parse(pp["eyr"]) >= 2020 && int.Parse(pp["eyr"]) <= 2030) // eyr
                            .FindAll(pp => hcl_regex.IsMatch(pp["hcl"])) // hcl
                            .FindAll(pp => pid_regex.IsMatch(pp["pid"])) // pid
                            .FindAll(pp => ecl_regex.IsMatch(pp["ecl"])) // ecl
                            .FindAll(pp =>
                            {
                                int hgt = int.Parse(pp["hgt"].Substring(0, pp["hgt"].Length - 2));

                                return ((hgt_cm_regex.IsMatch(pp["hgt"]) && hgt >= 150 && hgt <= 193) ||
                                       (hgt_in_regex.IsMatch(pp["hgt"]) && hgt >= 59 && hgt <= 76)); // hgt
                            })
                            .Count.ToString();
        }

        private void ParseInput()
        {
            passports = inputString.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(p =>
                               p.Split(new string[] { " ", "\n" }, StringSplitOptions.None)
                                .Select(f => f.Split(':'))
                                .ToDictionary(d => d[0], d => d[1])
                           )
                           .ToList();
        }
    }
}