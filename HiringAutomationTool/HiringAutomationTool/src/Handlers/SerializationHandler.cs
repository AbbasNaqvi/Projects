using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HiringAutomationTool
{
     class  SerializationHandler
    {

        public void SerializeJobs()
        {
            try
            {
                using (Stream stream = File.Open("Jobsdata.bin", FileMode.Create))
                {
                    PostedJobsLog log = PostedJobsLog.Create;
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, log.JobsList);
                }
            }
            catch (IOException)
            {

            }

        }
        public void DeserializeJobs()
        {

            try
            {
                using (Stream stream = File.Open("Jobsdata.bin", FileMode.Open))
                {
                    PostedJobsLog log = PostedJobsLog.Create;
                    BinaryFormatter bin = new BinaryFormatter();
                    var profiles = (List<PostedJobs>)bin.Deserialize(stream);

                    foreach (var x in profiles)
                    {
                        if (log.Contains(x.Name) == false)
                        {
                            if (x.Name.Length > 3)
                            {
                                log.JobsList.Add(x);
                            }

                        }
                    }
                }
            }
            catch (IOException)
            {


            }
        }
     public void SerializeApplicants()
         {
             try
             {
                 using (Stream stream = File.Open("Applicantsdata.bin", FileMode.Create))
                 {
                     ApplicantLog log = ApplicantLog.Create;
                     BinaryFormatter bin = new BinaryFormatter();
                     bin.Serialize(stream, log.applicantsList);
                 }
             }
             catch (IOException)
             {

             }
         
         }
         public void DeserializeApplicants()
         {

             try
             {
                 using (Stream stream = File.Open("Applicantsdata.bin", FileMode.Open))
                 {
                     ApplicantLog log = ApplicantLog.Create;
                     BinaryFormatter bin = new BinaryFormatter();
                     var profiles = (List<Applicant>)bin.Deserialize(stream);

                     foreach (var x in profiles)
                     {
                         if(String.IsNullOrEmpty(x.Name)==false&&log.Contains(x.CVNumber)==false&&x.Name.Length>3)
                         log.applicantsList.Add(x);
                     }
                 }
             }
             catch (IOException)
             {


             }
         }
        public void Serialize()
        {
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Create))
                {
                    ProfileLog log = ProfileLog.Create;
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, log.ProfileList);
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
                    ProfileLog log = ProfileLog.Create;
                    BinaryFormatter bin = new BinaryFormatter();
                    var profiles = (HashSet<Profile>)bin.Deserialize(stream);

                    foreach (var x in profiles)
                    {
                        Profile p = new Profile();
                        p.Email = x.Email;
                        p.Password = x.Password;
                        p.IsActive = x.IsActive;
                        log.Add(p);
                    }
                }
            }
            catch (IOException)
            { 
            
            
            }
		    
        
        
        }

    }
}
