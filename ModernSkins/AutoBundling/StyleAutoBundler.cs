using System;
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

    public class StyleBundleWithSameNameException : Exception
    {
        public StyleBundleWithSameNameException(AutoBundleBase existingBundle, AutoBundleBase newBundle)
            : base(string.Format("Cannot add {0}, there is already a bundle with the same name, from path {1}. " +
            "Each bundle is named after its filename without extension, so if you have a file called " +
            "'mybundle.less' and another called 'mybundle.scss' they will conflict. The exception to this rule " +
            "is CSS files, which will be silently ignored if they conflict. This is to allow tools to generate " +
            "these files at design time without interfering with debugging.",
            newBundle.FileSystemPath, existingBundle.FileSystemPath))
        {
        }
    }
}
