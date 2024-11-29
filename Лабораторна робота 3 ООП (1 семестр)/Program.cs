using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;
using Laba_3;

namespace Laba_3
{
    internal class Base_Task
    {
        // Головний метод програми
        public static void Main()
        {
            // Вказуємо шлях до папки Input, де зберігаються вхідні файли
            string files_tasks = @"D:\\VisualStudio\\Лабораторна робота 3 ООП (1 семестр)\\Input";

            // Списки для зберігання файлів з помилками
            List<string> files_not_exist = new List<string>();   // Файли, яких не існує
            List<string> files_not_parsing = new List<string>(); // Файли з помилками парсингу
            List<string> files_not_int = new List<string>();     // Файли з переповненням чисел

            // Список для зберігання результатів множення
            List<int> multiply = new List<int>();

            // Перебираємо файли з іменами від 10.txt до 29.txt
            for (int i = 10; i <= 29; i++)
            {
                string file = Path.Combine(files_tasks, $"{i}.txt"); // Створюємо шлях до файлу

                try
                {
                    // Читаємо файл
                    StreamReader sr = new StreamReader(file);
                    int first = int.Parse(sr.ReadLine());  // Читаємо перше число
                    int second = int.Parse(sr.ReadLine()); // Читаємо друге число

                    // Множимо числа і додаємо результат до списку
                    int dobutok = checked(first * second);
                    multiply.Add(dobutok);
                }
                catch (FileNotFoundException)  // Якщо файл не знайдено
                {
                    files_not_exist.Add($"{i}.txt"); // Додаємо до списку файлів, яких не існує
                }
                catch (FormatException)  // Якщо виникає помилка парсингу (нечислові дані)
                {
                    files_not_parsing.Add($"{i}.txt"); // Додаємо до списку файлів з помилками парсингу
                }
                catch (OverflowException)  // Якщо відбувається переповнення числа
                {
                    files_not_int.Add($"{i}.txt"); // Додаємо до списку файлів з переповненням
                }
            }

            try
            {
                // Обчислюємо середнє значення результатів множення
                long result = multiply.Select(x => (long)x).Sum() / multiply.Count;
                WriteLine($"Середнє значення: {result}");
            }
            catch (OverflowException)  // Якщо виникає помилка при обчисленні середнього (переповнення)
            {
                WriteLine("У нас немає середнього значення");
            }

            // Створюємо відповідні папки та переміщаємо файли на основі результатів
            Build_Directories(files_not_exist, files_not_parsing, files_not_int);

            // Зупинка програми для перегляду результатів
            Console.ReadLine();
        }

        // Метод для створення директорій та переміщення файлів в залежності від помилок
        public static void Build_Directories(List<string> a, List<string> b, List<string> c)
        {
            // Видаляємо файли в каталозі "no_file.txt", якщо вони існують
            foreach (var file in Directory.GetFiles(@"D:\\VisualStudio\\Лабораторна робота 3 ООП (1 семестр)\\Reports\\no_file.txt"))
            {
                File.Delete(file);
            }

            // Створюємо файли в папці "no_file.txt" для файлів, які не знайдені
            foreach (var i in a)
            {
                string folderPath = @"D:\\VisualStudio\\Лабораторна робота 3 ООП (1 семестр)\\Reports\\no_file.txt";
                string destinationPath = Path.Combine(folderPath, i);

                File.Create(destinationPath); // Створюємо порожній файл
            }

            // Видаляємо файли в каталозі "bad_data.txt", якщо вони існують
            foreach (var file in Directory.GetFiles($@"D:\\VisualStudio\\Лабораторна робота 3 ООП (1 семестр)\\Reports\\bad_data.txt"))
            {
                File.Delete(file);
            }

            // Копіюємо файли з помилками парсингу в каталог "bad_data.txt"
            foreach (var i in b)
            {
                string destinationPath = $@"D:\\VisualStudio\\Лабораторна робота 3 ООП (1 семестр)\\Reports\\bad_data.txt\\{i}";
                string sourcePath = $@"D:\VisualStudio\Лабораторна робота 3 ООП (1 семестр)\Input\{i}";

                try
                {
                    File.Copy(sourcePath, destinationPath); // Копіюємо файл
                }
                catch (Exception ex)
                {
                    WriteLine(ex.ToString()); // Якщо виникла помилка, виводимо її
                }
            }

            // Видаляємо файли в каталозі "overflow.txt", якщо вони існують
            foreach (var file in Directory.GetFiles($@"D:\\VisualStudio\\Лабораторна робота 3 ООП (1 семестр)\\Reports\\overflow.txt"))
            {
                File.Delete(file);
            }

            // Копіюємо файли з переповненням в каталог "overflow.txt"
            foreach (var i in c)
            {
                string destinationPath = $@"D:\\VisualStudio\\Лабораторна робота 3 ООП (1 семестр)\\Reports\\overflow.txt\\{i}";
                string sourcePath = $@"D:\VisualStudio\Лабораторна робота 3 ООП (1 семестр)\Input\{i}";

                try
                {
                    File.Copy(sourcePath, destinationPath); // Копіюємо файл
                }
                catch (Exception ex)
                {
                    WriteLine(ex.ToString()); // Якщо виникла помилка, виводимо її
                }
            }
        }
    }
}
