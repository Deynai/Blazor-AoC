using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(21, "Allergen Assessment")]
    public class Day21 : Solution
    {
        private string inputString = string.Empty;

        List<(string[] Allergen, string[] Ingredients)> input;
        List<(string Allergen, List<string> Ingredients)> ing_intersect;

        public Day21(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            // Most of this question is in the parsing of input
            // We're going to reverse the allergen association, i,e if food A contains (x,y), then rewrite as: x in A ingredients, y in A ingredients
            
            ing_intersect = input.SelectMany(i => i.Allergen.Select(A => (A, Ingredients: i.Ingredients)))
                                 .GroupBy(name => name.A)
                                 // we then intersect all the lists of ingredients that each allergen x is in
                                 // arriving at a small set of candidate allergen ingredients
                                 .Select(group => (
                                                    Allergen: group.Key,
                                                    Ingredients: group.Select(p => p.Ingredients)
                                                                      .Skip(1)
                                                                      .Aggregate(new List<string>(group.Select(q => q.Ingredients).First()),
                                                                                (h, e) => h.Intersect(e).ToList())
                                                   )
                                        )
                                 .OrderBy(allergen_ingredient => allergen_ingredient.Ingredients.Count())
                                 .ToList();

            // take all the ingredients, and select/count only the ingredients which don't appear in our candidate allergen ingredients
            int clean = input.SelectMany(food => food.Ingredients)
                             .Where(ing => !ing_intersect.SelectMany(candidate => candidate.Ingredients).Contains(ing))
                             .Count();

            return clean.ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            // now we need to cross compare each set of candidates to reduce further
            // continue reducing until a one-to-one map "single" from each allergen to ingredient exists
            // for a unique solution this should reduce nicely
            int singles = 0;
            int bp = 0;
            while (!singles.Equals(8))
            {
                singles = 0;
                for (int i = 0; i < ing_intersect.Count; i++)
                {
                    if (ing_intersect[i].Ingredients.Count.Equals(1))
                    {
                        singles++;
                        for (int j = 0; j < ing_intersect.Count; j++)
                        {
                            if (!i.Equals(j))
                            {
                                // i.e, if [i] is one ingredient, we know that's the i allergen and j cannot be that, so remove it from [j]'s
                                ing_intersect[j] = (ing_intersect[j].Allergen, ing_intersect[j].Ingredients.Except(ing_intersect[i].Ingredients).ToList());
                            }
                        }
                    }
                }
                bp++;
                if(bp > 50) { throw new Exception("Error in GetPart2: Candidate ingredients are not reducing to singles"); }
            }

            List<string> allergen_ingredients = ing_intersect.OrderBy(A => A.Allergen).Select(I => I.Ingredients.First()).ToList();
            return string.Join(",", allergen_ingredients);
        }

        private void ParseInput()
        {
            input = Regex.Replace(inputString, @"\r|\(|\)", "")
                         .Split("\n")
                         .Select(line => line.Split(" contains "))
                         .Select(g => (
                         Allergen: g[1].Split(", "),
                         Ingredients: g[0].Split(" ")
                         ))
                         .ToList();
        }
    }
}