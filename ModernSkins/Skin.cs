using System;
using System.Web;

namespace ModernSkins
{
    /// <summary>
    ///   Static helper class for use in views.
    /// </summary>
    public static class Skin
    {
        static readonly string SkinKey = typeof (SkinHelper).FullName;

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
            if (CurrentSkin != null)
            {
                throw new InvalidOperationException("You may only call SetSkin once per request.");
            }
            
            CurrentSkin = new SkinHelper(skinName);

            return null;
        }

        static SkinHelper CurrentSkin
        {
            get { return (SkinHelper) HttpContext.Current.Items[SkinKey]; }
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

        static void VerifySkinHasBeenSet()
        {
            if (CurrentSkin == null)
            {
                throw new InvalidOperationException("You must set the skin in each request by calling Skin.SetSkin(skinName)");
            }
        }
    }
}