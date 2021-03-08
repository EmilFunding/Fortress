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
            cryptoAlgorithm.Add(KeyType.Otp, new OtpCryptoAlgorithm());

            var rsa = new RsaCryptoAlgorithm();
            cryptoAlgorithm.Add(KeyType.RsaPublic, rsa);
            cryptoAlgorithm.Add(KeyType.RsaPrivate, rsa);
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

        public void CreateAESKey(string path)
        {
            keyManager.SaveKey(path, keyGenerator.CreateAESKey());
        }

        public void CreateOTPKey(string path, long size)
        {
            keyManager.SaveKey(path, keyGenerator.CreateOTPKey(size));
        }

        public void CreateRSAKey(string publicPath, string privatePath)
        {
            var rsaKey = keyGenerator.CreateRSAKey();
            keyManager.SaveKey(publicPath, rsaKey.Item1);
            keyManager.SaveKey(privatePath, rsaKey.Item2);
        }
    }
}
