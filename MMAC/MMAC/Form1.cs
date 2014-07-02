using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MMAC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            waspPOP3 emailsFactory = new waspPOP3();
            //try
            // {
            //}
            //catch (Exception e)
            ///{
            // richTextBoxMessages.Text += e.Message + "    ---Exception\n";            
            //}
            richTextBoxMessages.Text += emailsFactory.DoConnect("pop.mail.yahoo.com", 995, "pak_fast09@yahoo.com", "Disneyyahoo5") + "Fine\n";
            richTextBoxMessages.Text += emailsFactory.GetStat();
            richTextBoxMessages.Text += emailsFactory.Retr(1);
        //    richTextBoxMessages.Text += emailsFactory.GetList();
            richTextBoxMessages.Text += emailsFactory.GetNoop();
            richTextBoxMessages.Text += emailsFactory.GetUidl();
            richTextBoxMessages.Text += emailsFactory.GetTop(3);
        }
    }
}
