using System.IO;
using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public class ScriptAutoBundle : AutoBundleBase
    {
        public ScriptAutoBundle(string path) : base(path)
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