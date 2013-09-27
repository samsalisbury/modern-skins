using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class ScriptAutoBundler
    {
        readonly string _dir;
        readonly IFileSystem _fileSystem;

        public ScriptAutoBundler(string dir, IFileSystem fileSystem)
        {
            _dir = dir;
            _fileSystem = fileSystem;
        }

        public IDictionary<string, ScriptAutoBundle> GetScriptBundles()
        {
            var bundles = Directory.GetFileSystemEntries(_dir).Select(path => new ScriptAutoBundle(path, _fileSystem));

            return bundles.ToDictionary(bundle => bundle.Name);
        }
    }
}
