using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ItextSharp
{
    class SerializationHandler
    {
       
         private  string AdressLogFileName = "D://AdressLog.bin";

        public string SerializeAddress()
        {
            string Result = null;
            AddressesLog log = AddressesLog.Create;
            if (log.adressList.Count == 0)
            {
                Result = "Trace: Nothing to Serialize \n";
            }
            try
            {
                using (Stream stream = File.Open(AdressLogFileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, log.adressList);
                }
            }
            catch (IOException ex)
            {
                Result = "Exception:While Serializing Adress Log -"+ex.Message+"\n";
            }
            return Result;
        }




        public string DeserializeAddressesLog()
        {
            string Result = null;
            try
            {
                if (File.Exists(AdressLogFileName) == false)
                {
                    Result = "Error:  Can not Find File to Deserialize\n";
                    return Result;
                }
                else
                {
                    using (Stream stream = File.Open(AdressLogFileName, FileMode.Open))
                    {
                        AddressesLog log = AddressesLog.Create;
                        BinaryFormatter bin = new BinaryFormatter();
                        try
                        {
                            var profiles = (Dictionary<string, Adress>)bin.Deserialize(stream);
                             foreach (var x in profiles)
                             {
                                 if (log.adressList.ContainsKey(x.Key) == false)
                                 {
                                     log.adressList.Add(x.Key, x.Value);
                                 }
                             }
                        }
                        catch (Exception e)
                        {
                            Result = "Exception: While Deserializing Address";
                            return Result;
                        }
                       
                        return Result;
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
