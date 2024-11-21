using System.Runtime.CompilerServices;

namespace AOC2017.Day18
{
    internal class Instruction
    {
        public static Instruction Parse(string line, Dictionary<string, int> registers)
        {
            Instruction i = new Instruction();
            string[] data = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .Select(d => d.Trim()).ToArray();

            if (data.Length == 3)
            {
                i.Register = data[1].ToLower();
                if (int.TryParse(data[2], out _))
                {
                    i.Value = int.Parse(data[2]);
                }
                else 
                {
                    i.Value = registers.GetValueOrDefault(data[2].ToLower(), 0);
                }
            }
            else
            {
                i.Register = data[1].ToLower();
                i.Value = null;
            }

            InstructionType _type;
            Enum.TryParse<InstructionType>(data[0], true, out _type);
            i.Type = _type;

            return i;
        }

        public enum InstructionType
        {
            SND,
            SET, 
            ADD, 
            MUL, 
            MOD, 
            RCV, 
            JGZ,
        }

        public InstructionType Type { get; set; }

        public string Register { get; set; } = string.Empty;

        public int? Value { get; set; } = null;

        public override string ToString()
        {
            return $"{this.Type.ToString()}: (Register: {this.Register}, Value: {this.Value})";
        }
    }
}
