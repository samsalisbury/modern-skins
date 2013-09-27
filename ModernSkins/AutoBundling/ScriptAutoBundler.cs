using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class ScriptAutoBundler
    {
        readonly string _dir;

        public ScriptAutoBundler(string dir)
        {
            _dir = dir;
        }

        public IDictionary<string, ScriptAutoBundle> GetScriptBundles()
        {
            var bundles = Directory.GetFileSystemEntries(_dir).Select(path => new ScriptAutoBundle(path));

            return bundles.ToDictionary(bundle => bundle.Name);
        }
    }
}
