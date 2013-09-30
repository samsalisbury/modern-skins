using System.Collections.Generic;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class StyleAutoBundler : DirAutoBundleBase
    {
        public StyleAutoBundler(string stylesDir, IFileSystem fileSystem) : base(stylesDir, fileSystem)
        {
        }

        public IDictionary<string, StyleAutoBundle> GetStyleBundles()
        {
            var bundles = FileSystem.GetFiles(Path).Select(filePath => new StyleAutoBundle(filePath, FileSystem));

            return bundles.ToDictionary(bundle => bundle.Name);
        }
    }
}
