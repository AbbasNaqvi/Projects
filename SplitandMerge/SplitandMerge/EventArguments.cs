using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplitandMerge
{
   public class EventArguments:EventArgs
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private DateTime time;

        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        private string details;

        public string Details
        {
            get { return details; }
            set { details = value; }
        }
        



    }
}
