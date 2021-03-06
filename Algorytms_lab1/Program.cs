using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Algorytms_lab1
{
    public class Program
    {
        public static void Main()
        {
            var operationCount = 0;
            var counter = () => operationCount++;
            var xDoc = new XmlDocument();
            xDoc.Load(Path.Combine(Environment.CurrentDirectory, "experiment_plan.xml")); // Получаем xml-файл эксперимента
            var list = new List<string> { "aaa", "abcd", "zxcv", "ght", "a", "href", "div", "breakpoint" };
            var sorted = list.MergeSort(counter);
            sorted.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine($"Количество операций: {operationCount}");
        }
    }
}
