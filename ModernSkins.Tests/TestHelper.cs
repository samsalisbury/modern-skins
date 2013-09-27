using System;
using System.IO;

namespace ModernSkins.Tests
{
    class TestHelper
    {
        /// <summary>
        ///   NOTE: For the tests, all files inside 'Skins' MUST be set to copy to output directory: Always Copy.
        ///   'Copy if newer' is no good, since we always want to reset them to the versions in the project.
        /// </summary>
        internal static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

        internal static readonly string SkinsDir = ResolveAppDir("~/Skins");

        public static string ResolveAppDir(string relativeDirectory)
        {
            if (!relativeDirectory.StartsWith("~/"))
            {
                throw new ArgumentException("TestHelper: app relative directories must start with \"~/\"");
            }

            // Hacky, should work on Windows/Mac/Linux
            relativeDirectory = relativeDirectory
                .Replace("~/", "")
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);

            return Path.Combine(BaseDir, relativeDirectory);
        }
    }
}
