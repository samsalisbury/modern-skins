namespace ModernSkins.AutoBundling
{
    public class Skin : DirBundleBase
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
