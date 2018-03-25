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
            short x = X_Y_Z[0];  //по картинката изглежда, че трябва да завъртя стойностите на Х и Y
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

            //2c.Define some more parameters
            char redPlayerInGame = 'R';
            char bluePlayerInGame = 'B';

            short redIndex = 0;
            short blueIndex = 0;

            sbyte redResult = 0;   //default(draw) -> 0 / lost -> -1 / win -> 1
            sbyte blueResult = 0;  //default(draw) -> 0 / lost -> -1 / win -> 1

            for (int gameCycle = 0; gameCycle < Math.Max(redMotions.Count, blueMotions.Count); gameCycle++)
            {
                CatchPlayerMoves(ref redResult, redPlayerInGame, redMotions, ref currRedDirection, ref redIndex, matrix, ref redRow, ref redCol, gameCycle);

                CatchPlayerMoves(ref blueResult, bluePlayerInGame, blueMotions, ref currBlueDirection, ref blueIndex, matrix, ref blueRow, ref blueCol, gameCycle);
            }

        }

        private static void CatchPlayerMoves(ref sbyte playerResult, char playerInGame, List<string> playerMotions, ref byte playerDirection, ref short playerIndex, char[,] matrix, ref int row, ref int col, int gameCycle)
        {
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
                    short howManyMovesMAX = 0;
                    if (!(short.TryParse(tempNumb, out howManyMoves)))
                    {
                        howManyMoves = 1;
                        howManyMovesMAX = howManyMoves;
                    }
                    else
                    {
                        howManyMovesMAX = howManyMoves;
                    }

                    MovePlayerOnTheGrid(ref playerResult, playerInGame, playerDirection, ref howManyMoves, howManyMovesMAX, matrix, ref row, ref col, gameCycle);
                    playerIndex++;
                    break;
                }
            }
        }

        private static void MovePlayerOnTheGrid(ref sbyte playerResult, char playerInGame, byte playerDirection, ref short howManyMoves, short howManyMovesMAX, char[,] matrix, ref int row, ref int col, int gameCycle)
        {
            //directions: U = "Up", R = "Right", D = "Down", L = "Left"
            if (directions[playerDirection] == 'U')
            {
                if (!(gameCycle == 0 && (howManyMovesMAX - howManyMoves == 0)))  //skip very first move
                {
                    row--;
                }

                if (row < 0)  //player fall, out of grid
                {
                    playerResult = -1;
                    return;
                }

                playerResult = MakeTrailsOnTheGrid(playerResult, playerInGame, matrix, row, col);
            }
            else if (directions[playerDirection] == 'R')
            {
                if (!(gameCycle == 0 && (howManyMovesMAX - howManyMoves == 0)))  //skip very first move
                {
                    col++;
                }

                if (col >= matrix.GetLength(1))  //player will move from the last grid to the firs one
                {
                    col = 0;
                }

                playerResult = MakeTrailsOnTheGrid(playerResult, playerInGame, matrix, row, col);
            }
            else if (directions[playerDirection] == 'D')
            {
                if (!(gameCycle == 0 && (howManyMovesMAX - howManyMoves == 0)))  //skip very first move
                {
                    row++;
                }

                if (row >= matrix.GetLength(0))  //player fall, out of grid
                {
                    playerResult = -1;
                    return;
                }

                playerResult = MakeTrailsOnTheGrid(playerResult, playerInGame, matrix, row, col);
            }
            else if (directions[playerDirection] == 'L')
            {
                if (!(gameCycle == 0 && (howManyMovesMAX - howManyMoves == 0)))  //skip very first move
                {
                    col--;
                }

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
                MovePlayerOnTheGrid(ref playerResult, playerInGame, playerDirection, ref howManyMoves, howManyMovesMAX, matrix, ref row, ref col, gameCycle);
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