namespace AOC2017.Day09
{
    internal class Day09
    {
        private int total { get; set; }
        string path = @"2017\day09\Input.txt";

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
            return Play(ReadFile());
        }

        public int Part2()
        {
            return Play(ReadFile(), true);
        }

        private int Play(string line, bool returnSkipped = false)
        {
            int groups = 0, weigth = 0, skipped = 0;
            bool isGarbage = false,
                 ignore = false;

            for(int pos = 0; pos < line.Length; pos++)
            {
                char character = line[pos];
                
                if (ignore)
                {
                    ignore = false;
                    continue;
                }

                if(isGarbage && character != '>' && character != '!')
                {
                    skipped++;
                    continue;
                }

                switch(character)
                {

                    case '{':
                        weigth++;
                        break;
                    case '}':
                        groups += weigth;
                        weigth--;
                        break;
                    case '<':
                        isGarbage = true;
                        break;
                    case '>':
                        isGarbage = false;
                        break;
                    case '!':
                        ignore = true;
                        break;
                }
            }

            if (returnSkipped) return skipped;

            return groups;
        }
    }
}
