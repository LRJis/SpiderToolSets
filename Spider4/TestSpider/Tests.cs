using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Spider.Local;
using Spider.NetClient;

namespace TestSpider
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
        public async Task Test2()
        {
            NetClient.Logger = new ConsoleLogger();
            await NetClient.DownloadFromUrlAsync("", "https://img-home.csdnimg.cn/images/20201124032511.png");
            Console.WriteLine("end.");
            Assert.Pass();
        }

        [Test]
        public async Task Test3()
        {
            NetClient.Logger = new FileLogger();
            await NetClient.DownloadFromUrlAsync("", "https://www.baidu.com/img/flexible/logo/pc/result.png");
            Console.WriteLine("end.");
            Assert.Pass();
        }
    }
}