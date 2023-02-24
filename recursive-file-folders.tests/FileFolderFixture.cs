using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace recursive_file_folders.tests;

public class FileFolderFixture : IDisposable
{
    private bool _disposedValue;
    public string[] ExpectedFiles { get; private set; }
    public string RootTestDirPath { get; private set; }

    public FileFolderFixture()
    {
        var rootTestDir = PrepareRootDirectory();
        CreateTestFileOnDirectory(rootTestDir);
    }

    private DirectoryInfo PrepareRootDirectory()
    {
        var curDir = new DirectoryInfo(FileFolderTestConstants.CurrentDirectory);
        var rootTestDir = curDir.CreateSubdirectory(FileFolderTestConstants.RootDirectoryName);
        RootTestDirPath = rootTestDir.FullName;
        return rootTestDir;
    }

    private string CreateTestFile(string directoryName, string fileName)
    {
        var path = Path.Combine(directoryName, fileName);
        File.WriteAllText(path, $"Content of {fileName}");
        return path;
    }

    private void CreateTestFileOnDirectory(DirectoryInfo rootTestDir)
    {
        foreach (var directory in GetNames(FileFolderTestConstants.TestSubDirectoryName, FileFolderTestConstants.MaxFolderDepth))
        {
            var subDirectoryInfo = rootTestDir.CreateSubdirectory(directory);
            var expectedFiles = CreateExpectedTestFiles(subDirectoryInfo);
            ExpectedFiles = ExpectedFiles?.Union(expectedFiles).ToArray() ?? expectedFiles.ToArray();
        }
    }

    private IEnumerable<string> CreateExpectedTestFiles(DirectoryInfo subdir)
    {
        var files = GetNames("file", ".txt", FileFolderTestConstants.MaxFiles);
        return files.Select(file => CreateTestFile(subdir.FullName, file));
    }

    private string Pad(int num, int padLength) => num.ToString($"D{padLength.ToString().Length}");

    private IEnumerable<string> GetNames(string prefix, int length) => GetNames(prefix, string.Empty, length);

    private IEnumerable<string> GetNames(string prefix, string suffix, int length)
    {
        for (var i = 1; i <= length; i++)
        {
            yield return $"{prefix}-{Pad(i, length)}{suffix}";
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                Directory.Delete(RootTestDirPath, true);
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~FileFolderFixture()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
