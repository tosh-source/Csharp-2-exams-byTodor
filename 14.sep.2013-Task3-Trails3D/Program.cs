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
            StringReader reader = new StringReader(@"4 2 4
3M1M
2M1M1M");
            Console.SetIn(reader);

            //input
            short[] X_Y_Z = Console.ReadLine().Split(' ').Select(short.Parse).ToArray();
            short x = X_Y_Z[0];
            short y = X_Y_Z[1];
            short z = X_Y_Z[2];

            string red = Console.ReadLine();
            string blue = Console.ReadLine();

            //processing the inputs
            List<string> redMotions = new List<string>();
            assignMoves(red, redMotions);
            List<string> blueMotions = new List<string>();
            assignMoves(blue, blueMotions);

            //calculating
            int rows = y;
            int cols = x * z;
            byte[,] matrix = new byte[rows + 1, cols + 1];

            //start positions on 'X' and 'Y'
            //red player
            int startRed_X = (x + 1) / 2;  //start position on 'x'
            int startRed_Y = (y + 1) / 2;  //start position on 'y'
            //blue player
            int startBlue_X = x + z + startRed_X;
            int startBlue_Y = startRed_Y;

            for (int gameCycle = 0; gameCycle < Math.Max(redMotions.Count, blueMotions.Count); gameCycle++)
            {

            }

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