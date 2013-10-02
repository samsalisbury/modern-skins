namespace ModernSkins.AutoBundling
{
    public abstract class BundleStub
    {
        public string VirtualUrl { get; set; }
        public string[] AppRelativeContentPaths { get; set; }
    }

    public class ScriptBundleStub : BundleStub
    {
        
    }

    public class StyleBundleStub : BundleStub
    {
        
    }
}