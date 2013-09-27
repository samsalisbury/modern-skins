using System.IO;

namespace ModernSkins.AutoBundling
{
    public abstract class DirAutoBundleBase : AutoBundleBase
    {
        protected DirAutoBundleBase(string path) : base(path)
        {
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException(path);
            }
        }

        protected string SubPath(string subPath)
        {
            return System.IO.Path.Combine(Path, subPath);
        }
    }
}