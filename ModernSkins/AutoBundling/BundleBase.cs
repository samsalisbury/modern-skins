namespace ModernSkins.AutoBundling
{
    public abstract class BundleBase
    {
        public string Name { get; set; }
        public string Path { get; set; }

        protected BundleBase(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
}