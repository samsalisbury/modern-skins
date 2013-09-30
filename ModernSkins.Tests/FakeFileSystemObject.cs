namespace ModernSkins.Tests
{
    public abstract class FakeFileSystemObject
    {
        public string Name { get; private set; }

        protected FakeFileSystemObject(string name)
        {
            Name = name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as FakeFileSystemObject;

            if (other == null)
            {
                return false;
            }

            return other.Name == Name;
        }
    }
}