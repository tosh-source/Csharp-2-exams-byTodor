using System;

namespace _11.Feb._2013_Task3.KukataIsDancing
{
    class Program
    {
        private static char[] directions = { 'U', 'R', 'D', 'L' };   //Directions: U = "Up", R = "Right", D = "Down", L = "Left"

        static void Main(string[] args)
        {
            //input
            int n = int.Parse(Console.ReadLine());
            string[] dancerMoves = new string[n];

            for (int i = 0; i < n; i++)
            {
                dancerMoves[i] = Console.ReadLine();
            }

            string[][] theCube = {
                    new[] { "RED", "BLUE" , "RED"},
                    new[] { "BLUE", "GREEN", "BLUE"},
                    new[] { "RED", "BLUE", "RED"}
                    };

            //calculation
            Calculations(theCube, dancerMoves);
        }

        private static void Calculations(string[][] theCube, string[] dancerMoves)
        {
            //start calculations
            for (int groupOfSteps = 0; groupOfSteps <= dancerMoves.GetLength(0) - 1; groupOfSteps++)
            {
                int currentDirection = 0;
                int row = 1;  //start from GREEN square
                int col = 1;  //start from GREEN square

                for (int currentStep = 0; currentStep <= dancerMoves[groupOfSteps].Length - 1; currentStep++)
                {
                    if (dancerMoves[groupOfSteps][currentStep] == 'L')        //"Kukata stays in the current square and turns 90 degrees to the LEFT"
                    {
                        currentDirection = TurnLeft(currentDirection);
                    }
                    else if (dancerMoves[groupOfSteps][currentStep] == 'R')   //"Kukata stays in the current square and turns 90 degrees to the RIGHT."
                    {
                        currentDirection = TurnRight(currentDirection);
                    }
                    else if (dancerMoves[groupOfSteps][currentStep] == 'W')   //"Kukata WALKS on the next square in the current direction."
                    {
                        GoToNextSquare(ref row, ref col, currentDirection, theCube);
                    }

                    //print
                    if (currentStep == dancerMoves[groupOfSteps].Length - 1)
                    {
                        Console.WriteLine(theCube[row][col]);
                    }
                }
            }
        }

        private static void GoToNextSquare(ref int row, ref int col, int currentDirection, string[][] theCube)
        {
            //Go to next directions: U = "Up", R = "Right", D = "Down", L = "Left"
            if (directions[currentDirection] == 'U')    //"col" will not changed
            {
                row = row - 1;
                if (row < 0)
                {
                    row = theCube.GetLength(0) - 1;
                }
            }
            else if (directions[currentDirection] == 'R')   //"row" will not changed
            {
                col = col + 1;
                if (col > theCube[0].Length - 1)
                {
                    col = 0;
                }
            }
            else if (directions[currentDirection] == 'D')   //"col" will not changed
            {
                row = row + 1;
                if (row > theCube.GetLength(0) - 1)
                {
                    row = 0;
                }
            }
            else if (directions[currentDirection] == 'L')   //"row" will not changed
            {
                col = col - 1;
                if (col < 0)
                {
                    col = theCube[0].Length - 1;
                }
            }
        }

        private static int TurnLeft(int currentDirection)
        {
            currentDirection--;

            if (currentDirection < 0)
            {
                currentDirection = directions.Length - 1;
            }

            return currentDirection;
        }

        private static int TurnRight(int currentDirection)
        {
            currentDirection++;

            if (currentDirection > directions.Length - 1)
            {
                currentDirection = 0;
            }

            return currentDirection;
        }
    }
}

//another different solutions: http://my.telerikacademy.com/forum/Questions/4899/CSharp-Exam-11-Feb-2013-3-%D0%97%D0%B0%D0%B4%D0%B0%D1%87%D0%B0-Kukata-is-Dancing
