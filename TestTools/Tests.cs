using System;
using NUnit.Framework;
using ToolSets;
namespace TestTools
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.True(true);
        }

        [Test]
        public void Test2()
        {
            // this test is useless.
            // Assert.True(LocalFile.GetSolutionDir() == @"E:\project\Solution1");
            // Assert.True(LocalFile.GetProjectDir() == @"E:\project\Solution1\TestTools");
        }

        [Test]
        public void Test3()
        {
            // useless test.
            // Assert.True(LocalFile.LoadFile(LocalFile.GetProjectDir()+@"\ files\test.txt")=="Hello,World!");
        }

        [Test]
        public void Test4()
        {
            string path = LocalFile.GetProjectDir() + @"\files\test.txt";
            LocalFile.AddInFile(path, "\n add in some content..");
            Assert.Pass();
        }
    }
}