using System.IO;
using NUnit.Framework;

namespace ModernSkins.Tests
{
    [TestFixture]
    public class SkinTests
    {
        static Skin CreateSkin(string name)
        {
            return new Skin
                   {
                       Name = name,
                       Dir = Path.Combine(TestHelper.SkinsDir, name)
                   };
        }

        [Test]
        public void EnumerateStyleBundles_ShouldListCorrectStyles()
        {
            var skin = CreateSkin("testskin");

            var styles = skin.EnumerateStyleBundles();

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