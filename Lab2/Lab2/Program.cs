
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Console1
{
    class Program
    {
        private static int totalPrimeCount = 0;
        private const int MaxNumber = 10000;
        private const int ThreadCount = 4;

        private static readonly object locker = new object();
        private static readonly Mutex mutex = new Mutex();
        private static readonly Semaphore semaphore = new Semaphore(3, 3);

        static void Main()
        {
            RunVersion("Monitor", UseMonitor);
            RunVersion("Mutex", UseMutex);
            RunVersion("Semaphore", UseSemaphore);
        }

        static void RunVersion(string name, Action<int, int, int> sync)
        {
            totalPrimeCount = 0;
            Console.WriteLine(name);

            int step = MaxNumber / ThreadCount;
            Thread[] threads = new Thread[ThreadCount];
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < ThreadCount; i++)
            {
                int start = i * step + 1;
                int end = (i + 1) * step;
                int id = i + 1;

                threads[i] = new Thread(() => ProcessRange(start, end, id, sync));
                threads[i].Start();
            }
            foreach (var t in threads) t.Join();

            sw.Stop();
            Console.WriteLine($"Итого: {totalPrimeCount}, Время: {sw.ElapsedMilliseconds}мс");
        }

        static void ProcessRange(int start, int end, int threadId, Action<int, int, int> syncMethod)
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine($"Поток {threadId}: проверяет {i}");

                if (IsPrime(i))
                {
                    syncMethod(i, threadId, i);
                }
            }
        }

        static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }

        static void UseMonitor(int number, int threadId, int foundPrime)
        {
            lock (locker)
            {
                totalPrimeCount++;
            }
        }

        static void UseMutex(int number, int threadId, int foundPrime)
        {
            mutex.WaitOne();
            try
            {
                totalPrimeCount++;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        static void UseSemaphore(int number, int threadId, int foundPrime)
        {
            semaphore.WaitOne();
            try
            {
                totalPrimeCount++;
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
