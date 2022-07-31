using System.IO;
using System.Text;

namespace ToolSets
{
    /// <summary>
    /// 本地文件操作类
    /// </summary>
    public class LocalFile
    {
        /// <summary>
        /// 获取文件内容 (Get the content of the file.)
        /// </summary>
        /// <param name="path"> 文件路径 (the path of the file)</param>
        /// <returns>文件内容 (content)</returns>
        public static string LoadFile(string path)
        {
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///  文本写入 Write in file using plain text.
        /// </summary>
        /// <param name="path"> 路径  the path of the file </param>
        /// <param name="content"> 文本内容 the content of the file.</param>
        public static void WriteIn(string path, string content)
        {
            using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8))
                {
                    streamWriter.Write(content);
                }
            }
        }

        /// <summary>
        /// 流写入 Write in file using stream.
        /// </summary>
        /// <param name="path"> 路径 the path of the file.</param>
        /// <param name="writeStream"> 写入流 the WriteIn stream.</param>
        public static void WriteIn(string path, Stream writeStream)
        {
            using (Stream stream = File.Open(path, FileMode.OpenOrCreate))
            {
                writeStream.CopyTo(stream);
            }
        }
    }
}