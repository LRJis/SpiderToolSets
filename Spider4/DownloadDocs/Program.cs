using System;
using System.Threading.Tasks;
using Spider.Spider;
using Spider.Local;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace DownloadDocs
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            FileLogger logger = new FileLogger() { Name = "Download.log" };
            MainSpider spider = new MainSpider() { Logger = logger };
            spider.Init("https://docs.microsoft.com/zh-cn/dotnet/fsharp/", "--headless", "--nogpu");
            await spider.Work();
            Console.ReadKey();
        }
    }

    public class MainSpider : DriverSpider
    {
        public override async Task Work()
        {
            
            await Task.Run(() =>
            {
                Driver.ExecuteScript("var e = document.getElementsByClassName(\"tree-expander\");");
                Task.Delay(300);
                Driver.ExecuteScript("for (let index = 0; index < e.length; index++){e[index].click();}");
                Task.Delay(2000);
                By by = By.CssSelector("a[data-bi-name='tree-leaf']");
                foreach (var webElement in Driver.FindElements(by))
                {
                    string href = webElement.GetAttribute("href");
                    DownloadSpider spider = new DownloadSpider() { Logger = Logger, Home = "dotF" };
                    spider.Init(href);
                    spider.Work();
                }
            });
        }
    }
}