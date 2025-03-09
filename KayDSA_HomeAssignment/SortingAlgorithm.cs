using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayDSA_HomeAssignment
{
    public class SortingAlgorithm
    {
        public static List<int> MergeSort(List<int> numbers)
        {
            if (numbers.Count <= 1)
                return new List<int>(numbers);

            int mid = numbers.Count / 2;
            List<int> leftHalf = MergeSort(numbers.GetRange(0, mid));
            List<int> rightHalf = MergeSort(numbers.GetRange(mid, numbers.Count - mid));

            return Merge(leftHalf, rightHalf);
        }

        static List<int> Merge(List<int> left, List<int> right)
        {
            List<int> sortedList = new List<int>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i] < right[j])
                {
                    sortedList.Add(left[i]);
                    i++;
                }
                else
                {
                    sortedList.Add(right[j]);
                    j++;
                }
            }

            sortedList.AddRange(left.GetRange(i, left.Count - i));
            sortedList.AddRange(right.GetRange(j, right.Count - j));

            return sortedList;
        }
    }
}
