using System.Linq;
using ModernSkins.AutoBundling;

namespace ModernSkins.Tests
{
    public abstract class FakeFileSystem : IFileSystem
    {
        public FakeDirectory Root { get; private set; }

        protected FakeFileSystem()
        {
            Root = new FakeDirectory(string.Empty, this);
        }

        public FakeDirectory AddDirectory(string path)
        {
            return Root.AddDirectory(path);
        }

        public bool FileExists(string path)
        {
            return Root.Object(path) is FakeFile;
        }

        public bool DirExists(string path)
        {
            return Root.Object(path) is FakeDirectory;
        }

        public abstract char DirSeparator { get; }

        public string[] GetDirectories(string path)
        {
            return Root.Dir(path).Children.Where(child => child is FakeDirectory).Select(dir => path + DirSeparator + dir.Name).ToArray();
        }

        public string[] GetFiles(string path)
        {
            return Root.Dir(path).Children.Where(child => child is FakeFile).Select(dir => path + DirSeparator + dir.Name).ToArray();
        }

        public string[] GetFileSystemEntries(string path)
        {
            return Root.Dir(path).Children.Select(dir => path + DirSeparator + dir.Name).ToArray();
        }

        public void AddFiles(string filesPath)
        {
            var parts = filesPath.Split(new[] {'['});
            var dir = AddDirectory(parts[0]);
            var files = parts[1].Substring(0, parts[1].Length - 1).Split(',');

            dir.AddFiles(files);
        }

        public string CombinePaths(string p1, string p2)
        {
            if (p1.EndsWith(DirSeparator.ToString()))
            {
                p1 = p1.Substring(0, p1.Length - 1);
            }

            if (p2.StartsWith(DirSeparator.ToString()))
            {
                p2 = p2.Substring(1);
            }

            return p1 + DirSeparator + p2;
        }

        public string GetFileName(string path)
        {
            return path.Split(DirSeparator).Last();
        }

        public FakeFile AddFile(string path)
        {
            return AddFile(path, null);
        }

        public FakeFile AddFile(string path, string content)
        {
            var extensionSeparatorIndex = path.LastIndexOf(DirSeparator);
            var fileName = path.Substring(extensionSeparatorIndex + 1);
            var dirPath = path.Substring(0, extensionSeparatorIndex);

            var file = new FakeFile(fileName, content);

            AddDirectory(dirPath).AddChild(file);

            return file;
        }

        public string ReadFile(string path)
        {
            return ((FakeFile) Root.Object(path)).Content;
        }
    }
}
