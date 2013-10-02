using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class StylesDirAutoBundleTests
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

            var bundler = new StylesDirAutoBundle(styleDirPath, fs);

            var styles = bundler.GetStyleBundles();

            Assert.That(styles, Has.Count.EqualTo(1));
            Assert.That(styles.Keys, Contains.Item(name));
            Assert.That(styles[name].Name, Is.EqualTo(name));
            Assert.That(styles[name].FileSystemPath, Is.EqualTo(fileToCreate));
        }

        [TestCase("same-name.less", "same-name.scss")]
        [TestCase("same-name.less", "same-name.sass")]
        [TestCase("same-name.scss", "same-name.less")]
        [TestCase("same-name.scss", "same-name.sass")]
        [TestCase("same-name.sass", "same-name.less")]
        [TestCase("same-name.sass", "same-name.scss")]
        public void GetStyleBundles_ShouldThrow_WhenMultipleBundlesHaveTheSameNameUnlessItsCss(string file1, string file2)
        {
            const string styleDirPath = "/c/app/styles";
            var fs = new FakeUnixFileSystem();
            var dir = fs.AddDirectory(styleDirPath);
            dir.AddFiles(file1, file2);

            var bundler = new StylesDirAutoBundle(styleDirPath, fs);

            Assert.Throws<StyleBundleWithSameNameException>(() => bundler.GetStyleBundles());
        }

        [Test]
        public void GetStyleBundles_ShouldIgnoreCssFilesIfTheyMatchLessFileNames()
        {
            const string styleDirPath = "C:\\MyApp\\Skins\\MySkin\\styles";

            var fs = new FakeDosFileSystem();
            var dir = fs.AddDirectory(styleDirPath);
            dir.AddFiles("my_style.css", "my_other_style.css");
            dir.AddFiles("my_style.less", "my_other_style.less");

            var bundler = new StylesDirAutoBundle(styleDirPath, fs);

            var bundles = bundler.GetStyleBundles();

            Assert.That(bundles, Has.Count.EqualTo(2));
            Assert.That(bundles["my_style"].ListFilesToBundle()[0], Is.EqualTo("C:\\MyApp\\Skins\\MySkin\\styles\\my_style.less"));
            Assert.That(bundles["my_other_style"].ListFilesToBundle()[0], Is.EqualTo("C:\\MyApp\\Skins\\MySkin\\styles\\my_other_style.less"));
        }

        [Test]
        public void GetStyleBundles_ShouldIgnoreCssFilesIfTheyMatchScssFileNames()
        {
            const string styleDirPath = "C:\\MyApp\\Skins\\MySkin\\styles";

            var fs = new FakeDosFileSystem();
            var dir = fs.AddDirectory(styleDirPath);
            dir.AddFiles("my_style.css", "my_other_style.css");
            dir.AddFiles("my_style.scss", "my_other_style.scss");

            var bundler = new StylesDirAutoBundle(styleDirPath, fs);

            var bundles = bundler.GetStyleBundles();

            Assert.That(bundles, Has.Count.EqualTo(2));
            Assert.That(bundles["my_style"].ListFilesToBundle()[0], Is.EqualTo("C:\\MyApp\\Skins\\MySkin\\styles\\my_style.scss"));
            Assert.That(bundles["my_other_style"].ListFilesToBundle()[0], Is.EqualTo("C:\\MyApp\\Skins\\MySkin\\styles\\my_other_style.scss"));
        }

        [Test]
        public void GetStyleBundles_ShouldIgnoreCssFilesIfTheyMatchSassFileNames()
        {
            const string styleDirPath = "C:\\MyApp\\Skins\\MySkin\\styles";

            var fs = new FakeDosFileSystem();
            var dir = fs.AddDirectory(styleDirPath);
            dir.AddFiles("my_style.css", "my_other_style.css");
            dir.AddFiles("my_style.sass", "my_other_style.sass");

            var bundler = new StylesDirAutoBundle(styleDirPath, fs);

            var bundles = bundler.GetStyleBundles();

            Assert.That(bundles, Has.Count.EqualTo(2));
            Assert.That(bundles["my_style"].ListFilesToBundle()[0], Is.EqualTo("C:\\MyApp\\Skins\\MySkin\\styles\\my_style.sass"));
            Assert.That(bundles["my_other_style"].ListFilesToBundle()[0], Is.EqualTo("C:\\MyApp\\Skins\\MySkin\\styles\\my_other_style.sass"));
        }
    }
}
