using System.IO;

namespace RecursiveFileFolders.Tests;

public static class FileFolderTestConstants
{
    public const int MaxFolderDepth = 4;
    public const int MaxFiles = 4;
    public const string RootDirectoryName = "testRootDir";
    public const string TestSubDirectoryName = "subdir-test";
    public static string CurrentDirectory => Directory.GetCurrentDirectory();
}
