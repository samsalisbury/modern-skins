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
        [Test]
        public void EnumerateBundles_ReturnsExpectedBundles()
        {
            var styleDirPath = TestHelper.ResolveAppDir("~/Skins/testskin/styles");
            var bundler = new StyleBundler(styleDirPath);

            var styles = bundler.GetStyleBundles();

            Assert.That(styles, Has.Count.EqualTo(3));
            Assert.That(styles.Keys, Contains.Item("other_styles"));
            Assert.That(styles.Keys, Contains.Item("some_base_styles"));
            Assert.That(styles.Keys, Contains.Item("some_scss_styles"));

            Assert.That(styles["other_styles"].Name, Is.EqualTo("other_styles"));
            Assert.That(styles["other_styles"].FileName, Is.EqualTo("other_styles.less"));
            Assert.That(styles["some_base_styles"].Name, Is.EqualTo("some_base_styles"));
            Assert.That(styles["some_base_styles"].FileName, Is.EqualTo("some_base_styles.css"));
            Assert.That(styles["some_scss_styles"].Name, Is.EqualTo("some_scss_styles"));
            Assert.That(styles["some_scss_styles"].FileName, Is.EqualTo("some_scss_styles.scss"));
        }
    }
}
