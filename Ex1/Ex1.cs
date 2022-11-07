using System;
using System.Text.RegularExpressions;

class Ex1
{
    static void Main()
    {
        string pattern = @"\b[A-Z]{1}[a-z]*\s[A-Z]{1}[a-z]*\b"; // шаблон
        /*
            \b    границя слова, позиція між \w і \W, або між \w і початком строки
            [A-Z]{1}    перша буква велика і її кількість 1
            [a-z]*    подальші букти лише в малому регістрі і кількість обмежена до кінця строки або пробіла
            \s    пробіл
            [A-Z]{1}    перша буква після пробіла велика і її кількість 1
            [a-z]*    подальші букти лише в малому регістрі і кількість обмежена до кінця строки або пробіла
            \b    границя слова, позиція між \w і \W, або між \w і початком строки
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