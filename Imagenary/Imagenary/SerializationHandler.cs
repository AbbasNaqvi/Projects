using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Imagenary
{
    class SerializationHandler
    {
        private string directoryadress=null;
        private string PropertyfileName=null;
        private string PostalAdressfileName = null;
        private string ApplicationDataFileName = null;

        public SerializationHandler(string directoryadress)
        {
            this.directoryadress = directoryadress;
            this.PropertyfileName = directoryadress+"//Imaginarydata.bin";
            this.PostalAdressfileName = directoryadress + "//ImaginaryPOdata.bin";
            this.ApplicationDataFileName = directoryadress + "//ImaginaryApplicationData.bin";
        }
        public void SerializeApplicationData()
        {
            try
            {
                using (Stream stream = File.Open(ApplicationDataFileName, FileMode.Create))
                {
                    ApplicationData appDataObj = ApplicationData.Create;
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, appDataObj);
                }
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        
        
        //D://Imaginarydata.bin
        public void SerializeProperties()
        {
            try
            {
                using (Stream stream = File.Open(PropertyfileName, FileMode.Create))
                {
                    PropertyLog log = PropertyLog.Create;
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, log.propertyList);
                }
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void SerializePostalCodes()
        {
            try
            {
                using (Stream stream = File.Open(PostalAdressfileName, FileMode.Create))
                {
                    PostalAdressLog log = PostalAdressLog.Create;
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, log.adressList);
                }
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void DeserializeApplicationData()
        {
            try
            {
                if (File.Exists(ApplicationDataFileName) == false)
                {
                    return;
                }

                using (Stream stream = File.Open(ApplicationDataFileName, FileMode.Open))
                {
                 
                    BinaryFormatter bin = new BinaryFormatter();
                    ApplicationData.Create =(ApplicationData)bin.Deserialize(stream);
                }
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }
        
        
        }
        public void DeserializePostalCodes()
        {

            try
            {
                if (File.Exists(PostalAdressfileName) == false)
                {
                    return;
                }

                using (Stream stream = File.Open(PostalAdressfileName, FileMode.Open))
                {
                    PostalAdressLog log = PostalAdressLog.Create;
                    BinaryFormatter bin = new BinaryFormatter();
                    var profiles = (Dictionary<string,PostalAdress>)bin.Deserialize(stream);

                    foreach (var x in profiles)
                    {
                        log.adressList.Add(x.Key, x.Value);
                    }
                }
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeserializeProperties()
        {

            try
            {
                if (File.Exists(PropertyfileName) == false)
                {
                    return;
                }
                else
                {
                    using (Stream stream = File.Open(PropertyfileName, FileMode.Open))
                    {
                        PropertyLog log = PropertyLog.Create;
                        BinaryFormatter bin = new BinaryFormatter();
                        var profiles = (Dictionary<string,Property>)bin.Deserialize(stream);
                        foreach (var x in profiles)
                        {
                            if (log.Contains(x.Key) == false)
                            {
                                log.Add(x.Value);
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
