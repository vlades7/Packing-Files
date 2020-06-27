using System;
using System.IO;

namespace Packing_Files
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = Environment.CurrentDirectory;
            string prefix;              // Префикс в названии папки
            string suffix;              // Суффикс в названии папки
            string folderPath;          // Путь к папке

            int iteratorFiles = 0;      // Счётчик файлов
            int currentFolder = 1;      // Номер текущей папки
            int filesFolder;            // Файлов в одной папке
            int movedFiles = 0;         // Перемещено файлов

            if (Directory.Exists(dirName))
            {
                string[] files = Directory.GetFiles(dirName);
                Console.WriteLine($"Количество найденных файлов: {files.Length}");

                Console.WriteLine("Файлы размещаются в папках, именнованных по шаблону \"ПрефиксЧислоСуфикс\".");
                Console.Write("Введите префикс для шаблона: ");
                prefix = Console.ReadLine();
                Console.Write("Введите суффикс для шаблона: ");
                suffix = Console.ReadLine();
                Console.Write("Количество файлов в одной папке: ");
                do
                {
                    int.TryParse(Console.ReadLine(), out filesFolder);
                }
                while (filesFolder <= 0);

                movedFiles = filesFolder;

                while (iteratorFiles < files.Length)
                {
                    folderPath = Path.GetFullPath($"{dirName}\\{prefix}{currentFolder}{suffix}");
                    DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
                    if (!directoryInfo.Exists)
                    {
                        directoryInfo.Create();
                        while (iteratorFiles < movedFiles && iteratorFiles < files.Length)
                        {
                            FileInfo fileInfo = new FileInfo(files[iteratorFiles]);
                            fileInfo.MoveTo(folderPath + "\\" + fileInfo.Name);
                            iteratorFiles++;
                        }
                        movedFiles += filesFolder;
                    }
                    currentFolder++;
                }
            }
            Console.WriteLine("Работа программы завершена. Нажмите клавишу, чтобы выйти...");
            Console.ReadKey();
        }
    }
}
