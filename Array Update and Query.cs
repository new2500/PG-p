Array Update and Query


#region Naive

Query time: For query we access each element from the array in constant time and in the worst case we access n elements(query all), so it's O(N) time.
Update: we just update the specific element in the array, so it's O(1) time
Space: We don't use extra data structure, so it's O(1) space complexity

    public class NumArrayNaive
    {
        private int[] nums;
        public NumArrayNaive(int[] nums)
        {
            this.nums = nums;
        }
        public void Update(int i, int val)
        {
            nums[i] = val;   // Update the specific element
        }
        public int SumRange(int i, int j)
        {
            int sum = 0;
            for (int low = i; low <= j; low++) //Iterate from i to j, sum from i to j
                sum += nums[low];
            return sum;
        }
    }

#endregion




#region Naive with Buffer
We could achieve amortized O(1) Update and Query time.  O(N) initial time
We use a extra array to store the sum, so it;s O(N) space


public NumArrBuffer(int[] nums)
        {
            this.nums = nums;
            this.sums = new long[nums.Length +1];
            for (int i = 0; i < nums.Length; i++)
            {
                sums[i + 1] = nums[i] + sums[i];
            }
        }
        public void Update(int i, int val)
        {
           buffer.Add(new Tuple<int, int>(i, val - nums[i]));
            nums[i] = val;
            if (buffer.Count > 300)  //We can set a counter, if greater, we recalculate again.
            {
                for (int j = 0; j < nums.Length; j++)
                {
                    sums[j + 1] = nums[j] + sums[j];
                }
            }
        }
        public int SumRange(int i, int j)
        {
            long res = sums[j + 1] - sums[i];
            foreach (var p in buffer)
            {
                if (p.Item1 <= j && p.Item1 >= i)
                    res += p.Item2;
            }
            if (res >= int.MaxValue || res <= int.MinValue)
                throw new ArgumentOutOfRangeException();
            else
                return (int) res;
        }
    }
#endregion






#region Segment Tree

For solving numerous range query problem, we could consider using Segment Tree.
Segment Tree is a binary tree that each node contains aggregate information for a subrange[i..j] of the array, the left child contains information for [i....(i+j)/2], the right child contains information for range [(i+j)/2....j]

We define the Segment Tree object first:

    public class SegmentTreeNode
    {
        public int start, end;
        public SegmentTreeNode left, right;
        public int sum;
        public SegmentTreeNode(int start, int end)
        {
            this.start = start;
            this.end = end;
            left = null;
            right = null;
            sum = 0;
        }
    }

For Building tree:
we use Top-down approach to build segment tree. We define the node p holds the sum of [i...j], therefore to find the sum of node p, we need to calculate the sum of p's right and left in advance. We begin from leaves, initialize them with input [0....n-1]. Then we move upward to the higher level to calculate the parent's sum till we get to the root of the segment tree.

Time is O(2N), because we calculate the sum of one node during each iteration of the for loop. There are about 2N nodes in the segment tree. => Segment tree for array with n elements has n leaves. The number of nodes in each level is half the number in the level below. => n+n/2+n/4+...1 = 2n. Space is O(2N), we used 2n extra space to store the segment tree.



For Update tree: We start at the root and looking for specific value, When the root move the specific root, we just update the sum(which is the value itself), the whole process took O(logN) time. since we used recursion, there'll be O(h) extra space for recursion call.


