using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public class StyleAutoBundle : FileAutoBundleBase
    {
        public StyleAutoBundle(string filePath, IFileSystem fileSystem) : base(filePath, fileSystem)
        {
        }

        public StyleBundle ToStyleBundle(string skinsPath)
        {
            return new StyleBundle(VirtualPath(skinsPath));
        }

        public string[] ListFilesToBundle()
        {
            return new[] {FileSystemPath};
        }
    }
}