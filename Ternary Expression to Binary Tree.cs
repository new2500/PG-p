Ternary Expression to Binary Tree

Let's assume the input ternary expression is valid, we can build the tree in preorder manner.
Each time we scan two characters, the first character is either ? or :, the second character holds the value of the tree node. When we see ?, we add the new node to left. When we see :, we need to find out the ancestor node that doesn't have a right node, and make the new node as its right child.



#region  Niave, can't handle brackets
Requirement:
1. 不能有括号
2. Input string format, leading, trailing, space between characters
3. Input string必须有效。
4. Token's 长度必须是1， 不能有"a>0 ? b:c" 这种情况

      public TreeNode converToBinaryTree(string input)
        {
            if (input.Length < 1)
            {
                return null;
            }
            input = input.Replace(" ", "");    //Elimate the white space
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode root = new TreeNode(input[0]);   //Set root
            stack.Push(root);
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == '?')  //We could find left child
                {
                    TreeNode node = stack.Peek();
                    node.left = new TreeNode(input[i+1]);
                    stack.Push(node.left);
                }
                else if (input[i] == ':') //Right child
                {
                    stack.Pop();
                    TreeNode node = stack.Pop();
                    node.right = new TreeNode(input[i + 1]);
                    stack.Push(node.right);
                }
            }
            return root;
        }
#endregion




#region Can handle brackets
//Worst case : a ? (...) : b => O(2^n) time

  public TreeNode converToBinaryTree(string input)
        {
            if (input.Length < 1)
            {
                return null;
            }
            input = input.Replace(" ", "");    //Elimate the white space
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode root = new TreeNode(input[0]);   //Set root
            stack.Push(root);
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == '?')  //We could find left child
                {
                    TreeNode node = stack.Peek();
                    int j = i + 1;
                    while (!char.IsDigit(input[j]) || !char.IsLetter(input[j]))////Make sure next node is valid
                    {
                        j++;
                    }
                    node.left = new TreeNode(input[j]);
                    stack.Push(node.left);
                }
                else if (input[i] == ':') //Right child
                {
                    stack.Pop();
                    TreeNode node = stack.Pop();
                    int j = i + 1;
                    while (!char.IsDigit(input[j]) || !char.IsLetter(input[j])) //Make sure next node is valid
                    {
                        j++;
                    }
                    node.right = new TreeNode(input[j]);
                    stack.Push(node.right);
                }
            }
            return root;
        }


#endregion



#region One-pass O(N) time, O(2N) space. 

    publicTreeNode ConvertTenary(string s)
        {
            var dict = new Dictionary<int, TreeNode>();
            for (int i = 0; i < s.Length; i = i + 2)
                dict.Add(i, new TreeNode(s[i]));
            int j = 1, n = s.Length;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            while (j < n)
            {
                if (j < n && s[j] == '?') //Meet '?'
                {
                    TreeNode p = dict[j - 1];  //find root
                    if (j - 2 > -1 && stack.Any() && s[j - 2] != ':')
                        stack.Peek().left = p;  //find left
                    stack.Push(p);
                    j = j + 2;
                }
                else
                {
                    TreeNode top = stack.Pop();
                    TreeNode right = dict[j + 1];
                    top.right = right;
                    if (j - 2 > -1 && s[j - 2] != ':')
                    {
                        TreeNode left = dict[j - 1];
                        top.left = left;
                    }
                    j = j + 2;
                }
            }
            return dict[0];
        }
#endregion





#region Bonus helper method: Print tree line by line

  private static void printTreeLevel(TreeNode root)
        {
            //Base
            if (root == null)
                return;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Any())
            {
                int nodeCount = queue.Count;
                if (nodeCount == 0)
                    break;
                while (nodeCount > 0)
                {
                    TreeNode node = queue.Dequeue();
                    Console.Write((char)node.val + ",");
                    if (node.left != null)
                        queue.Enqueue(node.left);
                    if (node.right != null)
                        queue.Enqueue(node.right);
                    nodeCount--;
                }
                Console.WriteLine();
            }
            return;
        }

#end