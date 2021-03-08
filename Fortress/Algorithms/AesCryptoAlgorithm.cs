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

        private Tuple<byte[], byte[]> SplitKey(Key key)
        {
            string keyString = Encoding.UTF8.GetString(key.KeyBytes);
            var split = keyString.Split(',');
            return Tuple.Create(Convert.FromBase64String(split[0]), Convert.FromBase64String(split[1]));
        }

        public byte[] Encrypt(byte[] plain, Key key)
        {
            var aes = new AesManaged();

            var splitKey = SplitKey(key);
            aes.Key = splitKey.Item1;
            aes.IV = splitKey.Item2;

            byte[] cipher = null;
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plain, 0, plain.Length);
                }

                cipher = msEncrypt.ToArray();
            }

            return cipher;
        }

        public byte[] Decrypt(byte[] cipher, Key key)
        {
            var aes = new AesManaged();

            var splitKey = SplitKey(key);
            aes.Key = splitKey.Item1;
            aes.IV = splitKey.Item2;

            byte[] plain = null;
            using (var msDecrypt = new MemoryStream())
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    csDecrypt.Write(cipher, 0, cipher.Length);
                }

                plain = msDecrypt.ToArray();
            }

            return plain;
        }
    }
}
