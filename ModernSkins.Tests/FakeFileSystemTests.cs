using System.ComponentModel;
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

            fs.AddDirectory("path/that/is/duplicated");
            fs.AddDirectory("path/that/is/duplicated/thing-inside");
            fs.AddDirectory("path/that/is/cool");
            fs.AddDirectory("path/that/is/the/other/one");

            Assert.That(fs.GetDirectories("path/that/is"), Has.Length.EqualTo(3));
            Assert.That(fs.GetDirectories("path/that/is")[0], Is.EqualTo("path/that/is/duplicated"));
            Assert.That(fs.GetDirectories("path/that/is")[1], Is.EqualTo("path/that/is/cool"));
            Assert.That(fs.GetDirectories("path/that/is")[2], Is.EqualTo("path/that/is/the"));

            Assert.That(fs.GetDirectories("path/that"), Has.Length.EqualTo(1));
            Assert.That(fs.GetDirectories("path/that")[0], Is.EqualTo("path/that/is"));
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

        [Test]
        public void AddFiles_CreatesExpectedFiles()
        {
            var fs = new FakeFileSystem();

            fs.AddFiles("/base/dir/other/[file1.js,file2.coffee,file3.css,file4.jpg]");

            var createdFiles = fs.GetFiles("/base/dir/other");

            Assert.That(createdFiles, Has.Length.EqualTo(4));
            Assert.That(createdFiles[0], Is.EqualTo("/base/dir/other/file1.js"));
            Assert.That(createdFiles[1], Is.EqualTo("/base/dir/other/file2.coffee"));
            Assert.That(createdFiles[2], Is.EqualTo("/base/dir/other/file3.css"));
            Assert.That(createdFiles[3], Is.EqualTo("/base/dir/other/file4.jpg"));
        }

        [Test]
        public void AddFile_CreatesExpectedFile()
        {
            var fs = new FakeFileSystem();

            fs.AddFile("/some/dir.path/and/then/a/file");

            var createdFiles = fs.GetFiles("/some/dir.path/and/then/a");

            Assert.That(createdFiles.Length, Is.EqualTo(1));
            Assert.That(createdFiles[0], Is.EqualTo("/some/dir.path/and/then/a/file"));
        }

        [TestCase("/part1", "part2")]
        [TestCase("/part1/", "part2")]
        [TestCase("/part1", "/part2")]
        [TestCase("/part1/", "/part2")]
        public void CombinePaths_CombinesPathsAsExpected(string p1, string p2)
        {
            var fs = new FakeFileSystem();

            var result = fs.CombinePaths(p1, p2);

            Assert.That(result, Is.EqualTo("/part1/part2"));
        }
    }
}
