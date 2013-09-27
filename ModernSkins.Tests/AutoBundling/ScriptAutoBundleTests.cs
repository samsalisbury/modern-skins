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
    }
}
