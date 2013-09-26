using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

            return skinNames.ToDictionary(name => name, name => new Skin {Name = name, Dir = Path.Combine(_skinsDir, name)});
        }
    }

    public class Skin
    {
        public string Name { get; set; }
        public string Dir { get; set; }

        public string StyleDir
        {
            get { return Path.Combine(Dir, "styles"); }
        }

        public IDictionary<string, StyleBundle> EnumerateStyleBundles()
        {
            var files = Directory.GetFiles(StyleDir).Select(Path.GetFileName);

            return files.ToDictionary(Path.GetFileNameWithoutExtension,
                file => new StyleBundle
                        {
                            Name = Path.GetFileNameWithoutExtension(file),
                            FileName = file
                        });
        }
    }

    public class StyleBundle
    {
        public string Name { get; set; }
        public string FileName { get; set; }
    }
}
