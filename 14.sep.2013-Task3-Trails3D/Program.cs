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
        private static char[] directions = { 'U', 'R', 'D', 'L' };   //Directions: U = "Up", R = "Right", D = "Down", L = "Left"

        static void Main(string[] args)
        {   //BGCoder & Conditions: http://bgcoder.com/Contests/95/CSharp-Part-2-2013-2014-14-Sept-2013-Evening
            //video: http://youtu.be/iGzwyi70C2k

            //tests
            StringReader reader = new StringReader(@"4 2 4
3M1M
2M1M1M");
            Console.SetIn(reader);

            //1.input
            short[] X_Y_Z = Console.ReadLine().Split(' ').Select(short.Parse).ToArray();
            short x = X_Y_Z[0];
            short y = X_Y_Z[1];
            short z = X_Y_Z[2];

            string red = Console.ReadLine();
            string blue = Console.ReadLine();

            //1a.processing the inputs
            List<string> redMotions = assignMoves(red);
            List<string> blueMotions = assignMoves(blue);

            //2.Calculations

            //2a.Define the matrix
            int rows = y;
            int cols = x * z;
            char[,] matrix = new char[rows + 1, cols + 1];

            //2b.Define start 'X' and 'Y' coordinates
            //red player
            int redRow = (y + 1) / 2;   //start red on position "Y"
            int redCol = (x + 1) / 2;   //start red on position "X"
            byte currRedDirection = 1;  //directions[1] -> R = "Right

            //blue player
            int blueRow = redRow;         //start blue on position "Y"
            int blueCol = x + z + redCol; //start blue on position "X"
            byte currBlueDirection = 3;   //directions[3] -> L = "Left

            //2c.Define start positions on the matrix
            matrix[redRow, redCol] = 'R';    //RED player == R
            matrix[blueRow, blueCol] = 'B';  //BLUE player == B

            short redPlayerIndex = 0;
            short bluePlayerIndex = 0;

            for (int gameCycle = 0; gameCycle < Math.Max(redMotions.Count, blueMotions.Count); gameCycle++)
            {
                MovePlayerOnTheGrid(redMotions, ref currRedDirection, ref redPlayerIndex);

                MovePlayerOnTheGrid(blueMotions, ref currBlueDirection, ref bluePlayerIndex);
            }

        }

        private static void MovePlayerOnTheGrid(List<string> playerMotions, ref byte currentPlayerDirection, ref short playerIndex)
        {
            for (int motionCycles = playerIndex; motionCycles < playerMotions.Count; motionCycles++, playerIndex++)
            {
                if (playerMotions[playerIndex][0] == 'R')
                {
                    currentPlayerDirection++;
                }
                else if (playerMotions[playerIndex][0] == 'L')
                {
                    currentPlayerDirection--;
                }
                else //redMotions[redIndex][0] == M
                {
                    string tempNumb = string.Empty;
                    for (int parseNumbs = 0; parseNumbs < playerMotions[playerIndex].Length; parseNumbs++)
                    {
                        if (!(char.IsDigit(playerMotions[playerIndex][parseNumbs]))) //if no more digits, stop
                        {
                            break;
                        }
                        tempNumb += playerMotions[playerIndex][parseNumbs];
                    }

                    short howManyMoves = 0;
                    if (!(short.TryParse(tempNumb, out howManyMoves)))
                    {
                        howManyMoves = 1;
                    }

                    //MoveOnTheGrid(currentPlayerDirection, );
                    playerIndex++;
                    break;
                }
            }            
        }

        private static List<string> assignMoves(string player)
        {
            List<string> motions = new List<string>();

            //int integer = 0;
            string integersAsStr = string.Empty;
            for (int i = 0; i < player.Length; i++)
            {
                if (int.TryParse(player[i].ToString(), out int integer))
                {
                   integersAsStr += integer.ToString();
                }
                else if (integersAsStr != string.Empty)
                {
                    motions.Add(integersAsStr + player[i].ToString());
                    integersAsStr = string.Empty;
                }
                else
                {
                    motions.Add(player[i].ToString());
                }
            }

            return motions;
        }
    }
}

//info: http://my.telerikacademy.com/Forum/Questions/8445/CSharp-Part2-Exam-Evening-Trails-3D