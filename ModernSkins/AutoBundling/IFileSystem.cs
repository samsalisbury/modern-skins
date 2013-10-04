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
        string GetFileName(string path);
        string ReadFile(string path);
    }
}