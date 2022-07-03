using AoC.Code.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 11, "Seating System")]
    public class Day11 : Solution
    {
        private string inputString = string.Empty;
        private Seat[][] seat_grid;
        private List<Seat> seats_list;
        private (int, int)[] dirs = { (1, 0), (-1, 0), (0, 1), (0, -1), (1, 1), (-1, 1), (1, -1), (-1, -1) };

        private class Seat
        {
            public (int x, int y) pos;
            public bool isSeat;
            public bool isOccupied;
            public bool nextOccupied;
            public List<Seat> neighbours;

            public Seat(bool s, bool o, List<Seat> n)
            {
                isSeat = s;
                isOccupied = o;
                neighbours = n;
            }
        }

        public Day11(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            seats_list = ParseSeats();
            
            return FillSeatsUntilStable(seats_list, 4).ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            seats_list.ForEach(p => {
                p.isOccupied = false;
                p.nextOccupied = false;
                p.neighbours = GetNeighbours2((p.pos.x, p.pos.y));
                });

            return FillSeatsUntilStable(seats_list, 5).ToString();
        }

        private int FillSeatsUntilStable(List<Seat> seats, int rule2)
        {
            bool stateChange = true;

            while (stateChange)
            {
                stateChange = false;
                foreach (Seat seat in seats)
                {
                    int neighbourCount = NeighboursCount(seat.neighbours);
                    if (!seat.isOccupied && neighbourCount.Equals(0))
                    {
                        stateChange = true;
                        seat.nextOccupied = true;
                    }
                    else if (seat.isOccupied && neighbourCount >= rule2)
                    {
                        stateChange = true;
                        seat.nextOccupied = false;
                    }
                    else
                    {
                        seat.nextOccupied = seat.isOccupied;
                    }
                }
                seats.ForEach(o => o.isOccupied = o.nextOccupied);
            }
            return seats.Where(p => p.isOccupied).Count();
        }

        private int NeighboursCount(List<Seat> neighbours)
        {
            return neighbours.Where(p => p.isOccupied).Count();
        }

        private void ParseInput()
        {
            seat_grid = inputString.Replace("\r", "")
                              .Split("\n")
                              .Select(line => line.Select(c =>
                                                  {
                                                      Seat seat = new Seat(false, false, new List<Seat>());
                                                      if (c.Equals('L'))
                                                      {
                                                          seat.isSeat = true;
                                                      }
                                                      return seat;
                                                  }
                                                  )
                                                  .ToArray()
                              ).ToArray();
        }

        private List<Seat> ParseSeats()
        {
            List<Seat> seats = new List<Seat>();
            
            for(int j = 0; j < seat_grid.Length; j++)
            {
                for(int i = 0; i < seat_grid[0].Length; i++)
                {
                    if (seat_grid[j][i].isSeat)
                    {
                        seat_grid[j][i].pos = (i, j);
                        seat_grid[j][i].neighbours = GetNeighbours((i, j));
                        seats.Add(seat_grid[j][i]);
                    }
                }
            }
            return seats;
        }

        private List<Seat> GetNeighbours((int x, int y) pos)
        {
            List<Seat> neighbours = new List<Seat>();
            foreach((int i, int j) dir in dirs)
            {
                if (isValidPos(pos, dir.i, dir.j) && seat_grid[pos.y + dir.j][pos.x + dir.i].isSeat)
                {
                    neighbours.Add(seat_grid[pos.y + dir.j][pos.x + dir.i]);
                }
            }
            return neighbours;
        }

        private List<Seat> GetNeighbours2((int x, int y) pos)
        {
            List<Seat> neighbours = new List<Seat>();
            foreach ((int i, int j) dir in dirs)
            {
                // for each direction we're going to keep going until we reach a seat, or the edge of the seating area
                int di = dir.i; int dj = dir.j;
                while (isValidPos(pos, di, dj))
                {
                    if (seat_grid[pos.y + dj][pos.x + di].isSeat)
                    {
                        neighbours.Add(seat_grid[pos.y + dj][pos.x + di]);
                        break;
                    }
                    else
                    {
                        di += dir.i; dj += dir.j;
                    }
                }
            }
            return neighbours;
        }

        private bool isValidPos((int x, int y) pos, int i, int j)
        {
            return !( (i==0 && j==0) || pos.x + i < 0 || pos.x + i > seat_grid[0].Length-1 || pos.y + j < 0 || pos.y + j > seat_grid.Length-1);
        }
    }
}
