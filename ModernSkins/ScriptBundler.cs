using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

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
            return null;
        }
    }

    public class ScriptBundle
    {
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
