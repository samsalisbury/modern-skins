using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class ScriptAutoBundlerTests
    {
        [TestCase("bundle_a", "~/Skins/testskin/scripts/bundle_a")]
        [TestCase("bundle_b", "~/Skins/testskin/scripts/bundle_b")]
        [TestCase("js_bundle", "~/Skins/testskin/scripts/js_bundle.js")]
        [TestCase("another_js_bundle", "~/Skins/testskin/scripts/another_js_bundle.js")]
        public void GetScriptBundles_ReturnsExpectedBundles(string expectedName, string expectedPath)
        {
            var scriptsPath = TestHelper.ResolveAppDir("~/Skins/testskin/scripts");
            var bundler = new ScriptAutoBundler(scriptsPath, new FileSystem());

            var scripts = bundler.GetScriptBundles();

            Assert.That(scripts, Has.Count.EqualTo(4));
            Assert.That(scripts.Keys, Contains.Item(expectedName));
            Assert.That(scripts[expectedName].Name, Is.EqualTo(expectedName));
            Assert.That(scripts[expectedName].Path, Is.EqualTo(TestHelper.ResolveAppDir(expectedPath)));
        }
    }
}
