Sort Color 

Given an array wit n objects colored red, white or blue, sort them so that same color are adjacent, with the order red,white and blue. 
Use:
0 : Red
1: White
2: Blue


First go through the array, find out how many 0, 1 and 2; then loop through the array, rewrite the array based on the counter
#region O(2N) time, O(1) space. Two-pass algorithm

        public void SortColors(int[] nums)
        {
            int lowCount = 0, midCount = 0, highCount = nums.Length;
            for (int i = 0; i < nums.Length; i++)   //Count how mnay 0, 1 and 2
            {
                if (nums[i] == 0)
                {
                    lowCount++; highCount--;
                }
                else if (nums[i] == 1)
                {
                    midCount++; highCount--;
                }
            }
            for (int i = 0; i < nums.Length; i++) //Rewrite the nums based on th counter
            {
                if (lowCount > 0)
                {
                    nums[i] = 0; lowCount--;
                }
                else if (midCount > 0)
                {
                    nums[i] = 1; midCount--;
                }
                else
                {
                    nums[i] = 2;
                }
            }
        }
#endregion





#region O(N) time, O(1) space. One-pass algorithm
We set the middle value to 1 then loop from beginning, if nums[i] < mid, then swap nums[i] with nums[low], then increment low and i; If not, Swap nums[i] with nums[right], just decrese the right, if equal , just increase the i 

        public void SortColors(int[] nums)
        {
            int l = 0, i = 0, mid = 1, r = nums.Length - 1;
            while (i <= r)
            {
                if (nums[i] < mid)
                {
                    Swap(nums, i, l);
                    i++; l++;
                }
                else if (nums[i] > mid)
                {
                    Swap(nums, i, r);
                    r--;
                }
                else
                    i++;
            }
        }
        private void Swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
#endregion




//Not implement K colors, but asked in interivew to solve 4 colors
