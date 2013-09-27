using System.IO;

namespace ModernSkins.AutoBundling
{
    public abstract class FileAutoBundleBase : AutoBundleBase
    {
        protected FileAutoBundleBase(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
            if (!fileSystem.FileExists(path))
            {
                throw new FileNotFoundException(path);
            }
        }
    }
}
