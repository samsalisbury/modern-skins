using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class SkinAutoBundleTests
    {
        FakeFileSystem _fs;
        const string AppPath = "/";
        const string BasePath = "/skins";
        const string TestSkinDir = "/skins/my-skin";
        Dictionary<string, Tuple<string, Type>> _expectedBundles;

        [SetUp]
        public void SetUp()
        {
            _fs = new FakeUnixFileSystem();

            _expectedBundles = new Dictionary<string, Tuple<string, Type>>();

            // Scripts
            _fs.AddFile("/skins/my-skin/scripts/crazy_bundle/file1.js");
            _fs.AddFile("/skins/my-skin/scripts/crazy_bundle/file2.js");
            _fs.AddFile("/skins/my-skin/scripts/crazy_bundle/dirs_inside_scripts_are_bundles.js");
            _expectedBundles.Add("crazy_bundle", new Tuple<string, Type>("~/my-skin/scripts/crazy_bundle", typeof(ScriptAutoBundle)));

            _fs.AddFile("/skins/my-skin/scripts/bundlino/file_z.js");
            _expectedBundles.Add("bundlino", new Tuple<string, Type>("~/my-skin/scripts/bundlino", typeof(ScriptAutoBundle)));

            _fs.AddFile("/skins/my-skin/scripts/scripterific.js");
            _expectedBundles.Add("scripterific", new Tuple<string, Type>("~/my-skin/scripts/scripterific", typeof(ScriptAutoBundle)));

            _fs.AddFile("/skins/my-skin/scripts/java.coffee");
            _expectedBundles.Add("java", new Tuple<string, Type>("~/my-skin/scripts/java", typeof(ScriptAutoBundle)));

            _fs.AddFile("/skins/my-skin/scripts/all_files_directly_in_scripts_are_separate_bundles.coffee");
            _expectedBundles.Add("all_files_directly_in_scripts_are_separate_bundles",
                new Tuple<string, Type>("~/my-skin/scripts/all_files_directly_in_scripts_are_separate_bundles", typeof(ScriptAutoBundle)));

            // Styles
            _fs.AddFile("/skins/my-skin/styles/my_lovely_styles.css");
            _expectedBundles.Add("my_lovely_styles", new Tuple<string, Type>("~/my-skin/styles/my_lovely_styles", typeof(StyleAutoBundle)));

            _fs.AddFile("/skins/my-skin/styles/my_sytlings.sass");
            _expectedBundles.Add("my_sytlings", new Tuple<string, Type>("~/my-skin/styles/my_sytlings", typeof(StyleAutoBundle)));

            _fs.AddFile("/skins/my-skin/styles/my_rad_stiles.scss");
            _expectedBundles.Add("my_rad_stiles", new Tuple<string, Type>("~/my-skin/styles/my_rad_stiles", typeof(StyleAutoBundle)));

            _fs.AddFile("/skins/my-skin/styles/not_much.less");
            _expectedBundles.Add("not_much", new Tuple<string, Type>("~/my-skin/styles/not_much", typeof(StyleAutoBundle)));

            _fs.AddFile("/skins/my-skin/styles/some-lib/a_lib_file.less");
            _fs.AddFile("/skins/my-skin/styles/some-lib/another_file_in_a_lib.less");
            _fs.AddFile("/skins/my-skin/styles/some-lib/lib_files_do_not_show_up_as_bundles.css");
            _fs.AddFile("/skins/my-skin/styles/some-lib/and_nor_do_their_folders.css");
            // Note: The above stanza adds no expectations, since it's a lib in the styles folder. Just a red herring to try
            // to confuse the tests ;)
        }


        [TestCase("this is not an existent path")]
        [TestCase("C:\\Nor\\is\\this")]
        public void Ctor_Throws_DirectoryNotFound_IfPassedNonExistentPath(string nonExistentPath)
        {
            Assert.Throws<DirectoryNotFoundException>(() => new SkinAutoBundle(nonExistentPath, _fs));
        }

        [TestCase("/Skins/testskin/styles/other_styles.less")]
        [TestCase("/Skins/testskin2/styles/some_scss_styles2.scss")]
        public void Ctor_Throws_DirectoryNotFound_IfPassedPathToFile(string filePath)
        {
            _fs.AddFile(filePath);

            Assert.Throws<DirectoryNotFoundException>(() => new SkinAutoBundle(filePath, _fs));
        }

        [TestCase("/Skins/testskin", "testskin")]
        [TestCase("/Skins/testskin2", "testskin2")]
        public void Ctor_GivesSkinCorrectName(string skinDir, string expectedName)
        {
            var fs = new FakeUnixFileSystem();
            fs.AddDirectory(skinDir);

            var skin = new SkinAutoBundle(skinDir, fs);

            Assert.That(skin.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void CreateBundles_ReturnsExpectedBundles()
        {
            var skin = new SkinAutoBundle(TestSkinDir, _fs);

            var bundles = skin.CreateBundles(AppPath);

            Assert.That(bundles, Has.Length.EqualTo(_expectedBundles.Count));

            foreach (var expectation in _expectedBundles)
            {
                var expectedName = expectation.Key;
                var expectedVirtualPath = expectation.Value.Item1;
                var expectedType = expectation.Value.Item2;

                var bundle = bundles.SingleOrDefault(b => b.VirtualPath(BasePath) == expectedVirtualPath);
                Assert.That(bundle, Is.Not.Null, "Bundle {0} not found.", expectedVirtualPath);
                Assert.That(bundle, Is.TypeOf(expectedType));
                Assert.That(bundle.Name, Is.EqualTo(expectedName));
            }
        }

        [Test]
        public void ToBundles_ShouldReturnRealBundles()
        {
            var skin = new SkinAutoBundle("/skins/my-skin", _fs);

            var actualBundles = skin.CreateBundles(AppPath);

            Assert.That(actualBundles, Has.Length.EqualTo(_expectedBundles.Count));
        }
    }
}
