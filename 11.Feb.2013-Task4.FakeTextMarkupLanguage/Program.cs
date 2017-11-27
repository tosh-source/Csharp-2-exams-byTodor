using System;
using System.Linq;
using System.Text;

namespace _11.Feb._2013_Task4.FakeTextMarkupLanguage
{
    class Program
    {
        private static string[] openedTags = { "<upper>", "<lower>", "<rev>", "<toggle>", "<del>" };
        private static string[] closedTags = { "</upper>", "</lower>", "</rev>", "</toggle>", "</del>" };

        static void Main(string[] args)
        { //condition & BGCoder: http://bgcoder.com/Contests/55/CSharp-Part-2-2012-2013-11-Feb-2013

            //input
            int numberOfLines = int.Parse(Console.ReadLine());
            StringBuilder textAsSB = new StringBuilder();

            for (int line = 0; line < numberOfLines; line++)
            {
                textAsSB.Append(Console.ReadLine() + "\n");  //"Environment.NewLine" is better choice, "\n" is only for BGCoder
            }

            //calculation
            int currentCloseIndex = -1;
            int deepestOpenIndex = -1;
            int deepestCloseIndex = 0;
            int deepestTag = 0;

            Calculations(textAsSB, ref currentCloseIndex, ref deepestCloseIndex, ref deepestOpenIndex, ref deepestTag);

            //print
            Console.WriteLine(textAsSB);
        }

        private static void Calculations(StringBuilder textAsSB, ref int currentCloseIndex, ref int deepestCloseIndex, ref int deepestOpenIndex, ref int deepestTag)
        {
            StringBuilder subText = new StringBuilder();
            StringBuilder textToRemove = new StringBuilder();

            for (int processingSymbols = 0; processingSymbols < textAsSB.Length; processingSymbols++)
            {
                bool continueOrNot = false;

                //find deepest "closed tag"
                deepestCloseIndex = 0;

                for (byte current = 0; current < closedTags.Length; current++)
                {
                    //currentCloseIndex = text.IndexOf(closedTags[current], 0);
                    currentCloseIndex = SBIndexOf(textAsSB, closedTags[current]);

                    if ((deepestCloseIndex == 0 || currentCloseIndex <= deepestCloseIndex) && currentCloseIndex != -1)    //condition: "deepestCloseIndex == 0"                 , will parse FIRST founded "close tag"
                    {                                                                                                     //condition: "currentCloseIndex <= deepestCloseIndex" , will parse any other DEEPER "close tag"
                        deepestCloseIndex = currentCloseIndex;
                        deepestTag = current;
                        continueOrNot = true;
                    }
                }

                if (continueOrNot == false) break; //stop processing if no more tags

                //find deepest "opened tag"
                deepestOpenIndex = SBLastIndexOf(textAsSB, openedTags[deepestTag], deepestCloseIndex);

                //manipulate text
                int length = (deepestCloseIndex - deepestOpenIndex) + closedTags[deepestTag].Length;
                textToRemove.Clear();
                textToRemove.Append(textAsSB.ToString(deepestOpenIndex, length));

                ManipulateTextMethod(textAsSB, subText, deepestTag, deepestOpenIndex, deepestCloseIndex);

                textAsSB.Replace(textToRemove.ToString(), subText.ToString(), deepestOpenIndex, length);
            }
        }

        private static int SBIndexOf(StringBuilder text, string valueToSearch)
        {
            char firstChar = valueToSearch[0];  //take the first element
            int indexToReturn = -1;
            bool continueOrNot = true;
            int currentCheckedIndex = 0;

            //default value of startIndex
            int startIndex = 0;

            //Calculations
            //1.Start searching
            for (int textIndex = startIndex; textIndex < text.Length; textIndex++)
            {
                if (text[textIndex] == firstChar)  //if there is matching
                {
                    //2.Compare(char by char) the value with the text from current position.
                    for (int valueIndex = 0; valueIndex < valueToSearch.Length; valueIndex++)
                    {
                        if (valueIndex == 0) indexToReturn = textIndex + valueIndex;
                        currentCheckedIndex = textIndex + valueIndex;

                        if (currentCheckedIndex > text.Length - 1)  //check for "OverFlow Exception" and stop, because no chance for possible matches
                        {
                            indexToReturn = -1;
                            continueOrNot = false;
                            break;
                        }
                        else if (text[currentCheckedIndex] != valueToSearch[valueIndex])  //if all chars of "valueToSearch" not matched, break and return index -1
                        {
                            indexToReturn = -1;
                            break;
                        }
                    }
                }

                if (currentCheckedIndex > textIndex) textIndex = currentCheckedIndex;

                if (continueOrNot == false || indexToReturn != -1) break;
            }

            return indexToReturn;
        }

