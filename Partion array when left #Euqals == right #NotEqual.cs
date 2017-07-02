
找index, 数组左边#等于target == 右边#不等于target

/*
//O(N) time, O(N) space
We set two lists, and two pointer, i point to head;  j, point to the tail, and we start looping until i meet j, the exit condition is when i > j.
We then we need to let i and j point to the right position: which mean i must point to the first element that equals to target; j must point to the last element that not equal to the target number.

Then we can add the corresponding index into left and right list. Basically we just walk through the whole array once.

Then we can found whether the length of left equal to the length of right, if equal, just return the last element of right list, because we add the index into right from the tail. so the list element is the first index of the right part.
If not, return -1, means we can't find that element. 

		 int[] nums			     target
Pass cases:	(1,1,2,3),    				1
            	(1,1,2,3,1,1,1,1),	  		1
		(1,1,2,3,1),    			1
            	(2,1,1,2,3,1),  			1
*/

        public int GetIndex(int[] nums, int target)
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>();
            for (int i = 0, j = nums.Length - 1; i < nums.Length && j >= 0 && i <= j; i++, j--)
            {
                //Set i and j start at the first valid value
                while (nums[i] != target && i < nums.Length) i++;
                while (nums[j] == target && j >= 0) j--;
                //Add into list when meet valid cases
                if (nums[i] == target)
                {
                    left.Add(i);
                }
                if (nums[j] != target)
                {
                    right.Add(j);
                }
            }
            if (left.Count == right.Count && left.Count > 1) //Need more then 1 
            {    
                 return right.Last();
            }
            //Can't found that index.
            return -1;
        }

		
#region Follow up ：可不可能有多个符合的index   答：不能		
有四个相关的变量：xMet，nonXMet, xNotMet, nonXNotMet。其中xMet和nonXMet组成了扫过的部分，xNotMet和nonXNotMet组成了未扫过的部分。

符合的条件则是：xMet = nonXNotMet。而这两个变量一个只能递增(xMet)，一个只能递减(nonXNotMet)，而且并不能同时不变，因为如果当前index值是x，index++之后xMet也+1；如果当前的index值不是x，index++之后nonXNotMet就-1。所以不能同时不变，就不能有多个符合的。
#endregion
