using System;
using System.Numerics;
using System.Text;

namespace _7_dec_2016_SecretNumeralSystem
{
    class Program
    {
        static string[] secretNum = { "hristo", "tosho", "pesho", "hristofor", "vlad", "haralampi", "zoro", "vladimir" };
        static StringBuilder value = new StringBuilder();
        static BigInteger multipliedValueToDecimal = 1;

        static void Main(string[] args)
        {  //condition: https://github.com/TelerikAcademy/CSharp-Part-2/tree/master/Exams/2016-12-07/2016-12-07-Evening/1.%20Secret%20Numeral%20System
           //BGCoder: http://bgcoder.com/Contests/392/Telerik-Academy-Exam-CSharp-Advanced-7-December-2016-Evening

            //input 
            string[] input = Console.ReadLine().Split(new string[] { ", " }, StringSplitOptions.None);
            //calculation
            Decrypt(input);
            //print
            Console.WriteLine(multipliedValueToDecimal);
        }

        private static void Decrypt(string[] input)
        {
            int index = -1;
            int? tempValue = null;

            for (int i = 0; i < input.Length; i++)
            {
                for (int currentSubNum = 1; currentSubNum <= input[i].Length; currentSubNum++)
                {
                    for (int currentSecretNum = 0; currentSecretNum < secretNum.Length; currentSecretNum++)
                    {
                        if (((index + currentSubNum) + secretNum[currentSecretNum].Length) <= input[i].Length)  //check to prevent "OutOfRangeException"
                        {
                            index = input[i].IndexOf(secretNum[currentSecretNum], index + currentSubNum, secretNum[currentSecretNum].Length);  //IndexOf(value = secretNum[currentSecretNum], startIndex = index+1, count = secretNum[currentSecretNum].Length)
                            if (index > -1)
                            {
                                tempValue = currentSecretNum;
                                index = -1;
                            }
                        }
                    }

                    if (tempValue != null) value.Append(tempValue);
                    tempValue = null;
                }
                SecretNumeralSystemToDecimal();
                value.Clear();
            }
        }

        static void SecretNumeralSystemToDecimal()
        {
            int digit = 0;
            int position = 0;
            BigInteger tempSum = 0;
            for (int i = 0; i < value.Length; i++)
            {
                digit = int.Parse(value[i].ToString());
                position = value.Length - i - 1;
                tempSum += (BigInteger)digit * BigInteger.Pow(8, position);
            }
            multipliedValueToDecimal *= tempSum;
        }
    }
}
