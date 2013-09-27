using NUnit.Framework;

namespace ModernSkins.Tests
{
    [TestFixture]
    public class ScriptBundlerTests
    {
        [TestCase("bundle_a", "~/Skins/testskin/bundle_a")]
        [TestCase("bundle_b", "~/Skins/testskin/bundle_b")]
        [TestCase("js_bundle", "~/Skins/testskin/js_bundle.js")]
        [TestCase("another_js_bundle", "~/Skins/testskin/another_js_bundle.js")]
        public void GetScriptBundles_ReturnsExpectedBundles(string expectedName, string expectedPath)
        {
            var scriptsPath = TestHelper.ResolveAppDir("~/Skins/testskin/scripts");
            var bundler = new ScriptBundler(scriptsPath);

            var scripts = bundler.GetScriptBundles();

            Assert.That(scripts, Has.Count.EqualTo(3));
            Assert.That(scripts.Keys, Contains.Item(expectedName));
            Assert.That(scripts[expectedName].Name, Is.EqualTo(expectedName));
            Assert.That(scripts[expectedName].Path, Is.EqualTo(TestHelper.ResolveAppDir(expectedPath)));
        }
    }
}
