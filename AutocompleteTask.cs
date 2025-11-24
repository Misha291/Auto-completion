using System;
using System.Collections.Generic;

namespace Autocomplete;

internal class AutocompleteTask
{
    public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;

        if (startIndex < phrases.Count &&
            phrases[startIndex].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return phrases[startIndex];

        return null;
    }

    public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
    {
        var startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        var endIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);

        var result = new List<string>();

        for (var i = startIndex; i < Math.Min(startIndex + count, endIndex); i++)
            result.Add(phrases[i]);

        return result.ToArray();
    }

    public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var startIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        var endIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);

        return endIndex - startIndex;
    }
}
