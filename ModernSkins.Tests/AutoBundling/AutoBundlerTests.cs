using System.IO;
using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class AutoBundlerTests
    {
        [Test]
        public void EnumerateSkins_ShouldListCorrectSkins()
        {
            var autoBundler = new AutoBundler(TestHelper.SkinsDir, new FileSystem());

            var skins = autoBundler.EnumerateSkins();

            Assert.That(skins, Has.Count.EqualTo(2));
            Assert.That(skins.Keys, Contains.Item("testskin"));
            Assert.That(skins.Keys, Contains.Item("testskin2"));

            var expectedEndDirFormat = "{0}" + Path.DirectorySeparatorChar + "{1}";

            Assert.That(skins["testskin"].Name, Is.EqualTo("testskin"));
            Assert.That(skins["testskin"].Path, Is.StringEnding(string.Format(expectedEndDirFormat, TestHelper.SkinsDir, "testskin")));
            Assert.That(skins["testskin2"].Name, Is.EqualTo("testskin2"));
            Assert.That(skins["testskin2"].Path, Is.StringEnding(string.Format(expectedEndDirFormat, TestHelper.SkinsDir, "testskin2")));
        }

        [Test]
        public void CreateBundles_ShouldCreateExpectedNumberOfBundles()
        {
            var autoBundler = new AutoBundler(TestHelper.SkinsDir, new FileSystem());

            var bundles = autoBundler.CreateBundles();

            Assert.That(bundles, Has.Length.EqualTo(10));
        }
    }
}
