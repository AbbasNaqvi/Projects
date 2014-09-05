using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imagenary
{
    public class EventArguments
    {

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string details;

        public string Details
        {
            get { return details; }
            set { details = value; }
        }
        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

    }
}
