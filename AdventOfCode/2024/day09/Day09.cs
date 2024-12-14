namespace AOC2024.Day09
{
    internal class Day09
    {
        string path = @"2024\day09\Input.txt";

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
            string diskMap = ReadFile().Trim();
            long currentId = 0;
            List<long> cleanedDiskMap = new List<long>();
            Stack<long> reversed = new Stack<long>();

            for (int i = 0; i < diskMap.Length; i++)
            {
                int blockSize = int.Parse(diskMap[i].ToString());
                if (i % 2 == 0)
                {
                    cleanedDiskMap.AddRange(Enumerable.Repeat(currentId++, blockSize));
                }
                else
                {
                    cleanedDiskMap.AddRange(Enumerable.Repeat((long)-1, blockSize));
                }
            }

            for (int i = 0; i < cleanedDiskMap.Count; i++)
            {
                if (cleanedDiskMap[i] != -1) reversed.Push(cleanedDiskMap[i]);
            }


            while (true)
            {
                long nextId = reversed.Pop();
                int begin = cleanedDiskMap.IndexOf(-1), end = cleanedDiskMap.LastIndexOf(nextId);
                if (begin >= end) break;

                cleanedDiskMap[begin] = cleanedDiskMap[end];
                cleanedDiskMap[end] = -1;
            }

            return cleanedDiskMap.Select((current, index) => 
            {
                if (current == -1) return 0;
                return current * index;
            }).Aggregate((total, next) => total += next);
        }

        public long Part2()
        {
            string diskMap = ReadFile().Trim();
            List<KeyValuePair<long, int>> ids = new List<KeyValuePair<long, int>>();
            long blockId = 0;

            for (int i = 0; i < diskMap.Length; i++)
            {
                int blockSize = int.Parse(diskMap[i].ToString());
                if (blockSize == 0) continue;

                if (i % 2 == 0)
                {
                    ids.Add(new KeyValuePair<long, int>(blockId++, blockSize));
                }
                else
                {
                    ids.Add(new KeyValuePair<long, int>((long)-1, blockSize));
                }
            }

            List<KeyValuePair<long, int>> reversed = ids.AsEnumerable().Reverse().ToList();
            foreach (KeyValuePair<long, int> kvp in reversed)
            {
                if (kvp.Key == -1) continue;

                int iBlock = ids.FindIndex(d => d.Key == kvp.Key);
                KeyValuePair<long, int> block = ids[iBlock];
                int iFreeBlock = ids.FindIndex(d => d.Key == -1 && d.Value >= block.Value);

                if (iFreeBlock == -1 || iFreeBlock >= iBlock) continue; 
                

                KeyValuePair<long, int> freeBlock = ids[iFreeBlock];
                ids.RemoveAt(iBlock);
                ids.Insert(iBlock, new KeyValuePair<long, int>((long)-1, block.Value));
                ids.Insert(iFreeBlock, new KeyValuePair<long, int>(block.Key, block.Value));
                ids[iFreeBlock + 1] = new KeyValuePair<long, int>(freeBlock.Key, freeBlock.Value - block.Value);
            }

            int index = 0;
            long total = 0;
            foreach (var kvp in ids)
            {
                for (int i = 0; i < kvp.Value; i++)
                {
                    if (kvp.Key != -1) total += kvp.Key * index;
                    index++;
                }
            }

            return total;
        }
    }
}
