using System.Collections.Generic;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class ScriptAutoBundler : DirAutoBundleBase
    {
        public ScriptAutoBundler(string dir, IFileSystem fileSystem) : base(dir, fileSystem)
        {
        }

        public IDictionary<string, ScriptAutoBundle> GetScriptBundles()
        {
            var bundles = FileSystem.GetFileSystemEntries(Path).Select(path => new ScriptAutoBundle(path, FileSystem));

            return bundles.ToDictionary(bundle => bundle.Name);
        }
    }
}
