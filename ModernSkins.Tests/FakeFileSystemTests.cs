using System.Linq;
using NUnit.Framework;

namespace ModernSkins.Tests
{
    [TestFixture]
    public class FakeFileSystemTests
    {
        FakeFileSystem _fs;

        [SetUp]
        public void SetUp()
        {
            _fs = new FakeUnixFileSystem();
        }

        [Test]
        public void AddDirectory_DirExists()
        {
            const string dirName = "MyApp/Skins/skin1";

            _fs.AddDirectory(dirName);

            Assert.True(_fs.DirExists(dirName));
        }

        [Test]
        public void AddDirectory_AddDirectory_CreatesSubDirectory_NotFiles()
        {
            const string dirName = "MyApp/Skins/skin1";
            const string subDirName = "scripts";
            const string subDirPath = dirName + "/" + subDirName;
            
            _fs.AddDirectory(dirName).AddDirectory(subDirName);

            Assert.True(_fs.DirExists(subDirPath));
            Assert.False(_fs.FileExists(subDirPath));
        }

        [Test]
        public void AddDirectory_MergesDirectories()
        {
            _fs.AddDirectory("path/that/is/duplicated");
            _fs.AddDirectory("path/that/is/duplicated/thing-inside");
            _fs.AddDirectory("path/that/is/cool");
            _fs.AddDirectory("path/that/is/the/other/one");

            Assert.That(_fs.GetDirectories("path/that/is"), Has.Length.EqualTo(3));
            Assert.That(_fs.GetDirectories("path/that/is")[0], Is.EqualTo("path/that/is/duplicated"));
            Assert.That(_fs.GetDirectories("path/that/is")[1], Is.EqualTo("path/that/is/cool"));
            Assert.That(_fs.GetDirectories("path/that/is")[2], Is.EqualTo("path/that/is/the"));

            Assert.That(_fs.GetDirectories("path/that"), Has.Length.EqualTo(1));
            Assert.That(_fs.GetDirectories("path/that")[0], Is.EqualTo("path/that/is"));
        }

        [Test]
        public void AddDirectory_AddFiles_CreatesExpectedFiles_NotDirectories()
        {
            const string dirName = "SomeDirectory/Hello";
            var files = new[] {"file1.jpg", "file2", "file_three.txt"};
            var filePaths = files.Select(name => dirName + "/" + name).ToArray();

            _fs.AddDirectory(dirName).AddFiles(files);

            Assert.True(_fs.FileExists(filePaths[0]));
            Assert.True(_fs.FileExists(filePaths[1]));
            Assert.True(_fs.FileExists(filePaths[2]));

            Assert.False(_fs.DirExists(filePaths[0]));
            Assert.False(_fs.DirExists(filePaths[1]));
            Assert.False(_fs.DirExists(filePaths[2]));
        }

        [Test]
        public void GetDirectories_ReturnsExpectedDirectories()
        {
            var baseDir = _fs.AddDirectory("basedir");
            baseDir.AddDirectory("dir1");
            baseDir.AddDirectory("dir2");
            baseDir.AddDirectory("dir3");
            baseDir.AddFiles("file1", "file2");

            var result = _fs.GetDirectories("basedir");

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.That(result[0], Is.EqualTo("basedir/" + "dir1"));
            Assert.That(result[1], Is.EqualTo("basedir/" + "dir2"));
            Assert.That(result[2], Is.EqualTo("basedir/" + "dir3"));
        }

        [Test]
        public void GetFiles_ReturnsExpectedFiles()
        {
            var baseDir = _fs.AddDirectory("basedir");
            baseDir.AddDirectory("dir1");
            baseDir.AddDirectory("dir2");
            baseDir.AddFiles("file1", "file2", "file3");

            var result = _fs.GetFiles("basedir");

            Assert.That(result, Has.Length.EqualTo(3));
            Assert.That(result[0], Is.EqualTo("basedir/" + "file1"));
            Assert.That(result[1], Is.EqualTo("basedir/" + "file2"));
            Assert.That(result[2], Is.EqualTo("basedir/" + "file3"));
        }

        [Test]
        public void GetFileSystemEntries_ReturnsAllEntries()
        {
            var baseDir = _fs.AddDirectory("basedir");
            baseDir.AddDirectory("dir1");
            baseDir.AddDirectory("dir2");
            baseDir.AddDirectory("dir3");
            baseDir.AddFiles("file1", "file2", "file3");

            var result = _fs.GetFileSystemEntries("basedir");

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
            _fs.AddFiles("/base/dir/other/[file1.js,file2.coffee,file3.css,file4.jpg]");

            var createdFiles = _fs.GetFiles("/base/dir/other");

            Assert.That(createdFiles, Has.Length.EqualTo(4));
            Assert.That(createdFiles[0], Is.EqualTo("/base/dir/other/file1.js"));
            Assert.That(createdFiles[1], Is.EqualTo("/base/dir/other/file2.coffee"));
            Assert.That(createdFiles[2], Is.EqualTo("/base/dir/other/file3.css"));
            Assert.That(createdFiles[3], Is.EqualTo("/base/dir/other/file4.jpg"));
        }

        [Test]
        public void AddFile_CreatesExpectedFile()
        {
            _fs.AddFile("/some/dir.path/and/then/a/file");

            var createdFiles = _fs.GetFiles("/some/dir.path/and/then/a");

            Assert.That(createdFiles.Length, Is.EqualTo(1));
            Assert.That(createdFiles[0], Is.EqualTo("/some/dir.path/and/then/a/file"));
        }

        [TestCase("/part1", "part2")]
        [TestCase("/part1/", "part2")]
        [TestCase("/part1", "/part2")]
        [TestCase("/part1/", "/part2")]
        public void CombinePaths_CombinesPathsAsExpected(string p1, string p2)
        {
            var result = _fs.CombinePaths(p1, p2);

            Assert.That(result, Is.EqualTo("/part1/part2"));
        }

        [Test]
        public void AddFile_WithContentString_CreatesFileWithContent()
        {
            const string someContent = "Some content. Hello.";
            
            var file = _fs.AddFile("/path/my-file.txt", someContent);

            Assert.That(file.Content, Is.EqualTo(someContent));
        }

        [Test]
        public void ReadFile_ReturnsFileContent()
        {
            const string filePath = "/path/my-file.txt";
            const string writtenContent = "Some content. Hello.";
            _fs.AddFile(filePath, writtenContent);

            var readContent = _fs.ReadFile(filePath);

            Assert.That(readContent, Is.EqualTo(writtenContent));
        }
    }
}
