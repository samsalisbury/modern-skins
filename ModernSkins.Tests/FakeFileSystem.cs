using System.Linq;
using ModernSkins.AutoBundling;

namespace ModernSkins.Tests
{
    public class FakeFileSystem : IFileSystem
    {
        public FakeDirectory Root { get; private set; }

        public FakeFileSystem()
        {
            Root = new FakeDirectory(string.Empty);
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

        public char DirSeparator { get { return '/'; } }

        public string[] GetDirectories(string path)
        {
            return Root.Dir(path).Children.Where(child => child is FakeDirectory).Select(dir => path + "/" + dir.Name).ToArray();
        }

        public string[] GetFiles(string path)
        {
            return Root.Dir(path).Children.Where(child => child is FakeFile).Select(dir => path + "/" + dir.Name).ToArray();
        }

        public string[] GetFileSystemEntries(string path)
        {
            return Root.Dir(path).Children.Select(dir => path + "/" + dir.Name).ToArray();
        }

        public void AddFiles(string filesPath)
        {
            var parts = filesPath.Split(new[] {'['});
            var dir = AddDirectory(parts[0]);
            var files = parts[1].Substring(0, parts[1].Length - 1).Split(',');

            dir.AddFiles(files);
        }
    }
}
