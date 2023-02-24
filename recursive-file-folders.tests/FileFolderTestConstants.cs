using System.IO;

namespace recursive_file_folders.tests;

public static class FileFolderTestConstants
{
    public const int MaxFolderDepth = 4;
    public const int MaxFiles = 4;
    public const string RootDirectoryName = "testRootDir";
    public const string TestSubDirectoryName = "subdir-test";
    public static string CurrentDirectory => Directory.GetCurrentDirectory();
}
