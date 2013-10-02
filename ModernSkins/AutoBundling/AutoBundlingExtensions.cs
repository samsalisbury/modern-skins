using System;
using System.Web;
using System.Web.Optimization;

namespace ModernSkins.AutoBundling
{
    public static class AutoBundlingExtensions
    {
        /// <summary>
        ///   Call this from Global.Application_Start to register real life bundles on a website.
        /// </summary>
        public static void RegisterAutoBundles(this BundleCollection bundles)
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("HttpContext.Current is null, if you're calling this from a test please pass in an HttpServerUtilityBase");
            }

            RegisterAutoBundles(bundles, HttpContext.Current.Server, new TheLocalFileSystem());
        }

        /// <summary>
        ///   This is mainly for testing.
        /// </summary>
        public static void RegisterAutoBundles(this BundleCollection bundles, HttpServerUtility server, IFileSystem fileSystem)
        {
            RegisterAutoBundles(bundles, new HttpServerUtilityWrapper(server), fileSystem);
        }

        /// <summary>
        ///   This is mainly for testing, too.
        /// </summary>
        public static void RegisterAutoBundles(this BundleCollection bundles, HttpServerUtilityBase server, IFileSystem fileSystem)
        {
            var autoBundleConfig = new SkinsDirAutoBundle(server.MapPath("~/Skins"), server.MapPath("~/"), fileSystem);

            autoBundleConfig.RegisterBundles(bundles);
        }
    }
}