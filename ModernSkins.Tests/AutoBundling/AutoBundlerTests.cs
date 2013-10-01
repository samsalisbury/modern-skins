using System.Web.Optimization;
using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class AutoBundlerTests
    {
        FakeFileSystem _fs;
        const string SkinsPath = "/base/my-app/SkinsDir";

        [SetUp]
        public void SetUp()
        {
            _fs = new FakeUnixFileSystem();
            _fs.AddFiles(SkinsPath + "/skin1/styles[base_styles.css,other_styles.scss,my_styles.less]");
            _fs.AddFiles(SkinsPath + "/skin1/styles/lib[dirs,in,styles,dir,are,not,bundles]");
            _fs.AddFiles(SkinsPath + "/skin1/scripts[all.js,scripts.coffee,are.js,bundles.js]");
            _fs.AddFiles(SkinsPath + "/skin1/scripts/bundle_a[script_dirs.js,are_single_bundles.js,ok.coffee]");
            _fs.AddFiles(SkinsPath + "/skin1/scripts/bundle_b[script_dirs.js,are_single_bundles.js,ok.coffee]");
            _fs.AddFiles(SkinsPath + "/skin2/styles[base_styles.css,other_styles.scss,my_styles.less]");
            _fs.AddFiles(SkinsPath + "/skin2/styles/lib[dirs,in,styles,dir,are,not,bundles]");
            _fs.AddFiles(SkinsPath + "/skin2/scripts[all.js,scripts.coffee,are.js,bundles.js]");
            _fs.AddFiles(SkinsPath + "/skin2/scripts/bundle_a[script_dirs.js,are_single_bundles.js,ok.coffee]");
            _fs.AddFiles(SkinsPath + "/skin2/scripts/bundle_b[script_dirs.js,are_single_bundles.js,ok.coffee]");
        }

        [Test]
        public void EnumerateSkins_ShouldListCorrectSkins()
        {
            var autoBundler = new AutoBundler(SkinsPath, _fs);

            var skins = autoBundler.EnumerateSkins();

            Assert.That(skins, Has.Count.EqualTo(2));
            Assert.That(skins.Keys, Contains.Item("skin1"));
            Assert.That(skins.Keys, Contains.Item("skin2"));
            Assert.That(skins["skin1"].Name, Is.EqualTo("skin1"));
            Assert.That(skins["skin1"].FileSystemPath, Is.EqualTo(_fs.CombinePaths(SkinsPath, "skin1")));
            Assert.That(skins["skin2"].Name, Is.EqualTo("skin2"));
            Assert.That(skins["skin2"].FileSystemPath, Is.EqualTo(_fs.CombinePaths(SkinsPath, "skin2")));
        }

        [Test]
        public void CreateBundles_ShouldCreateExpectedNumberOfBundles()
        {
            var autoBundler = new AutoBundler(SkinsPath, _fs);

            var bundles = autoBundler.CreateBundles();

            Assert.That(bundles, Has.Length.EqualTo(18));
        }

        [Test]
        public void RegisterBundles_ShouldRegisterBundles()
        {
            var autoBundler = new AutoBundler(SkinsPath, _fs);

            var bundleCollection = new BundleCollection();

            autoBundler.RegisterBundles(bundleCollection);

            Assert.That(bundleCollection, Has.Count.EqualTo(18));
        }
    }
}