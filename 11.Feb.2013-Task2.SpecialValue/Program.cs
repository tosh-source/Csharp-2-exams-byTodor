using System;
using System.Linq;

namespace _11.Feb._2013_Task2.SpecialValue
{
    class Program
    {
        static void Main(string[] args)
        {
            //input
            int n = int.Parse(Console.ReadLine());
            int[][] matrix = new int[n][];
            bool[][] visitedMatrix = new bool[n][];

            for (int currentRow = 0; currentRow < n; currentRow++)
            {
                matrix[currentRow] = Console.ReadLine().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                visitedMatrix[currentRow] = new bool[matrix[currentRow].Length];
            }

            //variables
            int currentCol = 0;
            int sumOfPaths = 0;
            int bestSumOfPath = 0;
            //calculation
            for (int colsFromFirstRow = 0; colsFromFirstRow <= matrix[0].Length - 1; colsFromFirstRow++)
            {
                currentCol = colsFromFirstRow;
                sumOfPaths = 0;
                CleanVisitedMatrix(ref visitedMatrix);

                for (int currentRow = 0; true; currentRow++)
                {
                    if (visitedMatrix[currentRow][currentCol] == true)
                    {
                        break;
                    }
                    else if (matrix[currentRow][currentCol] < 0)
                    {
                        sumOfPaths++;
                        sumOfPaths += Math.Abs(matrix[currentRow][currentCol]);
                        if (sumOfPaths > bestSumOfPath) bestSumOfPath = sumOfPaths;
                        break;
                    }
                    else
                    {
                        visitedMatrix[currentRow][currentCol] = true;
                        currentCol = matrix[currentRow][currentCol];
                        sumOfPaths++;
                    }

                    if (currentRow >= matrix.Length - 1) //check for last row and return to first one
                    {
                        currentRow = -1;
                    }
                }
            }

            //print
            Console.WriteLine(bestSumOfPath);
        }

        private static void CleanVisitedMatrix(ref bool[][] visitedMatrix)
        {
            for (int i = 0; i < visitedMatrix.Length; i++)
            {
                Array.Clear(visitedMatrix[i], 0, visitedMatrix[i].Length);
            }
        }
    }
}
