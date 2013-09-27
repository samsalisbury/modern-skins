using System.Collections.Generic;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class AutoBundler
    {
        readonly string _skinsDir;
        readonly IFileSystem _fileSystem;

        public AutoBundler(string skinsDir, IFileSystem fileSystem)
        {
            _skinsDir = skinsDir;
            _fileSystem = fileSystem;
        }

        public IDictionary<string, Skin> EnumerateSkins()
        {
            var skins = _fileSystem.GetDirectories(_skinsDir).Select(dir => new Skin(dir, _fileSystem));

            return skins.ToDictionary(skin => skin.Name);
        }
    }
}
