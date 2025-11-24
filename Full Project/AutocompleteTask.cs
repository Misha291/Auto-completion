using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete;

internal class AutocompleteTask
{
    public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return phrases[index];
        return null;
    }

    public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
    {
        var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
        var result = new List<string>();
        for (int i = leftIndex; i < Math.Min(leftIndex + count, rightIndex); i++)
            result.Add(phrases[i]);
        return result.ToArray();
    }

    public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
        return rightIndex - leftIndex;
    }
}

[TestFixture]
public class AutocompleteTests
{
    [Test]
    public void TopByPrefix_IsEmpty_WhenNoPhrases()
    {
        var phrases = new List<string>();
        var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, "pre", 10);
        CollectionAssert.IsEmpty(actualTopWords);
    }

    [Test]
    public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
    {
        var phrases = new List<string> { "apple", "banana", "car" };
        var actualCount = AutocompleteTask.GetCountByPrefix(phrases, "");
        Assert.AreEqual(3, actualCount);
    }

    [Test]
    public void TopByPrefix_ReturnsCorrectWords_WhenPrefixMatches()
    {
        var phrases = new List<string> { "apple", "apply", "application", "banana", "car" };
        var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, "app", 2);
        CollectionAssert.AreEqual(new[] { "apple", "apply" }, actualTopWords);
    }

    [Test]
    public void TopByPrefix_ReturnsAllWords_WhenCountIsLarge()
    {
        var phrases = new List<string> { "apple", "apply", "application" };
        var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, "app", 10);
        CollectionAssert.AreEqual(new[] { "apple", "apply", "application" }, actualTopWords);
    }

    [Test]
    public void CountByPrefix_IsZero_WhenNoMatches()
    {
        var phrases = new List<string> { "apple", "banana", "car" };
        var actualCount = AutocompleteTask.GetCountByPrefix(phrases, "pre");
        Assert.AreEqual(0, actualCount);
    }

    [Test]
    public void CountByPrefix_ReturnsCorrectCount_WhenPrefixMatches()
    {
        var phrases = new List<string> { "apple", "apply", "application", "banana", "car" };
        var actualCount = AutocompleteTask.GetCountByPrefix(phrases, "app");
        Assert.AreEqual(3, actualCount);
    }
}