namespace HuffmanCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HuffmanCode<int> code = new HuffmanCode<int>();

            List<Node<int>> nodes = code.GetFrequency("hi my name is bob and i like pizza");
        }
    }
}