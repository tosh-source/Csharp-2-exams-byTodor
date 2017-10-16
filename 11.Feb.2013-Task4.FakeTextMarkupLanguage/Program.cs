using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11.Feb._2013_Task4.FakeTextMarkupLanguage
{
    class Program
    {
        private const string upperTagOpen = "<upper>";
        private const string upperTagClose = "</upper>";
        private const string lowerTagOpen = "<lower>";
        private const string lowerTagClose = "</lower>";
        private const string revTagOpen = "<rev>";
        private const string revTagClose = "</rev>";
        private const string toggleTagOpen = "<toggle>";
        private const string toggleTagClose = "</toggle>";
        private const string delTagOpen = "<del>";
        private const string delTagClose = "</del>";

        static void Main(string[] args)
        { //condition & BGCoder: http://bgcoder.com/Contests/55/CSharp-Part-2-2012-2013-11-Feb-2013

            //input
            short numberOfLines = short.Parse(Console.ReadLine());
            string inputData = Console.ReadLine(); //трябва да го направя в масив защото, ще се подават мн редове

            //calculation
            List<string> tempOutput = new List<string>();
			int openIndex = 0;
			int closeIndex = -1;
            string output = string.Empty;

            for (int i = 0; i < inputData.Length; i++)
            {
				if (inputData.IndexOf(upperTagClose, (closeIndex + 1)) != -1) //check and manipulate "upper tag"
                {
					closeIndex = inputData.IndexOf(upperTagClose, (closeIndex + 1));
					openIndex = inputData.IndexOf(upperTagOpen);
                }
                //if (inputdata.indexof(lowertagclose, ) //check and manipulate "lower tag"
                //{

                //}
                if (true) //check and manipulate "rev tag"
                {

                }
                if (true) //check and manipulate "del tag"
                {

                }
                closeIndex = -1;
            }
        }
    }
}
//да търси все по-навътре за отварящи тагове и при първия срещнат ЗАТВАРЯЩ да извършва действие с ПОСЛЕДНИЯ отварящ, после предходния и т.н.
