using System.IO;

namespace ModernSkins.AutoBundling
{
    public class ScriptBundle : BundleBase
    {
        public ScriptBundle(string path) : base(path)
        {
            if (!File.Exists(path) && !Directory.Exists(path))
            {
                throw new FileNotFoundException(
                    string.Format("Could not find either a file or directory at {0}", path),
                    path);
            }
        }
    }
}