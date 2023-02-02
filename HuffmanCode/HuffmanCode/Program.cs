namespace HuffmanCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HuffmanCode<int> code = new HuffmanCode<int>();

            byte[] array = code.Compress("hello");
            ;
            string thing = code.Decompress(array);
            ;
            //string Hello = code.Decompress(thing.Item1,thing.Item2);
        }
    }
}