using System.Web;
using System.Web.Optimization;

namespace ModernSkins
{
    public class SkinHelper
    {
        readonly string _skinName;

        public SkinHelper(string skinName)
        {
            _skinName = skinName;
        }

        public string StyleUrl(string styleBundleName)
        {
            return string.Format("~/{0}/styles/{1}", _skinName, styleBundleName);
        }

        public string ScriptUrl(string scriptBundleName)
        {
            return string.Format("~/{0}/scripts/{1}", _skinName, scriptBundleName);
        }

        public IHtmlString Style(string styleBundleName)
        {
            return Styles.Render(StyleUrl(styleBundleName));
        }

        public IHtmlString Script(string scriptBundleName)
        {
            return Scripts.Render(ScriptUrl(scriptBundleName));
        }
    }
}