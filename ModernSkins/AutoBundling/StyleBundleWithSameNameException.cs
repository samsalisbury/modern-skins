using System;

namespace ModernSkins.AutoBundling
{
    public class StyleBundleWithSameNameException : Exception
    {
        public StyleBundleWithSameNameException(AutoBundleBase existingBundle, AutoBundleBase newBundle)
            : base(string.Format("Cannot add {0}, there is already a bundle with the same name, from path {1}. " +
                                 "Each bundle is named after its filename without extension, so if you have a file called " +
                                 "'mybundle.less' and another called 'mybundle.scss' they will conflict. The exception to this rule " +
                                 "is CSS files, which will be silently ignored if they conflict. This is to allow tools to generate " +
                                 "these files at design time without interfering with debugging.",
                newBundle.FileSystemPath, existingBundle.FileSystemPath))
        {
        }
    }
}