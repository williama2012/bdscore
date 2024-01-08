using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bds.IO {
    
    public struct FileRemovalRecord {
        public string FileFolderName;
        public long FileFolderSize;
        public string Comment;
    }

    public class FileManager {

        public static IEnumerable<string> WindowsTempDirectories {
            get {
                return new[] { 
                    Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.Machine)
                    , Environment.GetEnvironmentVariable("TMP", EnvironmentVariableTarget.Machine)
                }.Distinct();
            }
        }

        //public static long ClearWindowsTemp(double hoursOld) {
        //    var files = new List<FileInfo>();
        //    foreach (var dir in WindowsTempDirectories) {
        //        files.AddRange(FilesLastWrittenHoursAgo(dir, hoursOld, true));
        //    }
        //    long total = 0;
        //    files.ForEach(file => total += FileDelete(file) ? file.Length : 0);
        //    return total;
        //}
        public static IDictionary<string, long> ClearWindowsTemp(double hoursOld) {
            IDictionary<string, long> dictionary = (IDictionary<string, long>)new Dictionary<string, long>();
            foreach (string windowsTempDirectory in FileManager.WindowsTempDirectories) {



                foreach (FileInfo file in FileManager.FilesLastWrittenHoursAgo(windowsTempDirectory, hoursOld, SearchOption.AllDirectories)) {
                    if (FileManager.FileDelete(file))
                        dictionary.Add(file.FullName, file.Length);
                }
            }
            return dictionary;
        }

        public static bool FileDelete(FileInfo file) {
            try {
                file.Delete();
                return true;
            } catch (IOException) {
                return false;
            } catch (System.Security.SecurityException) {
                return false;
            } catch (UnauthorizedAccessException) {
                return false;
            } catch (Exception) {
                return false;
            }
        }

        public static IEnumerable<FileInfo> FilesLastWrittenHoursAgo(string directory, double hoursOld, SearchOption searchOpt = SearchOption.TopDirectoryOnly) {
            return from file in FilesInDirectory(directory, searchOpt)
                   where file.LastWriteTimeUtc <= DateTime.UtcNow.AddHours(-hoursOld)
                   select file;
        }

        public static IEnumerable<FileInfo> FilesInDirectory(string directory, SearchOption searchOpt = SearchOption.TopDirectoryOnly) {
            try {
                var dir = new DirectoryInfo(directory);
                if (dir.Exists) {
                    return DirectoryInfoExtensions.EnumerateFiles(directory, "*", SearchOption.AllDirectories).Select(f => new FileInfo(f));
                }
            } catch (UnauthorizedAccessException uae) {
                return Enumerable.Empty<FileInfo>();
            } catch (Exception) {
                return null;
            }
            return null;
        }

        public static IEnumerable<FileInfo> FilesInDirectory(DirectoryInfo directory, string searchPattern = "*", SearchOption searchOpt = SearchOption.TopDirectoryOnly) {
            try {
                return directory.EnumerateFiles(searchPattern, searchOpt);
            } catch (UnauthorizedAccessException uae) {
                return Enumerable.Empty<FileInfo>();
            } catch (Exception e) {
                return Enumerable.Empty<FileInfo>();
            }
        }

        public static string ReadFile(string fileName) {
            if (!File.Exists(fileName)) throw new InvalidOperationException("File not found.");
            var fileBody = File.ReadAllText(fileName,Encoding.Default);
            return fileBody;
        }

    }

    public static class DirectoryInfoExtensions {
        public static IEnumerable<FileInfo> SafeFileEnum(this DirectoryInfo directory, string searchPattern = "*", SearchOption searchOpt = SearchOption.TopDirectoryOnly) {
            var fileList = Enumerable.Empty<FileInfo>();
            
            if(searchOpt == SearchOption.AllDirectories) {
                var dirs = directory.EnumerateDirectories();

            }



            return Enumerable.Empty<FileInfo>();
        }

        public static IEnumerable<string> EnumerateDirectories(string parentDirectory, string searchPattern, SearchOption searchOpt) {
            try {
                var directories = Enumerable.Empty<string>();
                if (searchOpt == SearchOption.AllDirectories) {


                    directories = Directory.EnumerateDirectories(parentDirectory)
                        .SelectMany(x => EnumerateDirectories(x, searchPattern, searchOpt));
                }
                return directories.Concat(Directory.EnumerateDirectories(parentDirectory, searchPattern));
            } catch (UnauthorizedAccessException ex) {
                return Enumerable.Empty<string>();
            }
        }

        public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOpt) {
            try {
                var dirFiles = Enumerable.Empty<string>();
                if (searchOpt == SearchOption.AllDirectories) {
                    dirFiles = Directory.EnumerateDirectories(path)
                                        .SelectMany(x => EnumerateFiles(x, searchPattern, searchOpt));
                }
                return dirFiles.Concat(Directory.EnumerateFiles(path, searchPattern));
            } catch (UnauthorizedAccessException ex) {
                return Enumerable.Empty<string>();
            }
        }


    }
}
