using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (left >= right - 1)
                return left;

            int middle = (left + right) / 2;
            if (string.Compare(phrases[middle], prefix, StringComparison.InvariantCultureIgnoreCase) < 0)
                return GetLeftBorderIndex(phrases, prefix, middle, right);
            return GetLeftBorderIndex(phrases, prefix, left, middle);
        }
    }
}