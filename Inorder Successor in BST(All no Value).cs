Inorder Successor with Parent no Value

Say the node for whose inorder suc­ces­sor needs to be find is p.

Case 1 : If the p has a right child then its inorder suc­ces­sor will the left most ele­ment in the right sub tree of p.

Case 2: If the p doesnt have a right child then its inorder suc­ces­sor will the one of its ances­tors, using par­ent link keep trav­el­ing up till you get the node which is the left child of its par­ent. Then this par­ent node will be the inorder successor.

Case 3: if p is the right most node in the tree then its inorder suc­ces­sor will be NULL.

#region Inorder Successor
Time complexity: O(h) - h - height of the tree
public TreeNode InorderSuccessor( TreeNode p)
        {
            //If p has a right child then its inorder successor will the leftmost node of p's right-subtree.
            if (p.right != null)
            {
                return leftMostNode(p.right);
            }
            //If p dosn't have right child then its inorder successor will be one of p's ancestors, using parent link keep traveling up untill you get the node which is the left child of its parent. Then this parent node will be the inorder successor
            TreeNode parent = p.parent;
            while (parent != null && p == parent.right)//We are not root && we are the "right" child
            {
                p = parent;                  
                parent = parent.parent;  //Return the ancestor, go up
            }
            return parent;
        }

        public TreeNode leftMostNode(TreeNode x)
        {
            while (x.left != null)
                x = x.left;
            return x;
        }
#end


//  Follow up: Find Successor, no parent point, no value to compare, but give the root of tree
#region Find Successor, Brute Force for case 2

There are two cases for the node.
1) if the node has right child. Then the successor would be the leftmost node of its right subtree.
2) if the node has no right child. Then the successor would be the lowest node whose left subtree contains the node.

publicTreeNode InorderSuccessor(TreeNode root, TreeNode p)
        {
            if (p.right != null) //Leftmost of its right child
            {
                return leftMostNode(p.right);
            }
            //2) if the p has no right child. Then the successor would be the lowest node whose left subtree contains the node.
            //Since it's hard to do that, we brute force travel the whole list and find p's next
            List<TreeNode> inorderList = new List<TreeNode>();
            InorderTrave(root,inorderList);    //O(N)
            for (int i = 0; i < inorderList.Count; i++)
            {
                if(inorderList[i] == p && i+1 < inorderList.Count - 1)
                    return inorderList[i+1];
            }
            //Not exist
            return null;
        }
        public TreeNode leftMostNode(TreeNode x)
        {
            while (x.left != null)
                x = x.left;
            return x;
        }
        public void InorderTrave(TreeNode root, List<TreeNode> inorderList)
        {
            if (root == null)
                return;
            InorderTrave(root.left, inorderList);
            inorderList.Add(root);
            InorderTrave(root.right, inorderList);
        }
#end




#region Find Successor, no parent point, no value; but given root
//第二个使用了DFS Backtracking, 来寻找p到root的路径， 而且修改了找不到返回本身， 如果需要返回Null的话， 删掉temp,。总体来说跟前面差不多，少许少了些无用功   
     publicTreeNode InorderSuccessorWithRoot(TreeNode root, TreeNode p)
        {
            TreeNode temp = p;
            if (p != null && p.right != null) //Leftmost of its right child
            {
                return leftMostNode(p.right);
            }
            //2) if the p has no right child. Then the successor would be the lowest node whose left subtree contains the node.
            //We need to backtrcking to get the p's ancestors
            List<TreeNode> AncestorsList = GetAncestors(root, p);
            int i = 2;  //left & right
            while (AncestorsList.Count - i > -1)
            {
                TreeNode parent = AncestorsList[AncestorsList.Count - i];
                if(parent.left == p)
                    return parent;
                p = parent;
                i++;
            }
            return temp;
        }
        public TreeNode leftMostNode(TreeNode x)
        {
            while (x.left != null)
                x = x.left;
            return x;
        }
        private List<TreeNode> GetAncestors(TreeNode root, TreeNode p)
        {
            List<TreeNode> res = new List<TreeNode>();
           dfs(root, p, new List<TreeNode>(), res);
            return res;
        }
        private void dfs(TreeNode root, TreeNode p, List<TreeNode> list, List<TreeNode> res)
        {
            //Exit
            if (root == null)
                return;
            list.Add(root);
            if (root == p)
            {
                res.AddRange(new List<TreeNode>(list));
                return;
            }
            dfs(root.left, p, list, res);
            dfs(root.right, p, list, res);
            list.RemoveAt(list.Count - 1);
        }
#end




#region Find predecessor with parent, no root

//The predecessor of a given node is the node containing the next smaller value. Finding the predecessor follows symmetric rules that we used for finding the successor.

        public TreeNode InorderPredcessor(TreeNode p)
        {
            if (p == null)
                return null;
            //if p have left child, the predcessor is the rightmost of its left-subtree.
            if (p.left != null)
            {
                return rightMostNode(p.left); 
            }
            TreeNode parent = p.parent;
            TreeNode curr = p;
            while (parent != null && curr == parent.left)
            {
                curr = parent;
                parent = parent.parent;
            }
            return parent;
        }
        public TreeNode rightMostNode(TreeNode root)
        {
            if (root == null)
                return null;
            if (root.right != null)
                root = root.right;
            return root;
        }

#end