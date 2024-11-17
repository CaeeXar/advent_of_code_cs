using AOC2017.Day08;

namespace AOC2017.Day16
{
    internal class Day16
    {      

        string path = @"2017\day16\Input.txt";
        const int RUNS = (int)1e9;

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

        public string Part1()
        {
            char[] programs = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            string[] instructions = ReadFile().Split(",").Select(d => d.Trim()).ToArray();
            RunInstructions(ref programs, instructions);
            return string.Join("", programs);
        }

        public string Part2()
        {
            char[] programs = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p' };
            string[] instructions = ReadFile().Split(",").Select(d => d.Trim()).ToArray();
            
            // idea: find loops and just run 1b%loop times instead
            // problem: no loop => wont work
            int loop = FindLoop(programs, instructions); 
            for (int i = 1; i <= RUNS % loop; i++)
            {
                RunInstructions(ref programs, instructions);
            }

            return string.Join("", programs);
        }

        private void Spin(string input, ref char[] programs)
        {
            int range = int.Parse(input.Substring(1));
            programs = programs[^range..^0].Concat(programs[0..^range]).ToArray();
        }
        
        private void Exchange(string input, ref char[] programs)
        {
            int[] indecis = input.Substring(1).Split("/").Select(int.Parse).ToArray();
            int i1 = indecis[0], i2 = indecis[1];
            char temp = programs[i1];
            programs[i1] = programs[i2];
            programs[i2] = temp;
        }
        
        private void Partner(string input, ref char[] programs)
        {
            char[] names = input.Substring(1).Split("/").Select(char.Parse).ToArray();
            int i1 = Array.IndexOf(programs, names[0]), i2 = Array.IndexOf(programs, names[1]);
            char temp = programs[i1];
            programs[i1] = programs[i2];
            programs[i2] = temp;
        }

        private void RunInstructions(ref char[] programs, string[] instructions)
        {
            foreach (string instruction in instructions)
            {
                if (instruction.ToLower()[0] == 's')
                {
                    Spin(instruction, ref programs);
                }
                else if (instruction.ToLower()[0] == 'x')
                {
                    Exchange(instruction, ref programs);
                }
                else if (instruction.ToLower()[0] == 'p')
                {
                    Partner(instruction, ref programs);
                }
            }
        }

        private int FindLoop(char[] programs, string[] instructions)
        {
            char[] copy = new char[16];
            programs.CopyTo(copy, 0);
            
            RunInstructions(ref copy, instructions);

            int i = 0;
            while (i != RUNS)
            {
                if (string.Join("", programs) == string.Join("", copy))
                {
                    return i + 1; // cuz we ran once initial
                }

                RunInstructions(ref copy, instructions);
                i++;
            }

            return -1;
        }
    }
}
