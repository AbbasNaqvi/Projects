using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace HiringAutomationTool
{
     class ProfileLog
    {
        private HashSet<Profile> profileList;

        public HashSet<Profile> ProfileList
        {
            get { return profileList; }
            set { profileList = value; }
        }
        
        ProfileLog()
        {
             profileList = new HashSet<Profile>();
        }
         static ProfileLog log  = new ProfileLog();

        public static ProfileLog Create
        {
            get
            {
                return log;
            }
        }

        public string FindPasswordofThisEmail(string email)
        {
            foreach (var x in profileList)
            {
                if (x.Email.Equals(email))
                {
                    return x.Password;
                }
            }
            return null;
        }
        public bool IsContainsProfile(string id)
        {
            if (String.IsNullOrEmpty(id) == false)
            {
                foreach (Profile x in profileList)
                {                   
                    if (String.IsNullOrEmpty(x.Email)==false&&x.Email.Equals(id))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void TurnActive(string email)
        {
            foreach (Profile x in profileList)
            {
                if(String.IsNullOrEmpty(email)==false&&String.IsNullOrEmpty(x.Email)==false)
                if (x.Email.Equals(email))
                {
                    x.IsActive = true;
                }
                else
                {
                    x.IsActive = false;
                }
            }
        }

        public bool Add(Profile item)
        {
            if (IsContainsProfile(item.Email))
            {
                return false;
            }
            profileList.Add(item);
            return true;
        }
       
    }
}
