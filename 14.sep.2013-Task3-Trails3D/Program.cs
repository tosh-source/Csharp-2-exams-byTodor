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
3MLR1M
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
            char[,] matrix = new char[rows + 1, cols + 1];  //???-може би ще трябва да махна "cols + 1" и да остане само "cols"

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

            sbyte redPlayerResult = 0;   //default(draw) -> 0 / lost -> -1 / win -> 1
            sbyte bluePlayerResult = 0;  //default(draw) -> 0 / lost -> -1 / win -> 1

            for (int gameCycle = 0; gameCycle < Math.Max(redMotions.Count, blueMotions.Count); gameCycle++)
            {
                CatchPlayerMoves(ref redPlayerResult, redMotions, ref currRedDirection, ref redPlayerIndex, matrix, ref redRow, ref redCol);

                CatchPlayerMoves(ref bluePlayerResult, blueMotions, ref currBlueDirection, ref bluePlayerIndex, matrix, ref blueRow, ref blueCol);
            }

        }

        private static void CatchPlayerMoves(ref sbyte playerResult, List<string> playerMotions, ref byte playerDirection, ref short playerIndex, char[,] matrix, ref int row, ref int col)
        {
            char playerInGame = matrix[row, col];

            for (int motionCycles = playerIndex; motionCycles < playerMotions.Count; motionCycles++, playerIndex++)
            {
                if (playerMotions[playerIndex][0] == 'R')
                {
                    playerDirection++;
                }
                else if (playerMotions[playerIndex][0] == 'L')
                {
                    playerDirection--;
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

                    MovePlayerOnTheGrid(ref playerResult, playerInGame, playerDirection,ref howManyMoves, matrix, ref row, ref col);
                    playerIndex++;
                    break;
                }
            }
        }

        private static void MovePlayerOnTheGrid(ref sbyte playerResult, char playerInGame, byte playerDirection,ref short howManyMoves, char[,] matrix, ref int row, ref int col)
        {
            //directions: U = "Up", R = "Right", D = "Down", L = "Left"
            if (directions[playerDirection] == 'U')
            {
                row--;
                if (row < 0)  //player fall, out of grid
                {
                    playerResult = -1;
                    return;
                }

                playerResult = MakeTrailsOnTheGrid(playerResult, playerInGame, matrix, row, col);
            }
            else if (directions[playerDirection] == 'R')
            {
                col++;
                if (col >= matrix.GetLength(1))  //player will move from the last grid to the firs one
                {
                    col = 0;
                }

                playerResult = MakeTrailsOnTheGrid(playerResult, playerInGame, matrix, row, col);
            }
            else if (directions[playerDirection] == 'D')
            {
                row++;
                if (row >= matrix.GetLength(0))  //player fall, out of grid
                {
                    playerResult = -1;
                    return;
                }

                playerResult = MakeTrailsOnTheGrid(playerResult, playerInGame, matrix, row, col);
            }
            else if (directions[playerDirection] == 'L')
            {
                col--;
                if (col < 0)  //player will move from the first grid to the last one
                {
                    col = matrix.GetLength(1) - 1;  //???-да видя дали няма хвърле exeption, ако да, да махна '-1'
                }

                playerResult = MakeTrailsOnTheGrid(playerResult, playerInGame, matrix, row, col);
            }

            //repeat operations
            if (howManyMoves > 1)
            {
                howManyMoves--;
                MovePlayerOnTheGrid(ref playerResult, playerInGame, playerDirection,ref howManyMoves, matrix, ref row, ref col);
            }
            else
            {
                return;
            }
        }

        private static sbyte MakeTrailsOnTheGrid(sbyte playerResult, char playerInGame, char[,] matrix, int row, int col)
        {
            if (matrix[row, col] == '\0')  //if no trails here, continue.. ('\0' == empty char)
            {
                matrix[row, col] = playerInGame;
            }
            else
            {
                playerResult = -1;
            }

            return playerResult;
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
//use .NET 4.6.1 (C# 6.0)