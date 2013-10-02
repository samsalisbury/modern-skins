using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public class SkinsDirAutoBundle
    {
        readonly string _appPath;
        readonly string _skinsDir;
        readonly IFileSystem _fileSystem;

        public SkinsDirAutoBundle(string skinsDir, string appPath) : this(skinsDir, appPath, new TheLocalFileSystem())
        {
        }

        public SkinsDirAutoBundle(string skinsDir, string appPath, IFileSystem fileSystem)
        {
            _skinsDir = skinsDir;
            _appPath = appPath;
            _fileSystem = fileSystem;
        }

        public IDictionary<string, SkinAutoBundle> EnumerateSkins()
        {
            var skins = _fileSystem.GetDirectories(_skinsDir).Select(dir => new SkinAutoBundle(dir, _fileSystem));

            return skins.ToDictionary(skin => skin.Name);
        }

        public IRepresentAnActualBundle[] CreateBundles()
        {
            var bundles = new List<IRepresentAnActualBundle>();
            var skins = EnumerateSkins();
            foreach (var skin in skins)
            {
                bundles.AddRange(skin.Value.CreateBundles(_skinsDir).ToList());
            }

            return bundles.ToArray();
        }

        public void RegisterBundles(BundleCollection bundles)
        {
            var autoBundles = CreateBundles();
            
            foreach (var bundle in autoBundles)
            {
                bundles.Add(bundle.ToBundle(_appPath));
            }
        }
    }
}
