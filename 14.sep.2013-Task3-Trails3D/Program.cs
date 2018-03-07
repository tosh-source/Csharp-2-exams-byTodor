using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _14.sep._2013_Task3_Trails3D
{
    class Program
    {
        static void Main(string[] args)
        {   //BGCoder & Conditions: http://bgcoder.com/Contests/95/CSharp-Part-2-2013-2014-14-Sept-2013-Evening
            //video: http://youtu.be/iGzwyi70C2k

            //tests
            StringReader reader = new StringReader(@"8 4 6
2MLM1MRM2MR2MLMLMR3MRM
LMMR2M4MRMLMRMR1M2MRM");
            Console.SetIn(reader);

            //input
            int[] X_Y_Z = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            string red = Console.ReadLine();
            string blue = Console.ReadLine();
            
            //processing the inputs
            List<string> redMotions = new List<string>();
            assignMoves(red, redMotions);
            List<string> blueMotions = new List<string>();
            assignMoves(blue, blueMotions);


        }

        private static void assignMoves(string player, List<string> motions)
        {
            int integer = 0;
            int sumOfIntegers = 0;
            for (int i = 0; i < player.Length; i++)
            {
                if (int.TryParse(player[i].ToString(), out integer))
                {
                    sumOfIntegers += integer;
                }
                else if (sumOfIntegers != 0)
                {
                    motions.Add(sumOfIntegers.ToString() + player[i].ToString());
                    sumOfIntegers = 0;
                }
                else
                {
                    motions.Add(player[i].ToString());
                }
            }
        }
    }
}

//info: http://my.telerikacademy.com/Forum/Questions/8445/CSharp-Part2-Exam-Evening-Trails-3D