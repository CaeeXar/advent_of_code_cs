namespace AOC2017.Day18
{
    internal class Instruction
    {
        public static Instruction Parse(string line)
        {
            Instruction i = new Instruction();
            string[] data = line.Split(" ").Select(d => d.Trim()).ToArray();
            string type = data[0];
            string x = data[1];

            if (int.TryParse(x, out _))
            {
                i.ValueX = int.Parse(x);
            }
            else
            {
                i.RegsiterX = x;
            }

            if (data.Length > 2)
            {
                string y = data[2];
                if (int.TryParse(y, out _))
                {
                    i.ValueY = int.Parse(y);
                }
                else
                {
                    i.RegsiterY = y;
                }
            }

            Enum.Parse(typeof(InstructionType), type, true);

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

        public string RegsiterX { get; set; } = string.Empty;

        public string RegsiterY { get; set; } = string.Empty;

        public int? ValueX { get; set; } = null;

        public int? ValueY { get; set; } = null;
    }
}
