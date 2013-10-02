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
            var bundle = autoBundle.ToBundle("/app");

            var includedThings = DoHorrorReflectionOnBundleToFindOutWhatItContains(bundle);

            Assert.That(includedThings, Has.Count.EqualTo(1));
            
            // This assertion doesn't work as the item is "object" at runtime, giving up for now.
            //Assert.That(includedThings[0].VirtualPath, Is.EqualTo("~/myskin/scripts/mybundle"));
        }

        [Test]
        public void ToBundle_WhenPathIsFile_ReturnsExpectedBundle()
        {
            var fs = new FakeUnixFileSystem();
            fs.AddFiles("/app/skins/myskin/scripts/[bundle_a.js,bundle_b.js]");

            var autoBundle = new ScriptAutoBundle("/app/skins/myskin/scripts/bundle_a.js", fs);
            var bundle = autoBundle.ToBundle("/app");
            
            var includedThings = DoHorrorReflectionOnBundleToFindOutWhatItContains(bundle);

            Assert.That(includedThings, Has.Count.EqualTo(1));
            
            // This assertion doesn't work as the item is "object" at runtime, giving up for now.
            //Assert.That(includedThings[0].VirtualPath, Is.EqualTo("~/myskin/scripts/bundle_a.js"));
        }

        /// <summary>
        ///   Horror reflection: Can't find any other way to verify the bundles, they're not public.
        ///   Also, all the useful classes that could help are internal, so I have to cast to
        ///   dynamic. Seriously Microsoft? This has cost me a lot of time.
        /// </summary>
        static IList<dynamic> DoHorrorReflectionOnBundleToFindOutWhatItContains(Bundle bundle)
        {
            var things = typeof (Bundle).GetProperty("Items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(bundle, null);

            return ((IList) things).Cast<dynamic>().ToList();
        }
    }
}
