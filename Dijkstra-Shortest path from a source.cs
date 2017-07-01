Dijkstra: Shortest path from a source

If Given is a distance[], source and total vertices's

Time complexity: Since for each vertex, we each to looking for the min distance, so total time is O(V^2). Can't handle negative weights


       public void Dijkstra(int[,] graph, int source, int verticesCount)
        {
            int[] distance = new int[verticesCount];     //Output, distance from source to each vertic
            bool[] shortestPathSet = new bool[verticesCount];   //falg to mark shortest, true if vertix i is in the shortest path
            //Initialize all distance as Infinite and false
            for (int i = 0; i < verticesCount; i++)
            {
                distance[i] = int.MaxValue;
                shortestPathSet[i] = false;
            }
            distance[source] = 0;  //Source to Source always 0
            //Find shortest path for all vertices
            for (int count = 0; count < verticesCount - 1; count++)
            {
                //Pick the minimum distance vertrx from the set of shortestPath set. u is always equal to src in the first iteration.
                int u = MinDistance(distance, shortestPathSet, verticesCount);
                //Inlcude the picked vertex as processed
                shortestPathSet[u] = true;
                //Update distance[v], Only if
                for (int v = 0; v < verticesCount; v++)
                {
                    if (!shortestPathSet[v]                 //Not in the shortestPath    
                        && Convert.ToBoolean(graph[u, v])   //Exist edge between from u to v
                        && distance[u] != int.MaxValue      //Check connect
                        && distance[u] + graph[u, v] < distance[v]) //Path from src to v through u < current value of dis[v] : from src to v
                    {
                        distance[v] = distance[u] + graph[u, v];
                    }
                }
            }
            Print(distance, verticesCount);
        }
        //Find the vertex with minimum distance value, from the set of vertics not yet included in the shortest path
        private int MinDistance(int[] distance, bool[] shortestPathSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIdx = 0;
            for (int v = 0; v < verticesCount; v++)
            {
                if (shortestPathSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIdx = v;
                }
            }
            return minIdx;
        }
        private void Print(int[] distance, int verticeCount)
        {
            Console.WriteLine("Vertex        Distance from source");
            for(int i = 0; i < verticeCount; i++)
                Console.WriteLine("{0}\t   {1}", i, distance[i]);
        }

