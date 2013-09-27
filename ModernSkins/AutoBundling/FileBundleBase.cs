using System.IO;

namespace ModernSkins.AutoBundling
{
    public abstract class FileBundleBase : BundleBase
    {
        protected FileBundleBase(string path) : base(path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
        }
    }
}