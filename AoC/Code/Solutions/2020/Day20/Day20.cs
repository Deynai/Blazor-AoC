using AoC.Code.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 20, "Jurassic Jigsaw")]
    public class Day20 : Solution
    {
        private string inputString = string.Empty;

        private const int TD = 10; // tile dimension
        private const int PD = 12; // puzzle dimension
        private Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();

        public Day20(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            // Quite ugly but we want to go through all tiles and add both the normal and reverse edge matches
            // take pairs of tiles
            foreach (Tile startTile in tiles.Values)
            {
                foreach (Tile compareTile in tiles.Values)
                {
                    if (startTile.Equals(compareTile)) { continue; }

                    // then compare the sides
                    foreach (int s_side in startTile.intsides)
                    {
                        foreach (int c_side in compareTile.intsides)
                        {
                            if (s_side.Equals(c_side) || s_side.Equals(RevBits(c_side)))
                            {
                                startTile.AddMatch(compareTile.id, s_side);
                                startTile.AddMatch(compareTile.id, RevBits(s_side));
                            }
                        }
                    }
                }
            }

            long prod = 1;
            foreach (Tile tile in tiles.Values)
            {
                if (tile.matches.Count.Equals(4)) // note we added both the number and reversebits, so match count is *2
                {
                    prod *= tile.id;
                }
            }
            return prod.ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            // construct all of the tiles in the right places with matched edges
            Tile[,] puzzle = ConstructPuzzle();

            // merge tiles together into a single big array of ints
            int[,] intpuzzle = TilesToInt(puzzle);

            // now let's look for monsters
            int monsterCount = FindMonsters(intpuzzle);

            // number of tiles per monster = 15
            int ones = -15 * monsterCount;
            for (int i = 0; i < intpuzzle.GetLength(0); i++)
            {
                for (int j = 0; j < intpuzzle.GetLength(0); j++)
                {
                    ones += intpuzzle[i, j].Equals(1) ? 1 : 0;
                }
            }

            return ones.ToString();
        }

        private void ParseInput()
        {
            tiles = inputString.Replace("\r", "").Trim().Split("\n\n")
                                .Select(t => new Tile(t))
                                .ToDictionary(i => i.id, i => i);
        }

        private Tile[,] ConstructPuzzle()
        {
            Tile[,] puzzle = new Tile[PD, PD];

            // pick the first tile we find which has only two (four) matches, this must be a corner piece
            puzzle[0, 0] = tiles.Values.Where(t => t.matches.Count.Equals(4)).First();

            // rotate first piece until the sides which have a match are 1 and 2
            // this places and orients the first piece in the top left
            int bp = 0;
            List<int> m = puzzle[0, 0].matches.Select(p => p.Item2).ToList();
            while (!m.Contains(puzzle[0, 0].intsides[1]) || !m.Contains(puzzle[0, 0].intsides[2]))
            {
                puzzle[0, 0].RotateTile();

                bp++;
                if (bp > 5) { throw new Exception("Error in ConstructPuzzle: Failed to orient the starting piece correctly"); }
            }

            // now we match the rest of the tiles
            for (int i = 0; i < PD; i++)
            {
                // place the first tile in the next row, matching edge down|up (skipping first)
                if (!i.Equals(0))
                {
                    int sideValue = puzzle[i - 1, 0].intsides[2];
                    int nextTileId = puzzle[i - 1, 0].matches.Where(tup => tup.Item2.Equals(sideValue)).Select(tup => tup.Item1).First();
                    Tile nextTile = tiles[nextTileId];
                    MatchSide(2, sideValue, nextTile);
                    puzzle[i, 0] = nextTile;
                }

                // finish off the row, matching edges right|left 
                for (int j = 1; j < PD; j++)
                {
                    int sideValue = puzzle[i, j - 1].intsides[1];
                    int nextTileId = puzzle[i, j - 1].matches.Where(tup => tup.Item2.Equals(sideValue)).Select(tup => tup.Item1).First();
                    Tile nextTile = tiles[nextTileId];
                    MatchSide(1, sideValue, nextTile);
                    puzzle[i, j] = nextTile;
                }
            }

            return puzzle;
        }

        private int[,] TilesToInt(Tile[,] puzzle)
        {
            int[,] intpuzzle = new int[PD * (TD - 2), PD * (TD - 2)];

            for (int i = 0; i < PD * (TD - 2); i++)
            {
                for (int j = 0; j < PD * (TD - 2); j++)
                {
                    intpuzzle[i, j] = puzzle[i / (TD - 2), j / (TD - 2)].grid[(i % (TD - 2)) + 1, (j % (TD - 2)) + 1];
                }
            }
            return intpuzzle;
        }

        private int FindMonsters(int[,] intpuzzle)
        {
            // try to find monsters in the first 4 orientations
            int count = ScanOrientation(intpuzzle);
            if (!count.Equals(0)) { return count; }

            for (int i = 0; i < 3; i++)
            {
                intpuzzle = RotatePuzzle(intpuzzle);
                count = ScanOrientation(intpuzzle);
                if (!count.Equals(0)) { return count; }
            }

            // flip the puzzle and continue with the last 4 orientations
            intpuzzle = FlipPuzzle(intpuzzle);
            count = ScanOrientation(intpuzzle);
            if (!count.Equals(0)) { return count; }

            for (int i = 0; i < 3; i++)
            {
                intpuzzle = RotatePuzzle(intpuzzle);
                count = ScanOrientation(intpuzzle);
                if (!count.Equals(0)) { return count; }
            }

            return 0;
        }

        private int ScanOrientation(int[,] p)
        {
            int count = 0;

            // ugly, but efficiently checks only required positions
            for (int i = 0; i < (PD * (TD - 2)) - 2; i++)
            {
                for (int j = 0; j < (PD * (TD - 2)) - 19; j++)
                {
                    if (p[i, j + 18] == 1 &&
                        p[i + 1, j] == 1 && p[i + 1, j + 5] == 1 && p[i + 1, j + 6] == 1 && p[i + 1, j + 11] == 1 && p[i + 1, j + 12] == 1 && p[i + 1, j + 17] == 1 && p[i + 1, j + 18] == 1 && p[i + 1, j + 19] == 1 &&
                        p[i + 2, j + 1] == 1 && p[i + 2, j + 4] == 1 && p[i + 2, j + 7] == 1 && p[i + 2, j + 10] == 1 && p[i + 2, j + 13] == 1 && p[i + 2, j + 16] == 1)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private int[,] RotatePuzzle(int[,] puzzle)
        {
            int[,] newPuzzle = new int[(TD - 2) * PD, (TD - 2) * PD];
            for (int i = 0; i < (TD - 2) * PD; i++)
            {
                for (int j = 0; j < (TD - 2) * PD; j++)
                {
                    newPuzzle[j, (TD - 2) * PD - 1 - i] = puzzle[i, j];
                }
            }
            return newPuzzle;
        }

        private int[,] FlipPuzzle(int[,] puzzle)
        {
            int[,] newPuzzle = new int[(TD - 2) * PD, (TD - 2) * PD];
            for (int i = 0; i < (TD - 2) * PD; i++)
            {
                for (int j = 0; j < (TD - 2) * PD; j++)
                {
                    newPuzzle[i, (TD - 2) * PD - 1 - j] = puzzle[i, j];
                }
            }
            return newPuzzle;
        }

        private void MatchSide(int side, int num, Tile nextTile)
        {
            int matchside = (side + 2) % 4;

            // annoying check of all 8 orientations to line up the next tile
            // could potentially have a switch statement that maps us directly to the right one?
            if (!nextTile.intsides[matchside].Equals(num))
            {
                for (int i = 0; i < 3; i++)
                {
                    nextTile.RotateTile();
                    if (nextTile.intsides[matchside].Equals(num))
                    {
                        break;
                    }
                }

                if (!nextTile.intsides[matchside].Equals(num))
                {
                    nextTile.FlipTile();
                }

                if (!nextTile.intsides[matchside].Equals(num))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        nextTile.RotateTile();
                        if (nextTile.intsides[matchside].Equals(num))
                        {
                            break;
                        }
                    }
                }
            }
        }

        // reverses the bits in an integer
        // useful for matching edges correctly after a rotation or flip
        private int RevBits(int num)
        {
            int revNum = 0;
            int pow = TD - 1;
            while (num != 0)
            {
                revNum += (num & 1) << pow;
                num = num >> 1;
                pow -= 1;
            }

            return revNum;
        }

        // Tile helper class to manage individual puzzle tiles
        // Contains ids, data, tile edge values, matches with other tiles, and methods for rotations/flips
        public class Tile
        {
            public int id;
            public int[,] grid = new int[TD, TD];
            public int[] intsides = new int[4];
            public List<(int, int)> matches = new List<(int, int)>();

            public Tile(string data)
            {
                var lines = data.Split("\n");
                id = int.Parse(lines[0].Substring(5, 4));

                for (int i = 0; i < TD; i++)
                {
                    for (int j = 0; j < TD; j++)
                    {
                        grid[i, j] = lines[i + 1][j].Equals('#') ? 1 : 0;
                    }
                }

                CalcIntSides();
            }

            public void RotateTile()
            {
                int[,] newGrid = new int[10, 10];
                for (int i = 0; i < 4; i++) { intsides[i] = 0; }

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        newGrid[j, 9 - i] = grid[i, j];
                    }
                }

                grid = newGrid;
                CalcIntSides();
            }

            public void FlipTile()
            {
                // flip horizontally, about a vertical axis
                int[,] newGrid = new int[10, 10];

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        newGrid[i, 9 - j] = grid[i, j];
                    }
                }

                grid = newGrid;
                CalcIntSides();
            }

            private void CalcIntSides()
            {
                for (int i = 0; i < 4; i++) { intsides[i] = 0; }
                for (int i = 0; i < 10; i++)
                {
                    int bit = 1 << (grid.GetLength(0) - 1 - i);
                    intsides[0] += grid[0, i].Equals(1) ? bit : 0;
                    intsides[1] += grid[i, grid.GetLength(0) - 1].Equals(1) ? bit : 0;
                    intsides[2] += grid[grid.GetLength(0) - 1, i].Equals(1) ? bit : 0;
                    intsides[3] += grid[i, 0].Equals(1) ? bit : 0;
                }
            }

            public void AddMatch(int id, int side)
            {
                matches.Add((id, side));
            }
        }
    }
}