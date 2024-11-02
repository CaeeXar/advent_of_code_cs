namespace AOC2017.Day12
{
    internal class ListComparer : IEqualityComparer<List<int>>
    {
        public bool Equals (List<int> x, List<int> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode (List<int> obj)
        {
            return obj.Aggregate(19, (current, item) => current ^ item.GetHashCode());
        }
    }
}
