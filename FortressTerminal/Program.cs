using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fortress;

namespace FortressTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            var fortress = new FortressCore();

            var currentDir = Environment.CurrentDirectory;
            //fortress.CreateAESKey(currentDir + @"\aes_key.bin");
            //fortress.Pack(currentDir + @"\test.txt", currentDir + @"\aes_test_encrypted.bin", currentDir + @"\aes_key.bin");
            //fortress.Unpack(currentDir + @"\aes_test_encrypted.bin", currentDir + @"\aes_test_original.txt", currentDir + @"\aes_key.bin");

            //var fileInfo = new FileInfo(currentDir + @"\test.txt");
            //fortress.CreateOTPKey(currentDir + @"\otp_key.bin", fileInfo.Length);
            //fortress.Pack(currentDir + @"\test.txt", currentDir + @"\otp_test_encrypted.bin", currentDir + @"\otp_key.bin");
            //fortress.Unpack(currentDir + @"\otp_test_encrypted.bin", currentDir + @"\otp_test_original.txt", currentDir + @"\otp_key.bin");

            fortress.CreateRSAKey(currentDir + @"\rsa_pub_key.bin", currentDir + @"\rsa_pri_key.bin");
            fortress.Pack(currentDir + @"\test.txt", currentDir + @"\rsa_test_encrypted.bin", currentDir + @"\rsa_pub_key.bin");
            fortress.Unpack(currentDir + @"\rsa_test_encrypted.bin", currentDir + @"\rsa_test_original.txt", currentDir + @"\rsa_pri_key.bin");

            Console.WriteLine("Done");
        }
    }
}
