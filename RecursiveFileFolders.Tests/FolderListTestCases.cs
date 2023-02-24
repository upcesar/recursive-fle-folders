using RecursiveFileFolders.ServiceWorker;
using Xunit;

namespace RecursiveFileFolders.Tests;

public class FolderListTestCases : IClassFixture<FileFolderFixture>
{
    private readonly FileFolderFixture _fixture;

    public FolderListTestCases(FileFolderFixture fixture) => _fixture = fixture;

    [Fact]
    public void CheckFileListTest() 
        => Assert.Equal(FileSystemHelper.GetAllFiles(_fixture.RootTestDirPath), _fixture.ExpectedFiles);
}
