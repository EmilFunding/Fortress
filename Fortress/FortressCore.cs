using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fortress
{
    public class FortressCore
    {
        KeyGenerator keyGenerator;
        KeyManager keyManager;
        Logging logging;
        Dictionary<KeyType, ICryptoAlgorithm> cryptoAlgorithm;

        public FortressCore()
        {
            keyGenerator = new KeyGenerator();
            keyManager = new KeyManager();
            logging = new Logging();

            cryptoAlgorithm = new Dictionary<KeyType, ICryptoAlgorithm>();
            cryptoAlgorithm.Add(KeyType.Aes, new AesCryptoAlgorithm());
            cryptoAlgorithm.Add(KeyType.Otp, new OtpCryptoAlgorithm());

            var rsa = new RsaCryptoAlgorithm();
            cryptoAlgorithm.Add(KeyType.RsaPublic, rsa);
            cryptoAlgorithm.Add(KeyType.RsaPrivate, rsa);
        }

        public void PackFast(string path, string password)
        {
            var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                stream.Position = 0;

                var filebytes = new byte[32];
                int count = stream.Read(filebytes, 0, filebytes.Length);
                for (int i = 0; i < count; i++)
                    filebytes[i] ^= hash[i];

                stream.Position = 0;
                stream.Write(filebytes, 0, count);
            }

            logging.Log("PackFast", path, password);
        }

        public void PackAES(string path, string output, string password)
        {
            var plain = File.ReadAllBytes(path);
            var key = keyGenerator.CreateAESKey(password);
            File.WriteAllBytes(output, cryptoAlgorithm[key.Type].Encrypt(plain, key));
            logging.Log("PackAES", path, output, password);
        }

        public void UnpackAES(string path, string output, string password)
        {
            var plain = File.ReadAllBytes(path);
            var key = keyGenerator.CreateAESKey(password);
            File.WriteAllBytes(output, cryptoAlgorithm[key.Type].Decrypt(plain, key));
            logging.Log("UnpackAES", path, output, password);
        }

        public void Pack(string path, string output, string key_)
        {
            var plain = File.ReadAllBytes(path);
            var key = keyManager.LoadKey(key_);
            File.WriteAllBytes(output, cryptoAlgorithm[key.Type].Encrypt(plain, key));
            logging.Log("Pack", path, output, key_);
        }

        public void Unpack(string path, string output, string key_)
        {
            var cipher = File.ReadAllBytes(path);
            var key = keyManager.LoadKey(key_);
            File.WriteAllBytes(output, cryptoAlgorithm[key.Type].Decrypt(cipher, key));
            logging.Log("Unpack", path, output, key_);
        }

        public void CreateAESKey(string path)
        {
            keyManager.SaveKey(path, keyGenerator.CreateAESKey());
            logging.Log("CreateAESKey" ,path);
        }

        public void CreateOTPKey(string path, long size)
        {
            keyManager.SaveKey(path, keyGenerator.CreateOTPKey(size));
            logging.Log("CreateOTPKey", path, ""+size);
        }

        public void CreateRSAKey(string publicPath, string privatePath)
        {
            var rsaKey = keyGenerator.CreateRSAKey();
            keyManager.SaveKey(publicPath, rsaKey.Item1);
            keyManager.SaveKey(privatePath, rsaKey.Item2);
            logging.Log("CreateRSAKey", publicPath, privatePath);
        }
    }
}
