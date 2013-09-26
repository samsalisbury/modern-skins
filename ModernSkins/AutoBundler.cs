using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins
{
    public class AutoBundler
    {
        readonly string _skinsDir;

        public AutoBundler(string skinsDir)
        {
            _skinsDir = skinsDir;
        }

        public IDictionary<string, Skin> EnumerateSkins()
        {
            var skinNames = Directory.GetDirectories(_skinsDir).Select(Path.GetFileName).ToList();

            return skinNames.ToDictionary(
                name => name,
                name => new Skin {Name = name, Dir = Path.Combine(_skinsDir, name)});
        }
    }
}
