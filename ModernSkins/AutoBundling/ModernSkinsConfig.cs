namespace ModernSkins.AutoBundling
{
    public class ModernSkinsConfig
    {
        public string AppRelativeSkinsPath { get; set; }

        public string ProxyCdnDomain { get; set; }

        /// <summary>
        ///   Creates a default config, with no CDN or anything fancy.
        ///   Use with an initialiser to override the defaults.
        /// </summary>
        public ModernSkinsConfig()
        {
            // Ctor sets default values
            AppRelativeSkinsPath = "~/Skins";
            ProxyCdnDomain = null;
        }

        /// <summary>
        ///   Note: all defaults are set in the constructor, this just makes that fact explicit.
        /// </summary>
        public static ModernSkinsConfig Default
        {
            get { return new ModernSkinsConfig(); }
        }
    }
}
