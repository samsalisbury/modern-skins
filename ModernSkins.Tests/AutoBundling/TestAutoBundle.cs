using System.Collections.Generic;
using System.Web.Optimization;
using ModernSkins.AutoBundling;

namespace ModernSkins.Tests.AutoBundling
{
    public class TestAutoBundle : AutoBundleBase
    {
        public TestAutoBundle(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
        }
    }
} 