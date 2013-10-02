using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class ScriptsDirAutoBundleTests
    {
        [TestCase("bundle_a", "/scripts/bundle_a/a_file.js", "/scripts/bundle_a")]
        [TestCase("bundle_b", "/scripts/bundle_b/a_file.js", "/scripts/bundle_b")]
        [TestCase("js_bundle", "/scripts/js_bundle.coffee", "/scripts/js_bundle.coffee")]
        [TestCase("another_js_bundle", "/scripts/another_js_bundle.js", "/scripts/another_js_bundle.js")]
        public void GetScriptBundles_ReturnsExpectedBundles(string expectedName, string fileToCreate, string expectedPath)
        {
            var fs = new FakeUnixFileSystem();
            fs.AddFile(fileToCreate);

            const string scriptsPath = "/scripts";

            var bundler = new ScriptsDirAutoBundle(scriptsPath, fs);

            var scripts = bundler.GetScriptBundles();

            Assert.That(scripts.Keys, Contains.Item(expectedName));
            Assert.That(scripts[expectedName].Name, Is.EqualTo(expectedName));
            Assert.That(scripts[expectedName].FileSystemPath, Is.EqualTo(expectedPath));
        }
    }
}
