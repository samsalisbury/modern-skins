namespace ModernSkins.AutoBundling
{
    public interface IRepresentAnActualBundle
    {
        BundleStub ToBundle(string appPath, string virtualPath);
        string CalculatedVirtualPath { get; set; }
        string VirtualPath(string appPath);
        string Name { get; }
    }
}
