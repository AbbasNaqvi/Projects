using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Imagenary
{
    public partial class OLEDBTEST : Form
    {
        OledbHandler handler;
        public OLEDBTEST()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("PropertyID", textBox1.Text);
            parameters.Add("Source", textBox2.Text);
            parameters.Add("FullAddress", textBox3.Text);
            parameters.Add("StreetAddress", textBox4.Text);
            parameters.Add("PostalCode", textBox5.Text);
            parameters.Add("Price", textBox6.Text);
            parameters.Add("MarketedDate", textBox7.Text);
            parameters.Add("MarketedBy", textBox8.Text);
            parameters.Add("SuccessURL", textBox9.Text);
            parameters.Add("AddedOn", textBox10.Text);
            parameters.Add("LastSaleDate", textBox11.Text);
            parameters.Add("LastSalePrice", textBox12.Text);
            handler.AddToDB(parameters);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            textBox14.Text = dialog.FileName;
            handler = new OledbHandler(textBox14.Text);
        }
    }
}
