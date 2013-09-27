using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class StyleAutoBundler
    {
        readonly string _stylesDir;

        public StyleAutoBundler(string stylesDir)
        {
            _stylesDir = stylesDir;
        }

        public IDictionary<string, StyleAutoBundle> GetStyleBundles()
        {
            var bundles = Directory.GetFiles(_stylesDir).Select(filePath => new StyleAutoBundle(filePath));

            return bundles.ToDictionary(bundle => bundle.Name);
        }
    }
}