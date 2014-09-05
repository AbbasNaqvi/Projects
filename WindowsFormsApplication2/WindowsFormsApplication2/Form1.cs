using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using System.IO;
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NameValueCollection headers = new NameValueCollection();
        private void Form1_Load(object sender, EventArgs e)
        {
           

 //          richTextBox1.Text += reader.ReadToEnd();
   //         reader.Close();



          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpWebResponse response = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://www.facebook.com/login.php?login_attempt=1");
          //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(@"https://www.facebook.com/login.php?login_attempt=1"));
            request.KeepAlive = false;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.20 Safari/537.36";
            request.Headers.Add(headers);
            try
            {
               response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception w)
            {
                richTextBox1.Text += "@<b>" + w.Message + "@</b>";
//              throw new Exception(w.Message);
            }
            //            headers =
            var a = response.Cookies;
            foreach (Cookie k in a)
            {
                try
                {
                    headers.Add(k.Name, k.Value);
                    richTextBox1.Text += "next)\na)" + k.Value + "\n";
                    richTextBox1.Text += "b)" + k.Name + "\n";
                    richTextBox1.Text += "c)" + k.Path + "\n";
                    richTextBox1.Text += "d)" + k.Port + "\n";
                    richTextBox1.Text += "e)" + k.Version + "\n\n\n";
                }
                catch (Exception x)
                {
//                    throw new Exception(x.Message);
                    richTextBox1.Text += "@<b>" + x.Message + "@</b>";
                }
                finally {
                    response.Close();
                }
            }
        }   
    }
}