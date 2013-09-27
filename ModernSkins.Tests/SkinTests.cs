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

        [Test]
        public void Ctor_Throws_DirectoryNotFound_IfPassedPathToFile()
        {
            var filePath = TestHelper.ResolveAppDir("~/Skins/testskin/styles/other_styles.less");

            Assert.Throws<DirectoryNotFoundException>(() => new Skin(filePath));
        }

        [Test]
        public void Ctor_GivesSkinCorrectName()
        {
            var testSkinDir = TestHelper.ResolveAppDir("~/Skins/testskin");

            var skin = new Skin(testSkinDir);

            Assert.That(skin.Name, Is.EqualTo("testskin"));
        }

        [Test]
        public void Ctor_ReallyGivesSkinCorrectName()
        {
            var testSkinDir = TestHelper.ResolveAppDir("~/Skins/testskin2");

            var skin = new Skin(testSkinDir);

            Assert.That(skin.Name, Is.EqualTo("testskin2"));
        }
    }
}