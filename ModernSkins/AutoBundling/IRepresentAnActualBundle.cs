using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public interface IRepresentAnActualBundle
    {
        Bundle ToBundle(string skinsPath);
        string VirtualPath(string skinsPath);
        string Name { get; }
    }
}