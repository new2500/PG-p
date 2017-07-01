Clone Graph (DFS and BFS)
此处皆为无向图；可能需要准备有向图

#region DFS

//We use a hash table to tracking the children node of each node. And we start from the given node, finding the node until it could not be found.


      public GraphNode Clone(GraphNode node)
        {
            if (node == null)
                return null;
            var root = new GraphNode(node.val);
            var dict = new Dictionary<GraphNode, GraphNode>();  //Orginal Node : Copied Node
            dict.Add(node, root);
            helper(node, root, dict);
            return root;
        }
        private void helper(GraphNode node, GraphNode root, Dictionary<GraphNode, GraphNode> dict)
        {
            foreach (var next in node.neighbors)
            {
                if (!dict.ContainsKey(next))
                {
                    var child = new GraphNode(next.val);   //Make a copy
                    root.neighbors.Add(child);             //Add child
                    dict[next] = child;       //Let next linked with copy, like node linked with root
                    helper(next, child, dict); //Let next act like root, keep searching the next's next
                }
                else //If contains that Key, adding a new neighbor
                {
                    root.neighbors.Add(dict[next]);
                }
            }
        }

#endregion




#region BFS
//BFS method, Time complexity is O(VE) in the worst case, V is # of nodes and E is # of edges. Space is O(V) at worst when we need to store all nodes. 

        public GraphNode Clone(GraphNode node)
        {
            if (node == null)
                return null;
            var newNode = new GraphNode(node.val); //New node for return
            var dict = new Dictionary<GraphNode, GraphNode>(); //Orginal Node : Copied Node
            dict.Add(node, newNode);
            Queue<GraphNode> queue = new Queue<GraphNode>(); //Sotre original nodes need to be visited
            queue.Enqueue(node);
            //BFS
            while (queue.Any())
            {
                var curr = queue.Dequeue();    //Pop first
                foreach (GraphNode neighbors in curr.neighbors) //Add neighbors of current orginal node
                {
                    if (!dict.ContainsKey(neighbors))   //if this node hasn't been visited before
                    {
                        dict.Add(neighbors, new GraphNode(neighbors.val));
                        queue.Enqueue(neighbors);
                    }
                    //Add to curr's neighbor list, keep BFS to next level
                    //Add neighbors to the copied node
                    //Copied node: map[curr] -> copied node of curr
                    //Neighbors: map[neighbors] -> copied node of neighbor
                    dict[curr].neighbors.Add(dict[neighbors]);
                }
            }
            return newNode;
        }




#endregion