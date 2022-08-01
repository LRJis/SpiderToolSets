using System;
using System.IO;

namespace Local
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

        public static string GetSolutionDir()
        {
            return Directory.GetParent(GetProjectDir()).FullName;
        }

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

        public static void WriteIn(string path, Stream stream)
        {
            using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
            {
                stream.CopyTo(fileStream);
            }
        }

        public static void AppendIn(string path, string content)
        {
            if (!File.Exists(path)) File.Create(path);
            using (FileStream f = File.Open(path, FileMode.Append))
            {
                using (StreamWriter w = new StreamWriter(f))
                {
                    w.WriteLine(content);
                }
            }
        }
        
        public static void CreateDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
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
            LocalFile.AppendIn(Name, head);
            LocalFile.AppendIn(Name, msg);
        }
    }
    
    public class ConsoleLogger: ILogger
    {
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
            
            Console.WriteLine(msg);
            Console.ForegroundColor = color;
        }
    }
}