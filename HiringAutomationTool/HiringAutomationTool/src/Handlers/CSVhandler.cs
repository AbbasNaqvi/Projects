using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HiringAutomationTool
{




    class CSVhandler
    {

        public string WriteFolderApplicantLists(string InitialPath)
        {
            
            string ErrorText = null;
            /*  InitialPath=System.IO.Path.Combine(InitialPath+ @"CV");
              if (Directory.Exists(InitialPath) == false)
              {
                  Directory.CreateDirectory(InitialPath);
              }

              InitialPath = System.IO.Path.Combine(InitialPath + @"/ApplicantsReport.csv");

              try
              {
                  if (File.Exists(InitialPath))
                  {
                      File.Delete(InitialPath);
                  }

              }
              catch (IOException)
              {
                  return "Path is ambigious or Secured";
              }
              var ThisFile = File.Create(InitialPath);
             ThisFile.Close();
          */    
    
            /*
             * 
             * Writing CSV file
             * 
             */

            PostedJobsLog postedJoblogObj = PostedJobsLog.Create;


            foreach (PostedJobs x in postedJoblogObj.JobsList)
            {
                if(Directory.Exists(InitialPath + @"\CV\" + x.Name + @"\")==false)
                {
                    Directory.CreateDirectory(InitialPath + @"\CV\" + x.Name + @"\");                
                }
                string headerLine = "Name,Nationality,Experience,Carrier Level,Expected Salary,Current Salary,Degree Level,Education,Industry,Functional Area,Professional Summary,imageLink,CV folder,CV number,Apply Date,marital Status,tags" + ",\r";
                File.AppendAllText(InitialPath + @"\CV\" + x.Name + @"\" + x.Name + @".csv", headerLine);
                headerLine = null;
                ApplicantLog applog = ApplicantLog.Create;
                foreach (Applicant i in applog.applicantsList)
                {
                    if (i.FolderName.Equals(x.Name) == true)
                    {
                        StringBuilder sb = new StringBuilder();
                        StringBuilder Tagsbuilder = new StringBuilder();

                        sb.Clear();
                        Tagsbuilder.Clear();
                        /*  foreach (string j in i.tags)
                          {
                              Tagsbuilder.Clear();                            
                              j.Replace(",", "");
                              sb.Append(j+"-");
                          }*/
                        sb.AppendLine(i.Name + "," + i.Nationality + "," + i.Experience + "," + i.CarrierLevel + "," + i.ExpectedSalary + "," + i.CurrentSalary + "," + i.DegreeLevel + "," + i.Education + "," + i.Industry + "," + i.FunctionalArea + "," + i.ProfessionalSummary + "," + i.PictureLink + "," + i.FolderName + "," + i.CVNumber + "," + i.ApplyDate + "," + i.MaritalStatus + "," + Tagsbuilder + ",");
                        File.AppendAllText(InitialPath + @"\CV\" + x.Name + @"\" + x.Name + ".csv", sb.ToString());
                    }
                }

            }
            return ErrorText;
        }





        /// <summary>
        /// ////////////////////////////////////////////
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>

        public string WriteTheApplicantLists(string Path)
        {
            Path=System.IO.Path.Combine(Path+ @"\\CV");
            if (Directory.Exists(Path) == false)
            {
                Directory.CreateDirectory(Path);
            }

            Path = System.IO.Path.Combine(Path + @"/ApplicantsReport.csv");

            try
            {
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }

            }
            catch (IOException)
            {
                return "Path is ambigious or Secured";
            }
            var ThisFile = File.Create(Path);
            ThisFile.Close();

            /*
             * 
             * Writing CSV file
             * 
             */

            StringBuilder sb = new StringBuilder();
            StringBuilder Tagsbuilder = new StringBuilder();
            string headerLine = "Name,Experience,Carrier Level,Expected Salary,Current Salary,Degree Level,Education,Industry,Functional Area,CV folder,CV number,Apply Date,marital Status,\r";

            File.AppendAllText(Path, headerLine);
            ApplicantLog log = ApplicantLog.Create;
            try
            {
                foreach (Applicant i in log.applicantsList)
                {

                    sb.Clear();
                    Tagsbuilder.Clear();
                    /*  foreach (string j in i.tags)
                      {
                          Tagsbuilder.Clear();                            
                          j.Replace(",", "");
                          sb.Append(j+"-");
                      }*/
                    sb.AppendLine(i.Name + "," + i.Experience + "," + i.CarrierLevel + "," + i.ExpectedSalary + "," + i.CurrentSalary + "," + i.DegreeLevel + "," + i.Education + "," + i.Industry + "," + i.FunctionalArea + "," + i.FolderName + "," + i.CVNumber + "," + i.ApplyDate + "," + i.MaritalStatus + ",");
                    File.AppendAllText(Path, sb.ToString());
                }
            }
            catch (IOException e)
            {
                return e.Message;
            }
            return null;
        }
    }
}
