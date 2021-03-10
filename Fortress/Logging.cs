using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Fortress
{
    public enum LogType
    {
        CreateKey,
        Pack,
        Unpack,

    }

    public class Logging
    {
        FileStream stream;

        public Logging()
        {
            if (!File.Exists("log.txt"))
            {
                stream = File.Create("log.txt");
            }
            else
            {
                stream = new FileStream("log.txt", FileMode.Open, FileAccess.Write);
            }
        }

        private void Log(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text + '\n');
            stream.Write(bytes, 0, bytes.Length);
        }

        public void CloseStream()
        {
            stream.Close();
        }

        public void PackFast(string path, string password)
        {
            var sha = SHA256.Create();
            Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
            Log($"PackFast \"{path}\" \"{Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)))}\"");
        }

        public void Pack(string path, string output, string key_)
        {
            Log($"Pack \"{path}\" \"{output}\" \"{key_}\"");
        }

        public void Unpack(string path, string output, string key_)
        {
            Log($"Unpack \"{path}\" \"{output}\" \"{key_}\"");
        }

        public void CreateAESKey(string path)
        {
            Log($"CreateAESKey \"{path}\"");
        }

        public void CreateOTPKey(string path, long size)
        {
            Log($"CreateOTPKey \"{path}\" \"{size}\" ");
        }

        public void CreateRSAKey(string publicPath, string privatePath)
        {
            Log($"CreateRSAKey \"{publicPath}\" \"{privatePath}\"");
        }

    }
}
