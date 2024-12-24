using System.Numerics;

namespace AOC2024.Day13
{
    internal class Day13
    {
        string path = @"2024\day13\Input.txt";

        private struct Equation
        {
            public Equation(long a, long b, long c)
            {
                this.A = a;
                this.B = b;
                this.C = c;
            }

            public long A { get; private set; }
            public long B { get; private set; }
            public long C { get; private set; }

            public bool Valid(long x, long y)
            {
                return this.A * x + this.B * y == (this.C);
            }

            public bool Valid(long x, long y, long offset)
            {
                return this.A * x + this.B * y == (this.C + offset);
            }
        }

        public string ReadFile()
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return null;
            }
        }

        public long Part1()
        {
            List<Tuple<Equation, Equation>> equations;
            ParseInput(out equations);

            long total = 0;
            foreach (Tuple<Equation,Equation> machine in equations)
            {
                Equation e1 = machine.Item1, e2 = machine.Item2;
                List<Tuple<long, long>> solutions = new List<Tuple<long, long>>();
                long max = Math.Max(e1.C, e2.C);
                for (long x = 1; e1.A * x < max; x++)
                {
                    for (long y = 1; e1.B * y < max; y++)
                    {
                        if (e1.Valid(x, y) && e2.Valid(x, y))
                        {
                            solutions.Add(new Tuple<long, long>(x, y));
                        }
                    }
                }

                Tuple<long, long>? min = solutions.MinBy(p => p.Item1 * 3 + p.Item2);
                if (min != null)
                {
                    total += (min.Item1 * 3 + min.Item2);
                }
            }

            return total;
        }

        public long Part2()
        {
            // 94x + 22y = 8400
            // 34x + 67y = 5400 ==> y = 5400/67 - 34x/67
            // e1.A * x + e1.B * y = e1.C
            // e2.A * x + e2.B * y = e2.C ==> y = (e2.C / e2.B) - (e2.A * x / e2.B)
            // Substitution:
            // https://www.varsitytutors.com/hotmath/hotmath_help/topics/solving-systems-of-linear-equations-using-substitution
            List<Tuple<Equation, Equation>> equations;
            ParseInput(out equations);

            long offset = (long)10e12, total = 0;
            foreach (Tuple<Equation, Equation> machine in equations)
            {
                Equation e1 = machine.Item1, e2 = machine.Item2;
                long x = (e2.B * (e1.C + offset) - e1.B * (e2.C + offset)) / (e2.B * e1.A - e1.B * e2.A);
                long y = ((e2.C + offset) / e2.B) - (e2.A * x) / e2.B;

                if (e1.Valid(x, y, offset) && e1.Valid(x, y, offset))
                {
                    total += (x * 3 + y);
                }
            }

            return total;
        }

        private void ParseInput(out List<Tuple<Equation, Equation>> equations)
        {
            string[][] lines = ReadFile().Split(new string[] { "\r\n\r\n" }, StringSplitOptions.None)
                                         .Select(line => line.Split(
                                                                new string[] { "\r\n", "\n", "\r" }, 
                                                                StringSplitOptions.None)
                                                             .ToArray())
                           .ToArray();

            equations = new List<Tuple<Equation, Equation>>();
            foreach (string[] line in lines)
            {
                int[] a = line[0].Replace("Button A: ", "")
                                 .Replace("X+", "")
                                 .Replace("Y+", "")
                                 .Split(",")
                                 .Select(d => int.Parse(d.Trim())).ToArray();
                int[] b = line[1].Replace("Button B: ", "")
                                .Replace("X+", "")
                                .Replace("Y+", "")
                                .Split(",")
                                .Select(d => int.Parse(d.Trim())).ToArray();
                int[] c = line[2].Replace("Prize: ", "")
                                 .Replace("X=", "")
                                 .Replace("Y=", "")
                                 .Split(",")
                                 .Select(d => int.Parse(d.Trim())).ToArray();

                Equation e1 = new Equation(a[0], b[0], c[0]);
                Equation e2 = new Equation(a[1], b[1], c[1]);

                equations.Add(new Tuple<Equation, Equation>(e1, e2));
            }
        }
    }
}
