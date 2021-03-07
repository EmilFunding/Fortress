using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortress
{
    public class FortressCore
    {
        KeyGenerator keyGenerator;
        KeyManager keyManager;
        Dictionary<KeyType, ICryptoAlgorithm> cryptoAlgorithm;

        public FortressCore()
        {
            keyGenerator = new KeyGenerator();
            keyManager = new KeyManager();

            cryptoAlgorithm = new Dictionary<KeyType, ICryptoAlgorithm>();
            cryptoAlgorithm.Add(KeyType.Aes, new AesCryptoAlgorithm());
        }

        public void Pack(string path, string output, string key_)
        {
            var plain = File.ReadAllBytes(path);
            var key = keyManager.LoadKey(key_);
            File.WriteAllBytes(output, cryptoAlgorithm[key.Type].Encrypt(plain, key));
        }

        public void Unpack(string path, string output, string key_)
        {
            var cipher = File.ReadAllBytes(path);
            var key = keyManager.LoadKey(key_);
            File.WriteAllBytes(output, cryptoAlgorithm[key.Type].Decrypt(cipher, key));
        }

        public void CreateKey(string path, KeyType keyType)
        {
            switch (keyType)
            {
                case KeyType.Aes:
                    keyManager.SaveKey(path, keyGenerator.CreateAESKey());
                    break;
                case KeyType.RsaPublic:
                case KeyType.RsaPrivate:

                    break;
                case KeyType.Otp:
                    break;
            }
        }
    }
}
