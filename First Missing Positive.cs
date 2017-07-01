First Missing Positive

Given an unsorted integer array, find the first missing positive integer.
For example,
Given [1,2,0] return 3,
and [3,4,-1,1] return 2.



#region Sort. O(NlogN) time, O(logN) space

////Using sort. O(NlogN) time, O(logN) space
     public int FirstMissingPositive(int[] nums)
        {
            Array.Sort(nums);
            int res = 0;
            int i = 0;
            while (i < nums.Length && nums[i] < 0)
            {
                i++;
            }
            //Reach positive
            if (i == nums.Length - 1) return 1;
            else
            {
                while (i < nums.Length - 1 && nums[i] == nums[i+1] - 1)
                    i++;
                if (i != nums.Length)   
                    return nums[i] + 1;
                else                     
                    return nums.Length + 1;
            }
        }

#endregion




#region   O(N) time, O(1) space          Can handle duplicate


/*
	1. Move every value to the position of its value.

          Iterate through array, if the value of current is between 1 and the length of array, swap value with item, which index is that value. 

	2. Find first location where index doesn't match the value. Then loop again, check value if equal to index + 1  to find missing positive.
*/

public int FirstMissingPositive(int[] nums)
{
     for(int i = 0; i < nums.Length;)
     {
          //Cases don't care 
          //1. nums[i] = i + 1 Normal case
          //2. nums[i] < 1; Negative number
          //3. nums[i] > nums.Length  Greatest value
          //4. nums[i[ == nums[nums[i] - 1] => n = nums[n+1]
          if(nums[i] == i + 1 || nums[i] < 1 || nums[i] > nums.Length || nums[i] == nums[nums[i] - 1])
          {
               i++; continue;
          }
          //Then Sorting:
          Swap nums[i] and nums[nums[i] - 1]   => n and nums[n-1]
          int temp = nums[i];
          nums[i] = nums[temp - 1];
          nums[temp - 1] = temp;
     }
     
     for(int i = 0; i < nums.Length; i++)
     {
          if(nums[i] != i + 1)
               return i + 1; //We found the first missing positive number
     }
     //If reach here, which mean the first missing positive number is greater then length
     return nums.Length + 1;
}


*/



#endregion