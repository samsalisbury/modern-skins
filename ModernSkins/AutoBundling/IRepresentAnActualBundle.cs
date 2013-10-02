using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public interface IRepresentAnActualBundle
    {
        Bundle ToBundle(string appPath);
        string CalculatedVirtualPath { get; set; }
        string VirtualPath(string appPath);
        string Name { get; }
    }
}
