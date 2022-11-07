using System;
using System.Text.RegularExpressions;

class Ex2
{
    static void Main()
    {
        string pattern = @"+(359)\s2\s[0-9]{3}\s[0-9]{4}\b"; // шаблон
        /*
         * 
        */

        while (true)
        {
            string? action = Console.ReadLine();


            if (action?.ToLower() == "end") break;

            if (action != null)
                if (Regex.IsMatch(action, pattern))
                {
                    // візуально змінюється вивід
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(action);

                    // візуально змінюється вивід назад
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
        }


        Console.ReadKey();
    }
}