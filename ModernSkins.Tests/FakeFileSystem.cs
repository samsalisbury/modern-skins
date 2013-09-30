using System;
using System.Collections.Generic;
using System.Linq;
using ModernSkins.AutoBundling;

namespace ModernSkins.Tests
{
    public class FakeFileSystem : IFileSystem
    {
        public FakeDirectory Root { get; private set; }

        public FakeFileSystem(string root = "/")
        {
            Root = new FakeDirectory(root);
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
            throw new System.NotImplementedException();
        }

        public string[] GetFiles(string path)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetFileSystemEntries(string path)
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class FakeFileSystemObject
    {
        public string Name { get; private set; }

        protected FakeFileSystemObject(string name)
        {
            Name = name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class FakeDirectory : FakeFileSystemObject
    {
        public ISet<FakeFileSystemObject> Children { get; private set; }

        public FakeDirectory(string name) : base(name)
        {
             Children = new HashSet<FakeFileSystemObject>();
        }

        public void AddChild(FakeFileSystemObject child)
        {
            Children.Add(child);
        }

        public FakeDirectory AddDirectory(string name)
        {
            var parts = Split(name);

            var dir = new FakeDirectory(parts[0]);
            Children.Add(dir);
            if (parts.Length == 2)
            {
                return dir.AddDirectory(parts[1]);
            }
            
            return dir;
        }

        public string[] Split(string path)
        {
            return path.Split(new[] { '/' }, 2, StringSplitOptions.RemoveEmptyEntries);
        }

        public FakeDirectory AddFiles(params string[] names)
        {
            foreach (var name in names)
            {
                AddChild(new FakeFile(name));
            }

            return this;
        }

        public bool Contains(string path)
        {
            return Object(path) != null;
        }

        public FakeFileSystemObject Object(string path)
        {
            var parts = Split(path);
            var child = Children.SingleOrDefault(c => c.Name == parts[0]);

            if (child == null)
            {
                return null;
            }
            if (parts.Length == 1)
            {
                return child;
            }
            if (child is FakeDirectory)
            {
                return ((FakeDirectory)child).Object(parts[1]);
            }

            return null;
        }
    }

    public class FakeFile : FakeFileSystemObject
    {
        public FakeFile(string name) : base(name)
        {
            
        }
    }
}
