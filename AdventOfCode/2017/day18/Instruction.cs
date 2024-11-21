using System.Runtime.CompilerServices;

namespace AOC2017.Day18
{
    internal class Instruction
    {
        public static Instruction Parse(string line, Dictionary<string, long> registers)
        {
            Instruction i = new Instruction();
            string[] data = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                .Select(d => d.Trim()).ToArray();

            if (data.Length == 3)
            {
                
                if (long.TryParse(data[1], out _))
                {
                    i.ValueX = long.Parse(data[1]);
                }
                else 
                {
                    i.Register = data[1].ToLower();
                }

                if (long.TryParse(data[2], out _))
                {
                    i.ValueY = long.Parse(data[2]);
                }
                else
                {
                    i.ValueY = registers.GetValueOrDefault(data[2].ToLower(), 0);
                }
            }
            else
            {
                if (long.TryParse(data[1], out _))
                {
                    i.ValueX = long.Parse(data[1]);
                }
                else
                {
                    i.Register = data[1].ToLower();
                }
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

        public long ValueY { get; set; }

        public long? ValueX { get; set; } = null;

        public override string ToString()
        {
            return $"{this.Type.ToString()}: (Register: {this.Register}, ValueX: {this.ValueX}, ValueY: {this.ValueY})";
        }
    }
}
