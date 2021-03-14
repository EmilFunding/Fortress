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
        private FileStream stream;
        private bool enableLogging;

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

        private void SetEnableLogging(bool enabled)
        {
            enableLogging = enabled;
        }

        private void WriteToLog(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text + '\n');
            stream.Write(bytes, 0, bytes.Length);
        }

        public void Log(string function, params string[] parameters)
        {
            string final = function;

            foreach (var param in parameters)
            {
                final += $"\"{param}\"";
            }

            WriteToLog(final);
        }

        public void CloseStream()
        {
            stream.Close();
        }
    }
}