        private static int SBLastIndexOf(StringBuilder text, string valueToSearch, int? startIndex = null)
        {
            char firstChar = valueToSearch[valueToSearch.Length - 1]; //take the (last)first element
            int indexToReturn = -1;
            bool continueOrNot = true;
            int currentCheckedIndex = text.Length - 1;

            //check default value of startIndex
            if (startIndex == null) startIndex = text.Length - 1;

            //Calculations
            //1.Start searching
            for (int textIndex = (int)startIndex; textIndex >= 0; textIndex--)
            {
                if (text[textIndex] == firstChar)  //if there is matching
                {
                    //2.Compare(char by char) the value with the text from current position.
                    for (int valueIndex = valueToSearch.Length - 1; valueIndex >= 0; valueIndex--)
                    {
                        if (valueIndex == valueToSearch.Length - 1)
                        {
                            indexToReturn = textIndex - valueIndex;
                        }
                        currentCheckedIndex = textIndex - (valueToSearch.Length - 1 - valueIndex);

                        if ((textIndex - valueIndex) < 0)  //check for "OverFlow Exception" and stop, because no chance for possible matches
                        {
                            indexToReturn = -1;
                            continueOrNot = false;
                            break;
                        }
                        else if (text[currentCheckedIndex] != valueToSearch[valueIndex])  //if all chars of "valueToSearch" not matched, break and return index -1
                        {
                            indexToReturn = -1;
                            break;
                        }
                    }
                }

                if (currentCheckedIndex < textIndex) textIndex = currentCheckedIndex;

                if (continueOrNot == false || indexToReturn != -1) break;
            }

            return indexToReturn;
        }

        private static void ManipulateTextMethod(StringBuilder textAsSB, StringBuilder subText, int deepestTag, int deepestOpenIndex, int deepestCloseIndex)
        {
            subText.Clear();

            switch (deepestTag)  //NOTE: "ToString()" with parameters (in StringBuilder) is like a "Substring()" (in string).
            {
                case 0: //The <upper> tag converts text to its uppercase variant        
                    subText.Append(textAsSB.ToString(deepestOpenIndex + openedTags[deepestTag].Length,     //openedTags[deepestTag].Length => "upper".Length
                                                     deepestCloseIndex - (deepestOpenIndex + openedTags[deepestTag].Length))
                                                    .ToUpper());
                    break;
                case 1: //The <lower> tag converts text to its lowercase variant        
                    subText.Append(textAsSB.ToString(deepestOpenIndex + openedTags[deepestTag].Length,      //openedTags[deepestTag].Length => "lower".Length
                                                     deepestCloseIndex - (deepestOpenIndex + openedTags[deepestTag].Length))
                                                    .ToLower());
                    break;
                case 2: //The <rev> tag reverses all text in it                         
                    subText.Append(textAsSB.ToString(deepestOpenIndex + openedTags[deepestTag].Length,     //openedTags[deepestTag].Length => "rev".Length
                                                     deepestCloseIndex - (deepestOpenIndex + openedTags[deepestTag].Length))
                                                    .Reverse().ToArray());
                    break;
                case 3: //"<toggle>" tag rules -> if a character is uppercase, it converts it to lowercase
                        //                        if a character is lowercase, it converts it to uppercase
                    string temp = textAsSB.ToString(deepestOpenIndex + openedTags[deepestTag].Length,      //openedTags[deepestTag].Length => "toggle".Length
                                                    deepestCloseIndex - (deepestOpenIndex + openedTags[deepestTag].Length));

                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (char.IsUpper(temp[i]))
                        {
                            subText.Append(char.ToLower(temp[i]));
                        }
                        else if (char.IsLower(temp[i]))
                        {
                            subText.Append(char.ToUpper(temp[i]));
                        }
                        else
                        {
                            subText.Append(temp[i]);
                        }
                    }
                    break;
                case 4: //The <del> tag deletes all text in it
                    subText.Append(string.Empty);
                    break;
            }
        }
    }
}
