using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(13, "Shuttle Search")]
    public class Day13 : Solution
    {
        private string inputString = string.Empty;

        int earliest_departure;
        List<int> busSchedule;

        public Day13(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override string GetPart1()
        {
            (int id, int wait) min = busSchedule.Where(bus_id => !bus_id.Equals(-1))
                                            .Select(id => (id, wait_time: id - (earliest_departure % id)))
                                            .Aggregate((min, t) => t.wait_time < min.wait_time ? t : min);

            return (min.id * min.wait).ToString();
        }

        public override string GetPart2()
        {
            List<(int id, int index)> buses = new List<(int, int)>();
            for(int i = 0; i < busSchedule.Count; i++)
            {
                if (busSchedule[i].Equals(-1)) { continue; }
                buses.Add((busSchedule[i], i));
            }

            /*
            We're going to construct a CRT solution via Bezout identities
            Bus schedule ids are such that gcd(x,y) = 1 for all x,y ids
            Given:
            x = a mod am
            x = b mod bm
            then for bezout coefficients b1, b2:
            x = (a*b2*bm + b*b1*am) mod (am*bm)         [1]
            */

            BigInteger solution = BigInteger.Zero;
            BigInteger a = buses[0].id - (buses[0].index % buses[0].id);
            BigInteger am = buses[0].id;

            for (int i = 1; i < buses.Count; i++)
            {
                BigInteger b = buses[i].id - (buses[i].index % buses[i].id);
                BigInteger bm = buses[i].id;
                BigInteger[] bez = GetBezoutCoefficients(am, bm);

                BigInteger mod = (am * bm);
                BigInteger term1 = (((bez[1] * bm) % mod) * a) % mod; // this is a*b2*bm in [1]
                BigInteger term2 = (((bez[0] * am) % mod) * b) % mod; // this is b*b1*am in [1]
                solution = (am * bm + term1 + term2) % mod;
   
                a = solution; // i.e merge two equivalences into one: (x = a mod am, x = b mod bm) => (x = c mod cm)
                am = am * bm; // and repeat for the rest of the buses
            }

            return solution.ToString();
        }

        private BigInteger[] GetBezoutCoefficients(BigInteger am, BigInteger bm)
        {
            // Using the extended Euclidian algorithm

            List<BigInteger[]> index = new List<BigInteger[]>();

            BigInteger[] one = { 0, am, 1, 0 };
            BigInteger[] two = { 0, bm, 0, 1 };
            index.Add(one);
            index.Add(two);

            while (!index[index.Count - 1][1].Equals(0))
            {
                int i = index.Count - 1;
                BigInteger[] newIndex = { 0, 0, 0, 0 };
                newIndex[0] = index[i - 1][1] / index[i][1];
                newIndex[1] = index[i - 1][1] % index[i][1];
                newIndex[2] = index[i - 1][2] - newIndex[0] * index[i][2];
                newIndex[3] = index[i - 1][3] - newIndex[0] * index[i][3];
                index.Add(newIndex);
            }

            BigInteger[] bezoutCoefficients = { index[index.Count - 2][2], index[index.Count - 2][3] };

            return bezoutCoefficients;
        }

        private void ParseInput()
        {
            earliest_departure = int.Parse(inputString.Replace("\r", "").Split("\n").First());
            busSchedule = inputString.Replace("\r", "").Split("\n").Skip(1)
                                                       .Select(ids => ids.Split(",")).First()
                                                       .Select(id => id.Equals("x") ? -1 : int.Parse(id))
                                                       .ToList();
        }
    }
}
