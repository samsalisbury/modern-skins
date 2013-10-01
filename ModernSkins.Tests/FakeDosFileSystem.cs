namespace ModernSkins.Tests
{
    public class FakeDosFileSystem : FakeFileSystem
    {
        public override char DirSeparator
        {
            get { return '\\'; }
        }
    }
}