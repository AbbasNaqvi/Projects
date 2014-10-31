using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpLearning
{
    class GoogleResult
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        private string link;

        public string Link
        {
            get { return link; }
            set { link = value; }
        }

    }
}
