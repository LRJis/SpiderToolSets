using System;
using System.IO;
using System.Linq;

namespace Spider.Local
{
    public class LocalFile
    {
        public static ILogger Logger;
        /// <summary>
        /// the function can be only used when the file is in {Solution}\{Project}\bin\{}\
        /// </summary>
        /// <returns></returns>
        public static string GetProjectDir()
        {
            DirectoryInfo path = Directory.GetParent(Environment.CurrentDirectory);
            if (path == null) return null;
            return path.Parent.Parent.FullName;
        }

        /// <summary>
        /// get the solution dir.
        /// </summary>
        /// <returns>the dir of the solution.</returns>
        public static string GetSolutionDir()
        {
            return Directory.GetParent(GetProjectDir()).FullName;
        }

        /// <summary>
        /// get the content of the file.
        /// </summary>
        /// <param name="path">the file path.</param>
        /// <returns>the file content.</returns>
        public static string LoadFile(string path)
        {
            try
            {
                using (FileStream fileStream = File.Open(path, FileMode.Open))
                {
                    return new StreamReader(fileStream).ReadToEnd();
                }
            }
            catch(FileNotFoundException)
            {
                Logger.Log("File doesn't exist.", 2);
                return null;
            }
        }

        /// <summary>
        /// write in file by plain text.
        /// </summary>
        /// <param name="path">the file path.</param>
        /// <param name="content">the content you want to write in.</param>
        public static void WriteIn(string path, string content)
        {
            using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter w = new StreamWriter(fileStream))
                {
                    w.WriteLine(content);
                }
            }
        }

        /// <summary>
        /// write in file by stream.
        /// </summary>
        /// <param name="path">the file path.</param>
        /// <param name="stream">the stream which will be used to write the file.</param>
        public static void WriteIn(string path, Stream stream)
        {
            using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
            {
                stream.CopyTo(fileStream);
            }
        }

        /// <summary>
        /// append some content in a file.
        /// </summary>
        /// <param name="path">the file path.</param>
        /// <param name="content">the content you want to append.</param>
        public static void AppendIn(string path, string content)
        {
            if (!File.Exists(path))
                using (FileStream fileStream = File.Create(path))
                {
                    fileStream.Close();
                }
            
            using (FileStream f = File.Open(path, FileMode.Append))
            {
                using (StreamWriter w = new StreamWriter(f))
                {
                    w.WriteLine(content);
                }
            }
        }
        
        /// <summary>
        /// create a directory(if it already exists, do nothing.)
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// get the filename from url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>the filename </returns>
        public static string GetFileNameByUrl(string url)
        {
            char[] sep = "/".ToCharArray();
            return url.Split(sep).Last();
        }
    }
    public interface ILogger
    {
        void Log(string msg, int rank);
    }

    public class FileLogger : ILogger
    {
        public string Name = "app.log";
        public void Log(string msg, int rank = 1)
        {
            string head = "";
            switch (rank)
            {
                case 0:
                {
                    head = "[info]";
                    break;
                }
                case 1:
                {
                    head = "[Warning]";
                    break;
                }
                case 2:
                {
                    head = "[Error]";
                    break;
                }
            }
            
            LocalFile.AppendIn(Name, $"Time:{DateTime.Now}");
            LocalFile.AppendIn(Name, head);
            LocalFile.AppendIn(Name, msg);
            LocalFile.AppendIn(Name, "");
        }
    }
    
    public class ConsoleLogger: ILogger
    {
        /// <summary>
        /// Log the message.
        /// </summary>
        /// <param name="msg">the message you want to print.</param>
        /// <param name="rank">the rank of the message(only 0:info, 1:warning, 2:error are supported.)</param>
        public void Log(string msg, int rank=1)
        {
            ConsoleColor color = Console.ForegroundColor;
            switch (rank)
            {
                case 0:
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[info]:");
                    break;
                }
                case 1:
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("[Warning]:");
                    break;
                }
                case 2:
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[Error]:");
                    break;
                }
            }
            Console.WriteLine($"Time: {DateTime.Now}");
            Console.WriteLine(msg);
            Console.ForegroundColor = color;
        }
    }
}