using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class StyleBundler
    {
        readonly string _stylesDir;

        public StyleBundler(string stylesDir)
        {
            _stylesDir = stylesDir;
        }

        public IDictionary<string, StyleBundle> GetStyleBundles()
        {
            var bundles = Directory.GetFiles(_stylesDir).Select(filePath => new StyleBundle(filePath));

            return bundles.ToDictionary(bundle => bundle.Name);
        }
    }
}