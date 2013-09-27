using System.IO;

namespace ModernSkins
{
    public class StyleBundle
    {
        public string Name { get; set; }
        public string FilePath { get; set; }

        public StyleBundle(string filePath)
        {
            FilePath = filePath;

            Name = Path.GetFileNameWithoutExtension(filePath);
        }
    }
}