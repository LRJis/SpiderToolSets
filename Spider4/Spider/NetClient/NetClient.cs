using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Spider.Local;

namespace Spider.NetClient
{
    public class NetClient
    {
        /// <summary>
        /// the logger you want to use.(Console or File).
        /// </summary>
        public static ILogger Logger;

        /// <summary>
        /// the default headers.You can modify it yourself.
        /// </summary>
        public static string Head =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36";
        
        /// <summary>
        ///  get stream from url.Notice that if the url is invalid or other error occured,it returns null.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>The stream from the url.</returns>
        public static async Task<Stream> GetStreamAsync(string url)
        {
            int i = 0;
            begin:
            try
            {
                HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
                webRequest.UserAgent = Head;
                WebResponse webResponse = await webRequest.GetResponseAsync();
                Stream stream = webResponse.GetResponseStream();
                if (stream == null)
                {
                    Logger?.Log($"Not Response at {url}", 1);
                    return null;
                }

                Logger?.Log($"Get at {url}", 0);
                return stream;
            }
            catch (FormatException)
            {
                Logger?.Log("Url is invalid!", 2);
                return null;
            }
            catch (TimeoutException)
            {
                if (i>=2) {Logger?.Log("Try failed.. ", 2);
                    return null;
                }
                i++;
                Logger?.Log($"Try again at {url}", 1);
                goto begin;
            }
        }
        
        /// <summary>
        /// Notice that if the url is invalid or other error occured,it will return null.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <returns>the content from the url.</returns>
        public static async Task<string> GetHtmlAsync(string url)
        {
            int i = 0;
            begin:
            try
            {
                HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
                webRequest.UserAgent = Head;
                
                WebResponse webResponse = await webRequest.GetResponseAsync();
                Stream stream = webResponse.GetResponseStream();
                
                if (stream == null)
                {
                    Logger?.Log($"Not Response at {url}", 1);
                    return null;
                }
                Logger?.Log($"Get at {url}", 0);
                StreamReader streamReader = new StreamReader(stream);
                return await streamReader.ReadToEndAsync();
            }
            catch (FormatException)
            {
                Logger?.Log("Url is invalid!", 2);
                return null;
            }
            catch (TimeoutException)
            {
                if (i>=2) {Logger?.Log("Try failed.. ", 2);
                    return null;
                }
                i++;
                Logger?.Log($"Try again at {url}", 1);
                goto begin;
            }
        }

        /// <summary>
        /// Download file from website.
        /// </summary>
        /// <param name="path">the directory you want to save the file in.</param>
        /// <param name="url">the website file's url.</param>
        public static async Task DownloadFromUrlAsync(string path, string url)
        {
            string file = LocalFile.GetFileNameByUrl(url);
            string filePath = Path.Combine(path, file);
            Stream webStream = await GetStreamAsync(url);
            if (webStream == null) return;
            LocalFile.WriteIn(filePath, webStream);
        }
    }
}