using System.IO;
using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public class ScriptAutoBundle : AutoBundleBase, IRepresentAnActualBundle
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

        public Bundle ToBundle(string skinsPath)
        {
            var bundle = new ScriptBundle(VirtualPath(skinsPath));

            if (FileSystem.DirExists(FileSystemPath))
            {
                bundle.IncludeDirectory(VirtualPath(skinsPath), "*.js");
            }
            else
            {
                bundle.Include(VirtualPath(skinsPath));
            }

            return bundle;
        }
    }
}
