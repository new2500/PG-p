Inorder Successor(Predecessor) in BST

Since in-order traversal of a BST is the nodes in ascending order. To find a successor, just need to find the smallest one that is larger then the give node vale since there are no duplicate value in a BST.  successpr is a pointer that keeps the possible successor. Whenever you go left the current root is the new possible successor, otherwise the it remains the same.


#region With Value and Root
//Best algorithm -> O(logN) time, only when the BST is a linear, it comes to O(N) time, O(1) space.

//O(h) time; in a balanced BST, h = logN, worst case h = N; O(1) space
        public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
        {
            TreeNode successor = null;
            while (root != null)
            {
                if (p.val < root.val)
                {
                    successor = root;
                    root = root.left;
                }
                else
                    root = root.right;
            }
            return successor;
        }
#end


#region Follow up: If ASK FOR the PREDECESSOR. O(logN) time, O(logN) space

        public TreeNode predecessor(TreeNode root, TreeNode p)
        {
            if (root == null)
                return null;
            if (root.val >= p.val)
            {
                return predecessor(root.left, p);
            }
            else
            {
                TreeNode right = predecessor(root.right, p);
                return (right != null) ? right : root;
            }
        }
#end