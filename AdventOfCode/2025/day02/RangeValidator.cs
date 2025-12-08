namespace AOC2025.Day02;

internal class RangeValidator
{
    private long _from;
    private long _to;

    public RangeValidator(long from, long to)
    {
        _from = from;
        _to = to;
    }

    public IEnumerable<long> FindInvalidIds(bool isPart2 = false)
    {
        for (long from = _from; from <= _to; from++)
        {
            var sFrom = from.ToString();
            var match = isPart2 
                ? SequenceRepeatedAtLeastTwice(sFrom)
                : SequenceRepeatedTwice(sFrom);

            if (match) yield return from;
        }
    }

    public override string ToString()
    {
        return $"{_from}-{_to}";
    }

    private bool SequenceRepeatedTwice(string s)
    {
        int mid = s.Length / 2;
        return s.Substring(0, mid) == s.Substring(mid);
    }

    private bool SequenceRepeatedAtLeastTwice(string s)
    {
        int n = s.Length;
        for (int len = 1; len <= n / 2; len++)
        {
            if (n % len != 0) continue;

            string pattern = s.Substring(0, len);
            bool ok = true;

            for (int i = len; i < n; i += len)
            {
                if (s.Substring(i, len) != pattern)
                {
                    ok = false;
                    break;
                }
            }

            if (ok) return true;
        }

        return false;
    }
}
