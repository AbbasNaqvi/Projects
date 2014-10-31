using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Expression.Encoder;
using Microsoft.Expression;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            MediaItem mediaItem1 = new MediaItem(@"C:\Users\jafar.baltidynamolog\Videos\2.wmv");
            mediaItem1.Sources.Add(new Source(@"C:\Users\jafar.baltidynamolog\Videos\1.wmv"));
            Job job = new Job();
            job.MediaItems.Add(mediaItem1);
            job.OutputDirectory = @"C://videoOutput";
            job.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(job_EncodeProgress);
            job.Encode();
        }

        static void job_EncodeProgress(object sender, EncodeProgressEventArgs e)
        {
            Console.WriteLine(e.Progress);
        }
    }
}
