using AoC.Code.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 17, "Conway Cubes")]
    public class Day17 : Solution
    {
        private string inputString = string.Empty;

        // We're going to keep a hashset of positions that are "active".
        // Each step, we iterate through the hashset of active positions and store adjacent positions in a new dictionary along with a counter
        // Then we go through every position in the dictionary and construct a new hashset of active cubes based on the game of life rules

        // Let's stick to 4 dimensions here
        HashSet<(int x, int y, int z, int w)> active_cubes;
        Dictionary<(int, int, int, int), int> position_values = new Dictionary<(int, int, int, int), int>();

        public Day17(string inputBox)
        { 
            inputString = inputBox;
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            ParseInput();
            int cycles = 6;

            for(int i = 0; i < cycles; i++)
            {
                SetAdjacentCountsPart1();
                SetNextCubes();
            }

            return CountActiveCubes().ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            ParseInput();
            int cycles = 6;

            for (int i = 0; i < cycles; i++)
            {
                SetAdjacentCountsPart2();
                SetNextCubes();
            }

            return CountActiveCubes().ToString();
        }

        private void SetNextCubes()
        {
            HashSet<(int, int, int, int)> next_active_cubes = new HashSet<(int, int, int, int)>();

            foreach (KeyValuePair<(int x, int y, int z, int w), int> pos in position_values)
            {
                if(pos.Value.Equals(3) || (active_cubes.Contains(pos.Key) && pos.Value.Equals(2)))
                {
                    next_active_cubes.Add(pos.Key);
                }
            }

            active_cubes = next_active_cubes;
        }

        private int CountActiveCubes()
        {
            int total_sum = 0;
            foreach((int x, int y, int z, int w) pos in active_cubes)
            {
                int count = 1;
                // don't forget symmetries
                if (!pos.z.Equals(0)) { count *= 2; }
                if (!pos.w.Equals(0)) { count *= 2; }
                total_sum += count;
            }
            return total_sum;
        }

        private void SetAdjacentCountsPart1()
        {
            position_values.Clear();
            int[] delta = new int[3] { -1, 0, 1 };

            foreach (int i in delta)
            {
                foreach (int j in delta)
                {
                    foreach (int k in delta)
                    {
                        if ((i, j, k).Equals((0, 0, 0))){ continue; }
                        foreach ((int x, int y, int z, int w) pos in active_cubes)
                        {
                            // there is symmetry in the z = 0 plane, so we need only keep track of one side
                            if ((pos.z + k) < 0) { continue; }
                            (int x, int y, int z, int w) newPos = (pos.x + i, pos.y + j, pos.z + k, pos.w);

                            // make sure we count correctly for z = 0 due to symmetries
                            // if projecting an adjacency from z = 1 to z = 0, then there's one from z = -1 too
                            // projecting z = 0 to z = 0 doesn't need to be counted twice though
                            int count = 1;
                            if(newPos.z.Equals(0) && !pos.z.Equals(0)) { count *= 2; }

                            AddAdjacentCount(newPos, count);
                        }
                    }
                }
            }
        }

        private void SetAdjacentCountsPart2()
        {
            position_values.Clear();
            int[] delta = new int[3] { -1, 0, 1 };

            foreach(int i in delta)
            {
                foreach (int j in delta)
                {
                    foreach (int k in delta)
                    {
                        foreach (int l in delta)
                        {
                            if ((i, j, k, l).Equals((0, 0, 0, 0))) { continue; }
                            foreach ((int x, int y, int z, int w) pos in active_cubes)
                            {
                                // we also have symmetry in the w = 0 plane similar to part 1, requires ~1/4th of the time!
                                if ((pos.z + k) < 0) { continue; }
                                if ((pos.w + l) < 0) { continue; }
                                (int x, int y, int z, int w) newPos = (pos.x + i, pos.y + j, pos.z + k, pos.w + l);

                                // make sure we count correctly for both z = 0 and w = 0 now
                                int count = 1;
                                if (newPos.z.Equals(0) && !pos.z.Equals(0)) { count *= 2; }
                                if (newPos.w.Equals(0) && !pos.w.Equals(0)) { count *= 2; }
                                AddAdjacentCount(newPos, count);

                                // aside: there's also another symmetry in z = w and z = -w
                                // accounting for this would speed up the calculation but requires more complex rules for adjacency counting
                                // e.g Take z < w. Then z=2, w=3 would have adjacencies from z=3, w=2. However, our only copy is z=2, w=3 and it would need to be projected to itself
                            }
                        }
                    }
                }
            }
        }

        private void AddAdjacentCount((int, int, int, int) pos, int count)
        {
            if (position_values.ContainsKey(pos))
            {
                position_values[pos] += count;
            }
            else
            {
                position_values.Add(pos, count);
            }
        }
             
        private void ParseInput()
        {
            active_cubes = new HashSet<(int x, int y, int z, int w)>();

            string[] input = inputString.Replace("\r", "").Split("\n").ToArray();
            for(int i = 0; i < input.Length; i++)
            {
                for(int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j].Equals('#'))
                    {
                        active_cubes.Add((j, -i, 0, 0));
                    }
                }
            }
        }
    }
}
