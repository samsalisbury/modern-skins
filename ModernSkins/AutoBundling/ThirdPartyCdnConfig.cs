using System.Collections.Generic;
using System.Web.Helpers;

namespace ModernSkins.AutoBundling
{
    public class ThirdPartyCdnConfig
    {
        public string BundleName { get; set; }
        public string Url { get; set; }
        public string FallbackExpression { get; set; }

        public static ThirdPartyCdnConfig[] FromJson(string json)
        {
            var data = Json.Decode(json);

            var configs = new List<ThirdPartyCdnConfig>();
            foreach (var config in data)
            {
                configs.Add(new ThirdPartyCdnConfig
                            {
                                BundleName = config.BundleName,
                                Url = config.CdnUrl,
                                FallbackExpression = config.FallbackExpression
                            });
            }

            return configs.ToArray();
        }
    }
}
