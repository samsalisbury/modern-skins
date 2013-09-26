using System.IO;
using NUnit.Framework;

namespace ModernSkins.Tests
{
    [TestFixture]
    public class SkinTests
    {
        static Skin CreateSkin(string name)
        {
            return new Skin
                   {
                       Name = name,
                       Dir = Path.Combine(TestHelper.SkinsDir, name)
                   };
        }

        [Test]
        public void EnumerateStyleBundles_ShouldListCorrectStyles()
        {

        }
    }
}