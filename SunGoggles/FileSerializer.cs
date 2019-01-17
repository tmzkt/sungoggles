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
            Stream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Open);
                return new BinaryFormatter().Deserialize(stream);
            }
            finally
            {
                stream?.Close();
            }
        }

        public void Save(string fileName, object obj)
        {
            Stream stream = null;
            try
            {
                stream = new FileStream(fileName, FileMode.Create);
                new BinaryFormatter().Serialize(stream, obj);
            }
            finally
            {
                stream?.Close();
            }
        }
    }
}
