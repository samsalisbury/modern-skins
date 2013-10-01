using ModernSkins.AutoBundling;
using Moq;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class AutoBundlerBaseTests
    {
        [TestCase("X:\\MySln\\MyApp\\Skins\\skin_name\\scripts\\bundle", "X:\\MySln\\MyApp\\Skins", "~/skin_name/scripts/bundle")]
        [TestCase("X:\\MySln\\MyApp\\Skins\\skin_name\\scripts\\bundle.extension", "X:\\MySln\\MyApp\\Skins", "~/skin_name/scripts/bundle")]
        [TestCase("X:\\MySln\\MyApp\\Skins\\skin.name\\scripts\\bundle", "X:\\MySln\\MyApp\\Skins", "~/skin.name/scripts/bundle")]
        public void VirtualPath_ReturnsTheExpectedVirtualPath_Dos(string filePath, string skinsPath, string expectedPath)
        {
            var bundler = (AutoBundleBase) new TestAutoBundler(filePath, new FakeDosFileSystem());

            var result = bundler.VirtualPath(skinsPath);

            Assert.That(result, Is.EqualTo(expectedPath));
        }

        [TestCase("/user/MySln/MyApp/Skins/skin_name/scripts/bundle", "/user/MySln/MyApp/Skins", "~/skin_name/scripts/bundle")]
        [TestCase("/user/MySln/MyApp/Skins/skin_name/scripts/bundle.extension", "/user/MySln/MyApp/Skins", "~/skin_name/scripts/bundle")]
        [TestCase("/user/MySln/MyApp/Skins/skin.name/scripts/bundle", "/user/MySln/MyApp/Skins", "~/skin.name/scripts/bundle")]
        public void VirtualPath_ReturnsTheExpectedVirtualPath_Unix(string filePath, string skinsPath, string expectedPath)
        {
            var bundler = (AutoBundleBase) new TestAutoBundler(filePath, new FakeUnixFileSystem());

            var result = bundler.VirtualPath(skinsPath);

            Assert.That(result, Is.EqualTo(expectedPath));
        }
    }

    public class TestAutoBundler : AutoBundleBase
    {
        public TestAutoBundler(string path, IFileSystem fileSystem) : base(path, fileSystem)
        {
        }
    }
}
