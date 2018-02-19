using System;
using System.Linq;

namespace _11.Feb._2013_Task5_ThreeInOne
{
    class Program
    {
        static void Main(string[] args)
        {  //condition & BGCoder: http://bgcoder.com/Contests/55/CSharp-Part-2-2012-2013-11-Feb-2013

            //input
            int[][] input = new int[4][];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                input[i] = Console.ReadLine().Split(',', ' ').Select(int.Parse).ToArray();
            }

            //I.First task
            int currentWinner = -1;
            int bestValue = -1; //or test with '0'

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
            //print first task
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
            //print second task
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
            int exchangeOperations = 0;

            for (int currentUserCoin = 0; currentUserCoin < 3; currentUserCoin++) //there is three type coins
            {
                bool hasValue = true;

                while (hasValue == true)
                {
                    if (G1_S1_B1[currentUserCoin] >= G2_S2_B2[currentUserCoin])
                    {
                        G1_S1_B1[currentUserCoin] -= G2_S2_B2[currentUserCoin];
                        G2_S2_B2[currentUserCoin] = 0;
                        break;
                    }
                    else if (G1_S1_B1[currentUserCoin] < G2_S2_B2[currentUserCoin])
                    {
                        G2_S2_B2[currentUserCoin] -= G1_S1_B1[currentUserCoin];
                        G1_S1_B1[currentUserCoin] = 0;

                        exchangeCoins(G1_S1_B1, G2_S2_B2, currentUserCoin, ref exchangeOperations, ref hasValue);
                    }
                }

                if (hasValue == false)
                {
                    exchangeOperations = -1;
                }
            }

            //print third task
            Console.WriteLine(exchangeOperations);
        }

        private static void exchangeCoins(int[] G1_S1_B1, int[] G2_S2_B2, int currentUserCoin, ref int exchangeOperations, ref bool hasValue)
        {
            while (hasValue == true && (G1_S1_B1[currentUserCoin] < G2_S2_B2[currentUserCoin]))
            {
                switch (currentUserCoin) //G1_S1_B1[0] - Gold  //G1_S1_B1[1] - Silver  //G1_S1_B1[2] - Bronze
                {
                    case 0:  //Gold
                        ExchangeGoldCoins(G1_S1_B1, ref exchangeOperations, ref hasValue);
                        break;
                    case 1:  //Silver
                        ExchangeSilverCoins(G1_S1_B1, ref exchangeOperations, ref hasValue);
                        break;
                    case 2:  //Bronze
                        ExchangeBronzeCoins(G1_S1_B1, ref exchangeOperations, ref hasValue);
                        break;
                }
            }
        }

        private static void ExchangeGoldCoins(int[] G1_S1_B1, ref int exchangeOperations, ref bool hasValue)
        {
            //the bank will give you 1 gold coin in exchange for 11 silver coins
            if (G1_S1_B1[1] >= 11) //if silver has 11 or more coins, operate..
            {
                G1_S1_B1[1] -= 11;
                G1_S1_B1[0] += 1;
                exchangeOperations++;
            }
            else if (G1_S1_B1[2] >= 11) //convert value FROM bronze TO silver: "the bank will give you 1 silver coin in exchange for 11 bronze coins"
            {
                G1_S1_B1[2] -= 11;
                G1_S1_B1[1] += 1;
                exchangeOperations++;
            }
            else
            {
                hasValue = false;
            }
        }

        private static void ExchangeSilverCoins(int[] G1_S1_B1, ref int exchangeOperations, ref bool hasValue)
        {
            //the bank will give you 9 silver coins in exchange for 1 gold coin
            if (G1_S1_B1[0] >= 1)
            {
                G1_S1_B1[0] -= 1;
                G1_S1_B1[1] += 9;
                exchangeOperations++;
            }
            else if (G1_S1_B1[2] >= 11) //the bank will give you 1 silver coin in exchange for 11 bronze coins
            {
                G1_S1_B1[2] -= 11;
                G1_S1_B1[1] += 1;
                exchangeOperations++;
            }
            else
            {
                hasValue = false;
            }
        }

        private static void ExchangeBronzeCoins(int[] G1_S1_B1, ref int exchangeOperations, ref bool hasValue)
        {
            //the bank will give you 9 bronze coins in exchange for 1 silver coin
            if (G1_S1_B1[1] >= 1)
            {
                G1_S1_B1[1] -= 1;
                G1_S1_B1[2] += 9;
                exchangeOperations++;
            }
            else if (G1_S1_B1[0] >= 1) //convert value FROM gold TO silver: "the bank will give you 9 silver coins in exchange for 1 gold coin"
            {
                G1_S1_B1[0] -= 1;
                G1_S1_B1[1] += 9;
                exchangeOperations++;
            }
            else
            {
                hasValue = false;
            }
        }
    }
}
