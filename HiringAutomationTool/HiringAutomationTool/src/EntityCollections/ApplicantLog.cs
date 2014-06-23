using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringAutomationTool
{
    class ApplicantLog
    {
        public List<Applicant> applicantsList = new List<Applicant>();
        static ApplicantLog log = new ApplicantLog();
        static public ApplicantLog Create
        {
            get { return log; }
        }
        public bool Contains(string CVNumber)
        {
            foreach (Applicant x in applicantsList)
            {
                if (x.CVNumber.Equals(CVNumber))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
