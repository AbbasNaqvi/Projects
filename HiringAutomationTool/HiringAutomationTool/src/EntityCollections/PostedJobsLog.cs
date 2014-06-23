using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HiringAutomationTool
{
    class PostedJobsLog
    {
        public List<PostedJobs> JobsList=new List<PostedJobs>();
              
        static PostedJobsLog log = new PostedJobsLog();
        PostedJobsLog()
        {
            JobsList = new List<PostedJobs>();
        }
        public static PostedJobsLog Create { get { return log; } }
        public bool Contains(string JobName)
        {
            foreach (PostedJobs x in JobsList)
            {
                string thisone = x.Name.Trim();
                string secondone = JobName.Trim();
                int a = JobName.Length;
                int b = x.Name.Length;
                if (String.IsNullOrEmpty(x.Name)==false)
                {
                    if (x.Name.Equals(JobName.Trim()))
                    {
                                  
                        return true;
                    }
                    
                }
            }
           return false;
        }
        public List<string> GetAllLinks()
        {
            List<string> Links = new List<string>();
            foreach (PostedJobs i in JobsList)
            {
                Links.Add(i.Link);
            }
            return Links;
        }
      
    }
}
