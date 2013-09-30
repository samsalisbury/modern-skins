using System.IO;

namespace ModernSkins.AutoBundling
{
    public class ScriptAutoBundle : AutoBundleBase
    {
        public ScriptAutoBundle(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
            if (!fileSystem.FileExists(path) && !fileSystem.DirExists(path))
            {
                throw new FileNotFoundException(
                    string.Format("Could not find either a file or directory at {0}", path),
                    path);
            }
        }

        public string[] ListFilesToBundle()
        {
            return FileSystem.FileExists(FileSystemPath) ? new[] {FileSystemPath} : FileSystem.GetFiles(FileSystemPath);
        }
    }
}
