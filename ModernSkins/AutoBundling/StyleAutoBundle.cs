using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public class StyleAutoBundle : FileAutoBundleBase, IRepresentAnActualBundle
    {
        public StyleAutoBundle(string filePath, IFileSystem fileSystem) : base(filePath, fileSystem)
        {
        }

        public Bundle ToBundle(string appPath)
        {
            return new StyleBundle(VirtualPath(appPath));
        }

        public string[] ListFilesToBundle()
        {
            return new[] {FileSystemPath};
        }
    }
}
