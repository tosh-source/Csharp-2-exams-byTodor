using System;
using System.Linq;
using System.Text;

namespace _7.dec._2016_Task3_SneakyTheSnake
{
    class Program
    {
        const string matrixEntrance = "e";
        const string matrixFood = "@";
        const string matrixRock = "%";
        const string matrixFreeSpace = "-";
        static int counter = 0;
        static int snakeLength = 3;

        static void Main(string[] args)
        {  //condition: https://github.com/TelerikAcademy/CSharp-Part-2/tree/master/Exams/2016-12-07/2016-12-07-Evening/3.%20Sneaky%20the%20Snake
           //BGCoder: http://bgcoder.com/Contests/392/Telerik-Academy-Exam-CSharp-Advanced-7-December-2016-Evening

            //input
            //matrix dimensions
            string[] dimensions = Console.ReadLine().Split('x').ToArray();
            int rows = int.Parse(dimensions[0]);
            int cols = int.Parse(dimensions[1]);
            //the matrix
            string[,] matrix = new string[rows, cols];
            MatrixReadLines(rows, cols, matrix);
            //step directions /w - up; s - down; a - left; d - right/
            string directions = Console.ReadLine().Replace(",", string.Empty);

            //calculations
            MovesCalculation(matrix, directions);

        }

        private static void MatrixReadLines(int rows, int cols, string[,] matrix)
        {
            StringBuilder tempReadLine = new StringBuilder();
            for (int row = 0; row < rows; row++)
            {
                tempReadLine.Append(Console.ReadLine());
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = tempReadLine[col].ToString();
                }
                tempReadLine.Clear();
            }
        }

        private static void MovesCalculation(string[,] matrix, string directions)
        {
            int entrance = FindEntrance(matrix);
            int row = 0;
            int col = entrance;
            int[] lastPosition = { row, col };
            bool exitProgram = false;

            for (int moves = 0; moves < directions.Length; moves++)
            {
                if (directions[moves] == 'w') //goes up => matrix[row-1, col]
                {
                    row = row - 1;
                    CheckSnakeMoves(ref matrix, row, ref col, directions, moves, ref snakeLength, ref exitProgram);
                }
                else if (directions[moves] == 's') //goes down => matrix[row+1, col]
                {
                    row = row + 1;
                    CheckSnakeMoves(ref matrix, row, ref col, directions, moves, ref snakeLength, ref exitProgram);
                }
                else if (directions[moves] == 'a') //goes left => matrix[row, col-1]
                {
                    col = col - 1;
                    CheckSnakeMoves(ref matrix, row, ref col, directions, moves, ref snakeLength, ref exitProgram);
                }
                else if (directions[moves] == 'd') //goes right => matrix[row, col+1]
                {
                    col = col + 1;
                    CheckSnakeMoves(ref matrix, row, ref col, directions, moves, ref snakeLength, ref exitProgram);
                }

                if (exitProgram == true) break; //exit loop
            }
        }

        private static int FindEntrance(string[,] matrix)
        {
            int valueToReturn;
            string temp = null;

            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                temp += matrix[0, col];
            }

            valueToReturn = temp.IndexOf('e');
            return valueToReturn;
        }

        private static void CheckSnakeMoves(ref string[,] matrix, int row, ref int col, string directions, int moves, ref int snakeLength, ref bool exitProgram)
        {
            if (col < (matrix.GetLength(1) - matrix.GetLength(1)))  //Snake goes from the leftmost column, to the rightmost column.
            {
                col = matrix.GetLength(1) - 1;
            }
            else if (col > matrix.GetLength(1) - 1)  //Snake goes form the rightmost column. to the leftmost column.
            {
                col = matrix.GetLength(1) - matrix.GetLength(1);
            }

            //reduce snake length, if needed
            counter++;
            if (counter >= 5)
            {
                snakeLength--;
                counter = 0;
            }

            //Snake moves below the provided depth (row)
            if (row >= matrix.GetLength(0) - 1)
            {
                Console.WriteLine("Sneaky is going to be lost into the depths with length {0}", snakeLength);
                exitProgram = true;
                return;
            }

            //check matrix symbols
            switch (matrix[row, col])
            {
                case matrixFreeSpace: break;
                case matrixFood:
                    snakeLength++;
                    matrix[row, col] = matrixFreeSpace;
                    break;
                case matrixRock:
                    Console.WriteLine("Sneaky is going to hit a rock at [{0},{1}]", row, col);
                    exitProgram = true;
                    return;
                case matrixEntrance:
                    Console.WriteLine("Sneaky is going to get out with length {0}", snakeLength);
                    exitProgram = true;
                    return;
            }

            //Snake length drop to 0 or lower
            if (snakeLength <= 0)
            {
                Console.WriteLine("Sneaky is going to starve at [{0},{1}]", row, col);
                exitProgram = true;
                return;
            }

            //The snake have no more moves
            if (moves >= directions.Length - 1)
            {
                Console.WriteLine("Sneaky is going to be stuck in the den at [{0},{1}]", row, col);
                exitProgram = true;
                return;
            }
        }
    }
}
