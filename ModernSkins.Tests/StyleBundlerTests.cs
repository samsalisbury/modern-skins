using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ModernSkins.Tests
{
    [TestFixture]
    public class StyleBundlerTests
    {
        [TestCase("some_base_styles", "~/Skins/testskin/styles/some_base_styles.css")]
        [TestCase("some_scss_styles", "~/Skins/testskin/styles/some_scss_styles.scss")]
        [TestCase("other_styles", "~/Skins/testskin/styles/other_styles.less")]
        public void GetStyleBundles_ReturnsExpectedBundles(string name, string path)
        {
            var styleDirPath = TestHelper.ResolveAppDir("~/Skins/testskin/styles");
            var bundler = new StyleBundler(styleDirPath);

            var styles = bundler.GetStyleBundles();

            Assert.That(styles, Has.Count.EqualTo(3));

            Assert.That(styles.Keys, Contains.Item(name));
            Assert.That(styles[name].Name, Is.EqualTo(name));
            Assert.That(styles[name].FilePath, Is.EqualTo(TestHelper.ResolveAppDir(path)));
        }
    }
}
