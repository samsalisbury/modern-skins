﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public class AutoBundler
    {
        readonly string _skinsDir;
        readonly IFileSystem _fileSystem;

        public AutoBundler(string skinsDir) : this(skinsDir, new FileSystem())
        {
        }

        public AutoBundler(string skinsDir, IFileSystem fileSystem)
        {
            _skinsDir = skinsDir;
            _fileSystem = fileSystem;
        }

        public IDictionary<string, Skin> EnumerateSkins()
        {
            var skins = _fileSystem.GetDirectories(_skinsDir).Select(dir => new Skin(dir, _fileSystem));

            return skins.ToDictionary(skin => skin.Name);
        }

        public AutoBundleBase[] CreateBundles()
        {
            var bundles = new List<AutoBundleBase>();
            var skins = EnumerateSkins();
            foreach (var skin in skins)
            {
                bundles.AddRange(skin.Value.CreateBundles().ToList());
            }

            return bundles.ToArray();
        }

        public void RegisterBundles(BundleCollection bundles)
        {
            throw new System.NotImplementedException();
        }
    }
}