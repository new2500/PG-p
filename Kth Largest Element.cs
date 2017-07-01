Kth Largest Element

#region Brute Force Sort and find

        //  O(N lgN) time + O(1) space
        public int FindKthLargest(int[] nums, int k)
        {
            int N = nums.Length;
            Array.Sort(nums);
            return nums[N - k];
        }
#end


#region Using MaxHeap 
  //           O(N lgK) time + O(N) space
using System.Linq;
    public int FindKthLargest(int[] nums, int k)
    {
        SortedDictionary<int, int> pq = new SortedDictionary<int, int>(new DuplicateKeyCom<int>());
        foreach (var num in nums)
        {
            pq.Add(num, num);
        }
        return pq.ElementAt(k - 1).Key;   //Get the key by index
    }
    public class DuplicateKeyCom<TKey> : IComparer<TKey> where TKey : IComparable
    {
        public int Compare(TKey x, TKey y)
        {
            int result = y.CompareTo(x);
            return result == 0 ? 1 : result;
        }
    }

#end


#region Quick Select

QuickSelect Algorithm, O(N) time , O(N^2) worst time, O(1) swap space cost
Like Quick Sort, but Quick Select just jump into one range to keep searching, avoiding the logN time.
Worst case: the pivot selection, if we decrease the range only by 1 rather than half, it becomes O(N^2) time

        //Move the K largest elements to the left part of array
        public int FindKthLargest(int[] nums, int k)
        {
            if (nums.Length == 1)
                return nums[0];
            int left = 0, right = nums.Length - 1;
            while (left <= right)
            {
                int pivotPos = Partition(nums, left, right);
                if (pivotPos - left + 1 < k)
                {
                    k = k - (pivotPos - left + 1); //Shrink k value
                    left = pivotPos + 1;           //Move left behind pivot
                }
                else if (pivotPos - left + 1 > k)
                    right = pivotPos - 1; //Shrink right by 1
                else
                    return nums[pivotPos];
            }
            //Not exist, default return
            return 0;
        }
        private int Partition(int[] nums, int left, int right)
        {
            int pivotIdx = left + (right - left) / 2;
            int pivot = nums[pivotIdx];
            Swap(nums, pivotIdx, right);
            int leftBound = left;
            int rightBound = right - 1;
            while (leftBound <= rightBound)
            {
                if (nums[leftBound] > pivot)
                    leftBound++;
                else if (nums[rightBound] > pivot)
                    rightBound--;
                else
                {
                    Swap(nums, leftBound, rightBound);
                    leftBound++;
                    rightBound--;
                }
            }
            Swap(nums, leftBound, right);
            return leftBound;
        }
        private void Swap(int[] nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }



#end