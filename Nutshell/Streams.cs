using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Nutshell.Streams
{
    class StandardStream
    {
        public static void Test()
        {
            FileStream();
            AsyncFileStream();
            ReadStream();
        }

        private static void ReadStream()
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = Path.Combine(FilePath, "Standard.txt");

            // StreamReader -> TextReader
            using (TextReader Reader = new StreamReader(FileName))
            {
                Console.WriteLine("Output the file {0}", FileName);

                string Content;
                while ((Content = Reader.ReadLine()) != null)
                {
                    Console.WriteLine(Content);
                }
            }
        }

        public static void FileStream()
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = Path.Combine(FilePath, "Standard.txt");

            using (Stream FileHandler = new FileStream(FileName, FileMode.Create))
            {
                Console.WriteLine("The file can read {0}", FileHandler.CanRead);
                Console.WriteLine("The file can write {0}", FileHandler.CanWrite);
                Console.WriteLine("The file can seek {0}", FileHandler.CanSeek);

                FileHandler.Write(Encoding.ASCII.GetBytes(FileName.ToCharArray(), 0, FileName.Length), 
                    0, FileName.Length);

                Console.WriteLine("The file length is {0}\n", FileHandler.Length);
            }

            Console.WriteLine("The content is {0}", File.ReadAllText(FileName));
        }

        public static async void AsyncFileStream()
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = Path.Combine(FilePath, "Async.txt");

            if (File.Exists(FileName))
                File.Delete(FileName);
            using (FileStream Handler = File.Open(FileName, FileMode.CreateNew))
            {
                await Handler.WriteAsync(Encoding.ASCII.GetBytes(FileName.ToCharArray(), 0, FileName.Length),
                    0, FileName.Length);

                Console.WriteLine("{0} is ready, the size is {1}, the content is {2}",
                    FileName, Handler.Length, FileName.Length);
            }

            Console.WriteLine("**** The content is {0}", File.ReadAllText(FileName));
        }
    }
}
