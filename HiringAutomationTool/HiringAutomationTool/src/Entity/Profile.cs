using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringAutomationTool
{
    [Serializable()]
    class Profile
    {
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        
        
    }
}
