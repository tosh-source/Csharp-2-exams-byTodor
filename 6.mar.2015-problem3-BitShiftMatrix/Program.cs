using System;
using System.Linq;
using System.Numerics;

namespace _6.mar._2015_problem3_BitShiftMatrix
{
    class Program
    {
        static void Main(string[] args)
        { //condition: https://github.com/TelerikAcademy/CSharp-Part-2/blob/master/Workshops/2016%20Nov%2023%20-%20Arrays%2C%20Methods%20and%20Objects/2.%20Bit%20Shift%20Matrix/README.md
          //BgCoder: http://bgcoder.com/Contests/223/CSharp-Part-2-2015-2016-6-March-2015-Evening
            
            //input and values
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            int pawnMoves = int.Parse(Console.ReadLine());
            int[] codes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();  //евентуален проблем при вх данни -> decimal
            bool[,] matrix = new bool[rows, cols];
            BigInteger sum = 0;
            //calculation
            PawnMovesAndCalcs(rows, cols, pawnMoves, codes, ref matrix, ref sum);
            //print
            Console.WriteLine(sum);
        }

        static void PawnMovesAndCalcs(int rows, int cols, int pawnMoves, int[] codes, ref bool[,] matrix, ref BigInteger sum)
        {
            int rowPosition = matrix.GetLength(0) - 1;
            int colPosition = 0;
            int row = matrix.GetLength(0) - 1;
            int col = 0;
            int maxCoef = Math.Max(rows, cols);

            for (int move = 1; move <= pawnMoves; move++)
            {
                rowPosition = codes[move - 1] / maxCoef;  //codes[move - 1] => current Code
                colPosition = codes[move - 1] % maxCoef;

                while (col != colPosition)
                {
                    if (col <= colPosition)
                    {
                        sum = AddSumFromCell(matrix, row, col, ((matrix.GetLength(0) - 1 - row) + col), sum);
                        matrix[row, col] = true;
                        col++;
                    }
                    else if (col >= colPosition)
                    {
                        sum = AddSumFromCell(matrix, row, col, ((matrix.GetLength(0) - 1 - row) + col), sum);
                        matrix[row, col] = true;
                        col--;
                    }
                }
                while (row != rowPosition)
                {
                    if (row <= rowPosition)
                    {
                        sum = AddSumFromCell(matrix, row, col, ((matrix.GetLength(0) - 1 - row) + col), sum);
                        matrix[row, col] = true;
                        row++;
                    }
                    else if (row >= rowPosition)
                    {
                        sum = AddSumFromCell(matrix, row, col, ((matrix.GetLength(0) - 1 - row) + col), sum);
                        matrix[row, col] = true;
                        row--;
                    }
                }
                sum = AddSumFromCell(matrix, row, col, ((matrix.GetLength(0) - 1 - row) + col), sum);
                matrix[row, col] = true;
            }
        }

        private static BigInteger AddSumFromCell(bool[,] matrix, int row, int col, int leftShift, BigInteger sum)
        { //calculate and add sum from current matrix cell
            if (matrix[row, col] == false)
            {
                sum += (BigInteger)1 << leftShift;
            }
            return sum;
        }
    }
}
