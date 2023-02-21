namespace B_Tree
{
    internal class Program
    {
        public static B_Tree<char> tree = new B_Tree<char>();

        public Node<char> root
        {
            get
            {
                return tree.Root;
            }
        }
        static void Main(string[] args)
        {            
            for(int i = 0;i < 26;i++)
            {
                char thing = (char)(65 + i);
                tree.Insert(thing);
            }
        }
    }
}