using System;
using System.Text;
using System.Text.RegularExpressions;

namespace _7._12._2016_Task4_GoshoCode
{
    class Program
    {
        static decimal result = 0;

        static void Main(string[] args)
        {   //condition: https://github.com/TelerikAcademy/CSharp-Part-2/tree/master/Exams/2016-12-07/2016-12-07-Evening/4.%20GoshoCode
            //BGCoder: http://bgcoder.com/Contests/392/Telerik-Academy-Exam-CSharp-Advanced-7-December-2016-Evening

            //input
            string keyWord = Console.ReadLine();
            int N = int.Parse(Console.ReadLine());
            string[] text = new string[N];
            for (int i = 0; i < N; i++)
            {
                text[i] = Console.ReadLine();
            }
            //input manipulations
            StringBuilder textAsOne = new StringBuilder();
            foreach (var lineText in text)
            {
                textAsOne.Append(lineText + " ");
            }
            //split text to sentences
            string[] sentences = Regex.Split(textAsOne.ToString().TrimEnd(), @"(?<=[.!])");

            //calculation
            bool isHaveKeyWord = false;
            char? dotOrExclamation = null;
            int index = -1;
            string sentence;

            foreach (var currentSentence in sentences)
            {
                sentence = currentSentence;
                if (sentence == string.Empty) continue;

                CheckCurrentSentenceForMathes(keyWord, ref sentence, ref isHaveKeyWord, ref index, ref dotOrExclamation);

                //calculation the ASCII value (sum of words)
                if (dotOrExclamation == '.' && isHaveKeyWord == true)
                {
                    ASCIIvalueCalculator(keyWord, isHaveKeyWord, sentence, (index + keyWord.Length), sentence.Length - 1, dotOrExclamation);
                }
                else if (dotOrExclamation == '!' && isHaveKeyWord == true)
                {
                    ASCIIvalueCalculator(keyWord, isHaveKeyWord, sentence, 0, index - 1, dotOrExclamation);
                }

                //print
                if (isHaveKeyWord == true) Console.WriteLine(result);

                //end the calculation if all above is done
                if (isHaveKeyWord == true) break;
            }

        }

        private static void ASCIIvalueCalculator(string keyWord, bool isHaveKeyWord, string sentence, int startIndex, int endIndex, char? dotOrExclamation)
        {
            int tempSum = 0;

            for (int i = startIndex; i <= endIndex; i++)
            {
                tempSum += ((int)sentence[i]) * keyWord.Length;
            }

            result = tempSum;
        }

        private static void CheckCurrentSentenceForMathes(string keyWord, ref string sentence, ref bool isHaveKeyWord, ref int index, ref char? dotOrExclamation)
        {
            sentence = sentence.Replace(" ", string.Empty);  //remove all white-spaces

            if ((index = sentence.IndexOf(keyWord)) != -1)  //The provided keyword will never be contained inside another word.
            {                                               //The provided keyword will be UNIQUE to the text.
                isHaveKeyWord = true;

                if (sentence.LastIndexOf('.') != -1)
                {
                    dotOrExclamation = '.';
                    sentence = sentence.Replace(".", string.Empty); //do not include the dot in ASCII sum calculation
                }
                else if (sentence.LastIndexOf('!') != -1)
                {
                    dotOrExclamation = '!';
                }
            }
        }
    }
}
