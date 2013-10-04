using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class AutoBundleTests
    {
        FakeFileSystem _fs;
        const string AppPath = "/base/my-app";
        const string SkinsPath = AppPath + "/SkinsDir";
        List<string> _expectedBundleVirtualPaths;

        [SetUp]
        public void SetUp()
        {
            _fs = new FakeUnixFileSystem();
            _expectedBundleVirtualPaths = new List<string>();
            // This should add 3 style bundles
            _fs.AddFiles(SkinsPath + "/skin1/styles[base_styles.css,other_styles.scss,my_styles.less]");
            _expectedBundleVirtualPaths.Add("~/skin1/styles/base_styles");
            _expectedBundleVirtualPaths.Add("~/skin1/styles/other_styles");
            _expectedBundleVirtualPaths.Add("~/skin1/styles/my_styles");

            // This should add no style bundles, since directories inside /styles are ignored.
            _fs.AddFiles(SkinsPath + "/skin1/styles/lib[dirs,in,styles,dir,are,not,bundles]");

            // This should just add one style, since css files are ignored
            _fs.AddFiles(SkinsPath + "/skin1/styles/[compiled.css,compiled.sass]");
            _expectedBundleVirtualPaths.Add("~/skin1/styles/compiled");

            // This should add 4 script bundles
            _fs.AddFiles(SkinsPath + "/skin1/scripts[all.js,scripts.js,are.js,bundles.js]");
            _expectedBundleVirtualPaths.Add("~/skin1/scripts/all");
            _expectedBundleVirtualPaths.Add("~/skin1/scripts/scripts");
            _expectedBundleVirtualPaths.Add("~/skin1/scripts/are");
            _expectedBundleVirtualPaths.Add("~/skin1/scripts/bundles");

            // These next 2 should add one bundle each, since they are dirs inside /scripts
            _fs.AddFiles(SkinsPath + "/skin1/scripts/bundle_a[script_dirs.js,are_single_bundles.js,ok.js]");
            _fs.AddFiles(SkinsPath + "/skin1/scripts/bundle_b[script_dirs.js,are_single_bundles.js,ok.js]");
            _expectedBundleVirtualPaths.Add("~/skin1/scripts/bundle_a");
            _expectedBundleVirtualPaths.Add("~/skin1/scripts/bundle_b");

            // Another skin, similar expectations...

            // This should add 3 style bundles
            _fs.AddFiles(SkinsPath + "/skin2/styles[base_styles.css,other_styles.scss,my_styles.less]");
            _expectedBundleVirtualPaths.Add("~/skin2/styles/base_styles");
            _expectedBundleVirtualPaths.Add("~/skin2/styles/other_styles");
            _expectedBundleVirtualPaths.Add("~/skin2/styles/my_styles");

            // No style bundles (dirs are ignored inside /styles)
            _fs.AddFiles(SkinsPath + "/skin2/styles/lib[dirs.css,in.sass,styles.scss,dir.other,are.css,not.less,bundles.sass]");

            // 3 script bundles
            _fs.AddFiles(SkinsPath + "/skin2/scripts[all.js,scripts.js,are.js,bundles.js]");
            _expectedBundleVirtualPaths.Add("~/skin2/scripts/all");
            _expectedBundleVirtualPaths.Add("~/skin2/scripts/scripts");
            _expectedBundleVirtualPaths.Add("~/skin2/scripts/are");
            _expectedBundleVirtualPaths.Add("~/skin2/scripts/bundles");

            // 2 script bundles, one for each directory
            _fs.AddFiles(SkinsPath + "/skin2/scripts/bundle_a[script_dirs.js,are_single_bundles.js,ok.js]");
            _fs.AddFiles(SkinsPath + "/skin2/scripts/bundle_b[script_dirs.js,are_single_bundles.js,ok.js]");
            _expectedBundleVirtualPaths.Add("~/skin2/scripts/bundle_a");
            _expectedBundleVirtualPaths.Add("~/skin2/scripts/bundle_b");
        }

        [Test]
        public void EnumerateSkins_ShouldListCorrectSkins()
        {
            var autoBundler = new SkinsDirAutoBundle(SkinsPath, AppPath, _fs);

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
        public void CreateBundles_ShouldCreateExpectedBundles()
        {
            var autoBundler = new SkinsDirAutoBundle(SkinsPath, AppPath, _fs);

            var bundles = autoBundler.CreateBundles();

            Assert.That(bundles, Has.Length.EqualTo(_expectedBundleVirtualPaths.Count));
            foreach (var virtualPath in _expectedBundleVirtualPaths)
            {
                Assert.That(bundles.Count(b => b.CalculatedVirtualPath == virtualPath), Is.GreaterThan(0), "Expected bundle '{0}' not created.", virtualPath);
                Assert.That(bundles.Count(b => b.CalculatedVirtualPath == virtualPath), Is.EqualTo(1), "Duplicate bundle '{0}' created.", virtualPath);
            }
        }
    }
}