using System.Net;
using ToolSets;

namespace Spider
{
    /// <summary>
    ///  Its main task is to download file from Internet.
    /// </summary>
    public class DownloaderSpider:ISpider
    {
        public event SpiderHandler SpiderEvent;
        public string Url { get; set; }
        
        
        /// <summary>
        /// the file you want to download in. You can modify it yourself before you start working.
        /// </summary>
        public string Home { get; set; } = "file";
        public void Init(string url, params string[] options)
        {
            Url = url;
        }

        public async void Work()
        {
            LocalFile.CreateDir(Home);
            string path = $@"{Home}\{LocalFile.GetNameByUrl(Url)}";
            WebRequest webRequest = WebRequest.Create(Url);
            WebResponse webResponse = await webRequest.GetResponseAsync();
            LocalFile.WriteIn(path, webResponse.GetResponseStream());
        }

        public void Finish()
        {
            
        }
    }
}