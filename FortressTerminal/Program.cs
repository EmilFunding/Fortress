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

            fortress.CreateKey(Environment.CurrentDirectory + @"key.bin", KeyType.Aes);


        }
    }
}
