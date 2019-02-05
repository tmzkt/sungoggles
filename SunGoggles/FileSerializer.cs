using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SunGoggles
{
    class FileSerializer
    {
        public object Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }

        public void Save(string fileName, object obj)
        {
            using (Stream stream = new FileStream(fileName, FileMode.Create))
            {
                new BinaryFormatter().Serialize(stream, obj);
            }
        }
    }
}
