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
            //fortress.CreateKey(currentDir + @"\aes_key.bin", KeyType.Aes);
            //fortress.Pack(currentDir + @"\test.txt", currentDir + @"\aes_test_encrypted.bin", currentDir + @"\aes_key.bin");
            //fortress.Unpack(currentDir + @"\aes_test_encrypted.bin", currentDir + @"\aes_test_original.txt", currentDir + @"\aes_key.bin");

            var fileInfo = new FileInfo(currentDir + @"\test.txt");
            fortress.CreateKey(currentDir + @"\otp_key.bin", KeyType.Otp, fileInfo.Length);
            fortress.Pack(currentDir + @"\test.txt", currentDir + @"\otp_test_encrypted.bin", currentDir + @"\otp_key.bin");
            fortress.Unpack(currentDir + @"\otp_test_encrypted.bin", currentDir + @"\otp_test_original.txt", currentDir + @"\otp_key.bin");

            Console.WriteLine("Done");
        }
    }
}
