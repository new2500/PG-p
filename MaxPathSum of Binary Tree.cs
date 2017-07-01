MaxPathSum of Binary Tree

#region DFS, recursive
//DFS recursive
        private int maxSum = int.MinValue;

        public int MaxPathSum(TreeNode root)
        {
            helper(root, 0);
            return maxSum;
        }

        public void helper(TreeNode root, int sum)
        {
            if (root != null)
            {
                sum = sum + root.val;
                if (sum > maxSum && root.left == null && root.right == null)
                {
                    maxSum = sum;   //maxLeaf = root
                }

                helper(root.left, sum);
                helper(root.right, sum);
            }
        }
#endregion		


#region If ask to print the path		
// If asking for print the Path rather than the maxSum number		
      private int maxSum = int.MinValue;
        private TreeNode maxLeaf = null;

        public List<TreeNode> MaxPathSum(TreeNode root)
        {
            List<TreeNode> res = new List<TreeNode>();
            helper(root, 0);
            path(root, maxLeaf, res);
           // res.Reverse();   //If require put the root on front, we need to reverse
            return res;   
        }
        public void helper(TreeNode root, int sum)
        {
            if (root != null)
            {
                sum = sum + root.val;
                if (sum > maxSum && root.left == null && root.right == null)
                {
                    maxSum = sum; 
                    maxLeaf = root;
                }
                helper(root.left, sum);
                helper(root.right, sum);
            }
        }
        public bool path(TreeNode root, TreeNode leaf, List<TreeNode> res)
        {
            if (root == null)
                return false;
            if ((root == leaf) || path(root.left, leaf,res) || path(root.right, leaf, res))
            {
                res.Add(root);
                return true;
            }
            return false;
        }



		
#endregion

