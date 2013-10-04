using System;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace ModernSkins
{
    /// <summary>
    ///   Static helper class for use in views.
    /// </summary>
    public static class Skin
    {
        static readonly string SkinKey = typeof (SkinHelper).FullName;

        static SkinHelper _defaultSkin;

        public static SkinHelper DefaultSkin
        {
            get { return _defaultSkin; }
        }

        /// <summary>
        ///   Sets the skin to use for calls to Skin.StyleBundle and Skin.ScriptBundle.
        /// </summary>
        /// <param name="skinName">
        ///   The skin name must be a directory name under the ~/Skins directory in
        ///   this web application.
        /// </param>
        /// <returns>
        ///   The return value is a hoax, to allow slightly neater one-line usage in views.
        /// </returns>
        /// <remarks>
        ///   Can also be used from Global, e.g. on Application_BeginRequest
        /// </remarks>
        public static IHtmlString SetSkin(string skinName)
        {
            CurrentSkin = new SkinHelper(skinName);

            return null;
        }

        /// <summary>
        ///   Sets the default skin for an application. May only be called once during an application life cycle. (Best to call in Global.Application_Start)
        /// </summary>
        /// <param name="skinName"></param>
        public static void SetDefaultSkin(string skinName)
        {
            if (_defaultSkin != null)
            {
                throw new InvalidOperationException("You may only call SetDefaultSkin once in an application life cycle. (e.g. once in Global.Application_Start)");
            }

            _defaultSkin = new SkinHelper(skinName);
        }

        public static SkinHelper CurrentSkin
        {
            get { return (SkinHelper) HttpContext.Current.Items[SkinKey] ?? DefaultSkin; }
            set { HttpContext.Current.Items.Add(SkinKey, value); }
        }

        public static IHtmlString StyleBundle(string styleBundleName)
        {
            VerifySkinHasBeenSet();

            return CurrentSkin.Style(styleBundleName);
        }

        public static IHtmlString ScriptBundle(string scriptBundleName)
        {
            VerifySkinHasBeenSet();

            return CurrentSkin.Script(scriptBundleName);
        }

        public static IHtmlString Image(string imageSkinPath, string altText = "")
        {
            VerifySkinHasBeenSet();

            return CurrentSkin.Image(imageSkinPath, altText);
        }

        public static IHtmlString Partial(string partialName, HtmlHelper html)
        {
            VerifySkinHasBeenSet();

            return CurrentSkin.Partial(partialName + ".cshtml", html);
        }

        static void VerifySkinHasBeenSet()
        {
            if (CurrentSkin == null)
            {
                throw new InvalidOperationException("You must set the skin in each request by calling Skin.SetSkin(skinName), or set a default skin using Skin.SetDefaultSkin(skinName).");
            }
        }
    }
}