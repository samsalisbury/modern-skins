using System.Collections.Generic;
using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class StylesDirAutoBundle : DirAutoBundleBase
    {
        public StylesDirAutoBundle(string stylesDir, IFileSystem fileSystem) : base(stylesDir, fileSystem)
        {
        }

        public IDictionary<string, StyleAutoBundle> GetStyleBundles()
        {
            var bundleDictionary = new Dictionary<string, StyleAutoBundle>();

            AddBundlesToDictionary(bundleDictionary, ".less");
            AddBundlesToDictionary(bundleDictionary, ".scss");
            AddBundlesToDictionary(bundleDictionary, ".sass");
            AddBundlesToDictionary(bundleDictionary, ".css", false);

            return bundleDictionary;
        }

        void AddBundlesToDictionary(Dictionary<string, StyleAutoBundle> bundleDictionary, string extension, bool throwOnDupe = true)
        {
            var scssFiles = FileSystem.GetFiles(FileSystemPath).Where(name => name.EndsWith(extension));
            var scssBundles = scssFiles.Select(filePath => new StyleAutoBundle(filePath, FileSystem));
            foreach (var bundle in scssBundles)
            {
                if (bundleDictionary.ContainsKey(bundle.Name))
                {
                    if (throwOnDupe)
                    {
                        throw new StyleBundleWithSameNameException(bundleDictionary[bundle.Name], bundle);
                    }
                }
                else
                {
                    bundleDictionary.Add(bundle.Name, bundle);
                }
            }
        }
    }
}
