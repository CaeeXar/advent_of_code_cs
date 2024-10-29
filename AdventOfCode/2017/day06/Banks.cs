namespace AOC2017.Day06
{
    class Banks
    {
        public Banks(int[] banks)
        {
            this.Data = banks;
        }

        public int[] Data { get; set; }

        public int FirstLoop { get; set; }

        public int SecondLoop { get; set; }

        public int GetIndexOfMax()
        {
            return Array.IndexOf(this.Data, this.Data.Max());
        }

        public string GetID() 
        {    
            return string.Join(",", this.Data);
        }

        public Banks GetNext()
        {
            Banks next = new Banks(this.Data);
            int max = next.GetIndexOfMax(),
                amount = next.Data[max],
                distributed = 0;

            next.Data[max] = 0;

            for (int i = max + 1; distributed < amount; i++)
            {
                next.Data[i % next.Data.Length]++;
                distributed++;
            }

            return next;
        }
    }
}
