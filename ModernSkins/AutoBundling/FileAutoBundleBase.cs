using System.IO;

namespace ModernSkins.AutoBundling
{
    public abstract class FileAutoBundleBase : AutoBundleBase
    {
        protected FileAutoBundleBase(string path) : base(path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
        }
    }
}