using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            string SubjectString=@"This is a normal demo text.
&Here an other demo text.
And one more demo text.
&&Here will continue this text.
Bla bla blaaa...";

            string ResultString = null;
            try
            {
                ResultString = Regex.Replace(SubjectString, "^&{2}((?!&).+)$", "<em>$1</em>",
                    RegexOptions.Multiline);
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }
            string[] xa = ResultString.Split(' ');

            foreach (var s in xa)
            {
                Console.WriteLine(s);
            
            }

        }
    }
}
