using System.Collections.Generic;

namespace ModernSkins.AutoBundling
{
    public class SkinAutoBundle : DirAutoBundleBase
    {
        const string StylesDirName = "styles";
        const string ScriptsDirName = "scripts";

        public SkinAutoBundle(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
        }

        public IRepresentAnActualBundle[] CreateBundles(string appPath)
        {
            var list = new List<IRepresentAnActualBundle>();

            if (SubPathExists(StylesDirName))
            {
                var styles = new StylesDirAutoBundle(SubPath(StylesDirName), FileSystem);
                list.AddRange(styles.GetStyleBundles().Values);
            }

            if (SubPathExists(ScriptsDirName))
            {
                var scripts = new ScriptsDirAutoBundle(SubPath(ScriptsDirName), FileSystem);
                list.AddRange(scripts.GetScriptBundles().Values);
            }

            foreach (var bundle in list)
            {
                bundle.CalculatedVirtualPath = bundle.VirtualPath(appPath);
            }
            
            return list.ToArray();
        }
    }
}
