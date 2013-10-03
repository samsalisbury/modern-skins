using System;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public static class AutoBundlingExtensions
    {
        /// <summary>
        ///   Call this from Global.Application_Start to register real life bundles on a website.
        /// </summary>
        public static void RegisterAutoBundles(this BundleCollection bundles, ModernSkinsConfig config = null)
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("HttpContext.Current is null, if you're calling this from a test please pass in an HttpServerUtilityBase");
            }

            RegisterAutoBundles(bundles, HttpContext.Current.Server, new TheLocalFileSystem(), config);
        }

        /// <summary>
        ///   This maps the simple overload for real-world usage (above) to the nitty gritty implementation (below).
        /// </summary>
        static void RegisterAutoBundles(this BundleCollection bundles, HttpServerUtility server, IFileSystem fileSystem, ModernSkinsConfig config)
        {
            RegisterAutoBundles(bundles, new HttpServerUtilityWrapper(server), fileSystem, config);
        }

        /// <summary>
        ///   You're usually better off calling the parameterless extension method "BundleTable.Bundles.RegisterAutoBundles()", unless you're writing a test...
        /// </summary>
        public static void RegisterAutoBundles(this BundleCollection bundles, HttpServerUtilityBase server, IFileSystem fileSystem, ModernSkinsConfig config = null)
        {
            config = config ?? ModernSkinsConfig.Default;

            // TODO: Why am I passing in appPath and skinsPath paths twice? Probably a smell...
            var appPath = server.MapPath("~/");
            var skinsPath = server.MapPath(config.AppRelativeSkinsPath);

            var autoBundleConfig = new SkinsDirAutoBundle(skinsPath, appPath, fileSystem);
            var bundleFactory = new CustomBundleFactory(appPath, skinsPath, config.ProxyCdnDomain);
            var realBundles = autoBundleConfig.CreateBundles().Select(bundleFactory.Manufacture);

            foreach (var bundle in realBundles)
            {
                bundles.Add(bundle);
            }
        }
    }
}
