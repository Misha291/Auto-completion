using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            while (left < right - 1)
            {
                int middle = (left + right) / 2;
                int comparison = string.Compare(phrases[middle], prefix, StringComparison.InvariantCultureIgnoreCase);

                if (comparison <= 0 || phrases[middle].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                    left = middle;
                else
                    right = middle;
            }
            return right;
        }
    }
}
