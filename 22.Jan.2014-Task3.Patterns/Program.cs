using System;
using System.Linq;

namespace _22.Jan._2014_Task3.Patterns
{
    class Program
    {
        static void Main(string[] args)
        {   //Condition & BGCoder: http://bgcoder.com/Contests/142/CSharp-Part-2-2013-2014-22-Jan-2014-Evening
            //video: https://www.youtube.com/watch?v=2UpXc0QM82E
            //video: https://www.youtube.com/watch?v=FzF5doGuSRM

            //input
            int n = int.Parse(Console.ReadLine());
            int[][] matrix = new int[n][];
            for (int i = 0; i < n; i++)
            {
                matrix[i] = Console.ReadLine().Trim().Split(' ').Select(int.Parse).ToArray();   //".Trim()" method is needed if use "BGCoder" (in last few test there is more "spaces" than usual)
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

        private static long PrintDiagonalOfMatrix(int[][] matrix)
        {
            long result = 0;
            for (int row = 0; row < matrix.Length; row++)
            {
                result += matrix[row][row];
            }

            return result;
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
