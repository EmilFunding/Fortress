using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortress
{
    class OtpCryptoAlgorithm : ICryptoAlgorithm
    {
        public byte[] Encrypt(byte[] plain, Key key)
        {
            for (int i = 0; i < plain.Length; i++)
                plain[i] ^= key.KeyBytes[i];

            return plain;
        }

        public byte[] Decrypt(byte[] cipher, Key key)
        {
            for (int i = 0; i < cipher.Length; i++)
                cipher[i] ^= key.KeyBytes[i];

            return cipher;
        }
    }
}
