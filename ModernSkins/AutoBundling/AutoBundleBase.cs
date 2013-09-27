namespace ModernSkins.AutoBundling
{
    public abstract class AutoBundleBase
    {
        public string Name { get; set; }
        public string Path { get; set; }

        protected AutoBundleBase(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }
}