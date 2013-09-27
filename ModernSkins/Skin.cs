using System.IO;

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

        public Skin(string dir)
        {
            if (!Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException(dir);
            }

            Dir = dir;
            Name = Path.GetFileName(dir);
        }
    }
}
