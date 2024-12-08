namespace AOC2024.Day05
{
    using Rules = Dictionary<int, List<int>>;

    internal class Day05
    {
        string path = @"2024\day05\Input.txt";

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
            string[] lines = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            Rules rules = new Rules();
            bool isRule = true;
            int sum = 0;

            foreach (string line in lines)
            {
                if (line.Trim() == string.Empty)
                {
                    isRule = false;
                    continue;
                }

                if (isRule)
                {
                    int[] data = line.Split("|").Select(d => int.Parse(d.Trim())).ToArray();
                    if (rules.ContainsKey(data[0])) rules[data[0]].Add(data[1]);
                    else rules.Add(data[0], new List<int>() { data[1] });
                }
                else
                {
                    int[] pages = line.Split(",").Select(d => int.Parse(d.Trim())).ToArray();
                    if (ValidPageOrder(pages, rules))
                    {
                        sum += (pages[pages.Length / 2]);
                    }
                }
            }

            return sum;
        }

        public int Part2()
        {
            string[] lines = ReadFile().Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
            Rules rules = new Rules();
            bool isRule = true;
            int sum = 0;

            foreach (string line in lines)
            {
                if (line.Trim() == string.Empty)
                {
                    isRule = false;
                    continue;
                }

                if (isRule)
                {
                    int[] data = line.Split("|").Select(d => int.Parse(d.Trim())).ToArray();
                    if (rules.ContainsKey(data[0])) rules[data[0]].Add(data[1]);
                    else rules.Add(data[0], new List<int>() { data[1] });
                }
                else
                {
                    int[] pages = line.Split(",").Select(d => int.Parse(d.Trim())).ToArray();
                    if (CorrectPageOrder(pages, rules))
                    {
                        sum += (pages[pages.Length / 2]);
                    }
                }
            }

            return sum;
        }

        private bool CorrectPageOrder(int[] pages, Rules rules)
        {
            bool corrected = false;
            for (int currentIndex= 0; currentIndex < pages.Length; currentIndex++)
            {
                int currentPage = pages[currentIndex];
                if (rules.ContainsKey(currentPage))
                {
                    foreach (int otherPage in rules[currentPage])
                    {
                        int otherIndex = Array.IndexOf(pages, otherPage);
                        if (otherIndex != -1 && otherIndex <= currentIndex)
                        {
                            pages[currentIndex] = otherPage;
                            pages[otherIndex] = currentPage;
                            currentIndex = otherIndex;
                            corrected = true;
                        }
                    }
                }
            }

            return corrected;
        }

        private bool ValidPageOrder(int[] pages, Rules rules)
        {
            for(int iPage = 0; iPage < pages.Length; iPage++)
            {
                int page = pages[iPage];
                if (rules.ContainsKey(page))
                {
                    foreach (int otherPage in rules[page])
                    {
                        int otherIndex = Array.IndexOf(pages, otherPage);
                        if (otherIndex != -1 && otherIndex <= iPage)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
