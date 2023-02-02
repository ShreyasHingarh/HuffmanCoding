namespace HuffmanCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HuffmanCode<int> code = new HuffmanCode<int>();
            (byte[], Node<int>) thing = code.Compress("hello my name is shreyas");
            byte[] array = thing.Item1;
            ;
            string actual = code.Decompress(array, thing.Item2);
            ;
            //string Hello = code.Decompress(thing.Item1,thing.Item2);
        }
    }
}