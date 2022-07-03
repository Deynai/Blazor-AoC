using System;
using System.Collections.Generic;
using System.Reflection;

namespace AoC.Code.Solutions
{
    public static class SolutionConstructor
    {
        private readonly static Dictionary<string, Type> puzzleSolutions = new();

        public static Solution GetPuzzleSolution(string year, string day, string input)
        {
            if (puzzleSolutions.Count == 0)
            {
                LocateAllPuzzleSolutions();
                if (puzzleSolutions.Count == 0)
                    return new Solution();
            }

            puzzleSolutions.TryGetValue($"{year}.{day}", out Type dayType);
            if (dayType == null) return new Solution();

            Solution soln = (Solution)Activator.CreateInstance(dayType, new[] { input });
            return soln ?? new Solution();
        }

        private static void LocateAllPuzzleSolutions()
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in asm.GetTypes())
                {
                    PuzzleAttribute puzzleAttribute = type.GetCustomAttribute<PuzzleAttribute>();
                    if (puzzleAttribute == null) continue;

                    puzzleSolutions.Add($"{puzzleAttribute.Year}.{puzzleAttribute.Day}", type);
                }
            }
        }
    }
}
