using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins
{
    public class ScriptBundler
    {
        readonly string _dir;

        public ScriptBundler(string dir)
        {
            _dir = dir;
        }

        public IDictionary<string, ScriptBundle> GetScriptBundles()
        {
            var bundles = Directory.GetFileSystemEntries(_dir).Select(path => new ScriptBundle(path));

            return bundles.ToDictionary(bundle => bundle.Name);
        }
    }

    public class ScriptBundle
    {
        public string BundlePath { get; set; }
        public string Name { get; set; }

        public ScriptBundle(string path)
        {
            BundlePath = path;

            Name = Path.GetFileNameWithoutExtension(path);
        }
    }
}
