using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins.AutoBundling
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
            var skins = Directory.GetDirectories(_skinsDir).Select(dir => new Skin(dir));

            return skins.ToDictionary(skin => skin.Name);
        }
    }
}
