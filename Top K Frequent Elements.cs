Top K Frequent Elements

#region Priority Queue, or MaxHeap
//O(N*logN) time, O(2N) space, using Sorted Dictionary act like Priority Queue

/*About the low level details of SortedDictionary: The SortedDictionary<TKey, TValue> generic class is a binary search tree with O(log n) retrieval, where n is the number of elements in the dictionary.

https://stackoverflow.com/questions/935621/whats-the-difference-between-sortedlist-and-sorteddictionary

SortedDictionary has O(logN) search time, add time, remove time.
*/

        public List<int> TopKFreq(int[] nums, int k)
        {
            Dictionary<int, int> countDict = new Dictionary<int, int>();

            foreach (int x in nums)  //Fill the hash table with the # of appearance, and find the max  O(N) time
            {
                int currCount = countDict.ContainsKey(x) ? countDict[x] + 1 : 1;
                countDict[x] = currCount;
            }
           
            //Use a SortedDictionary act like PriorityQueue, Key is the # of frequency, Value is element
            // We use a comparer to untilzie the Max comes first, and if key is duplicate, we implement first in fist out rule 
            SortedDictionary<int, int> maxHeap = new SortedDictionary<int, int>(new DuplicateKeyCom<int>());
            foreach (var entry in countDict)       O(N) time * O(logN) time
            {
                maxHeap.Add(entry.Value, entry.Key); 
            }

            List<int> res = new List<int>();
            foreach (var entry in maxHeap)
            {
                if (res.Count < k)
                {
                    res.Add(entry.Value);
                }
                else
                    break;
            }
            return res;
        }
          //Comparer to make Descending order and handle duplicate key
        public class DuplicateKeyCom<TKey> : IComparer<TKey> where TKey : IComparable  
        {
            public int Compare(TKey x, TKey y)
            {
                int result = y.CompareTo(x);    // aka x-y
                return result == 0 ? 1 : result;
            }

        }

#endregion





#region O(N) time, O(2N) space, using Bucket Sort

/*
1. First we build a hash table, tracking down the element and its number of appearance. And we need to tracking the Maximum number of appearance.
2. We build a array of lists, each list contains the elements that appears the same time. For example, if 1 and 3 appears 4 times in the array, the index 4 of the countArr, which is a list, contains 1,3.
3. Then we can iterate the countArr from the tail, take out the element , decrease the k, until k is 0,
*/

public List<int> TopKFreq(int[] nums, int k)
{
     Dictionary<int, int> countDict = new Dictionary <int, int>();
     int maxCount = 0;

     //Build Hash table and find maxCount
     foreach(int x in nums)
     {
          int currCount = countDict.ContainsKey(x) ? countDict[x] + 1 : 1;
          countDict[x] = currCount;
          if(currCount > maxCount)
               maxCount = currCount;
     }

     //Build array of list, Group up the element that appears the same.
     List<int>[] countArr = new List<int>[maxCount + 1];
     foreach(int val in countDict.Keys)
     {
          int currCount = countDict[val];
          if(countArr[currCount] == null)    //Initialize 
          {
               countArr[currCount] = new List<int>();
          }
          countArr[currCount].Add(val);
     }

     //Retrieve the top k, starting from the tail of countArr
     List<int> topK = new List<int>();
     for(int i = countArr.Length - 1; i >= 0 && topK.Count < k; k--)
     {
          if(countArr[i] != null)
          {
               topK.AddRange(countArr[i]);
          }
     } 
     return topK;
     
}

#endregion


//Follow up: If the input is a long steam, what should we do about it?

//We can't use bucket sort since bucket sort requires more space and we need reach the end of the steam. We can use PQ/Heap method. and divied the input steam for multiple machine to handle, 

