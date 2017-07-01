迷宫&钥匙

We use DFS to find the valid path, and we set two global variables:  minCount -> set to int.MaxValue and a minPath, which store the shortest path to destination.
A hashSet to store the key, if we meet a lowercase char, we add into the keySet; //If used, we remove that key from the keySet


public class Solution
    {
        public List<Node> minPath = new List<Node>();
        public int minCount = int.MaxValue;
        public List<Node> ShortPath(char[,] board)
        {
            int startX = 0;
            int startY = 1;
            int count = 0;
            var keySet = new HashSet<char>();
            bool[,] visited = new bool[board.GetLength(0), board.GetLength(1)];
            var path = new List<Node>();
            path.Add(new Node(startX, startY));
            DFS(board, startX, startY, count, keySet, path, visited);
            return minPath;
        }
        public void DFS(char[,] board, int x, int y, int count, HashSet<char> keySet, List<Node> path, bool[,] visited)
        {
            char curr = board[x, y];
            if (board[x, y] == '3') // Reacg DES
            {
                if (count <= minCount)
                {
                    minCount = count;
                    minPath = new List<Node>(path);
                }
                return;
            }
            if (board[x, y] >= 'A' && board[x, y] <= 'Z') //door
            {
                char smallCaseKey = (char)(board[x, y] - 'A' + 'a');
                if (!keySet.Contains(smallCaseKey))
                    return;
                keySet.Remove(smallCaseKey);  //Delete if key is ont-time used
            }
            if (board[x, y] >= 'a' && board[x, y] <= 'z') //key
            {
                keySet.Add(board[x, y]);
            }
            int[] dx = { 0, 0, 1, -1 };
            int[] dy = { 1, -1, 0, 0 };
            int m = board.GetLength(0);
            int n = board.GetLength(1);
            for (int i = 0; i < 4; i++)
            {
                int nextX = x + dx[i];
                int nextY = y + dy[i];
                if (nextX >= 0 && nextX < m && nextY >= 0 && nextY < n && !visited[nextX, nextY] && board[nextX, nextY] != '0')
                {
                    visited[nextX, nextY] = true;
                    path.Add(new Node(nextX, nextY));
                    DFS(board, nextX, nextY, count + 1, keySet, path, visited);
                    path.RemoveAt(path.Count - 1);
                    visited[nextX, nextY] = false;
                }
            }
        }
        private static void Main()
        {
            Solution sss = new Solution();
            char[,] map = {
                 {'0','2','1','1','1'},
                 {'0','1','0','0','1'},
                 {'0','1','0','0','3'},
                 {'0','b','0','0','1'},
                 {'0','1','1','1','1'}
               };
            List<Node> res = sss.ShortPath(map);
            foreach (var node in res)
            {
                Console.WriteLine("(" + node.x + ", " + node.y + ")");
            }
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }

