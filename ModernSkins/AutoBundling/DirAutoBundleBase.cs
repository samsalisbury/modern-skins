using System.IO;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public abstract class DirAutoBundleBase : AutoBundleBase
    {
        protected DirAutoBundleBase(string path, IFileSystem fileSystem) : base(path, fileSystem)
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

        protected bool SubPathExists(string subPath)
        {
            return Directory.EnumerateFileSystemEntries(Path).Contains(SubPath(subPath));
        }
    }
}