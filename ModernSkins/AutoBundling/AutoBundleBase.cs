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

        const char WebPathSeparator = '/';
        const char FileExtensionSeparator = '.';
        const string VirtualPathPrefix = "~/";

        public string VirtualPath(string relativeToPath)
        {
            var pathWithExtension = VirtualPathWithExtension(relativeToPath);

            var pieces = pathWithExtension.Split(WebPathSeparator);
            var lastPiece = pieces.Last();

            if (!lastPiece.Contains(FileExtensionSeparator))
            {
                return pathWithExtension;
            }

            var lastDotIndex = lastPiece.LastIndexOf(FileExtensionSeparator);
            pieces[pieces.Length - 1] = lastPiece.Substring(0, lastDotIndex);

            return string.Join(WebPathSeparator.ToString(), pieces);
        }

        public string VirtualPathWithExtension(string relativeToPath)
        {
            if (!relativeToPath.EndsWith(FileSystem.DirSeparator.ToString()))
            {
                relativeToPath += FileSystem.DirSeparator;
            }

            return FileSystemPath
                .Replace(relativeToPath, VirtualPathPrefix)
                .Replace(FileSystem.DirSeparator, WebPathSeparator);
        }
    }
}
