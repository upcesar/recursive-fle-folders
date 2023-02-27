using RecursiveFileFolders.ServiceWorker;
using FluentAssertions;
using Xunit;
using System;

namespace RecursiveFileFolders.Tests;

public class FolderListTestCases : IClassFixture<FileFolderFixture>
{
    private readonly FileFolderFixture _fixture;

    public FolderListTestCases(FileFolderFixture fixture) => _fixture = fixture;

    [Fact]
    public void CheckFileListTest_ShouldReturnFileList()
        => FileSystemHelper.GetAllFiles(_fixture.RootTestDirPath)
                           .Should()
                           .Equal(_fixture.ExpectedFiles);

    [Fact]
    public void CheckFileListTest_ShouldReturnEmptyList()
        => FileSystemHelper.GetAllFiles(@$"c:\fakedir-{DateTime.Now.Ticks}")
                           .Should()
                           .BeEmpty();
}
