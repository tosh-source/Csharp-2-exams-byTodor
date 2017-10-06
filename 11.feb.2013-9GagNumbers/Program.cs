using System;
using System.Numerics;

namespace _11.feb._2013_9GagNumbers
{
    class Program
    {
        public static readonly string[] arrayOf9GagDigits = { "-!", "**", "!!!", "&&", "&-", "!-", "*!!!", "&*!", "!!**!-" };
        //ако направя версия за Java да използвам това вместо константа -> Collections.unmodifiableList(Arrays.asList(yourOldArray)) 

        static void Main(string[] args)
        {
            //input
            string input = Console.ReadLine();
            //parse digits
            string valueHolder = string.Empty;
            int index = -1;
            int temp = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int current9GagDigit = 0; current9GagDigit < 9; current9GagDigit++)
                {
                    if ((index + arrayOf9GagDigits[current9GagDigit].Length) >= input.Length) continue; //check for longer 9gag digit length (on current position) than "input.Length"

                    temp = input.IndexOf(arrayOf9GagDigits[current9GagDigit], (index + 1), arrayOf9GagDigits[current9GagDigit].Length);
                    if (temp > -1)
                    {
                        index = temp + (arrayOf9GagDigits[current9GagDigit].Length - 1);
                        valueHolder = current9GagDigit + valueHolder;
                        break;
                    }
                }

                if (index >= input.Length - 1) break;
            }
            //convert
            BigInteger result = 0;

            for (int i = valueHolder.Length - 1; i >= 0; i--)
            {
                result += int.Parse(valueHolder[i].ToString()) * BigInteger.Pow(9, i);
            }
            //print
            Console.WriteLine(result);
        }
    }
}
