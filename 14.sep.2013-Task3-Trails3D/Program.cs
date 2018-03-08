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
            int[] X_Y_Z = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            string red = Console.ReadLine();
            string blue = Console.ReadLine();

            //processing the inputs
            List<string> redMotions = new List<string>();
            assignMoves(red, redMotions);
            List<string> blueMotions = new List<string>();
            assignMoves(blue, blueMotions);

            //calculating
            int[][] matrix = new int[3][];
            matrix[0] = new int[X_Y_Z[0] + 1];  //x
            matrix[1] = new int[X_Y_Z[1] + 1];  //y
            matrix[2] = new int[X_Y_Z[2] + 1];  //z

            int xStart = (matrix[0].Length) / 2;  //start position ox 'x'
            int yStart = (matrix[1].Length) / 2;  //start position on 'y'
            
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