﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Algorytms_lab1
{
    public class Program
    {
        public static void Main()
        {
            var list = new List<string> { "aaa", "abcd", "zxcv", "ght", "a", "href", "div"};
            var sorted = list.MergeSort();
            sorted.ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}