using System.Collections.Generic;
using System.Web.Optimization;
using ModernSkins.AutoBundling;

namespace ModernSkins.Tests.AutoBundling
{
    public class TestAutoBundler : AutoBundleBase
    {
        public TestAutoBundler(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
        }
    }
} 