namespace OOP
{
    #region Generic Binary Tree
    public class Node<T>
    {
        //Private attributes
        private T _node = default(T);
        private Node<T> _rightChild = default(Node<T>);
        private Node<T> _leftChild = default(Node<T>);
        //Constructor
        public Node()
        {
            _node = default(T);
            LeftChild = null;
            _rightChild = null;
        }
        public Node(T val)
        {
            _node = val;
            LeftChild = null;
            _rightChild = null;
        }
        //Accessors
        public T Value { get; set; }
        public Node<T> LeftChild
        {
            get { return _leftChild; }
            set { _leftChild = value; }
        }
        public Node<T> RightChild
        {
            get { return _rightChild; }
            set { _rightChild = value; }
        }
        #region Methods
        //Return total element of trees
        public int Count()//Should call the private method, but here we implement the whole logic.
        {
            return 1 +
                (_leftChild != null ? _leftChild.Count() : 0) +
                (_rightChild != null ? _rightChild.Count() : 0)
            ;
            //Or just 1 line...
            // return 1 + (_leftChild?.Count() ?? 0) + (_rightChild?.Count() ?? 0);
        }
        //Enumerate the Nodes
        public IEnumerable<T> EnumerateNodes()
        {
            yield return _node;
            if (_leftChild != null)
                foreach (T child in _leftChild.EnumerateNodes())
                {
                    yield return child;
                }
            if (_rightChild != null)
                foreach (T child in _rightChild.EnumerateNodes())
                {
                    yield return child;
                }
        }
        #endregion
    }
    #endregion
    public class MainEntry
    {
        public void Print<T>(Node<T> root)   //Print 1,2,null,null,3,4,null
        {
            if (root == null)
            {
                Console.Write("null, ");
                return;
            }
            Console.Write(root.Value + ",");
            Print(root.LeftChild);
            Print(root.RightChild);
        }
        private static void Main()
        {
            Node<char> root = new Node<char>('a');
            root.LeftChild = new Node<char>('b');
            root.RightChild = new Node<char>('c');
            root.RightChild.LeftChild = new Node<char>('d');
           
            Console.ReadKey();
        }
    }
}
