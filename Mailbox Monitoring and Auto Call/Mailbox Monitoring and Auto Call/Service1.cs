using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Mailbox_Monitoring_and_Auto_Call
{
    public partial class MyNewService : ServiceBase
    {
        public MyNewService()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("DoDyLogSourse"))
                System.Diagnostics.EventLog.CreateEventSource("DoDyLogSourse",
                                                                      "DoDyLog");

            eventLog1.Source = "DoDyLogSourse";
            // the event log source by which 

            //the application is registered on the computer

            eventLog1.Log = "DoDyLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("my service started"); 
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("my service stoped");
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
