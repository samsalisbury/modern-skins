using System;
using System.Web;
using System.Web.Optimization;
using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;

namespace ModernSkins.AutoBundling
{
    public class CustomBundleFactory
    {
        public string AppPath { get; set; }
        public string SkinsPath { get; set; }
        public string ProxyCdnDomain { get; set; }

        public CustomBundleFactory(string appPath, string skinsPath, string proxyCdnDomain)
        {
            AppPath = appPath;
            SkinsPath = skinsPath;
            ProxyCdnDomain = proxyCdnDomain;
        }

        public Bundle Manufacture(IRepresentAnActualBundle autoBundle)
        {
            var stub = CreateStub(autoBundle);
            return CreateBundle(stub);
        }

        public BundleStub CreateStub(IRepresentAnActualBundle autoBundle)
        {
            return autoBundle.ToBundle(AppPath, SkinsPath);
        }

        Bundle CreateBundle(BundleStub stub)
        {
            if (stub is ScriptBundleStub)
            {
                return CreateScriptBundle(stub);
            }

            if (stub is StyleBundleStub)
            {
                return CreateStyleBundle(stub);
            }

            throw new NotImplementedException(string.Format("Bundling not implemented for {0}.", stub.GetType()));
        }

        ScriptBundle CreateScriptBundle(BundleStub bundleStub)
        {
            var bundle = new ScriptBundle(bundleStub.VirtualUrl);
            bundle.Include(bundleStub.AppRelativeContentPaths);
            bundle.Orderer = new DefaultBundleOrderer();
            bundle.Builder = new DefaultBundleBuilder();
            bundle.Transforms.Add(new JsMinify());

            AddProxyCdnIfDefined(bundle);

            return bundle;
        }

        CustomStyleBundle CreateStyleBundle(BundleStub bundleStub)
        {
            var bundle = new CustomStyleBundle(bundleStub.VirtualUrl);
            bundle.Include(bundleStub.AppRelativeContentPaths);
            bundle.Orderer = new NullOrderer();

            AddProxyCdnIfDefined(bundle);

            return bundle;
        }

        string GenerateProxyCdnPath(string virtualUrl)
        {
            var root = "//" + ProxyCdnDomain;
            var absoluteUrl = VirtualPathUtility.ToAbsolute(virtualUrl);

            return root + absoluteUrl;
        }

        void AddProxyCdnIfDefined(Bundle bundle)
        {
            if (ProxyCdnDomain != null)
            {
                bundle.CdnPath = GenerateProxyCdnPath(bundle.Path);
            }
        }
    }
}