using System;
using System.Text.RegularExpressions;

class Ex3
{
    static void Main()
    {
        string? action = "aaaaabbbbbcdddeeeedssaa"; //Console.ReadLine();

        if (action != null)
        {
            string pattern = @"(\w)\1+";

            /*
               (   група 1
                    \w   яка складається з одної букви
                )
                \1+   повторення групи 1 один раз і більше
             */

            Regex regex = new(pattern);
            action = regex.Replace( action,
                                    "$1" // заміна групою 1
                                  );

            Console.WriteLine(action);
        }


        Console.ReadKey();
    }
}