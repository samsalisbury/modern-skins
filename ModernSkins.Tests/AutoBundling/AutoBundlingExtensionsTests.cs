using System;
using System.Web;
using System.Web.Optimization;
using ModernSkins.AutoBundling;
using Moq;
using NUnit.Framework;

namespace ModernSkins.Tests.AutoBundling
{
    [TestFixture]
    public class AutoBundlingExtensionsTests
    {
        [Test]
        public void RegisterAutoBundles_WhenCalledWithoutHttpContextCurrent_ThrowsInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => new BundleCollection().RegisterAutoBundles());
        }

        [Test]
        public void RegisterAutoBundles_RegistersExpectedBundles()
        {
            var server = new Mock<HttpServerUtilityBase>();
            var fs = new FakeDosFileSystem();
            fs.AddFiles("C:\\MyApp\\Skins\\skin1\\scripts[script1.js,script2.js]");
            fs.AddFiles("C:\\MyApp\\Skins\\skin1\\styles[style1.css,style2.sass]");
            server.Setup(s => s.MapPath("~/")).Returns("C:\\MyApp");
            server.Setup(s => s.MapPath("~/Skins")).Returns("C:\\MyApp\\Skins");

            var bundles = new BundleCollection();

            bundles.RegisterAutoBundles(server.Object, fs);

            Assert.That(bundles.Count, Is.EqualTo(4));
        }
    }
}