﻿using System;
using System.Linq;
using System.Xml;
namespace Лаб2_Сортировка_строк
{
    class HeapSorter
    {
        static void MainPoint(string[] args)
        {
            Console.Write("Выполнить эксперимент перед началом программы (yes/no)? ");
            var answer = Console.ReadLine();
            if (answer == "yes")
                GetExperimentResults();
            Console.Write("Введите строки для сортировки, разделяя их пробелом: ");
            var input = Console.ReadLine().Split();
            HeapSort(input);
            Console.Write("Отсортированный массив: ");
            PrintArray(input);
        }

        public static int HeapSort(string[] array) // Сортировка массива
        {
            var heapSize = array.Length;
            var swapsCount = 0; // Счётчик swap-ов
            var branchesCount = 0; // Счётчик ветвлений
            for (var i = heapSize / 2 - 1; i >= 0; i--) // Построение кучи (перегруппируем массив)
                Heapify(array, heapSize, i, ref swapsCount, ref branchesCount);
            for (var i = heapSize - 1; i >= 0; i--) // По очереди извлекаем элементы из кучи
            {
                var swap = array[0]; // Перемещаем текущий корень в конец
                array[0] = array[i];
                array[i] = swap;
                swapsCount++;
                Heapify(array, i, 0, ref swapsCount, ref branchesCount); // Вызываем процедуру heapify на уменьшенной куче
            }
            return swapsCount >= branchesCount ? swapsCount : branchesCount; // Возвращаем счётчик самой частой операции
        }

        private static void Heapify(string[] array, int heapSize, int i, ref int swapsCount,
            ref int branchesCount) // Преобразования в двоичную кучу размера heapSize поддерева скорневым узлом i(индекс в array)
        {
            var largestElem = i; // Инициализируем наибольший элемент как корень
            var leftChild = 2 * i + 1; // left = 2 * i + 1
            var rightChild = 2 * i + 2; // right = 2 * i + 2
            if (leftChild < heapSize && String.Compare(array[leftChild], array[largestElem],
           StringComparison.Ordinal) > 0) // Если левый дочерний элемент больше корня
                largestElem = leftChild;

            if (rightChild < heapSize && String.Compare(array[rightChild],
           array[largestElem], StringComparison.Ordinal) > 0) // Если правый дочерний элемент больше, чем самый большой элемент на данный момент
                largestElem = rightChild;
            if (largestElem != i) // Если самый большой элемент не корень
            {
                var swap = array[i];
                array[i] = array[largestElem];
                array[largestElem] = swap;
                swapsCount++;
                Heapify(array, heapSize, largestElem, ref swapsCount, ref branchesCount);
                // Рекурсивно преобразуем в двоичную кучу затронутое поддерево
            }
            branchesCount += 3;
        }

        private static void PrintArray(string[] array) // Вывод массива строк на экран
        {
            for (var i = 0; i < array.Length; i++)
                Console.Write(array[i] + " ");
            Console.Read();
        }

        private static void GetExperimentResults() // Выполнение эксперимента
        {
            var xDoc = new XmlDocument();            
            xDoc.Load(Path.Combine(Environment.CurrentDirectory, "experiment.xml")); // Получаем xml-файл эксперимента
            var xRoot = xDoc.DocumentElement;
            var experiment = xRoot.SelectSingleNode("experiment");
            foreach (XmlNode node in experiment.ChildNodes)
            {
                var minElement = int.Parse(node.SelectSingleNode("@minElement").Value);
                // Получение значений атрибутов каждого node
                var maxElement = int.Parse(node.SelectSingleNode("@maxElement").Value);
                var startLength = int.Parse(node.SelectSingleNode("@startLength").Value);
                var maxLength = int.Parse(node.SelectSingleNode("@maxLength").Value);
                var repeat = int.Parse(node.SelectSingleNode("@repeat").Value);
                var name = node.SelectSingleNode("@name").Value;

                if (name == "Arithmetic Progression")
                {
                    var diff = int.Parse(node.SelectSingleNode("@diff").Value);
                    Console.WriteLine("Эксперимент: " + name);
                    for (var length = startLength; length <= maxLength; length += diff)
                        Console.WriteLine("Длина массива: " 
                            + length + "\t" 
                            + "Кол-во массивов: " + repeat + "\t" 
                            + "Операций в среднем: " 
                            + GetOperationsCount(minElement, maxElement, repeat, length) / repeat);
                }

                if (name == "Geometric Progression")
                {
                    var znamen = double.Parse(node.SelectSingleNode("@Znamen").Value);
                    Console.WriteLine("Эксперимент: " + name);
                    for (var length = startLength; length <= maxLength; length = (int)Math.Round(length * znamen))
                        Console.WriteLine("Длина массива: " 
                            + length + "\t" 
                            + "Кол-во массивов: " + repeat + "\t" 
                            + "Операций в среднем: " 
                            + GetOperationsCount(minElement, maxElement, repeat, length) / repeat);
                }
                Console.WriteLine("\n");
            }
        }

        private static int GetOperationsCount(int minElement, int maxElement, int repeatsCount, int arrayLength) // Получаем количество операций для массивов заданной длины
        {
            var operationsCount = 0;
            for (var i = 0; i < repeatsCount; i++)
            {
                var array = new string[arrayLength];
                var random = new Random();
                for (var j = 0; j < array.Length; j++)
                    array[j] = random.Next(minElement, maxElement).ToString();
                operationsCount += HeapSort(array);
            }
            return operationsCount;
        }        
    }
}