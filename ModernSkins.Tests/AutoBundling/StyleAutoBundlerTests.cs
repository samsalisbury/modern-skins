using System.Web.Optimization;
using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class StyleAutoBundlerTests
    {
        [TestCase("some_base_styles", "/my-skin/styles/some_base_styles.css")]
        [TestCase("some_scss_styles", "/my-skin/styles/some_scss_styles.scss")]
        [TestCase("other_styles", "/my-skin/styles/other_styles.less")]
        public void GetStyleBundles_ReturnsExpectedBundles_IgnoringDirectories(string name, string fileToCreate)
        {
            var fs = new FakeUnixFileSystem();
            fs.AddFile(fileToCreate);
            fs.AddDirectory("/my-skin/styles/this_dir_should_be_ignored");
            
            const string styleDirPath = "/my-skin/styles";

            var bundler = new StyleAutoBundler(styleDirPath, fs);

            var styles = bundler.GetStyleBundles();

            Assert.That(styles, Has.Count.EqualTo(1));
            Assert.That(styles.Keys, Contains.Item(name));
            Assert.That(styles[name].Name, Is.EqualTo(name));
            Assert.That(styles[name].FileSystemPath, Is.EqualTo(fileToCreate));
        }

        [Test]
        public void GetStyleBundles_ShouldIgnoreCssFilesIfTheyMatchLessFileNames()
        {
            const string styleDirPath = "C:\\MyApp\\Skins\\MySkin\\styles";

            var fs = new FakeDosFileSystem();
            var dir = fs.AddDirectory(styleDirPath);
            dir.AddFiles("my_style.css", "my_other_style.css");
            dir.AddFiles("my_style.less", "my_other_style.less");

            var bundler = new StyleAutoBundler(styleDirPath, fs);

            var bundles = bundler.GetStyleBundles();

            Assert.That(bundles, Has.Count.EqualTo(2));
            Assert.That(bundles["my_style"].ListFilesToBundle()[0], Is.EqualTo("C:\\MyApp\\Skins\\MySkin\\styles\\my_style.less"));
            Assert.That(bundles["my_other_style"].ListFilesToBundle()[0], Is.EqualTo("C:\\MyApp\\Skins\\MySkin\\styles\\my_other_style.less"));
        }
    }
}
