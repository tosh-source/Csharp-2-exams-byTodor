using System;

namespace _22.Jan._2014_Task1.TRES4.Numbers
{
    class Program
    {
        static string[] TRESNUM4 = { "LON+", "VK-", "*ACAD", "^MIM", "ERIK|", "SEY&", "EMY>>", "/TEL", "<<DON" };

        static void Main(string[] args)
        {  //BGCoder & conditions: http://bgcoder.com/Contests/142/CSharp-Part-2-2013-2014-22-Jan-2014-Evening
           //video: https://www.youtube.com/watch?v=KOVOnFU-Gr8

            //input
            ulong inputAsDeci = ulong.Parse(Console.ReadLine());

            //calculation
            string deciToTresNum4 = string.Empty;
            ulong remainder = 0;

            do
            {
                remainder = inputAsDeci % 9;
                inputAsDeci = inputAsDeci / 9;

                deciToTresNum4 = TRESNUM4[remainder] + deciToTresNum4;
            } while (inputAsDeci != 0);


            //print
            Console.WriteLine(deciToTresNum4);
        }
    }
}
