using System.Linq;

namespace ModernSkins.AutoBundling
{
    public class Skin : DirAutoBundleBase
    {
        public Skin(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
        }

        public AutoBundleBase[] CreateBundles()
        {
            var styles = new StyleAutoBundler(SubPath("styles"), FileSystem);
            var scripts = new ScriptAutoBundler(SubPath("scripts"), FileSystem);

            var styleBundles = styles.GetStyleBundles().Values;
            var scriptBundles = scripts.GetScriptBundles().Values;

            return styleBundles.Union<AutoBundleBase>(scriptBundles).ToArray();
        }
    }
}
