using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorytms_lab1
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> collection) where T : IComparable
        {
            var collectionToSort = collection.ToArray();
            if (collectionToSort.Length <= 1)
                return collectionToSort;

            var leftPart = MergeSort(collectionToSort[..(collectionToSort.Length / 2)]).ToArray();
            var rightPart = MergeSort(collectionToSort[(collectionToSort.Length / 2)..]).ToArray();

            var result = new T[collectionToSort.Length];
            var leftPointer = 0;
            var rightPointer = 0;
            var resultPointer = 0;

            while (leftPointer < leftPart.Count() && rightPointer < rightPart.Count())
            {
                if (leftPart[leftPointer].CompareTo(rightPart[rightPointer]) <= 0)
                {
                    result[resultPointer] = leftPart[leftPointer];
                    leftPointer++;
                }
                else
                {
                    result[resultPointer] = rightPart[rightPointer];
                    rightPointer++;
                }
                resultPointer++;
            }

            while (leftPointer < leftPart.Count())
            {
                result[resultPointer] = leftPart[leftPointer];
                leftPointer++;
                resultPointer++;
            }

            while(rightPointer < rightPart.Count())
            {
                result[resultPointer] = rightPart[rightPointer];
                rightPointer++;
                resultPointer++;
            }

            return result;
        }
    }
}
