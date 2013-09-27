using System.Linq;

namespace ModernSkins.AutoBundling
{
    public abstract class AutoBundleBase
    {
        protected readonly IFileSystem FileSystem;
        public string Name { get; set; }
        public string Path { get; set; }

        protected AutoBundleBase(string path, IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public string VirtualPath(string skinsPath)
        {
            var pathWithExtension = Path
                .Replace(skinsPath + "\\", "~/")
                .Replace(skinsPath + "/", "~/")
                .Replace(FileSystem.DirSeparator, '/');

            var pieces = pathWithExtension.Split('/');
            var lastPiece = pieces.Last();

            if (!lastPiece.Contains('.'))
            {
                return pathWithExtension;
            }

            var lastDotIndex = lastPiece.LastIndexOf('.');
            pieces[pieces.Length - 1] = lastPiece.Substring(0, lastDotIndex);

            return string.Join("/", pieces);
        }
    }
}