For Query: O(logN) time, O(h) space


  public class NumArrayTree
    {
        public SegmentTreeNode root = null;
        public NumArrayTree(int[] nums)
        {
            root = BuildTree(nums, 0, nums.Length - 1);
        }
        //O(2n) algorithm, O(2n) space
        private SegmentTreeNode BuildTree(int[] nums, int start, int end)
        {
            if (start > end) //Check invalid inout
                return null;
            else
            {
                SegmentTreeNode root = new SegmentTreeNode(start, end);
                if (start == end)
                    root.sum = nums[start];
                else
                {
                    int mid = start + (end - start) / 2;
                    root.left = BuildTree(nums, start, mid);
                    root.right = BuildTree(nums, mid + 1, end);
                    root.sum = root.left.sum + root.right.sum;
                }
            }
            return root;
        }
        public void Update(int i, int val)
        {
            Update(root, i, val);
        }
        //O(logN) time, O(h) space
        private void Update(SegmentTreeNode root, int pos, int val)
        {
            if (root.start == root.end)
                root.sum = val;
            else
            {
                int mid = root.start + (root.end - root.start) / 2;
                if (pos <= mid)
                    Update(root.left, pos, val);
                else
                    Update(root.right, pos, val);
                root.sum = root.left.sum + root.right.sum;
            }
        }
        public int SumRange(int i, int j)
        {
            return sumRange(root,i, j);
        }
        //O(logN) time, O(h) space
        private int sumRange(SegmentTreeNode root, int start, int end)
        {
            if (root.end == end && root.start == start)
                return root.sum;
            else
            {
                int mid = root.start + (root.end - root.start) / 2;
                if (end <= mid)   //All in the left
                    return sumRange(root.left, start, end);
                else if(start >= mid + 1)   //In the right
                    return sumRange(root.right, start,end);
                else                //Across mid point
                    return sumRange(root.right, mid + 1, end) + sumRange(root.left, start,mid);
            }
        }
    }

#endregion





#region Binary Index tree
We can make use of a binary index tree to store the sum, for example a given array a[0]...a[7], we use BIT[8] array to represent a tree, where index[2] is the parent of [1] and [3], [6] is the parent of [5] and [7], [4] is parent of [2] and [6], [8] is parent of [4]
          ---------------------8                                     Cover area: -
          ---------4
          ---2        ---6
       ---1     3     5     7
index: 0  1  2  3  4  5  6  7  8 

And BIT[i] = [i] is a left child ? partial sum from its leftmost children to itself   : partial sum from parent(exclusive) to itself.

BIT[1] = a[0],   BIT[2] = a[1] + a[0],      BIT[3] = a[2]
BIT[4] = a[3] + BIT[3] + BIT[2] = a[3] + a[2] + a[1] + a[0]

BIT[6] = a[5] + BIT[5] = a[5] + a[4]
BIT[8] = a[7] + BIT[7] + BIT[6] + BIT[4] = a[7] + a[6] +...a[0]

For Update: if we update a[1] = BIT[2], we shall update BIT[2], BIT[4], BIT[8]
for current index[i], the next update [j] is j = i + (i & -i)   //Double the last 1-bit from [i]

For Query: to get the partial sum up to a[6] = BIT[7], we shall get the sum of BIT[7], BIT[6], BIT[4]
for current index[i], the next summand [j] is j = i - (i & -i)     //Delete the last 1-bit from i

To obtain the original value of a[7] ( which is index 8 of BIT), we have to subtract BIT[7], BIT[6], BIT[4] from BIT[8]
=> starting from [idx - 1], for current i,  the next subtract j is j = i - (i & -i), up to j == idx - (idx & -idx) exclusive. 
    However, a quicker way but using extra space is to store the original array

Build tree: O(N*logN)
query: O(logN)
update: O(logN)
space: O(N)




   public class NumArrayBIT
    {
        private int[] nums;
        private int[] BIT;
        private int n;
        public NumArrayBIT(int[] nums)
        {
            this.nums = nums;
            n = nums.Length;
            BIT = new int[n + 1];
            for(int i = 0; i < n; i++)   //O(N)
                init(i, nums[i]);        //O(logN)
        }
        private void init(int i, int val)  //logN
        {
            i++;
            while (i <= n) 
            {
                BIT[i] += val;
                i += (i & -i); //Least significant bit: get only last 1
            }
        }
        public void Update(int i, int val)
        {
            int diff = val - nums[i];
            nums[i] = val;
            init(i ,diff);
        }
        public int SumRange(int i, int j)
        {
            return getSum(j) - getSum(i - 1);
        }
        private int getSum(int i) //O(logN)
        {
            int sum = 0;
            i++;
            while (i > 0)
            {
                sum += BIT[i];
                i = i - (i & -i);  //Lowbit
            }
            return sum;
        }
    }




#endregion


