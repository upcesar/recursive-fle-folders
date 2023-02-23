using recursive_file_folders.service_workers;
using Xunit;

namespace recursive_file_folders.tests;

public class FolderListTestCases : IClassFixture<FileFolderFixture>
{
    private readonly FileFolderFixture _fixture;

    public FolderListTestCases(FileFolderFixture fixture) => _fixture = fixture;

    [Fact]
    public void CheckFileListTest() 
        => Assert.Equal(FileSystemHelper.GetAllFiles(_fixture.RootTestDirPath), _fixture.ExpectedFiles);
}
