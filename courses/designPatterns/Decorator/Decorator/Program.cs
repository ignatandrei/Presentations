using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            string nameFile = "test.txt";
            if (File.Exists(nameFile))
                File.Delete(nameFile);
            byte[] data = ASCIIEncoding.ASCII.GetBytes("Hello World!");

            using (var stream = new FileStream(nameFile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                //stream.Write(data, 0, data.Length);
                //return;

                var cryptic = new DESCryptoServiceProvider();

                cryptic.Key = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");
                cryptic.IV = ASCIIEncoding.ASCII.GetBytes("ABCDEFGH");

                using (var crStream = new CryptoStream(stream, cryptic.CreateEncryptor(), CryptoStreamMode.Write))
                {


                    //crStream.Write(data, 0, data.Length);
                    //return;
                    using (var gz = new GZipStream(crStream, CompressionLevel.Optimal))
                    {
                        gz.Write(data, 0, data.Length);
                    }

                }
            }
            
        }
    }
}
