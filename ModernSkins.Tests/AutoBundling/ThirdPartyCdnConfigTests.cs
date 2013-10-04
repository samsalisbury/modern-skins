using ModernSkins.AutoBundling;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    public class ThirdPartyCdnConfigTests
    {
        [Test]
        public void FromJson_ReturnsCorrectConfigs()
        {
            const string thirdPartyCdnsJsonFormat = "[ {{ 'BundleName': '{0}', 'CdnUrl': '{1}', 'FallbackExpression': '{2}' }} ]";

            var json = string.Format(thirdPartyCdnsJsonFormat, "coolscript-1.0", "//third-party.cdn.com/coolscript-1.0.min.js", "window.coolscript");

            var configs = ThirdPartyCdnConfig.FromJson(json);

            Assert.That(configs, Has.Length.EqualTo(1));
        }
    }
}
