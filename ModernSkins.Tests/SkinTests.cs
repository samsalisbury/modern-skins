using System.IO;
using NUnit.Framework;

namespace ModernSkins.Tests
{
    [TestFixture]
    public class SkinTests
    {
        [TestCase("this is not an existent path")]
        [TestCase("C:\\Nor\\is\\this")]
        public void Ctor_Throws_DirectoryNotFound_IfPassedNonExistentPath(string nonExistentPath)
        {
            Assert.Throws<DirectoryNotFoundException>(() => new Skin(nonExistentPath));
        }

        [TestCase("~/Skins/testskin/styles/other_styles.less")]
        [TestCase("~/Skins/testskin2/styles/some_scss_styles2.scss")]
        public void Ctor_Throws_DirectoryNotFound_IfPassedPathToFile(string appRelativeFileDir)
        {
            var filePath = TestHelper.ResolveAppDir(appRelativeFileDir);

            Assert.Throws<DirectoryNotFoundException>(() => new Skin(filePath));
        }

        [TestCase("~/Skins/testskin", "testskin")]
        [TestCase("~/Skins/testskin2", "testskin2")]
        public void Ctor_GivesSkinCorrectName(string appRelativeSkinDir, string expectedName)
        {
            var testSkinDir = TestHelper.ResolveAppDir(appRelativeSkinDir);

            var skin = new Skin(testSkinDir);

            Assert.That(skin.Name, Is.EqualTo(expectedName));
        }
    }
}