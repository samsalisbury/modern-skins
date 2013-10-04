namespace ModernSkins.Tests
{
    public class FakeFile : FakeFileSystemObject
    {
        public string Content { get; set; }

        public FakeFile(string name, string content) : base(name)
        {
            Content = content;
        }
    }
}