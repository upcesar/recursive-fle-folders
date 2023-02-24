using System.Collections.Generic;

namespace RecursiveFileFolders.Tests;

public class FileFolderNameGenerator
{
    public IEnumerable<string> GetNames(string prefix, int length) => GetNames(prefix, string.Empty, length);

    public IEnumerable<string> GetNames(string prefix, string suffix, int length)
    {
        for (var i = 1; i <= length; i++)
        {
            yield return $"{prefix}-{Pad(i, length)}{suffix}";
        }
    }
    private string Pad(int num, int padLength) => num.ToString($"D{padLength.ToString().Length}");
}
