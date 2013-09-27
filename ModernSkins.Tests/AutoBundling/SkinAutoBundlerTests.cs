using System.IO;
using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class SkinAutoBundlerTests
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
    }
}