using System.IO;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public abstract class DirAutoBundleBase : AutoBundleBase
    {
        protected DirAutoBundleBase(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
            if (!FileSystem.DirExists(path))
            {
                throw new DirectoryNotFoundException(path);
            }
        }

        protected string SubPath(string subPath)
        {
            return FileSystem.CombinePaths(Path, subPath);
        }

        protected bool SubPathExists(string subPath)
        {
            return FileSystem.GetFileSystemEntries(Path).Contains(SubPath(subPath));
        }
    }
}