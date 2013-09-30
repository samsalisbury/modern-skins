using System.IO;

namespace ModernSkins.AutoBundling
{
    public interface IFileSystem
    {
        bool FileExists(string path);
        bool DirExists(string path);
        char DirSeparator { get; }
        string[] GetDirectories(string path);
        string[] GetFiles(string path);
        string[] GetFileSystemEntries(string path);
        string CombinePaths(string path, string subPath);
    }

    public class FileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public bool DirExists(string path)
        {
            return Directory.Exists(path);
        }

        public char DirSeparator { get { return Path.DirectorySeparatorChar; } }
        public string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public string[] GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public string[] GetFileSystemEntries(string path)
        {
            return Directory.GetFileSystemEntries(path);
        }

        public string CombinePaths(string path, string subPath)
        {
            return Path.Combine(path, subPath);
        }
    }
}
