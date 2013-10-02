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

        public Bundle ToBundle(string appPath)
        {
            var bundle = new ScriptBundle(VirtualPath(appPath));

            if (FileSystem.DirExists(FileSystemPath))
            {
                bundle.IncludeDirectory(VirtualPath(appPath), "*.js");
            }
            else
            {
                bundle.Include(VirtualPath(appPath));
            }

            return bundle;
        }

        public string CalculatedVirtualPath { get; set; }
    }
}
