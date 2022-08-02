using System.Threading.Tasks;
using Spider.Local;

namespace Spider.Spider
{
    public interface ISpider
    {
        ILogger Logger { get; set; }
        string Url { get; set; }
        void Init(string url, params string[] args);
        Task Work();
        void Dispose();
    }
}