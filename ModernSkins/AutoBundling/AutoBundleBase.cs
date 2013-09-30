using System.Linq;

namespace ModernSkins.AutoBundling
{
    public abstract class AutoBundleBase
    {
        protected readonly IFileSystem FileSystem;
        public string Name { get; set; }
        public string FileSystemPath { get; set; }

        protected AutoBundleBase(string path, IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
            FileSystemPath = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
        }

        public string VirtualPath(string skinsPath)
        {
            var pathWithExtension = FileSystemPath
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
