using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Console1
{
    class Program
    {
        private static List<DataSetResult> results = new List<DataSetResult>();
        private static int totalSum = 0;
        private static readonly object resultsLocker = new object();
        private static readonly Mutex totalSumMutex = new Mutex();
        private static Semaphore semaphore;

        private static List<int[]> dataSets = new List<int[]>();
        private const int DataSetCount = 15;
        private const int NumbersPerSet = 100;
        private const int MaxConcurrentThreads = 3;

        static void Main()
        {
            LoadOrGenerateDataSets();

            Console.WriteLine("=== Задание 1.2: Обработка наборов чисел ===\n");

            semaphore = new Semaphore(MaxConcurrentThreads, MaxConcurrentThreads);

            Stopwatch sw = Stopwatch.StartNew();

            Thread[] threads = new Thread[DataSetCount];

            for (int i = 0; i < DataSetCount; i++)
            {
                int index = i;
                threads[i] = new Thread(() => ProcessDataSet(index));
                threads[i].Start();
            }

            foreach (var t in threads)
            {
                t.Join();
            }

            sw.Stop();

            Console.WriteLine("\n=== РЕЗУЛЬТАТЫ ===\n");

            foreach (var result in results)
            {
                Console.WriteLine($"Набор #{result.DataSetNumber}: сумма = {result.Sum}, обработан потоком #{result.ThreadId}");
            }

            Console.WriteLine($"\nОбщий итог по всем наборам: {totalSum}");
            Console.WriteLine($"Время выполнения: {sw.ElapsedMilliseconds} мс");
        }

        static void LoadOrGenerateDataSets()
        {
            string filePath = "datasets.txt";

            if (File.Exists(filePath))
            {
                Console.WriteLine("Загрузка наборов из файла...");
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    int[] numbers = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                                            .Select(int.Parse)
                                                            .ToArray();

                    if (numbers.Length == NumbersPerSet)
                    {
                        dataSets.Add(numbers);
                    }
                }

                if (dataSets.Count == DataSetCount)
                {
                    Console.WriteLine($"Загружено {dataSets.Count} наборов.\n");
                    return;

                }
                else
                {
                    Console.WriteLine($"Файл содержит {dataSets.Count} наборов, ожидалось {DataSetCount}. Будет выполнена перегенерация.");
                    dataSets.Clear();
                }
            }

            Console.WriteLine($"Генерация {DataSetCount} наборов по {NumbersPerSet} чисел...");
            Random rand = new Random();

            for (int i = 0; i < DataSetCount; i++)
            {
                int[] numbers = new int[NumbersPerSet];
                for (int j = 0; j < NumbersPerSet; j++)
                {
                    numbers[j] = rand.Next(1, 101);
                }
                dataSets.Add(numbers);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var set in dataSets)
                {
                    writer.WriteLine(string.Join(" ", set));
                }
            }
            Console.WriteLine($"Наборы сохранены в файл '{filePath}'\n");
        }


        static void ProcessDataSet(int dataSetIndex)
        {
            semaphore.WaitOne();
            try
            {
                int threadId = Thread.CurrentThread.ManagedThreadId;
                int[] numbers = dataSets[dataSetIndex];

                int sum = 0;
                foreach (int num in numbers)
                {
                    sum += num;
                }

                Console.WriteLine($"[Поток {threadId}] Набор #{dataSetIndex + 1}: сумма = {sum}");

                lock (resultsLocker)
                {
                    results.Add(new DataSetResult
                    {
                        DataSetNumber = dataSetIndex + 1,
                        Sum = sum,
                        ThreadId = threadId
                    });
                }

                totalSumMutex.WaitOne();
                try
                {
                    totalSum += sum;
                }
                finally
                {
                    totalSumMutex.ReleaseMutex();
                }
            }
            finally
            {
                semaphore.Release();
            }
        }
    }


    class DataSetResult
    {
        public int DataSetNumber { get; set; }
        public int Sum { get; set; }
        public int ThreadId { get; set; }
    }
}