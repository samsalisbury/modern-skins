using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public class StyleAutoBundle : FileAutoBundleBase, IRepresentAnActualBundle
    {
        public StyleAutoBundle(string filePath, IFileSystem fileSystem) : base(filePath, fileSystem)
        {
        }

        public Bundle ToBundle(string skinsPath)
        {
            return new StyleBundle(VirtualPath(skinsPath));
        }

        public string[] ListFilesToBundle()
        {
            return new[] {FileSystemPath};
        }
    }

    public interface IRepresentAnActualBundle
    {
        Bundle ToBundle(string skinsPath);
    }
}