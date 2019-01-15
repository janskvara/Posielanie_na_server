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

                DirectoryInfo di = new DirectoryInfo(@"C:\Users\janik\OneDrive\PC\prepis\");
                try
                {
                    // Determine whether the directory exists.
                    if (di.Exists)
                    {
                        // Indicate that the directory already exists.
                        Console.WriteLine("That path exists already.");
                       
                    }
                    else { 
                    di.Create();
                    Console.WriteLine("The directory was created successfully.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }

                


                foreach (FileInfo fi in di.GetFiles())
                    {
                        Console.WriteLine(@"Copying \{0}", fi.Directory + "\\" + fi.Name);
                   
                        client.Credentials = CredentialCache.DefaultCredentials;
                        //client.UploadFileCompleted += WebClientCompleted;
                       // client.UploadFile(new Uri("http://localhost/uploads/upload.php"), "POST", fi.Directory + "\\" + fi.Name);
                        client.Dispose();
                        NameValueCollection parameters = new NameValueCollection();
                        parameters.Add("name", fi.Name);
                        //parameters.Add("value2", "xyz");
                        client.QueryString = parameters;
                        client.UploadFile(new Uri("http://localhost/uploads/upload.php"), "POST", fi.Directory + "\\" + fi.Name);
                }
               
                 
            }
            catch (Exception err)
            {
                Console.WriteLine("Nepreslo " + err);
            }
            Console.ReadKey();
        }
      
    }
}
