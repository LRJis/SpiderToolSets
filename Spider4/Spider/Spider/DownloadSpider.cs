using System.IO;
using System.Threading.Tasks;
using Spider.Local;

namespace Spider.Spider
{
    public class DownloadSpider:ISpider
    {
        public ILogger Logger { get; set; }
        public string Url { get; set; }
        public string Home { get; set; } = "";

        public void Init(string url, params string[] args)
        {
            Url = url;
        }

        public async Task Work()
        {
            if (Home != "")
            {
                LocalFile.CreateDir(Home);
            }

            Logger?.Log($"Start At {Url}", 0);
            string file = LocalFile.GetFileNameByUrl(Url);
            string filePath = Path.Combine(Home, file);
            Stream stream = await NetClient.NetClient.GetStreamAsync(Url);
            if (stream == null)
            {
                Logger?.Log("Download failed..", 2);
            }
            LocalFile.WriteIn(filePath, stream);
            Logger?.Log($"End writing {filePath}", 0);
        }

        public void Dispose()
        {
            
        }
    }
}