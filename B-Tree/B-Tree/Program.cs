namespace B_Tree
{
    internal class Program
    {
        public static B_Tree<int> tree = new B_Tree<int>();

       
        static void Main(string[] args)
        {            
            for(int i = 0;i < 10;i++)
            {
                tree.Insert(1);
            }
            ;
        }
    }
}