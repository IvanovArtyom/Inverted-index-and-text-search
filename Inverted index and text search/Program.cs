using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Inverted_index_and_text_search
{
    public class Program
    {
        private const string STANDARD_PATH = "..\\..\\..\\..\\Test files";
        private static SortedDictionary<string, List<StatisticalInformation>> invertedIndex = new();

        public static void Main()
        {
            // Файлы для тестов лежат в папке Inverted index and text search\Test files
            // Рекомендуемые лексемы для поиска: we11come, wellc0me, n1, H1, i5
            InvertedIndexAndTextSearch();
        }

        public static void InvertedIndexAndTextSearch()
        {
            Console.Write("Введите путь к директории: ");
            string directoryPath = Console.ReadLine() ?? "";
            List<string>? directory = FindDirectory(directoryPath);

            if (directory == null)
                return;

            if (directory.Count == 0)
            {
                Console.WriteLine("Не удалось найти файлы в директории!");
                return;
            }

            List<string> allData = ReadAllDataFromDirectory(directory);
            string[] fileNames = new string[allData.Count];

            for (int i = 0; i < fileNames.Length; i++)
                fileNames[i] = Path.GetFileName(directory[i]);

            invertedIndex = CreateInvertedIndex(allData, fileNames);

            Console.Write("Введите слово для поиска: ");
            string? lexem = Console.ReadLine();

            if (lexem == null)
            {
                Console.WriteLine("Нельзя найти то, чего нет!");
                return;
            }

            var foundInfo = TextSearch(lexem);

            if (foundInfo == null)
                return;

            Console.WriteLine("Найденная информация:");

            foreach (StatisticalInformation info in foundInfo)
                Console.WriteLine($"Имя файла: \"{info.FileName}\", кол-во вхождений: {info.Positions.Count}");

            DetailedInformationOutput(foundInfo, lexem);
        }

        public static List<string>? FindDirectory(string directoryPath)
        {
            if (directoryPath == "")
            {
                Console.WriteLine("Вы ввели пустой путь, Ваш путь был изменен на стандартный");
                directoryPath = STANDARD_PATH;
            }

            List<string> directory;

            try
            {
                directory = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories).ToList();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return directory;
        }

        public static List<string> ReadAllDataFromDirectory(List<string> directory)
        {
            List<string> allData = new();

            foreach (var path in directory)
            {
                StreamReader stream = new(path);
                string text = stream.ReadToEnd();
                allData.Add(text);
                stream.Close();
                stream.Dispose();
            }

            return allData;
        }

        public static SortedDictionary<string, List<StatisticalInformation>> CreateInvertedIndex(List<string> allData, string[] fileNames)
        {
            List<string>[] clearedData = new List<string>[allData.Count];
            Regex regex = new(@"\s+");

            for (int i = 0; i < clearedData.Length; i++)
            {
                string result = regex.Replace(allData[i], " ");
                clearedData[i] = result.Split().ToList();
            }

            for (int k = 0; k < fileNames.Length; k++)
            {
                for (int i = 0; i < clearedData[k].Count; i++)
                {
                    string lexem = clearedData[k][i][(clearedData[k][i].IndexOf(')') + 1)..];

                    if (lexem == "")
                        continue;

                    if (invertedIndex.ContainsKey(lexem))
                    {
                        if (invertedIndex[lexem][^1].FileName == fileNames[k])
                            invertedIndex[lexem][^1].AddPosition(i);

                        else invertedIndex[lexem].Add(new StatisticalInformation(fileNames[k], i));
                    }

                    else
                    {
                        List<StatisticalInformation> information = new()
                        {
                            new StatisticalInformation(fileNames[k], i)
                        };

                        invertedIndex.Add(lexem, information);
                    }
                }
            }

            return invertedIndex;
        }

        public static List<StatisticalInformation>? TextSearch(string wordToSearch)
        { 
            if (!invertedIndex.ContainsKey(wordToSearch))
            {
                Console.WriteLine($"Лексемы \"{wordToSearch}\" не нашлось в инвертированном индексе");
                return null;
            }

            var statisticalInformation = invertedIndex[wordToSearch];
            var sortedInfo = statisticalInformation.OrderByDescending(x => x.Positions.Count).ToList();

            return sortedInfo;
        }

        public static void DetailedInformationOutput(List<StatisticalInformation> info, string lexem)
        {
            while (true)
            {
                if (info.Count == 1)
                    Console.WriteLine("Введите число 1, чтобы посмотреть детальную информацию");

                else Console.WriteLine($"Введите число от 1 до {info.Count}, чтобы посмотреть детальную информацию");

                if (!int.TryParse(Console.ReadLine(), out int num) || num < 1 || num > info.Count)
                    continue;

                Console.WriteLine($"Детальная информация:\nВ файле \"{info[num - 1].FileName}\" лексема \"{lexem}\" встречается на позициях:");
                Console.WriteLine(string.Join(" ", info[num - 1].Positions));
                Console.WriteLine("Желаете продолжить? Введите \"Y\" для продолжения, для выхода нажмите любую другую кнопку");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    continue;

                break;
            }
        }
    }
}