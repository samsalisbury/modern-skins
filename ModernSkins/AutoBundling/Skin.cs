namespace ModernSkins.AutoBundling
{
    public class Skin : DirAutoBundleBase
    {
        public Skin(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
        }

        public string StyleDir
        {
            get { return SubPath("styles"); }
        }
    }
}
