using System;
using System.IO;
using System.Net;
using ToolSets;
namespace ToolSets
{
    public class NetWorkTool
    {
        /// <summary>
        /// get the content of the URL.
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public static string GetHtml(string url)
        {
            int i = 0;
            begin:
            try
            {
                
                WebRequest webRequest = WebRequest.Create(url);
                WebResponse webResponse = webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();

                if (stream != null)
                {
                    StreamReader streamReader = new StreamReader(stream);
                    return streamReader.ReadToEnd();
                }

                return null;
            }
            catch (Exception e)
            {
                if (e is TimeoutException)
                {
                    if (i>=3){goto end;}
                    i++;
                    LocalFile.ShowVitalMessage("Try Again!", ConsoleColor.Yellow);
                    goto begin;
                    
                }
                LocalFile.ShowVitalMessage("some error occured.");
            }

            end:
            return null;
        }
    }
}