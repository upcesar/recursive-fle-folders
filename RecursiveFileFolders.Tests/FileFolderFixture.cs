using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecursiveFileFolders.Tests;

public class FileFolderFixture : IDisposable
{
    private bool _disposedValue;
    private readonly FileFolderNameGenerator _generator = new();
    public string[] ExpectedFiles { get; private set; }
    public string ValidRootTestDirPath { get; private set; }
    public string InvalidRootTestDirPath => @$"c:\fakedir-{DateTime.Now.Ticks}";
    public int DepthOneSubFolder => 1;
    public int ExpectedQuantityFilesOneDepth => 4;

    public FileFolderFixture()
    {
        var rootTestDir = PrepareRootDirectory();
        CreateTestFileOnDirectory(rootTestDir);
    }

    private DirectoryInfo PrepareRootDirectory()
    {
        var curDir = new DirectoryInfo(FileFolderTestConstants.CurrentDirectory);
        var rootTestDir = curDir.CreateSubdirectory(FileFolderTestConstants.RootDirectoryName);
        ValidRootTestDirPath = rootTestDir.FullName;
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
        foreach (var directory in _generator.GetNames(FileFolderTestConstants.TestSubDirectoryName, FileFolderTestConstants.MaxFolderDepth))
        {
            var subDirectoryInfo = rootTestDir.CreateSubdirectory(directory);
            var expectedFiles = CreateExpectedTestFiles(subDirectoryInfo);
            ExpectedFiles = ExpectedFiles?.Union(expectedFiles).ToArray() ?? expectedFiles.ToArray();
        }
    }

    private IEnumerable<string> CreateExpectedTestFiles(DirectoryInfo subdir)
    {
        var files = _generator.GetNames("file", ".txt", FileFolderTestConstants.MaxFiles);
        return files.Select(file => CreateTestFile(subdir.FullName, file));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                Directory.Delete(ValidRootTestDirPath, true);
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
