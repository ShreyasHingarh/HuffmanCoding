using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HuffmanCode
{
    public class Node<T>
    {
        public int Frequency;
        public char Letter;
        public Node(int frequency, char letter)
        {
            Frequency = frequency;
            Letter = letter;
        }
    }
    internal class HuffmanCode<T>
    {
        public PriorityQueue<Node<T>,int> Queue = new PriorityQueue<Node<T>,int>();
        
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
                    nodes.Add(new Node<T>(1, Text[i]));
                }
            }
            return nodes;
        }

        public string Compress(string Text)
        {
            //find how many of each letter is in the text
            //construct queue
            //popping two nodes at a time and create a parent node which holds a left and right child and the combined weight of the two nodes and reinsert it into the Priority Queue.
            // when queue.count == 1 the last node is a root
            // when node reached has no character is mapped to binary string made from the path return the tree
            return "";
        }
        public string Decompress()
        {
            return "";
        }
    }
}
