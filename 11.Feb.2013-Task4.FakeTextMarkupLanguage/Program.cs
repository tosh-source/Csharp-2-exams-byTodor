﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11.Feb._2013_Task4.FakeTextMarkupLanguage
{
    class Program
    {
        private static string[] openedTags = { "<upper>", "<lower>", "<rev>", "<toggle>", "<del>" };
        private static string[] closedTags = { "</upper>", "</lower>", "</rev>", "</toggle>", "</del>" };

        static void Main(string[] args)
        { //condition & BGCoder: http://bgcoder.com/Contests/55/CSharp-Part-2-2012-2013-11-Feb-2013

            //test
            var test = new StringReader(@"3
So<rev><upper>saw</upper> txet em</rev>
<lower><upper>here</upper></lower>");
            Console.SetIn(test);

            //input
            short numberOfLines = short.Parse(Console.ReadLine());
            string[] text = new string[numberOfLines];
            for (int line = 0; line < numberOfLines; line++)
            {
                text[line] = Console.ReadLine().ToString();
            }

            //calculation
            int currentCloseIndex = -1;
            int tempIndex = -1;
            int deepestOpenIndex = -1;
            int deepestCloseIndex = -1;
            int deepestTag = 0;
            StringBuilder currentLine = new StringBuilder();
            StringBuilder subText = new StringBuilder();

            Calculations(ref text, ref currentCloseIndex, ref tempIndex, ref deepestOpenIndex, ref deepestCloseIndex, ref deepestTag, currentLine, subText);

            //print
            Console.WriteLine(string.Join("\n", text));
        }

        private static void Calculations(ref string[] text, ref int currentCloseIndex, ref int tempIndex, ref int deepestOpenIndex, ref int deepestCloseIndex, ref int deepestTag, StringBuilder currentLine, StringBuilder subText)
        {
            for (int line = 0; line < text.Length; line++)
            {
                currentLine.Clear();
                currentLine.Append(text[line]);

                for (int processingLine = 0; processingLine < text[line].Length; processingLine++)
                {
                    bool continueOrNot = false;

                    //find deepest "closed tag"
                    tempIndex = deepestCloseIndex + 1;
                    if (deepestCloseIndex + 1 > text[line].Length - 1) deepestCloseIndex = -1; //check for overflow exception

                    for (byte current = 0; current < closedTags.Length - 1; current++)
                    {
                        currentCloseIndex = text[line].IndexOf(closedTags[current], deepestCloseIndex + 1);

                        if ((tempIndex == deepestCloseIndex + 1 || currentCloseIndex < tempIndex) && currentCloseIndex != -1) //condition: "tempIndex == deepestCloseIndex + 1" , will parse FIRST founded "close tag"
                        {                                                                                                     //condition: "currentCloseIndex < tempIndex"      , will parse any other DEEPER "close tag"
                            tempIndex = currentCloseIndex;
                            deepestTag = current;
                            continueOrNot = true;
                        }
                    }

                    if (continueOrNot == false) break; //stop processing if no more tags and step to the next line

                    deepestCloseIndex = tempIndex;

                    //find deepest "opened tag"
                    deepestOpenIndex = text[line].LastIndexOf(openedTags[deepestTag], deepestCloseIndex);

                    //manipulate text
                    currentLine.Remove(deepestOpenIndex, (deepestCloseIndex - deepestOpenIndex) + closedTags[deepestTag].Length);
                    currentLine.Insert(deepestOpenIndex,
                                       ManipulateTextMethod(subText, ref text, line, deepestTag, deepestOpenIndex, deepestCloseIndex));

                    text[line] = currentLine.ToString();
                }
            }
        }

        private static StringBuilder ManipulateTextMethod(StringBuilder subText, ref string[] text, int line, int deepestTag, int deepestOpenIndex, int deepestCloseIndex)
        {
            subText.Clear();

            switch (deepestTag)
            {
                case 0: //The <upper> tag converts text to its uppercase variant
                    subText.Append(text[line]
                                   .Substring(deepestOpenIndex + "<upper>".Length, deepestCloseIndex - (deepestOpenIndex + "<upper>".Length))
                                   .ToUpper());
                    break;
                case 1: //The <lower> tag converts text to its lowercase variant
                    subText.Append(text[line]
                                   .Substring(deepestOpenIndex + "<lower>".Length, deepestCloseIndex - (deepestOpenIndex + "<lower>".Length))
                                   .ToLower());
                    break;
                case 2: //The <rev> tag reverses all text in it

                    subText.Append(text[line]
                                   .Substring(deepestOpenIndex + "<rev>".Length, deepestCloseIndex - (deepestOpenIndex + "<rev>".Length))
                                   .Reverse().ToArray());
                    break;
                case 3: //"<toggle>" tag rules -> if a character is uppercase, it converts it to lowercase
                        //                        if a character is lowercase, it converts it to uppercase
                    string temp = text[line]
                                  .Substring(deepestOpenIndex + "<toggle>".Length, deepestCloseIndex - (deepestOpenIndex + "<toggle>".Length));

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

            return subText;
        }
    }
}
