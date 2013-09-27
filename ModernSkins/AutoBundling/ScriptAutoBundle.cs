using System.IO;

namespace ModernSkins.AutoBundling
{
    public class ScriptAutoBundle : AutoBundleBase
    {
        public ScriptAutoBundle(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
            if (!File.Exists(path) && !Directory.Exists(path))
            {
                throw new FileNotFoundException(
                    string.Format("Could not find either a file or directory at {0}", path),
                    path);
            }
        }
    }
}
