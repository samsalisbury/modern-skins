﻿using System;
using System.Collections.Generic;
using System.Linq;
using ModernSkins.AutoBundling;

namespace ModernSkins.Tests
{
    public class FakeDirectory : FakeFileSystemObject
    {
        readonly IFileSystem _fileSystem;
        public ISet<FakeFileSystemObject> Children { get; private set; }

        public FakeDirectory(string name, IFileSystem fileSystem) : base(name)
        {
            _fileSystem = fileSystem;
            Children = new HashSet<FakeFileSystemObject>();
        }

        public void AddChild(FakeFileSystemObject child)
        {
            Children.Add(child);
        }

        public FakeDirectory AddDirectory(string name)
        {
            var parts = Split(name);

            var sameName = (FakeDirectory) Children.SingleOrDefault(child => child.Name == parts[0]);

            var dir = sameName ?? new FakeDirectory(parts[0], _fileSystem);
            Children.Add(dir);
            if (parts.Length == 2)
            {
                return dir.AddDirectory(parts[1]);
            }
            
            return dir;
        }

        public string[] Split(string path)
        {
            return path.Split(new[] { _fileSystem.DirSeparator }, 2, StringSplitOptions.RemoveEmptyEntries);
        }

        public FakeDirectory AddFiles(params string[] names)
        {
            foreach (var name in names)
            {
                AddChild(new FakeFile(name, null));
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

        public FakeDirectory Dir(string path)
        {
            return Object(path) as FakeDirectory;
        }
    }
}