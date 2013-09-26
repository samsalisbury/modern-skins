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
        internal static readonly string SkinsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Skins");
    }
}
