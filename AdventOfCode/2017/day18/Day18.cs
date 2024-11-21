using System.Security.Cryptography.X509Certificates;

namespace AOC2017.Day18
{
    internal class Day18
    {      

        string path = @"2017\day18\Input.txt";

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
            string[] lines = ReadFile().Split("\n").Select(d => d.Trim().ToLower()).ToArray();
            Dictionary<string, int> registers = new Dictionary<string, int>();
            int lastPlayed = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                Instruction instruction = Instruction.Parse(lines[i]);
                try 
                {
                    switch (instruction.Type)
                    {
                        case Instruction.InstructionType.SND:
                            lastPlayed = PlaySound(instruction, registers);
                            break;
                        case Instruction.InstructionType.SET:
                            Set(instruction, registers);
                            break;
                        case Instruction.InstructionType.ADD:
                            Add(instruction, registers);
                            break;
                        case Instruction.InstructionType.MUL:
                            Multiply(instruction, registers);
                            break;
                        case Instruction.InstructionType.MOD:
                            ReduceModulo(instruction, registers);
                            break;
                        case Instruction.InstructionType.RCV:
                            Recover(instruction, lastPlayed);
                            break;
                        case Instruction.InstructionType.JGZ:
                            i += Jump(instruction, registers);
                            break;
                    }
                } 
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return 0;
        }

        public int Part2()
        {
            return 0;
        }

        private int PlaySound(Instruction i, Dictionary<string, int> registers)
        {
            if (i.RegsiterX == string.Empty && i.ValueX == null) 
            { 
                throw new Exception("X: Register and value not defined"); 
            }

            return registers[i.RegsiterX];
        }

        private void Set(Instruction i, Dictionary<string, int> registers)
        {
            if (i.RegsiterX == string.Empty) throw new Exception("X: Register not defined");
            if (i.ValueY == null) throw new Exception("Y: Value not defined");

            if (registers.ContainsKey(i.RegsiterX))
            {
                registers[i.RegsiterX] = (int)i.ValueY;
            }
            else
            {
                registers.Add(i.RegsiterX, (int)i.ValueY);
            }
        }

        private void Add(Instruction i, Dictionary<string, int> registers)
        {
            if (i.RegsiterX == string.Empty)
            {
                throw new Exception("X: Register to update is not defined");
            }

            if (i.RegsiterY == string.Empty && i.ValueY == null)
            {
                throw new Exception("Y: Register and value to add are not defined");
            }

            if (i.RegsiterY != string.Empty)
            {
                registers[i.RegsiterX] += registers.GetValueOrDefault(i.RegsiterY, 0);
            }
            else if (i.ValueY != null)
            {
                registers[i.RegsiterX] += (int)i.ValueY;
            }
        }

        private void Multiply(Instruction i, Dictionary<string, int> registers)
        {
            if (i.RegsiterX == string.Empty)
            {
                throw new Exception("X: Register to update is not defined");
            }

            if (i.RegsiterY == string.Empty && i.ValueY == null)
            {
                throw new Exception("Y: Register and value to multiply are not defined");
            }

            if (i.RegsiterY != string.Empty)
            {
                registers[i.RegsiterX] *= registers.GetValueOrDefault(i.RegsiterY, 0);
            }
            else if (i.ValueY != null)
            {
                registers[i.RegsiterX] *= (int)i.ValueY;
            }
        }

        private void ReduceModulo(Instruction i, Dictionary<string, int> registers)
        {
            if (i.RegsiterX == string.Empty)
            {
                throw new Exception("X: Register to update is not defined");
            }

            if (i.RegsiterY == string.Empty && i.ValueY == null)
            {
                throw new Exception("Y: Register and value to reduce to modulo are not defined");
            }

            if (i.RegsiterY != string.Empty)
            {
                registers[i.RegsiterX] %= registers.GetValueOrDefault(i.RegsiterY, 0);
            }
            else if (i.ValueY != null)
            {
                registers[i.RegsiterX] %= (int)i.ValueY;
            }
        }

        private int Recover(Instruction i, int lastPlayed)
        {
            if (i.RegsiterX == string.Empty && i.ValueX == null)
            {
                throw new Exception("X: Register and value are not defined");
            }

            if (lastPlayed != 0) return lastPlayed;
            return 0;
        }

        private int Jump(Instruction i, Dictionary<string, int> registers)
        {
            if (i.RegsiterX == string.Empty && i.ValueX == null)
            {
                throw new Exception("X: Register and value are not defined");
            }

            if (i.RegsiterX == string.Empty && i.ValueY == null)
            {
                throw new Exception("Y: Yalue are not defined");
            }

            if (registers.GetValueOrDefault(i.RegsiterX, 0) > 0)
            {
                return registers.GetValueOrDefault(i.RegsiterY, (int)i.ValueY);
            }

            return 0;
        }
    }
}
