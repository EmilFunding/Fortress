using System;
using System.Collections.Generic;
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
            fortress.CreateKey(currentDir + @"\key.bin", KeyType.Aes);

            fortress.Pack(currentDir + @"\test.txt", currentDir + @"\test_encrypted.bin", currentDir + @"\key.bin");

            fortress.Unpack(currentDir + @"\test_encrypted.bin", currentDir + @"\test_original.txt", currentDir + @"\key.bin");

            Console.WriteLine("Done");
        }
    }
}
