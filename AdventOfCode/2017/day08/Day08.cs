using Microsoft.Win32;
using System.Security.Cryptography;

namespace AOC2017.Day08
{
    internal class Day08
    {
        private int total { get; set; }
        string path = @"2017\day08\Input.txt";

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
            string[] lines = ReadFile().Split("\n");
            Dictionary<string, int> registers = GetRegisters(lines);
            RunInstructions(ref registers, lines);
            return registers.Values.Max();
        }

        public int Part2()
        {
            string[] lines = ReadFile().Split("\n");
            Dictionary<string, int> registers = GetRegisters(lines);
            return RunInstructions(ref registers, lines);
        }

        private int RunInstructions(ref Dictionary<string, int> registers, string[] lines)
        {
            int max = int.MinValue;
            foreach (string line in lines)
            {
                Instruction ins = new Instruction(line);
                bool check = false;
                switch (ins.Condition)
                {
                    case "<":
                        check = (registers[ins.RegisterToCheck] < ins.ConditionValue);
                        break;
                    case ">":
                        check = (registers[ins.RegisterToCheck] > ins.ConditionValue);
                        break;
                    case "<=":
                        check = (registers[ins.RegisterToCheck] <= ins.ConditionValue);
                        break;
                    case ">=":
                        check = (registers[ins.RegisterToCheck] >= ins.ConditionValue);
                        break;
                    case "!=":
                        check = (registers[ins.RegisterToCheck] != ins.ConditionValue);
                        break;
                    case "==":
                        check = (registers[ins.RegisterToCheck] == ins.ConditionValue);
                        break;
                    default:
                        throw new Exception("Operation not found.");
                }

                if(check)
                {
                    if (ins.Increase)
                    {
                        registers[ins.RegisterToModify] += ins.Amount;
                    }
                    else
                    {
                        registers[ins.RegisterToModify] -= ins.Amount;
                    }
                }

                int maybeMax = FindMax(registers);
                if (max < maybeMax) max = maybeMax;

                check = false;
            }

            return max;
        }

        private int FindMax(Dictionary<string, int> registers)
        {
            return registers.Values.Max();
        }

        private void DebugPrint(Dictionary<string, int> registers)
        {
            foreach (KeyValuePair<string, int> kvp in registers)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        private Dictionary<string, int> GetRegisters(string[] lines)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            foreach (string line in lines)
            {
                string[] trimmedData = line.Split(" ").Select(d => d.Trim()).ToArray();
                string regName = trimmedData[0];

                if(!registers.ContainsKey(regName)) registers.Add(regName, 0);
            }

            return registers;
        }
    }
}
