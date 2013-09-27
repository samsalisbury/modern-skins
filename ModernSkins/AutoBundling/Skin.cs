namespace ModernSkins.AutoBundling
{
    public class Skin : DirAutoBundleBase
    {
        public Skin(string path) : base(path)
        {
        }

        public string StyleDir
        {
            get { return SubPath("styles"); }
        }
    }
}
