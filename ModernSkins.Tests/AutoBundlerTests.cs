using System.IO;
using NUnit.Framework;

namespace ModernSkins.Tests
{
    [TestFixture]
    public class AutoBundlerTests
    {
        [Test]
        public void EnumerateSkins_ShouldListCorrectSkins()
        {
            var autoBundler = new AutoBundler(TestHelper.SkinsDir);

            var skins = autoBundler.EnumerateSkins();

            Assert.That(skins, Has.Count.EqualTo(2));
            Assert.That(skins.Keys, Contains.Item("testskin"));
            Assert.That(skins.Keys, Contains.Item("testskin2"));

            var expectedEndDirFormat = "{0}" + Path.DirectorySeparatorChar + "{1}";

            Assert.That(skins["testskin"].Name, Is.EqualTo("testskin"));
            Assert.That(skins["testskin"].Dir, Is.StringEnding(string.Format(expectedEndDirFormat, TestHelper.SkinsDir, "testskin")));
            Assert.That(skins["testskin2"].Name, Is.EqualTo("testskin2"));
            Assert.That(skins["testskin2"].Dir, Is.StringEnding(string.Format(expectedEndDirFormat, TestHelper.SkinsDir, "testskin2")));
        }
    }
}
