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
                // TODO: IncludeDirectory needs to be passed a virtual path relative to the application
                // Rather than relative to the Skins directory, as is done presently.
                // Sort it!

                bundle.IncludeDirectory(VirtualPath(appPath), "*.js");
            }
            else
            {
                bundle.Include(VirtualPath(appPath));
            }

            return bundle;
        }
    }
}
