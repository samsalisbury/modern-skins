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

        static readonly char DirSeparator = System.IO.Path.DirectorySeparatorChar;

        public string VirtualPath(string skinsPath)
        {
            //if (Path.StartsWith(skinsPath))
            //{
            return Path.Replace(skinsPath + "\\", "~/")
                .Replace(skinsPath + "/", "~/")
                .Replace(DirSeparator, '/');

            //}
        }
    }
}