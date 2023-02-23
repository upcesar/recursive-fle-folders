using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace recursive_file_folders.tests
{
    public class FileFolderFixture : IDisposable
    {
        private bool _disposedValue;
        private readonly IList<string> _files;
        
        public string[] ExpectedFiles { get; private set; }

        public string RootTestDirPath { get; private set; }

        public FileFolderFixture()
        {
            _files = new List<string>();

            var rootTestDir = PrepareRootDirectory();
            
            CreateTestSubdirectory(rootTestDir, 4);

            ExpectedFiles = _files.ToArray();
        }
        
        private DirectoryInfo PrepareRootDirectory()
        {
            var curDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            var rootTestDir = curDir.CreateSubdirectory("testRootDir");

            RootTestDirPath = rootTestDir.FullName;

            return rootTestDir;
        }

        private void CreateTestSubdirectory(DirectoryInfo rootTestDir, int maxSubdir)
        {
            for (var i = 1; i <= maxSubdir; i++)
            {
                var subdir = rootTestDir.CreateSubdirectory($"subdir-test-{Pad(i, maxSubdir)}");

                CreateTestFiles(subdir, 4);
            }
        }

        private void CreateTestFiles(DirectoryInfo subdir, int maxFiles)
        {
            for (var i = 1; i <= maxFiles; i++)
            {
                var fileNum = Pad(i, maxFiles);
                var fileName = Path.Combine(subdir.FullName, $"file{fileNum}.txt");
                File.WriteAllText(fileName, $"Content of file{fileNum}");
                _files.Add(fileName);
            }
        }

        private string Pad(int num, int padLength) => num.ToString($"D{padLength.ToString().Length}");
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    foreach (var file in ExpectedFiles)
                        File.Delete(file);                    

                    foreach (var dir in Directory.GetDirectories(RootTestDirPath))
                        Directory.Delete(dir);

                    Directory.Delete(RootTestDirPath);
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
}
