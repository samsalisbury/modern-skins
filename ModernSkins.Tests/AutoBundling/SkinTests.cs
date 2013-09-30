using System;
using System.Collections.Generic;
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
            var fs = new FakeFileSystem();

            Assert.Throws<DirectoryNotFoundException>(() => new Skin(nonExistentPath, fs));
        }

        [TestCase("/Skins/testskin/styles/other_styles.less")]
        [TestCase("/Skins/testskin2/styles/some_scss_styles2.scss")]
        public void Ctor_Throws_DirectoryNotFound_IfPassedPathToFile(string filePath)
        {
            var fs = new FakeFileSystem();
            fs.AddFile(filePath);

            Assert.Throws<DirectoryNotFoundException>(() => new Skin(filePath, fs));
        }

        [TestCase("/Skins/testskin", "testskin")]
        [TestCase("/Skins/testskin2", "testskin2")]
        public void Ctor_GivesSkinCorrectName(string skinDir, string expectedName)
        {
            var fs = new FakeFileSystem();
            fs.AddDirectory(skinDir);

            var skin = new Skin(skinDir, fs);

            Assert.That(skin.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void CreateBundles_ReturnsExpectedBundles()
        {
            const string basePath = "/skins";
            const string testSkinDir = "/skins/my-skin";

            var fs = new FakeFileSystem();

            var expectations = new Dictionary<string, Tuple<string, Type>>();

            // Scripts
            fs.AddFile("/skins/my-skin/scripts/crazy_bundle/file1.js");
            fs.AddFile("/skins/my-skin/scripts/crazy_bundle/file2.js");
            fs.AddFile("/skins/my-skin/scripts/crazy_bundle/dirs_inside_scripts_are_bundles.js");
            expectations.Add("crazy_bundle", new Tuple<string, Type>("~/my-skin/scripts/crazy_bundle", typeof (ScriptAutoBundle)));

            fs.AddFile("/skins/my-skin/scripts/bundlino/file_z.js");
            expectations.Add("bundlino", new Tuple<string, Type>("~/my-skin/scripts/bundlino", typeof (ScriptAutoBundle)));

            fs.AddFile("/skins/my-skin/scripts/scripterific.js");
            expectations.Add("scripterific", new Tuple<string, Type>("~/my-skin/scripts/scripterific", typeof (ScriptAutoBundle)));

            fs.AddFile("/skins/my-skin/scripts/java.coffee");
            expectations.Add("java", new Tuple<string, Type>("~/my-skin/scripts/java", typeof (ScriptAutoBundle)));

            fs.AddFile("/skins/my-skin/scripts/all_files_directly_in_scripts_are_separate_bundles.coffee");
            expectations.Add("all_files_directly_in_scripts_are_separate_bundles",
                new Tuple<string, Type>("~/my-skin/scripts/all_files_directly_in_scripts_are_separate_bundles", typeof (ScriptAutoBundle)));

            // Styles
            fs.AddFile("/skins/my-skin/styles/my_lovely_styles.css");
            expectations.Add("my_lovely_styles", new Tuple<string, Type>("~/my-skin/styles/my_lovely_styles", typeof (StyleAutoBundle)));

            fs.AddFile("/skins/my-skin/styles/my_sytlings.sass");
            expectations.Add("my_sytlings", new Tuple<string, Type>("~/my-skin/styles/my_sytlings", typeof (StyleAutoBundle)));

            fs.AddFile("/skins/my-skin/styles/my_rad_stiles.scss");
            expectations.Add("my_rad_stiles", new Tuple<string, Type>("~/my-skin/styles/my_rad_stiles", typeof (StyleAutoBundle)));

            fs.AddFile("/skins/my-skin/styles/not_much.less");
            expectations.Add("not_much", new Tuple<string, Type>("~/my-skin/styles/not_much", typeof (StyleAutoBundle)));

            fs.AddFile("/skins/my-skin/styles/some-lib/a_lib_file.less");
            fs.AddFile("/skins/my-skin/styles/some-lib/another_file_in_a_lib.less");
            fs.AddFile("/skins/my-skin/styles/some-lib/lib_files_do_not_show_up_as_bundles.css");
            fs.AddFile("/skins/my-skin/styles/some-lib/and_nor_do_their_folders.css");
            // Note: The above stanza adds no expectations, since it's a lib in the styles folder :)

            var skin = new Skin(testSkinDir, fs);

            var bundles = skin.CreateBundles();

            Assert.That(bundles, Has.Length.EqualTo(expectations.Count));

            foreach (var expectation in expectations)
            {
                var expectedName = expectation.Key;
                var expectedVirtualPath = expectation.Value.Item1;
                var expectedType = expectation.Value.Item2;

                var bundle = bundles.SingleOrDefault(b => b.VirtualPath(basePath) == expectedVirtualPath);
                Assert.That(bundle, Is.Not.Null, "Bundle {0} not found.", expectedVirtualPath);
                Assert.That(bundle, Is.TypeOf(expectedType));
                Assert.That(bundle.Name, Is.EqualTo(expectedName));
            }
        }
    }
}
