using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Spider.Local;
using Spider.NetClient;
using Spider.Spider;
using OpenQA.Selenium.Edge;

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

        [Test]
        public async Task Test4()
        {
            NetClient.Logger = new FileLogger(){Name = "test.log"};
            DownloadSpider spider = new DownloadSpider(){Logger = NetClient.Logger, Home = "css"};
            spider.Init("https://cdn.staticfile.org/twitter-bootstrap/5.1.1/css/bootstrap.min.css");
            await spider.Work();
        }

        [Test]
        public async Task Test5()
        {
            ILogger logger = new FileLogger(){Name = "index.log"};
            MainDriverSpider spider = new MainDriverSpider() { Logger = logger };
            spider.Init("file:///E:/projects/Spider4/TestSpider/bin/Release/html/index.html","--nogpu", "--headless");
            await spider.Work();
            Console.WriteLine("end.");
        }

        [Test]
        public void Test6()
        {
            ILogger logger = new ConsoleLogger();
            DriverSpider1 spider1 = new DriverSpider1() { Logger = logger };
            spider1.Init("https://www.baidu.com", "--nogpu");
            spider1.GoToWithoutClose("https://python.org");
            spider1.GoAndClose("https://www.baidu.com");
            Thread.Sleep(1000);
            Console.WriteLine(spider1.Driver.WindowHandles.Count);
            // Console.WriteLine(spider1.GetHtml());
            
        }
    }

    public class MainDriverSpider : DriverSpider
    {
        public override async Task Work()
        {
            await Task.Run(() =>
            {
                RunJsFromFile("jquery.min.js", 2000);
            });
            RunJs("$('#first').click()", 300);
            Console.WriteLine(GetHtml());
        }
    }

    public class DriverSpider1 : DriverSpider
    {
        
    }
}