using System.Linq;
using NUnit.Framework;

namespace ModernSkins.Tests
{
    [TestFixture]
    public class FakeFileSystemTests
    {
        [Test]
        public void AddDirectory_DirExists()
        {
            var fs = new FakeFileSystem();
            const string dirName = "MyApp/Skins/skin1";

            fs.AddDirectory(dirName);

            Assert.True(fs.DirExists(dirName));
        }

        [Test]
        public void AddDirectory_AddDirectory_CreatesSubDirectory_NotFiles()
        {
            var fs = new FakeFileSystem();
            const string dirName = "MyApp/Skins/skin1";
            const string subDirName = "scripts";
            const string subDirPath = dirName + "/" + subDirName;
            
            fs.AddDirectory(dirName).AddDirectory(subDirName);

            Assert.True(fs.DirExists(subDirPath));
            Assert.False(fs.FileExists(subDirPath));
        }

        [Test]
        public void AddDirectory_MergesDirectories()
        {
            var fs = new FakeFileSystem();

            var dir1 = fs.AddDirectory("path/that/is/duplicated");
            var dir2 = fs.AddDirectory("path/that/is/duplicated/thing-inside");
            var dir3 = fs.AddDirectory("path/that/is/cool");
            var dir4 = fs.AddDirectory("paht/that/is/the/other/one");

            Assert.That(fs.GetDirectories("path/that/is"), Has.Length.EqualTo(3));
            Assert.That(fs.GetDirectories("path/that/is")[0], Is.EqualTo("path/that/is/duplicated"));
            Assert.That(fs.GetDirectories("path/that/is")[1], Is.EqualTo("path/that/is/cool"));
            Assert.That(fs.GetDirectories("path/that/is")[2], Is.EqualTo("path/that/is/the"));
        }

        [Test]
        public void AddDirectory_AddFiles_CreatesExpectedFiles_NotDirectories()
        {
            var fs = new FakeFileSystem();
            const string dirName = "SomeDirectory/Hello";
            var files = new[] {"file1.jpg", "file2", "file_three.txt"};
            var filePaths = files.Select(name => dirName + "/" + name).ToArray();

            fs.AddDirectory(dirName).AddFiles(files);

            Assert.True(fs.FileExists(filePaths[0]));
            Assert.True(fs.FileExists(filePaths[1]));
            Assert.True(fs.FileExists(filePaths[2]));

            Assert.False(fs.DirExists(filePaths[0]));
            Assert.False(fs.DirExists(filePaths[1]));
            Assert.False(fs.DirExists(filePaths[2]));
        }

        [Test]
        public void GetDirectories_ReturnsExpectedDirectories()
        {
            var fs = new FakeFileSystem();
            var baseDir = fs.AddDirectory("basedir");
            baseDir.AddDirectory("dir1");
            baseDir.AddDirectory("dir2");
            baseDir.AddDirectory("dir3");
            baseDir.AddFiles("file1", "file2");

            var result = fs.GetDirectories("basedir");

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.That(result[0], Is.EqualTo("basedir/" + "dir1"));
            Assert.That(result[1], Is.EqualTo("basedir/" + "dir2"));
            Assert.That(result[2], Is.EqualTo("basedir/" + "dir3"));
        }

        [Test]
        public void GetFiles_ReturnsExpectedFiles()
        {
            var fs = new FakeFileSystem();
            var baseDir = fs.AddDirectory("basedir");
            baseDir.AddDirectory("dir1");
            baseDir.AddDirectory("dir2");
            baseDir.AddFiles("file1", "file2", "file3");

            var result = fs.GetFiles("basedir");

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.That(result[0], Is.EqualTo("basedir/" + "file1"));
            Assert.That(result[1], Is.EqualTo("basedir/" + "file2"));
            Assert.That(result[2], Is.EqualTo("basedir/" + "file3"));
        }

        [Test]
        public void GetFileSystemEntries_ReturnsAllEntries()
        {
            var fs = new FakeFileSystem();
            var baseDir = fs.AddDirectory("basedir");
            baseDir.AddDirectory("dir1");
            baseDir.AddDirectory("dir2");
            baseDir.AddDirectory("dir3");
            baseDir.AddFiles("file1", "file2", "file3");

            var result = fs.GetFileSystemEntries("basedir");

            Assert.That(result, Has.Length.EqualTo(6));
            Assert.That(result[0], Is.EqualTo("basedir/" + "dir1"));
            Assert.That(result[1], Is.EqualTo("basedir/" + "dir2"));
            Assert.That(result[2], Is.EqualTo("basedir/" + "dir3"));
            Assert.That(result[3], Is.EqualTo("basedir/" + "file1"));
            Assert.That(result[4], Is.EqualTo("basedir/" + "file2"));
            Assert.That(result[5], Is.EqualTo("basedir/" + "file3"));
        }
    }
}
