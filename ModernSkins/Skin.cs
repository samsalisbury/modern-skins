using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModernSkins
{
    public class Skin
    {
        public string Name { get; set; }
        public string Dir { get; set; }

        public string StyleDir
        {
            get { return Path.Combine(Dir, "styles"); }
        }
    }
}