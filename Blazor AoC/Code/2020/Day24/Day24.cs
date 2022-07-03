using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(24, "Lobby Layout")]
    public class Day24 : Solution
    {
        private string inputString = string.Empty;

        private (int, int)[][] directions;
        private Dictionary<(int x, int y), int> black_tiles = new Dictionary<(int x, int y), int>();
        private static (int dx, int dy)[] dir = { (1, 0), (-1, 0), (0, 1), (0, -1), (-1, 1), (1, -1) };

        public Day24(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            // sum all of the steps to get the end point of each route
            var end_points = directions.Select(line => line.Aggregate((sum, dir) => (sum.Item1 + dir.Item1, sum.Item2 + dir.Item2))).ToArray();

            // keep track of how many times we finish at a tile
            foreach((int x, int y) end in end_points)
            {
                if (black_tiles.ContainsKey(end))
                {
                    black_tiles[end] += 1;
                }
                else
                {
                    black_tiles.Add(end, 1);
                }
            }

            // the black tiles are == 1 % 2
            return black_tiles.Select(t => t.Value % 2).Aggregate((s, x) => s + x).ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            HashSet<(int x, int y)> active_tiles = black_tiles.Where(t => t.Value % 2 == 1).Select(k => k.Key).ToHashSet();
            Dictionary<(int x, int y), int> neighbours;
            int days = 100;

            for(int i = 0; i < days; i++)
            {
                neighbours = AddToNeighbours(active_tiles);
                active_tiles = CalculateActiveTiles(neighbours, active_tiles);
            }
            
            return active_tiles.Count().ToString();
        }

        private Dictionary<(int x, int y), int> AddToNeighbours(HashSet<(int x, int y)> active_tiles)
        {
            Dictionary<(int x, int y), int> neighbours = new Dictionary<(int x, int y), int>();

            // for each active tile, add each neighbour to the dictionary and/or add 1 to it.
            foreach((int x, int y) pos in active_tiles)
            {
                foreach((int dx, int dy) delta in dir)
                {
                    (int x, int y) nPos = (pos.x + delta.dx, pos.y + delta.dy);
                    if (neighbours.ContainsKey(nPos))
                    {
                        neighbours[nPos]++;
                    }
                    else
                    {
                        neighbours.Add(nPos, 1);
                    }
                }
            }

            return neighbours;
        }

        private HashSet<(int x, int y)> CalculateActiveTiles(Dictionary<(int x, int y), int> neighbours, HashSet<(int x, int y)> active_tiles)
        {
            HashSet<(int x, int y)> next_tiles = new HashSet<(int x, int y)>();

            // for each tile in neighbours decide whether it will be an active tile next round based on the rules given
            foreach(KeyValuePair<(int x, int y), int> kvp in neighbours)
            {
                if(kvp.Value.Equals(2) || (active_tiles.Contains(kvp.Key) && kvp.Value.Equals(1)))
                {
                    next_tiles.Add(kvp.Key);
                }
            }
            return next_tiles;
        }

        private (int x, int y) ConvertDirection(string dir)
        {
            switch (dir)
            {
                case "e":
                    return (1, 0);
                case "w":
                    return (-1, 0);
                case "ne":
                    return (0, 1);
                case "sw":
                    return (0, -1);
                case "nw": // z = y - x
                    return (-1, 1);
                case "se":
                    return (1, -1);
                default: // error?
                    return (0, 0);
            }
        }

        private void ParseInput()
        {
            Regex reg_dir = new Regex(@"ne|nw|se|sw|e|w");

            directions = inputString.Replace("\r", "").Trim().Split("\n")
                                    .Select(line => reg_dir.Matches(line).Select(m => ConvertDirection(m.Value)).ToArray())
                                    .ToArray();
        }
    }
}
