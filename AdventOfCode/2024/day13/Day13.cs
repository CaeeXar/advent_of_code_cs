namespace AOC2024.Day13
{
    internal class Day13
    {
        string path = @"2024\day13\Input.txt";

        private struct Equation
        {
            public Equation(int a, int b, int c)
            {
                this.A = a;
                this.B = b;
                this.C = c;
            }

            public int A { get; private set; }
            public int B { get; private set; }
            public int C { get; private set; }

            public bool Valid(int x, int y)
            {
                return this.A * x + this.B * y == this.C;
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

        public int Part1()
        {
            List<Tuple<Equation, Equation>> equations;
            ParseInput(out equations);

            int total = 0;
            foreach (Tuple<Equation,Equation> machine in equations)
            {
                Equation e1 = machine.Item1, e2 = machine.Item2;
                List<Tuple<int, int>> solutions = new List<Tuple<int, int>>();
                int max = Math.Max(e1.C, e2.C);
                for (int x = 1; e1.A * x < max; x++)
                {
                    for (int y = 1; e2.B * y < max; y++)
                    {
                        if (e1.A * x + e1.B * y == e1.C && e2.A * x + e2.B * y == e2.C)
                        {
                            solutions.Add(new Tuple<int, int>(x, y));
                        }
                    }
                }

                Tuple<int, int>? min = solutions.MinBy(p => p.Item1 * 3 + p.Item2);
                if (min != null)
                {
                    total += (min.Item1 * 3 + min.Item2);
                }
            }

            return total;
        }

        public long Part2()
        {
            return 0;
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
