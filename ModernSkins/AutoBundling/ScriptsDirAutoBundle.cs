using System.Collections.Generic;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class ScriptsDirAutoBundle : DirAutoBundleBase
    {
        public ScriptsDirAutoBundle(string dir, IFileSystem fileSystem) : base(dir, fileSystem)
        {
        }

        public IDictionary<string, ScriptAutoBundle> GetScriptBundles()
        {
            var bundles = FileSystem.GetFileSystemEntries(FileSystemPath).Select(path => new ScriptAutoBundle(path, FileSystem));

            return bundles.ToDictionary(bundle => bundle.Name);
        }
    }
}
