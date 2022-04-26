using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorytms_lab1
{
    internal static class IEnumerableExtensions
    {
        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> collection, Func<int> counter = null) where T : IComparable
        {
            var collectionToSort = collection.ToArray();
            if (collectionToSort.Length <= 1)
                return collectionToSort;

            var leftPart = MergeSort(collectionToSort[..(collectionToSort.Length / 2)], counter).ToArray();
            var rightPart = MergeSort(collectionToSort[(collectionToSort.Length / 2)..], counter).ToArray();

            var result = new T[collectionToSort.Length];
            var leftPointer = 0;
            var rightPointer = 0;

            for (int i = 0; i < result.Length; i++)
            {
                if (rightPointer >= rightPart.Length
                    || (leftPointer < leftPart.Length && leftPart[leftPointer].CompareTo(rightPart[rightPointer]) <= 0))
                {
                    result[i] = leftPart[leftPointer];
                    leftPointer++;
                }
                else
                {
                    result[i] = rightPart[rightPointer];
                    rightPointer++;
                }
                if (counter != null)
                    counter();
            }

            return result;
        }
    }
}
