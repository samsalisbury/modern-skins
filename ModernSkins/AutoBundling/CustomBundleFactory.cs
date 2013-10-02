using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;

namespace ModernSkins.AutoBundling
{
    public class CustomBundleFactory
    {
        public IList<Bundle> CreateBundles(IEnumerable<BundleStub> bundles)
        {
            return bundles.Select(CreateBundle).ToList();
        }

        static Bundle CreateBundle(BundleStub stub)
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

        static ScriptBundle CreateScriptBundle(BundleStub bundleStub)
        {
            var bundle = new ScriptBundle(bundleStub.VirtualUrl);
            bundle.Include(bundleStub.AppRelativeContentPaths);
            bundle.Orderer = new DefaultBundleOrderer();
            bundle.Builder = new DefaultBundleBuilder();
            bundle.Transforms.Add(new JsMinify());

            return bundle;
        }

        static CustomStyleBundle CreateStyleBundle(BundleStub bundleStub)
        {
            var bundle = new CustomStyleBundle(bundleStub.VirtualUrl);
            bundle.Include(bundleStub.AppRelativeContentPaths);
            bundle.Orderer = new NullOrderer();

            return bundle;
        }
    }
}