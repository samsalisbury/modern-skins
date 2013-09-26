using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;
using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;

namespace ModernSkins
{
    public class AutoCssBundleConfig
    {
        const string AppRelativeSkinPath = "~/Skins";

        static readonly Dictionary<string, Skin> Skins = new Dictionary<string, Skin>();

        static void Init(string skinsBaseDir)
        {
            //var skinsBaseDir = HttpContext.Current.Server.MapPath("~/Skins");
            var skinPaths = Directory.GetDirectories(skinsBaseDir);
            foreach (var path in skinPaths)
            {
                var name = GetLastPathPart(path);
                Skins[name] = CreateSkin(path);
            }
        }

        static string GetLastPathPart(string path)
        {
            return path.Split(Path.DirectorySeparatorChar).Last();
        }

        static Skin CreateSkin(string skinPath)
        {
            var skinName = GetLastPathPart(skinPath);

            var cssBundles = CreateCssBundles(Path.Combine(skinPath, "styles"), skinName);

            var jsBundles = CreateJsBundles(Path.Combine(skinPath, "scripts"), skinName);

            return new Skin
            {
                //StyleBundles = cssBundles
            };
        }

        static object CreateJsBundles(string scriptDirectory, string skinName)
        {
            throw new NotImplementedException();
        }

        static Dictionary<string, Bundle> CreateCssBundles(string cssDirectory, string skinName)
        {
            var files = Directory.GetFiles(cssDirectory);
            var bundles = new Dictionary<string, Bundle>();
            foreach (var file in files)
            {
                var fileName = GetLastPathPart(file);
                var bundleName = GetFileNameWithoutExtension(fileName);
                bundles[bundleName] = CreateCssBundle(file, skinName);
            }

            return bundles;
        }

        static Bundle CreateCssBundle(string filePath, string skinName)
        {
            var styleBundleName = GetFileNameWithoutExtension(GetLastPathPart(filePath));
            var bundlePath = string.Format("~/{0}/styles/{1}", skinName, styleBundleName);
            var bundle = new CustomStyleBundle(bundlePath);
            bundle.Include(skinName);
            bundle.Orderer = new NullOrderer();
            //bundle.Transforms.Add(new CssMinify());

            return bundle;
        }

        static string GetFileNameWithoutExtension(string fileName)
        {
            var parts = fileName.Split('.');
            return string.Join(".", parts.Take(parts.Length - 1));
        }

        public AutoCssBundleConfig()
        {

        }
    }
}
