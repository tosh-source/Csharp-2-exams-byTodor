using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11.Feb._2013_Task5_ThreeInOne
{
    class Program
    {
        static void Main(string[] args)
        {
            //input
            int[][] input = new int[4][];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                input[i] = Console.ReadLine().Split(',', ' ').Select(int.Parse).ToArray();
            }

            //I.First task
            int currentWinner = -1;
            int bestValue = -1; //or test with '0'
            Lazy<int> inGamePlayers = new Lazy<int>();

            for (int currentPlayer = 0; currentPlayer < input[0].Length; currentPlayer++)
            {
                if (input[0][currentPlayer] > 21)
                {
                    continue;
                }
                else
                {
                    if (input[0][currentPlayer] > bestValue)
                    {
                        bestValue = input[0][currentPlayer];
                        currentWinner = currentPlayer;
                    }
                    else if (input[0][currentPlayer] == bestValue)
                    {
                        currentWinner = -1;
                        break;
                    }
                }
            }
            //print - first task result
            Console.WriteLine(currentWinner);

            //II.Second task
            int[] cakesInDescendingOrder = input[1].OrderByDescending(x => x).ToArray();
            int myCakeSum = 0;
            int friends = input[2][0];

            for (int cakes = 0, currentFriend = 0; cakes < input[1].Length; cakes++, currentFriend++)
            {
                if (currentFriend == 0)
                {
                    myCakeSum += cakesInDescendingOrder[cakes];
                }

                if (currentFriend == friends) currentFriend = -1;
            }
            //print - second task result
            Console.WriteLine(myCakeSum);

            //III.Third task
            //IIIa.parse input data
            int[] G1_S1_B1 = new int[3];   //coins holded by character 
            for (int i = 0; i < 3; i++)
            {
                G1_S1_B1[i] = input[3][i];
            }

            int[] G2_S2_B2 = new int[3];   //coins needed for beer
            for (int i = 0; i < 3; i++)
            {
                G2_S2_B2[i] = input[3][3 + i];
            }

            //IIIb.calculate
            for (int i = 0; i < 3; i++) //there is three type of coins
            {
                for (int i = 0; i < length; i++)
                {

                }
            }
        }
    }
}

//почвам от ЗЛАТОТО, после минавам към СРЕБРОТО и накрая проверявам БРОНЗА. Ако нямам достатъчно още за ЗЛАТОТО, не продължавам и връщам -1 (т.е. няма пари)