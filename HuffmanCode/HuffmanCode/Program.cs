namespace HuffmanCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HuffmanCode<int> code = new HuffmanCode<int>();
            (byte[], Node<int>) thing = code.Compress("aaaaaaaaaaaa");
            byte[] array = thing.Item1;
            ;
            string actual = code.Decompress(array, thing.Item2);
            ;
            foreach(var item in array)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(actual);
        }
    }
}