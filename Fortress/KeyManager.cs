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
        private Dictionary<string ,Key> keys;

        public KeyManager()
        {
            keys = new Dictionary<string, Key>();
        }

        public void AddKey(string name, Key key)
        {
            if (!keys.ContainsKey(name))
            {
                throw new Exception($"Key ({name}) not found!");
            }

            keys.Add(name, key);
        }
        public Key GetKey(string name)
        {
            if (!keys.ContainsKey(name))
            {
                throw new Exception($"Key ({name}) not found!");
            }

            return keys[name];
        }

        private void LoadAllKeys(string folder)
        {
            var files = Directory.GetFiles(folder);
            foreach (var file in files)
            {
                LoadKey(file);
            }
        }

        private void LoadKey(string path)
        {
            var name = Path.GetFileName(path);
            if (!keys.ContainsKey(name))
            {
                throw new Exception($"A key with the name ({name}) was already loaded!");
            }

            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var formatter = new BinaryFormatter();
            var key = (Key)formatter.Deserialize(stream);
            stream.Close();

            keys.Add(name, key);
        }

        private void SaveKey(string path, string key)
        {
            if (!keys.ContainsKey(key))
            {
                throw new Exception($"No keys matched the name ({key})!");
            }

            var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, keys[key]);
            stream.Close();
        }
    }
}
