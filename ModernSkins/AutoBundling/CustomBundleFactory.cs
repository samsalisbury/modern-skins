using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using BundleTransformer.Core.Bundles;

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

        static CustomScriptBundle CreateScriptBundle(BundleStub bundleStub)
        {
            var bundle = new CustomScriptBundle(bundleStub.VirtualUrl);
            bundle.Include(bundleStub.AppRelativeContentPaths);

            return bundle;
        }

        static CustomStyleBundle CreateStyleBundle(BundleStub bundleStub)
        {
            var bundle = new CustomStyleBundle(bundleStub.VirtualUrl);
            bundle.Include(bundleStub.AppRelativeContentPaths);

            return bundle;
        }
    }
}