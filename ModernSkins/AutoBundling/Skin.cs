using System.Collections.Generic;

namespace ModernSkins.AutoBundling
{
    public class Skin : DirAutoBundleBase
    {
        const string StylesDirName = "styles";
        const string ScriptsDirName = "scripts";

        public Skin(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
        }

        public IRepresentAnActualBundle[] CreateBundles()
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
            
            return list.ToArray();
        }
    }
}
