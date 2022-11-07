using System;
using System.Numerics;
using System.Text.RegularExpressions;

class Ex8
{
    static void Main()
    {
        // основний патерн
        string pattern1 = @"<(div)(\s?((\w|[-]|[:])+[=][""](\w|[-]|[:])+[""]\s?)*((id|class)[=][""](\w|[-]|[:])+[""])\s?(\w+[=][""](\w|[-]|[:])+[""]\s?)*)>";
        string pattern2 = @"(div)"; // <div> | </div>
        string pattern4 = @"(((id)|(class))\s?[=]\s?[""](\w+)[""])(\w)*"; // (id | class)="..."

        Regex regex2 = new(pattern2); // <div> | </div>
        Regex regex4 = new(pattern4); // (id | class)="..."


        // зчитувана строка
        string line = "<p class = \"section\" > <div style = \"border: 1px\" id = \"header\" > Header <div id = \"nav\" > Nav </div> <!-- nav --> </div> <!--header--> </p> <!-- end paragraph section -->";
        ChangeAndPrint(ref line); // додається '\n' і виводиться (для зручності читання з консолі)
        string[] arr = line.Split('\n'); // строка розбирається на масив підстрок


        string temp = ""; // для записування тегу
        for (int i = 0; i < arr.Length; i++)
        {
            if (Regex.IsMatch(arr[i], pattern2)) // чи є в цій строці <div> або </div>
            {
                Match m = regex4.Match(arr[i]); // витягується (id або class)="..."

                if (Regex.IsMatch(arr[i], @"<(div)")) // якщо це відкриваючий тег
                    temp = m.Groups[5].Value; // витягується значення з 5-ї групи (те що в лапках, id | class = "(5-а група)")
            }

            if (Regex.IsMatch(arr[i], pattern1) || // якщо це відкриваючий тег з якимось id або class
                Regex.IsMatch(arr[i], pattern2)) // якщо це відкриваючий або закриваючий тег
            {
                arr[i] = regex2.Replace(arr[i], temp); // замість div вставляється значення з лапок id або class

                for (int l = arr.Length - 1; l > i; l--) // шукається коментар і закриваючий тег щоб ї його змінити
                {
                    if (Regex.IsMatch(arr[l], $@"</div")) // якщо закриваючий тег
                    {
                        arr[l] = regex2.Replace(arr[l], temp); // замість div вставляється значення з лапок id або class

                        if (!Regex.IsMatch(arr[l], $@"<!--\s?({temp})\s?-->")) // якщо після нього не іде коментар зі значенням з лапок id або class
                            break; // пошук закриваючого тега і коментара завершений
                    }
                    if (Regex.IsMatch(arr[l], $@"<!--\s?({temp})\s?-->")) // якщо коментар зі значенням з лапок id або class
                    {
                        arr[l] = new Regex($@"<!--\s?({temp})\s?-->").Replace(arr[l], ""); // коментар видаляється

                        break; // пошук закриваючого тега і коментара завершений
                    }
                }

                arr[i] = regex4.Replace(arr[i], ""); // видалення (id | class)="..."

            }
            arr[i] = new Regex(@"\s+").Replace(arr[i], " "); // 1 і більше пробілів замінюються одним пробілом
            arr[i] = new Regex(@"\s+>").Replace(arr[i], ">"); // якщо є пробіли перед закриттям тегу "<tag >", то вони видаляються
        }

        Console.WriteLine("\n");
        line = string.Join("", arr); // масив підстрок зливається в одну строку
        ChangeAndPrint(ref line); // додається '\n' і виводиться (для зручності читання з консолі)


        Console.ReadKey();
    }

    static void ChangeAndPrint(ref string line)
    {
        for (int i = 1; i < line.Length; i++)
            if (
                    line[i - 1] == '>' && // якщо тег закрився
                    (i + 1 < line.Length && line[i + 1] != '\n') && // і після нього немає переносу строки
                    (i + 2 < line.Length && line[i + 1] != '<' && line[i + 2] != '!') // і це не коментар
                )
                    line = line.Insert(++i, "\n"); // додається перенос строки
            else
                if (
                        line[i - 1] == ' ' && line[i] == '<' && // якщо тег відкрився
                        (i + 1 < line.Length && line[i + 1] != '\n' && line[i + 1] != '!') // і це не коментар
                    )
                    line = line.Insert(i, "\n"); // додається перенос строки


        Console.WriteLine(line); // змінена строка виводиться
    }
}