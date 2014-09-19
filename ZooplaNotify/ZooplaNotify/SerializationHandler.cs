using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ZooplaNotify
{
    class SerializationHandler
    {
        string FileName="D://SerializeApplicationData.bin";
        public void SerializeObject(ApplicationData objectToSerialize)
        {
            Stream stream = File.Open(FileName, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public ApplicationData DeSerializeObject()
        {
            ApplicationData objectToSerialize;
            Stream stream = File.Open(FileName, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (ApplicationData)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
