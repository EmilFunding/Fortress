using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Fortress
{
    class KeyManager
    {
        public KeyManager()
        {

        }

        private void LoadAllKeys(string folder)
        {
            var files = Directory.GetFiles(folder);
            foreach (var file in files)
            {
                LoadKey(file);
            }
        }

        public Key LoadKey(string path)
        {
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var formatter = new BinaryFormatter();
            var key = (Key)formatter.Deserialize(stream);
            stream.Close();
            return key;
        }

        public void SaveKey(string path, Key key)
        {
            var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, key);
            stream.Close();
        }
    }
}
