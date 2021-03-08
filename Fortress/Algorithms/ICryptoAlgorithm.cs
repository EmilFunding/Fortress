using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortress
{
    interface ICryptoAlgorithm
    {
        byte[] Encrypt(byte[] plain, Key key);
        byte[] Decrypt(byte[] cipher, Key key);
    }
}
