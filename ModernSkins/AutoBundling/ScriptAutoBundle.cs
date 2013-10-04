using System.IO;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    // TODO: There is a distinction between "virtual source paths" and "virtual urls" that
    // needs to be a lot clearer in the code. This would also make it easier to push refs to
    // skins path and path up away from the leaf nodes in the object graph, where they're causing mess.

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

        public BundleStub ToBundle(string appPath, string skinsPath)
        {
            var bundle = new ScriptBundleStub {VirtualUrl = VirtualPath(skinsPath)};

            if (FileSystem.DirExists(FileSystemPath))
            {
                var fileNames = FileSystem.GetFiles(FileSystemPath).Select(FileSystem.GetFileName);

                bundle.AppRelativeContentPaths = fileNames.Select(name => VirtualPath(appPath) + '/' + name).ToArray();
            }
            else
            {
                bundle.AppRelativeContentPaths = new[] {VirtualPathWithExtension(appPath)};
            }

            return bundle;
        }

        public string CalculatedVirtualPath { get; set; }

        public string ThirdPartyCdnUrl { get; set; }

        public string CdnFallbackExpression { get; set; }
    }
}
