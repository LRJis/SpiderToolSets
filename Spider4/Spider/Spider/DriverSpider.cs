using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Spider.Local;

namespace Spider.Spider
{
    public abstract class DriverSpider:ISpider
    {
        public Dictionary<string, string> HrefsDictionary= new Dictionary<string, string>();
        public ILogger Logger { get; set; }
        public string Url { get; set; }
        public EdgeDriver Driver { get; set; }
        public virtual void Init(string url, params string[] args)
        {
            EdgeOptions options = new EdgeOptions();
            foreach (string s in args)
            {
                options.AddArgument(s);
            }

            Driver = new EdgeDriver(options);
            Url = url;

            Driver.Url = Url;
            HrefsDictionary.Add(url, Driver.CurrentWindowHandle);
        }

        public void RunJsFromFile(string name, int sleep=1000, string home = "js")
        {
            string path = Path.Combine(home, name);
            string content = LocalFile.LoadFile(path);
            Driver.ExecuteScript(content);
            Thread.Sleep(sleep);
        }

        public async Task RunJsFromUrl(string url, int sleep = 1000, string home="js")
        {
            DownloadSpider spider = new DownloadSpider() { Logger = Logger ,Home = home};
            spider.Init(url);
            await spider.Work();
            spider.Dispose();
            RunJsFromFile(Path.Combine(home, LocalFile.GetFileNameByUrl(url)));
        }

        public void GoAndClose(string url, int sleep = 500)
        {
            HrefsDictionary.Remove(Driver.Url);
            Url = url;
            if (!HrefsDictionary.ContainsKey(url))
            {
                Driver.ExecuteScript($"window.location.href = '{url}'");
                HrefsDictionary.Add(url, Driver.CurrentWindowHandle);
            }
            else
            {
                Driver.ExecuteScript("window.close()");
                
                Driver.SwitchTo().Window(HrefsDictionary[url]);
            }

            Driver.Url = url;
            Thread.Sleep(sleep);
        }
        
        public void GoToWithoutClose(string url, int sleep=500)
        {
            Url = url;
            if (HrefsDictionary.ContainsKey(url))
            {
                Driver.SwitchTo().Window(HrefsDictionary[url]);
            }
            Driver.ExecuteScript($"window.open('{url}','_blank')");
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            HrefsDictionary.Add(url, Driver.CurrentWindowHandle);
            Driver.Url = url;
            Thread.Sleep(sleep);
        }

        public void RunJs(string js, int sleep=500)
        {
            Driver.ExecuteScript(js);
            Thread.Sleep(sleep);
        }
        public virtual async Task Work()
        {
            await Task.Delay(0);
        }

        public string GetHtml()
        {
            return Driver.PageSource;
        }

        public virtual void Dispose()
        {
            Driver.Close();
            Driver.Dispose();
        }
    }
}