using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _14.sep._2013_Task3_Trails3D
{
    class Trails3D
    {
        private static char[] directions = { 'U', 'R', 'D', 'L' };   //Directions: U = "Up", R = "Right", D = "Down", L = "Left"

        static void Main(string[] args)
        {   //BGCoder & Conditions: http://bgcoder.com/Contests/95/CSharp-Part-2-2013-2014-14-Sept-2013-Evening
            //video: http://youtu.be/iGzwyi70C2k

//			//tests (WRONG output in test: test.009.in && test.014.in)
//			var test = new StringReader(@"6 4 6
//LMLMMMMMMMMMRMMMLMMRMMMRM
//RMLMMLMRMMMRMMLMMMMMMRMMM");
            //Console.SetIn(test);

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
            int cols = (2 * x) + (2 * z);
            char[,] matrix = new char[rows + 1, cols];

            //2b.Define start 'X' and 'Y' coordinates
            //red player
            int redRowStartPoint = (y + 1) / 2;
            int redColStartPoint = (x + 1) / 2;
            int redRow = redRowStartPoint;   //start red on position "Y"
            int redCol = redColStartPoint;   //start red on position "X"
            sbyte currRedDirection = 1;  //directions[1] -> R = "Right

            //blue player
            int blueRow = redRow;         //start blue on position "Y"
            int blueCol = x + z + redCol; //start blue on position "X"
            sbyte currBlueDirection = 3;   //directions[3] -> L = "Left

            //2c.Define some other parameters
            matrix[redRow, redCol] = 'R';   //first move
            matrix[blueRow, blueCol] = 'B'; //first move
            char redPlayerInGame = 'R';
            char bluePlayerInGame = 'B';

            short redIndex = 0;
            short blueIndex = 0;

            sbyte redResult = 0;   //default(draw) -> 0 / lost -> -1 / win -> 1
            sbyte blueResult = 0;  //default(draw) -> 0 / lost -> -1 / win -> 1

            //3.Calculation
            //3a.Start game
            for (int gameCycle = 0; gameCycle < Math.Max(redMotions.Count, blueMotions.Count); gameCycle++)
            {
                CatchPlayerMoves(ref redResult, redPlayerInGame, redMotions, ref currRedDirection, ref redIndex, matrix, ref redRow, ref redCol, gameCycle);
 
                CatchPlayerMoves(ref blueResult, bluePlayerInGame, blueMotions, ref currBlueDirection, ref blueIndex, matrix, ref blueRow, ref blueCol, gameCycle);

                if (redRow == blueRow && redCol == blueCol)
                {
                    redResult = 0;
                    blueResult = 0;
                    break;
                }
                else if (redResult == -1 || blueResult == -1)  //Players lost or out of the Grid. 
                {
                    break;
                }
            }
            //3b.Calculate distance
            int redStartEndDistance = CalculateDistance(matrix, redRowStartPoint, redColStartPoint, redRow, redCol, redResult);

            //4.Results
			if ((redResult == 0 && blueResult == 0) || (redResult == -1 && blueResult == -1))  //draw
            {
                Console.WriteLine("DRAW" + $"\n{redStartEndDistance}");
            }
            else if (redResult == -1)  //blue win
            {
                Console.WriteLine("BLUE" + $"\n{redStartEndDistance}");
            }
            else if (blueResult == -1)  //red win
            {
                Console.WriteLine("RED" + $"\n{redStartEndDistance}");
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
                else if (player[i] == ' ')
                {
                    continue;
                }
                else
                {
                    motions.Add(player[i].ToString());
                }
            }

            return motions;
        }

        private static int CalculateDistance(char[,] matrix, int rowStartPoint, int colStartPoint, int row, int col, sbyte playerResult)
        {
            //check if red player lost in OUT OF the Grid
            if (playerResult == -1)
            {
                if (row < 0)
                {
                    row = 0;  //correct eventual negative value to prevent "OverFlowException"
                }
                else if (row >= matrix.GetLength(0))
                {
                    row = matrix.GetLength(0) - 1;  //correct eventual higher value to prevent "OverFlowException"
                }
            }

            //Calculation
            bool endFound = false;
            bool rowFound = false;
            bool colFound = false;
            int distCounter = 0;

            while (endFound == false)
            {
                if (row < rowStartPoint)
                {
                    rowStartPoint--;
                    distCounter++;
                }
                else if (row > rowStartPoint)
                {
                    rowStartPoint++;
                    distCounter++;
                }
                else if (row == rowStartPoint)
                {
                    rowFound = true;
                }

                if (col < colStartPoint)
                {
                    colStartPoint--;
                    distCounter++;
                }
                else if (col > colStartPoint)
                {
                    colStartPoint++;
                    distCounter++;
                }
                else if (col == colStartPoint)
                {
                    colFound = true;
                }

                if (rowFound == true && colFound == true) endFound = true;
            }

            return distCounter;
        }

        private static void CatchPlayerMoves(ref sbyte playerResult, char playerInGame, List<string> playerMotions, ref sbyte playerDirection, ref short playerIndex, char[,] matrix, ref int row, ref int col, int gameCycle)
        {
            //Calculation
            for (int motionCycles = playerIndex; motionCycles < playerMotions.Count; motionCycles++, playerIndex++)
            {
                if (playerMotions[playerIndex][0] == 'R') //Turn Right -> //playerDirection++
                {
                    playerDirection = TurnRight(playerDirection);
                }
                else if (playerMotions[playerIndex][0] == 'L') //Turn Left -> //playerDirection--
                {
                    playerDirection = TurnLeft(playerDirection);
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

        private static sbyte TurnRight(sbyte playerDirection)
        {
            playerDirection++;

            if (playerDirection > directions.Length - 1)
            {
                playerDirection = 0;
            }

            return playerDirection;
        }

        private static sbyte TurnLeft(sbyte playerDirection)
        {
            playerDirection--;

            if (playerDirection < 0)
            {
                playerDirection = (sbyte)(directions.Length - 1);
            }

            return playerDirection;
        }

        private static void MovePlayerOnTheGrid(ref sbyte playerResult, char playerInGame, sbyte playerDirection, ref short howManyMoves, short howManyMovesMAX, char[,] matrix, ref int row, ref int col, int gameCycle)
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
                    col = matrix.GetLength(1) - 1;
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
    }
}

//Use .NET 4.6.1 (C# 6.0)
//LINKS:
//info in Telerik forum -> http://my.telerikacademy.com/Forum/Questions/8445/CSharp-Part2-Exam-Evening-Trails-3D
//How to find Manhattan distance and ecuildean distance in big data analytics -> https://www.youtube.com/watch?v=yrzdimXHRDU
//Manhattan_distance -> https://en.wikipedia.org/wiki/Manhattan_distance