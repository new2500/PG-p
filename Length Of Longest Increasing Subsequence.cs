Longest Increasing Subsequence
Try to find all increase subsequences and then return the maximum length of longest increasing subsequence.
这里input是int,有出现给string的情况

#region Brute Force
We can make use of a recursive function which returns the length of LIS possible from the current element. Inside each function call, we consider two cases:

	1. The current element > than the previous element included in the LIS. In this case, we can include the current element in the LIS. 
	2. The current element < than the previous element included in the LIS. In this case, we can't include the current element.

        public int LengthOfLIS(int[] nums)
        {
            return FindLength(nums, int.MinValue, 0);
        }
        public int FindLength(int[] nums, int prev, int curPos)
        {
            if (curPos == nums.Length)
                return 0;
            int taken = 0;
            if (nums[curPos] > prev)
            {
                taken = 1 + FindLength(nums, nums[curPos], curPos + 1);
            }
            int notTaken = FindLength(nums, prev, curPos + 1);
            return Math.Max(taken, notTaken);
        }
		
//Time: Size of recursion tree will be O(2^n). Space complexity: O(n^2). we take size n*n for recursion.

#endregion



#region Dynamic Programming

Say we know the length of LIS at i index, then we can figure out the same on i+1 index based on ith index.

We make use of a dp array to store the required data. dp[i] represents the length of the longest increasing subsequence possible considering the array element up to i index only. 

In order to find out dp[i], we need to try to append the current element(nums[i]) in every possible increasing subsequence up to i-1 index, such that the new sequence formed by adding the current element is also an increasing subsequence. 

so dp[i] = Math of(dp[j]) + 1 ; LIS = Max(dp[i])


       public int LengthOfLIS(int[] nums)
        {
            if (nums.Length <= 1)
                return nums.Length;
            //Fill each position with 1, if not, we need to i start at 0 in the next loop
            int[] dp= new int[nums.Length];
            //for (int i = 0; i < dp.Length; i++)
            //{
            //    dp[i] = 1;
            //}
            int max = 0;
            for (int i = 0; i < nums.Length; i++) //If we don't fill the dp[i] = 1 previously, we need to start at 0, not 1
            {
                dp[i] = 1;
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        if (dp[j] + 1 > dp[i])
                            dp[i] = dp[j] + 1;
                    }
                } 
                max = Math.Max(max, dp[i]);
            }
            return max;
        }

//Time: we have two loops of n are used, so it's O(n^2) time, We use a dp array, so it's O(n) space

#endregion



#region DP, Improve to O(N*logN) time: O(N) space

We make use a tails array, which is an array storing the smallest tail of all increasing subsequence with length i+1 in tails[i].

For example, say we have nums = 4,5,6,3, the all the available increasing subsequence are:

len = 1  :  4, 5, 6, 3     => tail[0] = 3
len = 2  :  4, 5  &  5, 6  => tail[1] = 5
len = 3  :  4, 5, 6        => tail[2] = 6

We can easily prove that tails is increasing array. Therefore it is possible to do a binary search in tails array to find the one needs update.

Each time we only do one of 2 things:
a. if x is larger than all tails, append it, increase the maxLen by 1
b. if tails[i-1] < x <= tails[i], update tails[i]


      public int LengthOfLIS(int[] nums)
        {
            int[] tails = new int[nums.Length];
            int maxLen = 0;
            foreach (var num in nums)
            {
                int i = 0, j = maxLen;
                while (i != j)
                {
                    int mid = i + (j - i) / 2;
                    if (tails[mid] < num)
                        i = mid + 1;
                    else
                        j = mid;
                }
                tails[i] = num;
                if (i == maxLen)
                    maxLen++;
            }
            return maxLen;
        }




#endregion