using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class StyleAutoBundlerTests
    {
        [TestCase("some_base_styles", "~/Skins/testskin/styles/some_base_styles.css")]
        [TestCase("some_scss_styles", "~/Skins/testskin/styles/some_scss_styles.scss")]
        [TestCase("other_styles", "~/Skins/testskin/styles/other_styles.less")]
        public void GetStyleBundles_ReturnsExpectedBundles(string name, string path)
        {
            var styleDirPath = TestHelper.ResolveAppDir("~/Skins/testskin/styles");
            var bundler = new StyleAutoBundler(styleDirPath, new FileSystem());

            var styles = bundler.GetStyleBundles();

            Assert.That(styles, Has.Count.EqualTo(3));

            Assert.That(styles.Keys, Contains.Item(name));
            Assert.That(styles[name].Name, Is.EqualTo(name));
            Assert.That(styles[name].Path, Is.EqualTo(TestHelper.ResolveAppDir(path)));
        }
    }
}
