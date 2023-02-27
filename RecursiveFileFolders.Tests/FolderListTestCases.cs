using RecursiveFileFolders.ServiceWorker;
using FluentAssertions;
using Xunit;

namespace RecursiveFileFolders.Tests;

public class FolderListTestCases : IClassFixture<FileFolderFixture>
{
    private readonly FileFolderFixture _fixture;

    public FolderListTestCases(FileFolderFixture fixture) => _fixture = fixture;

    [Fact]
    public void CheckFileListTest_ShouldReturnFileList()
        => FileSystemHelper.GetAllFiles(_fixture.ValidRootTestDirPath)
                           .Should()
                           .Equal(_fixture.ExpectedFiles);

    [Fact]
    public void CheckFileListTest_ShouldReturnEmptyList()
        => FileSystemHelper.GetAllFiles(_fixture.InvalidRootTestDirPath)
                           .Should()
                           .BeEmpty();
}
