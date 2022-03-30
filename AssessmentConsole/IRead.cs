using System;
using System.Collections.Generic;
using System.Text;

namespace AssessmentConsole
{
    internal class IRead
    {
        public static string Option()
        {
            Console.Write("> ");
            return Console.ReadLine();
        }

        public static string PutMessageGetOption(string message)
        {
            Console.WriteLine(message);
            return Option();
        }
    }
}
