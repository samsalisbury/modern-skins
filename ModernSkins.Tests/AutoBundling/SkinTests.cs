using System;
using System.IO;
using System.Linq;
using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class SkinTests
    {
        [TestCase("this is not an existent path")]
        [TestCase("C:\\Nor\\is\\this")]
        public void Ctor_Throws_DirectoryNotFound_IfPassedNonExistentPath(string nonExistentPath)
        {
            Assert.Throws<DirectoryNotFoundException>(() => new Skin(nonExistentPath, new FileSystem()));
        }

        [TestCase("~/Skins/testskin/styles/other_styles.less")]
        [TestCase("~/Skins/testskin2/styles/some_scss_styles2.scss")]
        public void Ctor_Throws_DirectoryNotFound_IfPassedPathToFile(string appRelativeFilePath)
        {
            var filePath = TestHelper.ResolveAppDir(appRelativeFilePath);

            Assert.Throws<DirectoryNotFoundException>(() => new Skin(filePath, new FileSystem()));
        }

        [TestCase("~/Skins/testskin", "testskin")]
        [TestCase("~/Skins/testskin2", "testskin2")]
        public void Ctor_GivesSkinCorrectName(string appRelativeSkinDir, string expectedName)
        {
            var testSkinDir = TestHelper.ResolveAppDir(appRelativeSkinDir);

            var skin = new Skin(testSkinDir, new FileSystem());

            Assert.That(skin.Name, Is.EqualTo(expectedName));
        }

        [Category("FileSystem")]
        [TestCase("~/Skins/testskin", "bundle_a", typeof(ScriptAutoBundle), "~/testskin/scripts/bundle_a")]
        [TestCase("~/Skins/testskin", "bundle_b", typeof(ScriptAutoBundle), "~/testskin/scripts/bundle_b")]
        [TestCase("~/Skins/testskin", "js_bundle", typeof(ScriptAutoBundle), "~/testskin/scripts/js_bundle")]
        [TestCase("~/Skins/testskin", "another_js_bundle", typeof(ScriptAutoBundle), "~/testskin/scripts/another_js_bundle")]
        [TestCase("~/Skins/testskin", "other_styles", typeof(StyleAutoBundle), "~/testskin/styles/other_styles")]
        [TestCase("~/Skins/testskin", "some_base_styles", typeof(StyleAutoBundle), "~/testskin/styles/some_base_styles")]
        [TestCase("~/Skins/testskin", "some_scss_styles", typeof(StyleAutoBundle), "~/testskin/styles/some_scss_styles")]
        public void CreateBundles_ReturnsExpectedBundles(string skinPath, string bundleName, Type bundleType, string virtualPath)
        {
            var basePath = TestHelper.ResolveAppDir("~/Skins");
            var testSkinDir = TestHelper.ResolveAppDir(skinPath);
            var skin = new Skin(testSkinDir, new FileSystem());
            
            var bundles = skin.CreateBundles();

            Assert.That(bundles, Has.Length.EqualTo(7));
            var bundle = bundles.SingleOrDefault(b => b.VirtualPath(basePath) == virtualPath);
            Assert.That(bundle, Is.Not.Null, "Bundle {0} not found.", virtualPath);
            Assert.That(bundle, Is.TypeOf(bundleType));
            Assert.That(bundle.Name, Is.EqualTo(bundleName));
        }
    }
}
