using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Fortress
{
    class AesCryptoAlgorithm : ICryptoAlgorithm
    {
        public byte[] Encrypt(byte[] plain, Key key)
        {
            var aes = new AesManaged();
            aes.Key = key.KeyBytes;
            aes.GenerateIV();

            var cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV);
            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, cryptoTransform, CryptoStreamMode.Write);
            csEncrypt.Write(plain, 0, plain.Length);

            return msEncrypt.ToArray();
        }

        public byte[] Decrypt(byte[] cipher, Key key)
        {
            var aes = new AesManaged();
            aes.Key = key.KeyBytes;
            aes.GenerateIV();

            var cryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV);
            var msDecrypt = new MemoryStream();
            var csDecrypt = new CryptoStream(msDecrypt, cryptoTransform, CryptoStreamMode.Read);
            csDecrypt.Write(cipher, 0, cipher.Length);

            return msDecrypt.ToArray();
        }
    }
}
