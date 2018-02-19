using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _22.Jan._2014_Task3.Patterns
{
    class Program
    {
        static void Main(string[] args)
        {
            //tests
            //            StringReader testInput = new StringReader(@"7
            //1 2 3 4 5 6 7
            //7 6 5 4 3 2 1
            //1 2 3 4 5 6 7
            //7 6 5 4 3 2 1
            //1 2 3 4 5 6 7
            //7 6 5 4 3 2 1
            //1 2 3 4 5 6 7");
            //            Console.SetIn(testInput);

            //input
            int n = int.Parse(Console.ReadLine());
            int[][] matrix = new int[n][];
            for (int i = 0; i < n; i++)
            {
                matrix[i] = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            }

            //calculation
            long bestPattern = long.MinValue;
            long currentBestPattern = 0;
            bool hasPattern = true;
            byte counter = 0;

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    hasPattern = true;
                    CalculatePattern(matrix, row, col, ref currentBestPattern, ref counter, ref hasPattern);

                    if (currentBestPattern > bestPattern && hasPattern == true)
                    {
                        bestPattern = currentBestPattern;
                    }
                    currentBestPattern = 0;
                    counter = 0;
                }
            }

            //print
            if (bestPattern != long.MinValue)
            {
                Console.WriteLine("YES {0}", bestPattern);
            }
            else
            {
                Console.WriteLine("NO " + PrintDiagonalOfMatrix(matrix));
            }
        }

        private static string PrintDiagonalOfMatrix(int[][] matrix)
        {
            int result = 0;
            for (int row = matrix.Length - 1; row >= 0; row--)  //row in reverse order
            {
                result += matrix[row][matrix.Length - 1 - row];
            }

            string resultToPrint = result.ToString();
            return resultToPrint;
        }

        private static void CalculatePattern(int[][] matrix, int row, int col, ref long currentBestPattern, ref byte counter, ref bool hasPattern)
        {
            if (hasPattern == false || row > matrix.Length - 1 || col > matrix.Length - 1)
            {
                hasPattern = false;
                return;
            }

            currentBestPattern += matrix[row][col];

            switch (counter)
            {
                case 0:
                    counter++;
                    CalculatePattern(matrix, row, col + 1, ref currentBestPattern, ref counter, ref hasPattern);
                    break;
                case 1:
                    if ((matrix[row][col] - 1) == matrix[row][col - 1])
                    {
                        counter++;
                        CalculatePattern(matrix, row, col + 1, ref currentBestPattern, ref counter, ref hasPattern);
                    }
                    else
                    {
                        hasPattern = false;
                        return;
                    }
                    break;
                case 2:
                    if ((matrix[row][col] - 1) == matrix[row][col - 1])
                    {
                        counter++;
                        CalculatePattern(matrix, row + 1, col, ref currentBestPattern, ref counter, ref hasPattern);
                    }
                    else
                    {
                        hasPattern = false;
                        return;
                    }
                    break;
                case 3:
                    if ((matrix[row][col] - 1) == matrix[row - 1][col])
                    {
                        counter++;
                        CalculatePattern(matrix, row + 1, col, ref currentBestPattern, ref counter, ref hasPattern);
                    }
                    else
                    {
                        hasPattern = false;
                        return;
                    }
                    break;
                case 4:
                    if ((matrix[row][col] - 1) == matrix[row - 1][col])
                    {
                        counter++;
                        CalculatePattern(matrix, row, col + 1, ref currentBestPattern, ref counter, ref hasPattern);
                    }
                    else
                    {
                        hasPattern = false;
                        return;
                    }
                    break;
                case 5:
                    if ((matrix[row][col] - 1) == matrix[row][col - 1])
                    {
                        counter++;
                        CalculatePattern(matrix, row, col + 1, ref currentBestPattern, ref counter, ref hasPattern);
                    }
                    else
                    {
                        hasPattern = false;
                        return;
                    }
                    break;
                case 6:
                    if ((matrix[row][col] - 1) == matrix[row][col - 1])
                    {
                        return;
                    }
                    else
                    {
                        hasPattern = false;
                        return;
                    }
            }
        }
    }
}

//да тествам ако матрицата е ОТРИЦАТЕЛНА, дали няма "Runtime error"