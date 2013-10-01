namespace ModernSkins.Tests
{
    public class FakeUnixFileSystem : FakeFileSystem
    {
        public override char DirSeparator
        {
            get { return '/'; }
        }
    }
}