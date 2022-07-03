using System;

namespace AoC.Code.Solutions
{
    [AttributeUsage(AttributeTargets.Class |
                    AttributeTargets.Struct)
        ]
    public class PuzzleAttribute : Attribute
    {
        public int Year { get; }
        public int Day { get; }
        public string Title { get; }

        public PuzzleAttribute(int year, int day, string title)
        {
            Year = year;
            Day = day;
            Title = title;
        }
    }
}
