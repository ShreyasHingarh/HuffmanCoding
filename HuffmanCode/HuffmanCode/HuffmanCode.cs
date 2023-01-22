using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace HuffmanCode
{
    public class Node<T>
    {
        public int Frequency;
        public char Letter;
        public Node<T> Right;
        public Node<T> Left;
        public Node(int frequency, char letter, Node<T> right, Node<T> left)
        {
            Frequency = frequency;
            Letter = letter;
            Right = right;
            Left = left;
        }
    }
    internal class HuffmanCode<T>
    {
        public PriorityQueue<Node<T>, int> Queue = new PriorityQueue<Node<T>, int>();

        public void Traverse()
        {

            // traverse tree left path = 0 and right path = 1
        }
        public List<Node<T>> GetFrequency(string Text)
        {
            List<Node<T>> nodes = new List<Node<T>>();
            for (int i = 0; i < Text.Length; i++)
            {
                bool didAdd = false;
                foreach (var node in nodes)
                {
                    if (node.Letter == Text[i])
                    {
                        didAdd = true;
                        node.Frequency++;
                        break;
                    }
                }
                if (!didAdd)
                {
                    nodes.Add(new Node<T>(1, Text[i], null, null));
                }
            }
            return nodes;

        }

        public byte[] copyArray(byte newNum, byte[] tempByte)
        {
            byte[] newByte = new byte[tempByte.Length + 1];
            int index = 0;
            foreach (var thing in tempByte)
            {
                newByte[index] = tempByte[index];
                index++;
            }
            newByte[tempByte.Length] = newNum;
            return newByte;
        }
        public Dictionary<char, byte[]> BinaryStringTraversal(Node<T> Root)
        {
            if (Root == null || (Root.Right == null && Root.Left == null))
            {
                return null;
            }
            Stack<Node<T>> Nodes = new Stack<Node<T>>();
            Stack<byte[]> Bytes = new Stack<byte[]>();
            Dictionary<Node<T>, byte[]> Dictionary = new Dictionary<Node<T>, byte[]>(); 
            Nodes.Push(Root);
            Bytes.Push(new byte[0] );
            while(Nodes.Count != 0)
            {
                Node<T> temp = Nodes.Pop();
                byte[] tempByte = Bytes.Pop();
                Dictionary.Add(temp, tempByte);
                if (temp.Left != null)
                {
                    Nodes.Push(temp.Left);
                    Bytes.Push(copyArray(0,tempByte));
                }
                if(temp.Right != null)
                {
                    Nodes.Push(temp.Right);
                    Bytes.Push(copyArray(1, tempByte));
                }
                
            }
            Dictionary<char, byte[]> actual = new Dictionary<char, byte[]>();
            foreach(var thing in Dictionary)
            {
                if (thing.Key.Letter == '$') continue;
                actual.Add(thing.Key.Letter, thing.Value);
            }
            return Dictionary;
        }
        public Node<T> ConstructTree(List<Node<T>> node)
        {
            foreach (var item in node)
            {
                Queue.Enqueue(item, item.Frequency);
            }
            while (Queue.Count != 1)
            {
                Node<T> temp1 = Queue.Dequeue();
                Node<T> temp2 = Queue.Dequeue();
                Node<T> Parent = new Node<T>(temp1.Frequency + temp2.Frequency, '$', temp1, temp2);
                Queue.Enqueue(Parent, Parent.Frequency);
            }
            return Queue.Dequeue();
        }
        public byte[] Compress(string Text)
        {         
            //find how many of each letter is in the text
            List<Node<T>> node = GetFrequency(Text);
            //construct tree
            Node<T> Root = ConstructTree(node);
            //Traverse 
            Dictionary<Node<T>, byte[]> dictionary = BinaryStringTraversal(Root);
            //Encode
            byte[] array = new byte[1];
            for(int i = 0;i < Text.Length;i++)
            {
                dictionary[Text[]]
            }
        }
        public string Decompress(byte[] code)
        {
            return "";
        }
    }
}
