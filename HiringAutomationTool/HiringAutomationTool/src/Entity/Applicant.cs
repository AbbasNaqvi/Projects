using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace HiringAutomationTool
{
    [Serializable()]
    class Applicant
    {

       public List<string> tags = new List<string>();

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value.Replace(",", "-"); ; }
        }

        private string cvNumber;

        public string CVNumber
        {
            get { return cvNumber; }
            set { cvNumber = value.Replace(",", "-"); ; }
        }

        private string applyDate;

        public string ApplyDate
        {
            get { return applyDate; }
            set { applyDate = value.Replace(",", "-"); ; }
        }

        private string pictureLink;

        public string PictureLink
        {
            get { return pictureLink; }
            set { pictureLink = value; }
        }


        private string industry;

        public string Industry
        {
            get { return industry; }
            set { industry = value.Replace(",","-"); }
        }

        private string folderName;

        public string FolderName
        {
            get { return folderName; }
            set { folderName = value.Replace(",", "-"); }
        }


        private string currentSalary;

        public string CurrentSalary
        {
            get { return currentSalary; }
            set { currentSalary = value.Replace(",", "-"); }
        }



        private string nationality;

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value.Replace(",", "-"); }
        }

        private string education;

        public string Education
        {
            get { return education; }
            set { education = value.Replace(",", "-"); }
        }

        private string maritalStatus;

        public string MaritalStatus
        {
            get { return maritalStatus; }
            set { maritalStatus = value.Replace(",", "-"); }
        }
        

        private string experience;
        public string Experience
        {
            get { return experience; }
            set { experience = value.Replace(",", "-"); }
        }

        private float skillScore;

        public float SkillScore
        {
            get { return skillScore; }
            set { skillScore = value; }
        }

        private string carrierLevel;

        public string CarrierLevel
        {
            get { return carrierLevel; }
            set { carrierLevel = value.Replace(",", "-"); ; }
        }

        private string expectedSalary;

        public string ExpectedSalary
        {
            get { return expectedSalary; }
            set { expectedSalary = value.Replace(",", "-"); ; }
        }

        private string degreeLevel;

        public string DegreeLevel
        {
            get { return degreeLevel; }
            set { degreeLevel = value.Replace(",", "-"); ; }
        }

        private string functionalArea;

        public string FunctionalArea
        {
            get { return functionalArea; }
            set { functionalArea = value.Replace(",", "-"); ; }
        }

        private string professionalSummary;

        public string ProfessionalSummary
        {
            get { return professionalSummary; }
            set { professionalSummary = value.Replace(",", "-"); ; }
        }

        private string onlineFileName;

        public string OnlineFileName
        {
            get { return onlineFileName; }
            set { onlineFileName = value; }
        }
        
        private string cvLink;

        public string CVLink
        {
            get { return cvLink; }
            set { cvLink = value; }
        }
        

    }
}
