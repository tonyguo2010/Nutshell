using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Nutshell.Streams
{
    class StreamSamples
    {
        public static void Test()
        {
            FileStream();
            AsyncFileStream();
            MemoryStream();
            PipeStream();
            Infos();
        }

        public static void FileStream()
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory;
            string FileName = Path.Combine(FilePath, "Standard.txt");

            // use FileStream <- Stream to write data into file
            using (Stream StreamHandler = new FileStream(FileName, FileMode.Create))
            {
                Console.WriteLine("The file can read {0}", StreamHandler.CanRead);
                Console.WriteLine("The file can write {0}", StreamHandler.CanWrite);
                Console.WriteLine("The file can seek {0}", StreamHandler.CanSeek);

                StreamHandler.Write(Encoding.ASCII.GetBytes(FileName.ToCharArray(), 0, FileName.Length), 
                    0, FileName.Length);

                Console.WriteLine("The file length is {0}\n", StreamHandler.Length);
            }

            Console.WriteLine("============== File Ready ===============");

            // use StreamAdapter to read data from file
            using (TextReader StreamHandler = new StreamReader(FileName))
            {
                string LineContent;
                while ((LineContent = StreamHandler.ReadLine()) != null)
                {
                    Console.WriteLine(LineContent);
                }
            }

            Console.WriteLine("==============  File End  ===============\n\n");
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

            Console.WriteLine("**** The content is {0}, this line can appear different place per time.", File.ReadAllText(FileName));
        }

        public static void MemoryStream()
        {
            string DataContent = "Hello, MemoryStream world!";

            Console.WriteLine("==================== MemoryStream Test ==================");
            using (Stream CoreStream = new MemoryStream())
            {
                TextWriter Writer = new StreamWriter(CoreStream);

                Writer.WriteLine(DataContent);
                // must flush before seeking to the start!!!
                Writer.Flush();

                CoreStream.Seek(0, SeekOrigin.Begin);

                TextReader Reader = new StreamReader(CoreStream);

                Console.WriteLine(Reader.ReadLine());


                // NEVER close the attached Adapter too early, otherwise Stream object will be closed!!!
                // Reader.Close();
                Writer.Close();
            }

            Console.WriteLine("==================== MemoryStream End ==================\n\n");
        }
        public static void PipeStream()
        {
            Console.WriteLine("==================== PipeStream Test ==================");
            string DataContent = "Hello, Mario, this is Luigi!";

            Thread Client = new Thread(ClientPipeThread);
            Client.Start();
            using (NamedPipeServerStream CoreStream = new NamedPipeServerStream("PipeMario"))
            {
                CoreStream.WaitForConnection();
                CoreStream.Write(Encoding.ASCII.GetBytes(DataContent), 0, DataContent.Length);

                Client.Join();

                Console.WriteLine("==================== PipeStream End ==================");
            }
        }

        private static void ClientPipeThread()
        {
            Console.WriteLine("==================== Client Thread Started ==================");
            byte[] buffer = new byte[200];
            int Length = 0;
            using (NamedPipeClientStream CoreStream = new NamedPipeClientStream("PipeMario"))
            {
                CoreStream.Connect();
                Length = CoreStream.Read(buffer, 0, buffer.Length);

                Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, Length));
            }
            Console.WriteLine("==================== Client Thread Ended ==================");
        }
        public static void Infos()
        {
            Console.WriteLine("==================== DirectoryInfo Test ==================");
            // DirectoryInfo DInfo = new DirectoryInfo(Environment.CurrentDirectory);
            DirectoryInfo DInfo = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            Console.WriteLine("--- Searching {0} ---", Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            foreach (FileInfo FInfo in DInfo.GetFiles())
            {
                Console.WriteLine("{0} : {1}", FInfo.FullName, FInfo.Length );
            }

            foreach(DriveInfo DrInfo in DriveInfo.GetDrives())
            {
                Console.WriteLine("Path {0} ({1})\n ", 
                    DrInfo.Name, DrInfo.DriveType);
            }
            Console.WriteLine("==================== DirectoryInfo End  ==================");
        }
    }
}
