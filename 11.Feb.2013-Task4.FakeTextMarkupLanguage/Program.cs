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
            StringBuilder[] textAsSB = new StringBuilder[numberOfLines];

            for (int line = 0; line < numberOfLines; line++)
            {
                textAsSB[line] = new StringBuilder();
                textAsSB[line].Append(Console.ReadLine() + "\n");  //"Environment.NewLine" is better choice, "\n" is only to test in "BGCoder"
            }

            //calculation
            StringBuilder result = new StringBuilder();
            Calculations(textAsSB, result);

            //print
            Console.WriteLine(result);
        }

        private static StringBuilder Calculations(StringBuilder[] textAsSB, StringBuilder result)
        {
            int currentOpenIndex = -1;
            int deepestOpenIndex = -1;
            int deepestCloseIndex = 0;
            int deepestTag = 0;
            bool switchToMultiLine = false;
            StringBuilder subText = new StringBuilder();
            StringBuilder textToRemove = new StringBuilder();
            StringBuilder singleOrMultiLineText = new StringBuilder();

            //calculations
            for (int line = 0; line < textAsSB.Length; line++)
            {
                if (switchToMultiLine == false) singleOrMultiLineText.Clear();
                singleOrMultiLineText.Append(textAsSB[line]);

                for (int processingLine = 0; processingLine < singleOrMultiLineText.Length; processingLine++)
                {
                    bool continueOrNot = false;

                    //find deepest "closed tag"
                    deepestOpenIndex = -1;

                    for (byte current = 0; current < closedTags.Length; current++)
                    {
                        currentOpenIndex = SBinnerIndexOf(singleOrMultiLineText, openedTags[current]);

                        if ((deepestOpenIndex == -1 || currentOpenIndex >= deepestOpenIndex) && currentOpenIndex != -1)  //condition: "deepestOpenIndex == -1"               , will parse FIRST founded "open tag"
                        {                                                                                                //condition: "currentOpenIndex >= deepestOpenIndex" , will parse any other DEEPER "open tag"
                            deepestOpenIndex = currentOpenIndex;
                            deepestTag = current;
                            continueOrNot = true;
                        }
                    }

                    if (continueOrNot == false) break; //stop processing if no more tags and step to the next line

                    //find deepest "close tag", if NO, switch to multi-line mode
                    int length = (deepestOpenIndex + openedTags[deepestTag].Length - 1);
                    deepestCloseIndex = SBIndexOf(singleOrMultiLineText, closedTags[deepestTag], length);

                    if (deepestCloseIndex == -1)
                    {
                        switchToMultiLine = true;
                        break;
                    }
                    else
                    {
                        switchToMultiLine = false;
                    }

                    //manipulate text
                    length = (deepestCloseIndex - deepestOpenIndex) + closedTags[deepestTag].Length;
                    textToRemove.Clear();
                    textToRemove.Append(singleOrMultiLineText.ToString(deepestOpenIndex, length));

                    ManipulateText(singleOrMultiLineText, subText, deepestTag, deepestOpenIndex, deepestCloseIndex);

                    singleOrMultiLineText.Replace(textToRemove.ToString(), subText.ToString(), deepestOpenIndex, length);
                }

                if (switchToMultiLine == false) result.Append(singleOrMultiLineText);
            }

            return result;
        }

        private static int SBinnerIndexOf(StringBuilder text, string valueToSearch, int? startIndex = null)
        {
            char firstChar = valueToSearch[0];  //take the first element
            int bestIndex = -1;
            int currentBestIndex = -1;
            bool continueOrNot = true;
            int currentCheckedIndex = 0;

            //default value of startIndex
            if (startIndex == null) startIndex = 0;

            //Calculations
            //1.Start searching
            for (int textIndex = (int)startIndex; textIndex < text.Length; textIndex++)
            {
                if (text[textIndex] == firstChar)  //if there is matching
                {
                    //2.Compare(char by char) the value with the text from current position.
                    for (int valueIndex = 0; valueIndex < valueToSearch.Length; valueIndex++)
                    {
                        if (valueIndex == 0) currentBestIndex = textIndex + valueIndex;
                        currentCheckedIndex = textIndex + valueIndex;

                        if (currentCheckedIndex > text.Length - 1)  //check for "OverFlow Exception" and stop, because no chance for possible matches
                        {
                            currentBestIndex = -1;
                            continueOrNot = false;
                            break;
                        }
                        else if (text[currentCheckedIndex] != valueToSearch[valueIndex])  //if all chars of "valueToSearch" not matched, break and return index -1
                        {
                            currentBestIndex = -1;
                            break;
                        }
                    }
                }

                if (currentBestIndex > bestIndex) bestIndex = currentBestIndex;

                if (currentCheckedIndex > textIndex) textIndex = currentCheckedIndex;

                if (continueOrNot == false) break;
            }

            return bestIndex;
        }

        private static int SBIndexOf(StringBuilder text, string valueToSearch, int? startIndex = null)
        {
            char firstChar = valueToSearch[0];  //take the first element
            int indexToReturn = -1;
            bool continueOrNot = true;
            int currentCheckedIndex = 0;

            //default value of startIndex
            if (startIndex == null) startIndex = 0;

            //Calculations
            //1.Start searching
            for (int textIndex = (int)startIndex; textIndex < text.Length; textIndex++)
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

        private static void ManipulateText(StringBuilder singleOrMultiLineText, StringBuilder subText, int deepestTag, int deepestOpenIndex, int deepestCloseIndex)
        {
            subText.Clear();

            switch (deepestTag)  //NOTE: "ToString()" with parameters (in StringBuilder) is like a "Substring()" (in string).
            {
                case 0: //The <upper> tag converts text to its uppercase variant        
                    subText.Append(singleOrMultiLineText.ToString(deepestOpenIndex + openedTags[deepestTag].Length,     //openedTags[deepestTag].Length => "upper".Length
                                                                  deepestCloseIndex - (deepestOpenIndex + openedTags[deepestTag].Length))
                                                                 .ToUpper());
                    break;
                case 1: //The <lower> tag converts text to its lowercase variant        
                    subText.Append(singleOrMultiLineText.ToString(deepestOpenIndex + openedTags[deepestTag].Length,      //openedTags[deepestTag].Length => "lower".Length
                                                                  deepestCloseIndex - (deepestOpenIndex + openedTags[deepestTag].Length))
                                                                 .ToLower());
                    break;
                case 2: //The <rev> tag reverses all text in it                         
                    subText.Append(singleOrMultiLineText.ToString(deepestOpenIndex + openedTags[deepestTag].Length,     //openedTags[deepestTag].Length => "rev".Length
                                                                  deepestCloseIndex - (deepestOpenIndex + openedTags[deepestTag].Length))
                                                                 .Reverse().ToArray());
                    break;
                case 3: //"<toggle>" tag rules -> if a character is uppercase, it converts it to lowercase
                        //                        if a character is lowercase, it converts it to uppercase
                    string temp = singleOrMultiLineText.ToString(deepestOpenIndex + openedTags[deepestTag].Length,      //openedTags[deepestTag].Length => "toggle".Length
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
