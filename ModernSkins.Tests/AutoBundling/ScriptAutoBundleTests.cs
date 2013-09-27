using System.IO;
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
    }
}
