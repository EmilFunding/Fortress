using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fortress
{
    class RsaCryptoAlgorithm : ICryptoAlgorithm
    {
        public byte[] Encrypt(byte[] plain, Key key)
        {
            byte[] cipher;

            using (var RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportCspBlob(key.KeyBytes);
                cipher = RSA.Encrypt(plain, false);
            }

            return cipher;
        }

        public byte[] Decrypt(byte[] cipher, Key key)
        {
            byte[] plain;

            using (var RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportCspBlob(key.KeyBytes);
                plain = RSA.Decrypt(cipher, false);
            }

            return plain;
        }
    }
}
