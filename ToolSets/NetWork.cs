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
            catch (Exception)
            {
                LocalFile.ShowVitalMessage("some error occured.");
            }

            return null;
        }
    }
}