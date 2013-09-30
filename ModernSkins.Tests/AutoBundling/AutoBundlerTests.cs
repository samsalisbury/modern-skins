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
            var fs = new FakeFileSystem();
            fs.AddDirectory("/my-app/Skins/testskin");
            fs.AddDirectory("/my-app/Skins/testskin2");

            const string skinsPath = "/my-app/Skins";

            var autoBundler = new AutoBundler(skinsPath, fs);

            var skins = autoBundler.EnumerateSkins();

            Assert.That(skins, Has.Count.EqualTo(2));
            Assert.That(skins.Keys, Contains.Item("testskin"));
            Assert.That(skins.Keys, Contains.Item("testskin2"));
            Assert.That(skins["testskin"].Name, Is.EqualTo("testskin"));
            Assert.That(skins["testskin"].Path, Is.EqualTo(fs.CombinePaths(skinsPath, "testskin")));
            Assert.That(skins["testskin2"].Name, Is.EqualTo("testskin2"));
            Assert.That(skins["testskin2"].Path, Is.EqualTo(fs.CombinePaths(skinsPath, "testskin2")));
        }

        [Test]
        public void CreateBundles_ShouldCreateExpectedNumberOfBundles()
        {
            var fs = new FakeFileSystem();
            fs.AddFiles("/base/my-app/SkinsDir/skin1/styles[base_styles.css,other_styles.scss,my_styles.less]");
            fs.AddFiles("/base/my-app/SkinsDir/skin1/styles/lib[dirs,in,styles,dir,are,not,bundles]");
            fs.AddFiles("/base/my-app/SkinsDir/skin1/scripts[all.js,scripts.coffee,are.js,bundles.js]");
            fs.AddFiles("/base/my-app/SkinsDir/skin1/scripts/bundle_a[script_dirs.js,are_single_bundles.js,ok.coffee]");
            fs.AddFiles("/base/my-app/SkinsDir/skin1/scripts/bundle_b[script_dirs.js,are_single_bundles.js,ok.coffee]");

            fs.AddFiles("/base/my-app/SkinsDir/skin2/styles[base_styles.css,other_styles.scss,my_styles.less]");
            fs.AddFiles("/base/my-app/SkinsDir/skin2/styles/lib[dirs,in,styles,dir,are,not,bundles]");
            fs.AddFiles("/base/my-app/SkinsDir/skin2/scripts[all.js,scripts.coffee,are.js,bundles.js]");
            fs.AddFiles("/base/my-app/SkinsDir/skin2/scripts/bundle_a[script_dirs.js,are_single_bundles.js,ok.coffee]");
            fs.AddFiles("/base/my-app/SkinsDir/skin2/scripts/bundle_b[script_dirs.js,are_single_bundles.js,ok.coffee]");

            var autoBundler = new AutoBundler("/base/my-app/SkinsDir", fs);

            var bundles = autoBundler.CreateBundles();

            Assert.That(bundles, Has.Length.EqualTo(18));
        }
    }
}
