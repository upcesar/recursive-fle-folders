using System;
using System.IO;
using System.Linq;

namespace recursive_file_folders.service_workers;

public static class FileSystemHelper
{
    private const string ALL_FILES = "*.*";
    private const int DEFAULT_INITIAL_DEPTH = 1;

    public static string[] GetAllFiles(string folder) 
        => GetAllFiles(folder, ALL_FILES);
    
    public static string[] GetAllFiles(string folder, string patternSearch) 
        => GetAllFiles(folder, patternSearch, int.MaxValue);
    
    public static string[] GetAllFiles(string folder, int maximumDepth) 
        => GetAllFiles(folder, ALL_FILES, maximumDepth);        
    
    public static string[] GetAllFiles(string folder, string patternSearch, int maximumDepth) 
        => GetAllFiles(folder, patternSearch, DEFAULT_INITIAL_DEPTH, maximumDepth);
    
    private static string[] GetAllFiles(string folder, string patternSearch, int initialDepth, int maximumDepth)
    {
        if (Directory.Exists(folder) && initialDepth <= maximumDepth)
        {
            try
            {
                Func<string, string[]> subFolders = subfolder => 
                    GetAllFiles(subfolder, patternSearch, initialDepth++, maximumDepth);
                
                var childrenFiles = Directory.GetDirectories(folder)
                                             .SelectMany(subFolders);

                return Directory.GetFiles(folder, patternSearch)
                                .Union(childrenFiles)
                                .ToArray();

            }
            catch (UnauthorizedAccessException)
            {
                return new string[] { $"{folder} -> Unauthorized" };
            }

            catch (Exception) { }
        }

        return new string[] { };
    }
}