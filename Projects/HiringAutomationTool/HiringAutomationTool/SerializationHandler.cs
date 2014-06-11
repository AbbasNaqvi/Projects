using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HiringAutomationTool
{
    class SerializationHandler
    {

        public void Serialize()
        {
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, ProfileLog.profileList);
                }
            }
            catch (IOException)
            {

            }
        }
        public void Deserialize()
        {
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    var profiles = (HashSet<Profile>)bin.Deserialize(stream);

                    foreach (var x in profiles)
                    {
                        Profile p = new Profile();
                        p.Email = x.Email;
                        p.Password = x.Password;
                        ProfileLog.Add(p);
                    }
                }
            }
            catch (IOException)
            { 
            
            
            }
		    
        
        
        }

    }
}
