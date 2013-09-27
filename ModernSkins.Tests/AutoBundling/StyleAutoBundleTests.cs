using ModernSkins.AutoBundling;
using Moq;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class StyleAutoBundleTests
    {
        [TestCase("/my/app/Skins/name/styles/bundle.css", "/my/app/Skins", "~/name/styles/bundle")]
        public void ToStyleBundle_ReturnsBundleWithExpectedPath(string bundlePath, string skinsPath, string virtualPath)
        {
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fs => fs.FileExists(bundlePath)).Returns(true);

            var autoBundle = new StyleAutoBundle(bundlePath, mockFileSystem.Object);

            var bundle = autoBundle.ToStyleBundle(skinsPath);

            Assert.That(bundle.Path, Is.EqualTo(virtualPath));
        }
    }
}
