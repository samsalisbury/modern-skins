using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class StyleAutoBundler
    {
        readonly string _stylesDir;
        readonly IFileSystem _fileSystem;

        public StyleAutoBundler(string stylesDir, IFileSystem fileSystem)
        {
            _stylesDir = stylesDir;
            _fileSystem = fileSystem;
        }

        public IDictionary<string, StyleAutoBundle> GetStyleBundles()
        {
            var bundles = Directory.GetFiles(_stylesDir).Select(filePath => new StyleAutoBundle(filePath, _fileSystem));

            return bundles.ToDictionary(bundle => bundle.Name);
        }
    }
}
