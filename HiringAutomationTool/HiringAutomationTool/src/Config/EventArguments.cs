using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringAutomationTool
{
    public class EventArguments:EventArgs
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

        private DateTime date;

        public DateTime Date
        {
            get { return date.ToLocalTime();}
            set { date = value; }
        }

        private string recordNO;

        public string RecordNo
        {
            get { return recordNO; }
            set { recordNO = value; }
        }
        
        
    }
}
