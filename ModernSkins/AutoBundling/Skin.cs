using System.Collections.Generic;

namespace ModernSkins.AutoBundling
{
    public class Skin : DirAutoBundleBase
    {
        public Skin(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
        }

        public AutoBundleBase[] CreateBundles()
        {
            var list = new List<AutoBundleBase>();

            if (SubPathExists("styles"))
            {
                var styles = new StyleAutoBundler(SubPath("styles"), FileSystem);
                list.AddRange(styles.GetStyleBundles().Values);
            }

            if (SubPathExists("scripts"))
            {
                var scripts = new ScriptAutoBundler(SubPath("scripts"), FileSystem);
                list.AddRange(scripts.GetScriptBundles().Values);
            }
            
            return list.ToArray();
        }
    }
}
