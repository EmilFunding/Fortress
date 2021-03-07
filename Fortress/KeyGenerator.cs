﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Fortress
{
    class KeyGenerator
    {
        RNGCryptoServiceProvider rngCsp;

        public KeyGenerator()
        {
            rngCsp = new RNGCryptoServiceProvider();
        }

        public Key CreateAESKey()
        {
            var aes = new AesCryptoServiceProvider();
            aes.KeySize = 128;
            aes.GenerateKey();
            aes.GenerateIV();

            string keyString = $"{Convert.ToBase64String(aes.Key)},{Convert.ToBase64String(aes.IV)}";

            return new Key(KeyType.Aes, Encoding.UTF8.GetBytes(keyString));
        }

        public Tuple<Key, Key> CreateRSAKey()
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.KeySize = 4096;
            return Tuple.Create(new Key(KeyType.RsaPublic, rsa.ExportCspBlob(false)), new Key(KeyType.RsaPrivate, rsa.ExportCspBlob(true)));
        }

        public Key CreateOTPKey(long size)
        {
            var keyBytes = new byte[size];
            rngCsp.GetBytes(keyBytes);
            return new Key(KeyType.Otp, keyBytes);
        }

    }
}
