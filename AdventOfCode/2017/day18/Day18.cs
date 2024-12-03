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

        public long Part1()
        {
            string[] lines = ReadFile().Split("\n").Select(d => d.Trim().ToLower()).ToArray();
            Dictionary<string, long> registers = CreateRegisters();
            long lastPlayed = 0, i = 0;
            while (true)
            {
                if (i >= lines.Length) return 0;

                Instruction instruction0 = Instruction.Parse(lines[i], registers);
                try
                {
                    switch (instruction0.Type)
                    {
                        case Instruction.InstructionType.SND:
                            lastPlayed = PlaySound(instruction0, ref registers);
                            break;
                        case Instruction.InstructionType.SET:
                            Set(instruction0, ref registers);
                            break;
                        case Instruction.InstructionType.ADD:
                            Add(instruction0, ref registers);
                            break;
                        case Instruction.InstructionType.MUL:
                            Multiply(instruction0, ref registers);
                            break;
                        case Instruction.InstructionType.MOD:
                            ReduceModulo(instruction0, ref registers);
                            break;
                        case Instruction.InstructionType.RCV:
                            long? recovered = Recover(instruction0, registers, lastPlayed);
                            if (recovered != null) return (long)recovered;
                            break;
                        case Instruction.InstructionType.JGZ:
                            long? jump = Jump(instruction0, ref registers);
                            if (jump != null)
                            {
                                i += (long)jump;
                                i--;
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
        }

        public long Part2()
        {
            Dictionary<string, long> register0 = CreateRegisters();
            Dictionary<string, long> register1 = CreateRegisters('p', 1);
            Queue<long> values0 = new Queue<long>();
            Queue<long> values1 = new Queue<long>();
            string[] lines = ReadFile().Split("\n").Select(d => d.Trim().ToLower()).ToArray();
            long i0 = 0, i1 = 0, times = 0;
            bool waiting0 = false, waiting1 = false;
            while (true)
            {
                if (i0 >= lines.Length || i1 >= lines.Length) return times;

                Instruction instruction0 = Instruction.Parse(lines[i0], register0);
                Instruction instruction1 = Instruction.Parse(lines[i1], register1);
                
                if (waiting0 && waiting1) return times;

                if (!waiting0 || values0.Count != 0)
                {
                    switch (instruction0.Type)
                    {
                        case Instruction.InstructionType.SND:
                            Send(instruction0, register0, ref values1);
                            break;
                        case Instruction.InstructionType.SET:
                            Set(instruction0, ref register0);
                            break;
                        case Instruction.InstructionType.ADD:
                            Add(instruction0, ref register0);
                            break;
                        case Instruction.InstructionType.MUL:
                            Multiply(instruction0, ref register0);
                            break;
                        case Instruction.InstructionType.MOD:
                            ReduceModulo(instruction0, ref register0);
                            break;
                        case Instruction.InstructionType.RCV:
                            waiting0 = !Receive(instruction0, register0, ref values0);
                            if (waiting0) i0--;
                            break;
                        case Instruction.InstructionType.JGZ:
                            long? jump = Jump(instruction0, ref register0);
                            if (jump != null)
                            {
                                i0 += (long)jump;
                                i0--;
                            }
                            break;
                    }

                    i0++;
                }

                if (!waiting1 || values1.Count != 0)
                {
                    switch (instruction1.Type)
                    {
                        case Instruction.InstructionType.SND:
                            Send(instruction1, register1, ref values0);
                            times++;
                            break;
                        case Instruction.InstructionType.SET:
                            Set(instruction1, ref register1);
                            break;
                        case Instruction.InstructionType.ADD:
                            Add(instruction1, ref register1);
                            break;
                        case Instruction.InstructionType.MUL:
                            Multiply(instruction1, ref register1);
                            break;
                        case Instruction.InstructionType.MOD:
                            ReduceModulo(instruction1, ref register1);
                            break;
                        case Instruction.InstructionType.RCV:
                            waiting1 = !Receive(instruction1, register1, ref values1);
                            if (waiting1) i1--;
                            break;
                        case Instruction.InstructionType.JGZ:
                            long? jump = Jump(instruction1, ref register1);
                            if (jump != null)
                            {
                                i1 += (long)jump;
                                i1--;
                            }
                            break;
                    }

                    i1++;
                }
            }
        }

        private Dictionary<string, long> CreateRegisters(char defaultRegister = 'p', int defaultValue = 0)
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();
            for (char c = 'a'; c <= 'z'; c++)
            {
                if (c == defaultRegister) registers.Add(c.ToString(), defaultValue);
                else registers.Add(c.ToString(), 0);
            }

            return registers;
        }

        private bool Receive(Instruction i, Dictionary<string, long> register, ref Queue<long> queue)
        {
            if (queue.Count == 0) return false;
            long value = queue.Dequeue();
            register[i.Register] = value;
            return true;
        }

        private void Send(Instruction i, Dictionary<string, long> register, ref Queue<long> queue)
        {
            if (i.ValueX != null && i.Register == string.Empty) queue.Enqueue((long)i.ValueX);
            else if (i.ValueX == null && i.Register != string.Empty) queue.Enqueue(register[i.Register]);

        }

        private long PlaySound(Instruction i, ref Dictionary<string, long> registers)
        {
            if (!registers.ContainsKey(i.Register)) throw new Exception($"Error with {i}");
            return registers[i.Register];
        }

        private void Set(Instruction i, ref Dictionary<string, long> registers)
        {
            if (!registers.ContainsKey(i.Register)) throw new Exception($"Error with {i}");
            registers[i.Register] = i.ValueY;
        }

        private void Add(Instruction i, ref Dictionary<string, long> registers)
        {
            if (!registers.ContainsKey(i.Register)) throw new Exception($"Error with {i}");
            registers[i.Register] += i.ValueY;
        }

        private void Multiply(Instruction i, ref Dictionary<string, long> registers)
        {
            if (!registers.ContainsKey(i.Register)) throw new Exception($"Error with {i}");
            registers[i.Register] *= i.ValueY;
        }

        private void ReduceModulo(Instruction i, ref Dictionary<string, long> registers)
        {
            if (!registers.ContainsKey(i.Register)) throw new Exception($"Error with {i}");
            registers[i.Register] %= i.ValueY;
        }

        private long? Recover(Instruction i, Dictionary<string, long> registers, long lastPlayed)
        {
            if (!registers.ContainsKey(i.Register)) throw new Exception($"Error with {i}");
            if (registers[i.Register] != 0)
            {
                return lastPlayed;
            }

            return null;
        }

        private long? Jump(Instruction i, ref Dictionary<string, long> registers)
        {
            if (i.ValueX != null && i.Register == string.Empty && i.ValueX > 0)
            {
                return i.ValueY;
            }

            if (i.ValueX == null && i.Register != string.Empty && registers[i.Register] > 0)
            {
                return i.ValueY;
            }

            return null;
        }
    }
}
