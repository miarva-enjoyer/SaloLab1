using Async_and_sync_server_shenanigans;
using System;
using System.Diagnostics;

Console.WriteLine("=== СИНХРОННАЯ ВЕРСИЯ ===");
var stopwatch = Stopwatch.StartNew();


    Fakeserver server = new Fakeserver();

    // Первый запрос  
    Console.WriteLine("Отправка запроса 1...");
    int result1 = server.Fakerequest();
    Console.WriteLine($"Запрос 1 завершен. Ответ: {result1}.");

// Второй запрос  
Console.WriteLine("Отправка запроса 1...");
int result2 = server.Fakerequest();
Console.WriteLine($"Запрос 2 завершен. Ответ: {result2}.");

// Третий запрос  
Console.WriteLine("Отправка запроса 1...");
int result3 = server.Fakerequest();
Console.WriteLine($"Запрос 3 завершен. Ответ: {result3}.");


stopwatch.Stop();
Console.WriteLine($"\\nВсего затрачено времени: {stopwatch.ElapsedMilliseconds} мс.");




Console.WriteLine("=== АСИНХРОННАЯ ВЕРСИЯ ===");
stopwatch = Stopwatch.StartNew();

Task<int> task1 = server.FakerequestAsync();
Task<int> task2 = server.FakerequestAsync();
Task<int> task3 = server.FakerequestAsync();

int[] results = await Task.WhenAll(task1, task2, task3);

Console.WriteLine($"Запрос 1 завершен. Ответ: {results[0]}.");
Console.WriteLine($"Запрос 2 завершен. Ответ: {results[1]}.");
Console.WriteLine($"Запрос 3 завершен. Ответ: {results[2]}.");

stopwatch.Stop();
Console.WriteLine($"\\nВсего затрачено времени: {stopwatch.ElapsedMilliseconds} мс.");
