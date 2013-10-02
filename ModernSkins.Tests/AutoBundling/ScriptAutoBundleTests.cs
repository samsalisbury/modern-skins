using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Optimization;
using ModernSkins.AutoBundling;
using Moq;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class ScriptAutoBundleTests
    {
        [TestCase("C:\\MyApp\\Skins\\scripts\\MyScripts.js")]
        public void ListFilesToBundle_WhenSingleFile_ReturnsThatFile(string singleFileBundlePath)
        {
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);

            var autoBundle = new ScriptAutoBundle(singleFileBundlePath, mockFileSystem.Object);

            var result = autoBundle.ListFilesToBundle();

            Assert.That(result, Has.Length.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(singleFileBundlePath));
        }

        [Test]
        public void ListFilesToBundle_GivenDirectory_ReturnsExpectedFiles()
        {
            var filesInDir = new[]
                             {
                                 "some-file.js",
                                 "another-js-file.js"
                             };

            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fs => fs.GetFiles(It.IsAny<string>())).Returns(filesInDir);
            mockFileSystem.Setup(fs => fs.DirExists(It.IsAny<string>())).Returns(true);

            var autoBundle = new ScriptAutoBundle("any-path", mockFileSystem.Object);

            var result = autoBundle.ListFilesToBundle();

            Assert.That(result.Length, Is.EqualTo(filesInDir.Length));
            Assert.That(result[0], Is.EqualTo(filesInDir[0]));
            Assert.That(result[1], Is.EqualTo(filesInDir[1]));
        }

        [Test]
        public void ToBundle_WhenPathIsDirectory_ReturnsExpectedBundle()
        {
            var fs = new FakeUnixFileSystem();
            fs.AddFiles("/app/skins/myskin/scripts/mybundle[bundle_file_1.js,bundle_file_2.js]");
            fs.AddFiles("/app/skins/myskin/scripts/[bundle_a.js,bundle_b.js]");

            var autoBundle = new ScriptAutoBundle("/app/skins/myskin/scripts/mybundle", fs);
            var bundle = autoBundle.ToBundle("/app", "/app/skins");

            Assert.That(bundle.VirtualUrl, Is.EqualTo("~/myskin/scripts/mybundle"));
            Assert.That(bundle.AppRelativeContentPaths, Has.Length.EqualTo(2));
            Assert.That(bundle.AppRelativeContentPaths[0], Is.EqualTo("~/skins/myskin/scripts/mybundle/bundle_file_1.js"));
            Assert.That(bundle.AppRelativeContentPaths[1], Is.EqualTo("~/skins/myskin/scripts/mybundle/bundle_file_2.js"));
        }

        [Test]
        public void ToBundle_WhenPathIsFile_ReturnsExpectedBundle()
        {
            var fs = new FakeUnixFileSystem();
            fs.AddFiles("/app/skins/myskin/scripts/[bundle_a.js,bundle_b.js]");

            var autoBundle = new ScriptAutoBundle("/app/skins/myskin/scripts/bundle_a.js", fs);
            var bundle = autoBundle.ToBundle("/app", "/app/skins");

            Assert.That(bundle.VirtualUrl, Is.EqualTo("~/myskin/scripts/bundle_a"));
            Assert.That(bundle.AppRelativeContentPaths, Has.Length.EqualTo(1));
            Assert.That(bundle.AppRelativeContentPaths[0], Is.EqualTo("~/skins/myskin/scripts/bundle_a.js"));
        }
    }
}
