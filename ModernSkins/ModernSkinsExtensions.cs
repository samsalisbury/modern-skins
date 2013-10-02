using System.Web.Mvc;

namespace ModernSkins
{
    public static class ModernSkinsExtensions
    {
        public static SkinHelper Skin(this HtmlHelper htmlHelper, string skinName)
        {
            return new SkinHelper(htmlHelper, skinName);
        }
    }
}
