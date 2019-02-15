using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SunGoggles
{
    class FileSerializer
    {
        private BinaryFormatter formatter = new BinaryFormatter();

        public object Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            using (Stream stream = new FileStream(fileName, FileMode.Open))
            {
                return formatter.Deserialize(stream);
            }
        }

        public void Save(string fileName, object obj)
        {
            using (Stream stream = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(stream, obj);
            }
        }
    }
}
