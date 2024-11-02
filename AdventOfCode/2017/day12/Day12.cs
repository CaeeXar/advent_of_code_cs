using System.Collections.Generic;

namespace AOC2017.Day12
{
    internal class Day12
    {
        string path = @"2017\day12\Input.txt";

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
            Dictionary<int, Program> programs = CreatePrograms(ReadFile());
            return ContainsProgram(programs, 0);
        }

        public int Part2()
        {
            Dictionary<int, Program> programs = CreatePrograms(ReadFile());
            HashSet<List<int>> groups = FindGroups(programs);          
            return groups.Count;
        }

        private Dictionary<int, Program> CreatePrograms(string input)
        {
            string[] lines = input.Split("\n");
            Dictionary<int, Program> programs = new Dictionary<int, Program>();
            foreach (string line in lines)
            {
                string[] data = line.Split("<->");
                int ID = int.Parse(data[0].Trim());
                int[] neighbours = data[1].Split(",").Select(d => int.Parse(d.Trim())).ToArray();

                if (!programs.ContainsKey(ID))
                {
                    programs.Add(ID, new Program(ID, neighbours));
                }
            }

            return programs;
        }

        private int ContainsProgram(Dictionary<int, Program> programs, int sID)
        {
            int group = 0;
            foreach (int pID in programs.Keys)
            {
                List<int> visited = new List<int>();
                List<int> queue = new List<int>() { pID };

                while (queue.Count > 0)
                {
                    visited.Add(queue[0]);
                 
                    // check if contains ID
                    if (queue[0] == sID || programs[queue[0]].Neighbours.Contains(sID))
                    {
                        group++;
                        break;
                    }

                    // enqueue others
                    foreach (int nID in programs[queue[0]].Neighbours)
                    {
                        if (!visited.Contains(nID)) queue.Add(nID);
                    }

                    queue.RemoveAt(0);
                }
            }

            return group;
        }

        private HashSet<List<int>> FindGroups(Dictionary<int, Program> programs)
        {
            HashSet<List<int>> groups = new HashSet<List<int>>(new ListComparer());
            foreach (int pID in programs.Keys)
            {
                List<int> visited = new List<int>();
                List<int> queue = new List<int>() { pID };
                List<int> group = new List<int>() { pID };

                while (queue.Count > 0)
                {
                    visited.Add(queue[0]);

                    foreach (int nID in programs[queue[0]].Neighbours)
                    {
                        if (!visited.Contains(nID))
                        { 
                            queue.Add(nID);
                        }

                        if(!group.Contains(nID))
                        {
                            group.Add(nID);
                        }
                    }

                    queue.RemoveAt(0);
                }

                groups.Add(group.OrderBy(d => d).ToList());
            }

            return groups;
        }
    }

}
