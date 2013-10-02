namespace ModernSkins.AutoBundling
{
    public class StyleAutoBundle : FileAutoBundleBase, IRepresentAnActualBundle
    {
        public StyleAutoBundle(string filePath, IFileSystem fileSystem) : base(filePath, fileSystem)
        {
        }

        public BundleStub ToBundle(string appPath, string skinsPath)
        {
            return new StyleBundleStub
                   {
                       VirtualUrl = VirtualPath(skinsPath),
                       AppRelativeContentPaths = new[] {VirtualPathWithExtension(appPath)}
                   };
        }

        public string CalculatedVirtualPath { get; set; }

        public string[] ListFilesToBundle()
        {
            return new[] {FileSystemPath};
        }
    }
}
