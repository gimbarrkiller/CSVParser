using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<string>> list = new List<List<string>>();
            string path;
            int column = -1;
            if (args.Length >= 1) //Если количество аргументов больше нуля, то читаем путь файла
            { path = args[0]; }
            else //если аргументы не были указаны, то запрашиваем их через консоль
            {
                Console.WriteLine("Введите путь к файлу:");
                path = Console.ReadLine();
                try
                {
                    StreamReader fstream = new StreamReader(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Файл не найден");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Введите номер столбца для сортировки:");
                try
                {
                    column = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Столбец не существует. Сортировка не будет выполнена.");
                }
            }
            if (args.Length == 2) // Если количество входных параметров равно двум, то записываем
            { column = int.Parse(args[1]); }


            try
            {
                using (StreamReader fstream = new StreamReader(path))
                {
                    string line;
                    Regex r = new Regex("(([a-zA-z0-9 .]{2,})|(\")([a-zA-Z0-9 .,]{2,})(\"))"); //регулярное выражение которое парсит строку.
                    while ((line = fstream.ReadLine()) != null) //пока не дошли до конца файла выполняем следующее
                    {
                        if (r.IsMatch(line)) //если есть совпадение в строке то
                        {
                            list.Add(new List<string>());
                            foreach (Match a in r.Matches(line))
                            {
                                Console.Write(a.Groups[2].Value + a.Groups[4].Value + "|");
                                list[list.Count - 1].Add(a.Groups[2].Value + a.Groups[4].Value); //записываем 2 и 4 группу
                            }
                            Console.WriteLine();
                        }
                    }

                }
                if (column != -1) //если параметр был указан то выполняем сортировку и выводим результат
                {
                    Console.WriteLine("\n\nСтолбец, по которому проводится сортировка: " + column);
                    SortedList<string, List<string>> sorted = new SortedList<string, List<string>>(); //SortedList - это список который сортирует записи по ключу
                    foreach (var a in list) //перекачиваем данные для сортировки
                    {
                        sorted.Add(a[column], a);
                    }
                    foreach (var a in sorted)
                    {
                        foreach (var b in a.Value)
                            Console.Write(b + " ");
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception error)
            {
                Console.Write("Ошибка: " + error.Message.ToString());
            }
            Console.ReadKey();


        }
    }
}
