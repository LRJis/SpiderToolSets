using System;
using System.Net;
using AngleSharp.Dom;
using Spider;
using ToolSets;

namespace UseTools
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LocalFile.CreateDir("jquery");
            MainSpider mainSpider = new MainSpider();
            mainSpider.Init("https://www.runoob.com/jquery/jquery-tutorial.html/");
            mainSpider.Work();
            Console.ReadKey();
        }
    }

    public class MainSpider : PlainSpider
    {
        public string Home = "https://www.runoob.com/";
        public override void Work()
        {
            foreach (IElement element in CssPickElements("a[target=_top]"))
            {
                string href = Home + element.GetAttribute("href");
                new Downloader(href).Work();
            }
        }
    }

    public class Downloader
    {
        public string Url;

        public Downloader(string url)
        {
            LocalFile.ShowVitalMessage($"Start at {url} !", ConsoleColor.Magenta);
            Url = url;
        }

        public async void Work()
        {
            WebRequest webRequest = WebRequest.Create(Url);
            WebResponse webResponse = await webRequest.GetResponseAsync();
            string path = $@"jquery\{LocalFile.GetNameByUrl(Url)}";
            LocalFile.WriteIn(path, webResponse.GetResponseStream());
            LocalFile.ShowVitalMessage($"End at {Url}", ConsoleColor.Cyan);
        }
    }
}