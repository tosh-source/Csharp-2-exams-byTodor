using System;
using System.Numerics;
using System.Text;

namespace _31.may._2016_1.MessagesDescription
{
    class Program
    {
        static string[] numSystem = { "cad", "xoz", "nop", "cyk", "min", "mar", "kon", "iva", "ogi", "yan" };
        static BigInteger firstDecryptedToDeci = 0;
        static BigInteger secondDecryptedToDeci = 0;
        static BigInteger resultAsDeci = 0;
        static StringBuilder output = new StringBuilder();

        static void Main(string[] args)
        { //condition: https://github.com/TelerikAcademy/CSharp-Part-2/tree/master/Workshops/2016%20Nov%2023%20-%20Arrays%2C%20Methods%20and%20Objects/1.%20Messages
          //BgCoder: http://bgcoder.com/Contests/354/CSharp-Advanced-2016-2017-31-May-2016-Evening

            //input
            string firstInput = Console.ReadLine();
            char sign = char.Parse(Console.ReadLine());
            string secondInput = Console.ReadLine();
            //calculation
            firstDecryptedToDeci = ConvertToDecimal(firstInput);
            secondDecryptedToDeci = ConvertToDecimal(secondInput);
            resultAsDeci = Calculator(firstDecryptedToDeci, sign, secondDecryptedToDeci);
            output = DeciToEncryptedNumSystem(resultAsDeci, output);
            //print
            Console.WriteLine(output);
        }

        private static BigInteger ConvertToDecimal(string encryptedInput)
        {
            BigInteger tempResult = 0;
            StringBuilder currentEncryptNumber = new StringBuilder();
            int currentIndex = -1;
            for (int i = 0; i < encryptedInput.Length; i += 3)
            {
                currentEncryptNumber.Append(encryptedInput.Substring(i, 3));
                currentIndex = Array.IndexOf(numSystem, currentEncryptNumber.ToString());
                if (currentIndex > -1)
                {
                    tempResult = (tempResult * 10) + currentIndex;
                }
                currentEncryptNumber.Clear();
            }
            return tempResult;
        }

        private static BigInteger Calculator(BigInteger firstDecryptedToDeci, char sign, BigInteger secondDecryptedToDeci)
        {
            if (sign == '-')
            {
                secondDecryptedToDeci = ~secondDecryptedToDeci + 1; //make same value negative
            }
            return firstDecryptedToDeci + (secondDecryptedToDeci);
        }

        private static StringBuilder DeciToEncryptedNumSystem(BigInteger result, StringBuilder output)
        {
            string resultAsString = result.ToString();
            int index = 0;
            for (int i = 0; i < resultAsString.Length; i++)
            {
                index = int.Parse(resultAsString[i].ToString());
                output.Append(numSystem[index]);
            }
            return output;
        }
    }
}
