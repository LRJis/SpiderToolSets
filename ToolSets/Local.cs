using System;
using System.IO;
using System.Linq;
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
            try
            {
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not found the file!");
                Console.ForegroundColor = color;
            }
            catch (Exception)
            {
                Console.WriteLine("An error unknown occured!");
            }
            return null;
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
        ///  add some content in a file.
        /// </summary>
        /// <param name="path">the path of the file.</param>
        /// <param name="content">the content you want to add in.</param>
        public static void AddInFile(string path, string content)
        {
            try
            {
                Stream stream = File.Open(path, FileMode.Append);
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.Write(content);
            }
            catch (Exception e)
            {
                ShowVitalMessage(e is FileNotFoundException ? "File doesn't exist!" : "Some error unknown occured.");
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

        /// <summary>
        ///  get the file's name through URL.
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>the file name</returns>
        public static string GetNameByUrl(string url)
        {
            char[] sep = "/".ToCharArray();
            return url.Split(sep).Last();
        }

        /// <summary>
        ///  create a new directory.
        /// </summary>
        /// <param name="name">the directory name.</param>
        public static void CreateDir(string name)
        {
            if (!Directory.Exists(name))
            {
                Directory.CreateDirectory(name);
            }
        }

        /// <summary>
        ///  NOTE! this function can only be used when the dll file is in the bin\[Debug|Release]\
        /// </summary>
        /// <returns>the solution dir.</returns>
        public static string GetSolutionDir()
        {
            DirectoryInfo directoryInfo = Directory.GetParent(Environment.CurrentDirectory);
            string root = directoryInfo.Parent.Parent.FullName;
            return root;
        }

        /// <summary>
        /// Get the project path
        /// </summary>
        /// <returns>the path of the project.</returns>
        public static string GetProjectDir()
        {
            DirectoryInfo directoryInfo = Directory.GetParent(Environment.CurrentDirectory);
            string root = directoryInfo.Parent.FullName;
            return root;
        }

        /// <summary>
        ///  Print a vital message use special color.
        /// </summary>
        /// <param name="msg">the vital message you want to print.</param>
        /// <param name="color">the font color you want to use.</param>
        public static void ShowVitalMessage(string msg, ConsoleColor color = ConsoleColor.Red)
        {
            ConsoleColor color1 = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = color1;
        }

        /// <summary>
        /// format the file name(replace the invalid char with empty string.)
        /// </summary>
        /// <param name="file">the file name you want to format.</param>
        /// <returns>the format result.</returns>
        public static string FormatFileName(string file)
        {
            string result=file;
            foreach (char c in Path.GetInvalidPathChars())
            {
                result = file.Replace(c, Char.Parse(""));
            }
            return result;
        }
    }
}