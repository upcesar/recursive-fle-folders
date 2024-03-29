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
    {
        var files = FileSystemHelper.GetAllFiles(_fixture.ValidRootTestDirPath);
        files.Should().Equal(_fixture.ExpectedFiles);
    }

    [Fact]
    public void CheckInvalidRootFolderTest_ShouldReturnEmptyList()
    {
        var files = FileSystemHelper.GetAllFiles(_fixture.InvalidRootTestDirPath);
        files.Should().BeEmpty();
    }

    [Fact]
    public void CheckFileListWithOneDepthTest_ShouldReturnFourElements()
    {
        var files = FileSystemHelper.GetAllFiles(_fixture.ValidRootTestDirPath, _fixture.DepthOneSubFolder);
        files.Should().HaveCount(_fixture.ExpectedQuantityFilesOneDepth);
    }

    [Fact]
    public void CheckFileListWithFiltersTest_ShouldReturnEmpty()
    {
        var files = FileSystemHelper.GetAllFiles(_fixture.ValidRootTestDirPath, _fixture.InvalidFilePattern);
        files.Should().BeEmpty();
    }
}
