using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins
{
    public class StyleBundle
    {
        public string Name { get; set; }
        public string FileName { get; set; }
    }

    public class StyleBundler
    {
        readonly string _stylesDir;

        public StyleBundler(string stylesDir)
        {
            _stylesDir = stylesDir;
        }

        public IDictionary<string, StyleBundle> GetStyleBundles()
        {
            var files = Directory.GetFiles(_stylesDir).Select(Path.GetFileName);

            return files.ToDictionary(Path.GetFileNameWithoutExtension,
                file => new StyleBundle
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    FileName = file
                });
        }
    }
}