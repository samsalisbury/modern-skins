using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Optimization;
using System.Web.Routing;
using IronRuby.Builtins;
using ModernSkins.AutoBundling;

namespace ModernSkins
{
    public class SkinHelper
    {
        readonly string _skinName;
        readonly ModernSkinsConfig _config;

        public SkinHelper(string skinName, ModernSkinsConfig config = null)
        {
            _skinName = skinName;
            _config = config ?? ModernSkinsConfig.Default;
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

        public string ImageUrl(string skinRelativeImagePath)
        {
            // TODO: NB: Make this CDN Aware!!!

            return VirtualPathUtility.ToAbsolute(string.Format("{0}/{1}/images/{2}", _config.AppRelativeSkinsPath, _skinName, skinRelativeImagePath));
        }

        public IHtmlString Image(string imageSkinPath, string altText)
        {
            const string format = "<img src='{0}' alt='{1}' />";

            return MvcHtmlString.Create(string.Format(format, ImageUrl(imageSkinPath), altText));
        }

        public IHtmlString Partial(string partialViewName, HtmlHelper helper)
        {
            var virtualPath = string.Format("{0}/{1}/{2}", _config.AppRelativeSkinsPath, _skinName, partialViewName);

            return helper.Partial(virtualPath);
        }
    }
}
