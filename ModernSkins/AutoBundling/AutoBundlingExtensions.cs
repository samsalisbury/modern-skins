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
        ///   You're usually better off calling the parameterless extension method "RegisterAutoBundles"
        /// </summary>
        public static void RegisterAutoBundles(this BundleCollection bundles, HttpServerUtilityBase server, IFileSystem fileSystem)
        {
            var autoBundleConfig = new SkinsDirAutoBundle(server.MapPath("~/Skins"), server.MapPath("~/"), fileSystem);
            var stubs = autoBundleConfig.CreateBundles().Select(b => b.ToBundle(server.MapPath("~/"), server.MapPath("~/Skins")));
            var realBundles = new CustomBundleFactory().CreateBundles(stubs);

            foreach (var bundle in realBundles)
            {
                bundles.Add(bundle);
            }
        }
    }
}