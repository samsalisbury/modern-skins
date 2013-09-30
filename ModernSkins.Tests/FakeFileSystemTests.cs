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
            //scriptsDir.AddFiles("myscript1.js", "otherscripts2.js");

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
    }
}
