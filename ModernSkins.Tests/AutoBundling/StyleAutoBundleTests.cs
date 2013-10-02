using ModernSkins.AutoBundling;
using Moq;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class StyleAutoBundleTests
    {
        [TestCase("/my/app/Skins/name/styles/bundle.css", "/my/app", "/my/app/Skins", "~/name/styles/bundle")]
        public void ToStyleBundle_ReturnsBundleWithExpectedPath(string bundlePath, string appPath, string skinsPath, string virtualPath)
        {
            var fs = new FakeUnixFileSystem();
            fs.AddFile(bundlePath);

            var autoBundle = new StyleAutoBundle(bundlePath, fs);

            var bundle = autoBundle.ToBundle(appPath, skinsPath);

            Assert.That(bundle.VirtualUrl, Is.EqualTo(virtualPath));
        }

        [TestCase("C:\\MyApp\\Skins\\MyBundle.css")]
        [TestCase("C:\\MyApp\\Skins\\MyBundle.less")]
        [TestCase("C:\\MyApp\\Skins\\MyBundle.scss")]
        public void ListFilesToBundle_ReturnsExpectedFileList(string path)
        {
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fs => fs.FileExists(It.IsAny<string>())).Returns(true);

            var autoBundle = new StyleAutoBundle(path, mockFileSystem.Object);

            var result = autoBundle.ListFilesToBundle();

            Assert.That(result, Has.Length.EqualTo(1));
            Assert.That(result, Contains.Item(path));
        }
    }
}
