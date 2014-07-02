using System;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can use Console.WriteLine for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");
namespace ConsoleApplication1
{
    class Solution
    {
        public int solution(int[] A)
        {
            double maximum = 0;
            double sum = 0;
            if (A.Length == 0)
            {
                return 1;
            }

            for (int i = 0; i < A.Length; i++)
            {
                if (maximum < A[i])
                {
                    maximum = A[i];
                }
                sum += A[i];
            }

            for(int i=(int)maximum;i>=0;i--)
            {
                sum -= i;
            }
            if(sum==0)
            {
            return 1;
            }else
            {
            return 0;
            }

        }
      
    }
}