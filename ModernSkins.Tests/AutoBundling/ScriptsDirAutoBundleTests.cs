using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class ScriptsDirAutoBundleTests
    {
        [TestCase("bundle_a", "/scripts/bundle_a/a_file.js", "/scripts/bundle_a")]
        [TestCase("bundle_b", "/scripts/bundle_b/a_file.js", "/scripts/bundle_b")]
        [TestCase("js_bundle", "/scripts/js_bundle.js", "/scripts/js_bundle.js")]
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

        [Test]
        public void GetScriptBundles_WhenThereAreNonJsFilesInTheScriptsDir_TheyAreIgnored()
        {
            var fs = new FakeUnixFileSystem();
            fs.AddFile("/scripts/a_js_file.js");
            fs.AddFile("/scripts/a_non_js_file.json");
            fs.AddFile("/scripts/another_non_je_file.json");

            var bundler = new ScriptsDirAutoBundle("/scripts", fs);

            var scripts = bundler.GetScriptBundles();

            Assert.That(scripts, Has.Count.EqualTo(1));
            Assert.That(scripts["a_js_file"].Name, Is.EqualTo("a_js_file"));
        }

        [Test]
        public void GetScriptBundles_WithThirdPartyCdnConfigs_CreatesCorrectScriptAutoBundle()
        {
            const string thirdPartyCdnsJsonFormat = "{{'{0}': {{ 'cdnUrl': '{1}', 'FallbackExpression': '{2}' }} }}";

            var json = string.Format(thirdPartyCdnsJsonFormat, "coolscript-1.0", "//third-party.cdn.com/coolscript-1.0.min.js", "window.coolscript");

            var fs = new FakeUnixFileSystem();
            fs.AddFiles("/app/skins/skin1/scripts[coolscript-1.0.js,otherscript.js]");

            fs.AddFile("/app/skins/skin1/scripts/third_party_cdns.json", json);

            var bundler = new ScriptsDirAutoBundle("/app/skins/skin1/scripts", fs);

            var scripts = bundler.GetScriptBundles();

            Assume.That(scripts, Has.Count.EqualTo(2));
            Assert.That(scripts["coolscript-1.0"].ThirdPartyCdnUrl, Is.EqualTo("//third-party.cdn.com/coolscript-1.0.min.js"));
            Assert.That(scripts["coolscript-1.0"].CdnFallbackExpression, Is.EqualTo("window.coolscript"));
            Assert.That(scripts["otherscript"].ThirdPartyCdnUrl, Is.Null);
            Assert.That(scripts["otherscript"].CdnFallbackExpression, Is.Null);
        }
    }
}
