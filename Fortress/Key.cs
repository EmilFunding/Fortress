using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Fortress
{
    public enum KeyType
    {
        Aes,
        RsaPublic,
        RsaPrivate,
        Otp,
    }

    [Serializable]
    public class Key
    {
        public KeyType Type { get; set; }
        public byte[] KeyBytes { get; set; }

        public Key(KeyType type, byte[] keyBytes)
        {
            Type = type;
            KeyBytes = keyBytes;
        }
    }
}
