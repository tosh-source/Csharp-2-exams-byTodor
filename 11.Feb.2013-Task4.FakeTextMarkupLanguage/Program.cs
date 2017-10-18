using System;
using System.Collections.Generic;
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

            //input
            short numberOfLines = short.Parse(Console.ReadLine());
            string inputData = Console.ReadLine(); //трябва да го направя в масив защото, ще се подават мн редове

            //calculation
            int currentCloseIndex = -1;
            int tempIndex = -1;
            int deepestOpenIndex = -1;
            int deepestCloseIndex = -1;
            int deepestTag = 0;

            string output = string.Empty;

            for (int i = 0; i < inputData.Length; i++)
            {
                //find deepest "closed tag"
                tempIndex = deepestCloseIndex + 1;
                for (byte current = 0; current < closedTags.Length - 1; current++)
                {
                    currentCloseIndex = inputData.IndexOf(closedTags[current], deepestCloseIndex + 1);

                    if ((tempIndex == deepestCloseIndex + 1 || currentCloseIndex < tempIndex) && currentCloseIndex != -1) //condition: "tempIndex == deepestCloseIndex + 1" , will parse FIRST founded "close tag"
                    {                                                                                                     //condition: "currentCloseIndex < tempIndex"      , will parse any other DEEPER "close tag"
                        tempIndex = currentCloseIndex;
                        deepestTag = current;
                    }
                }
                deepestCloseIndex = tempIndex;

                //find deepest "opened tag"
                deepestOpenIndex = inputData.LastIndexOf(openedTags[deepestTag], deepestCloseIndex);

            }
        }
    }
}

