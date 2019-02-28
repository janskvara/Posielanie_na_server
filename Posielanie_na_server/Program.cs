using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Posielanie_na_server
{
    class Program
    {
        public static object MessageBox { get; private set; }

        static void Main(string[] args)
        {
            try
            {
                WebClient client = new WebClient();

                DirectoryInfo di = new DirectoryInfo(@"C:\Users\SKVARA\Desktop\obrazky");
                try
                {
                    if (di.Exists)
                    {
                       
                       
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                    Console.ReadKey();
                }

                


                foreach (FileInfo fi in di.GetFiles())
                {
                   
                   
                    WebRequest request = WebRequest.Create("ftp://localhost" + @"/" + fi.Name);
                    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                    request.Credentials = new NetworkCredential("jano", "jano");
                        
                    request.Method = WebRequestMethods.Ftp.UploadFile;

                    Console.WriteLine(@"Copying \{0}", fi.Directory + "\\" + fi.Name);
                    byte[] fileContents;
                    fileContents = File.ReadAllBytes(fi.Directory + "\\" + fi.Name);

                    request.ContentLength = fileContents.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(fileContents, 0, fileContents.Length);
                    }

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                        if (response.StatusCode == FtpStatusCode.ClosingData)
                        {
                            Console.WriteLine("Deleting {0} \n", fi.Directory + "\\" + fi.Name);
                            File.Delete(fi.Directory + "\\" + fi.Name);
                        }
                    }
                }
               
                 
            }
            catch (Exception err)
            {
                Console.WriteLine("Nepreslo " + err);
                Console.ReadKey();
            }
            
        }
      
    }
}
