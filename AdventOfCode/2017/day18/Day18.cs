using System.Reflection.Metadata.Ecma335;
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
            // 862 is WRONG
            string[] lines = ReadFile().Split("\n").Select(d => d.Trim().ToLower()).ToArray();
            Dictionary<string, int> registers = CreateRegisters();
            int lastPlayed = 0, i = 0;
            while (true)
            {
                Instruction instruction = Instruction.Parse(lines[i], registers);
                Console.WriteLine(i + ": " + instruction);
                try
                {
                    switch (instruction.Type)
                    {
                        case Instruction.InstructionType.SND:
                            lastPlayed = PlaySound(instruction, ref registers);
                            break;
                        case Instruction.InstructionType.SET:
                            Set(instruction, ref registers);
                            break;
                        case Instruction.InstructionType.ADD:
                            Add(instruction, ref registers);
                            break;
                        case Instruction.InstructionType.MUL:
                            Multiply(instruction, ref registers);
                            break;
                        case Instruction.InstructionType.MOD:
                            ReduceModulo(instruction, ref registers);
                            break;
                        case Instruction.InstructionType.RCV:
                            int? recovered = Recover(instruction, registers, lastPlayed);
                            if (recovered != null) return (int)recovered;
                            break;
                        case Instruction.InstructionType.JGZ:
                            int? jump = Jump(instruction, ref registers);
                            if (jump != null)
                            {
                                i += (int)jump;
                                continue;
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                i++;
            }

            return 0;
        }

        public int Part2()
        {
            return 0;
        }

        private Dictionary<string, int> CreateRegisters()
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            for (char c = 'a'; c <= 'z'; c++)
            {
                registers.Add(c.ToString(), 0);
            }

            return registers;
        }

        private int PlaySound(Instruction i, ref Dictionary<string, int> registers)
        {
            return registers[i.Register];
        }

        private void Set(Instruction i, ref Dictionary<string, int> registers)
        {
            if (i.Value == null) throw new Exception("Wrong instruction (SET)!");
            registers[i.Register] = (int)i.Value;
        }

        private void Add(Instruction i, ref Dictionary<string, int> registers)
        {
            if (i.Value == null) throw new Exception("Wrong instruction (ADD)!");
            registers[i.Register] += (int)i.Value;
        }

        private void Multiply(Instruction i, ref Dictionary<string, int> registers)
        {
            if (i.Value == null) throw new Exception("Wrong instruction (MUL)!");
            registers[i.Register] *= (int)i.Value;
        }

        private void ReduceModulo(Instruction i, ref Dictionary<string, int> registers)
        {
            if (i.Value == null) throw new Exception("Wrong instruction (MOD)!");
            registers[i.Register] %= (int)i.Value;
        }

        private int? Recover(Instruction i, Dictionary<string, int> registers, int lastPlayed)
        {
            if (registers[i.Register] != 0)
            {
                return lastPlayed;
            }

            return null;
        }

        private int? Jump(Instruction i, ref Dictionary<string, int> registers)
        {
            if (i.Value == null) throw new Exception("Wrong instruction (JGZ)!");
            if (registers[i.Register] > 0)
            {
                return i.Value;
            }

            return null;
        }
    }
}
