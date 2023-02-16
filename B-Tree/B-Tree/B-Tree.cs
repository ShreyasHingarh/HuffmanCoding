using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    public enum TypeOfNode
    {
        TwoNode,
        ThreeNode,
        FourNode
    }
    public class Node<T>
    {
        public LinkedList<Node<T>> Children;
        public LinkedList<T> Nodes;
        public TypeOfNode Type
        {
            get
            {
                return (TypeOfNode)Nodes.Count - 1;
            }
        }
        public Node(T Value)
        {
            Children = new LinkedList<Node<T>>();
            Nodes = new LinkedList<T>();
            Nodes.AddFirst(Value);
        }
    }
    internal class B_Tree<T> where T : IComparable
    {
        Node<T> Root;
        List<Node<T>> Nodes = new List<Node<T>>();
        
        Stack<Node<T>> StackOfNodes = new Stack<Node<T>>();
        public void AddValue(Node<T> node,T Value)
        {
            if (Value.CompareTo(node.Nodes.Last.Value) > 0)
            {
                node.Nodes.AddLast(Value);
            }
            else if (node.Type != TypeOfNode.TwoNode && Value.CompareTo(node.Nodes.Last.Previous.Value) > 0)
            {
                node.Nodes.AddBefore(node.Nodes.Last, Value);
            }
            else if (node.Type != TypeOfNode.TwoNode && Value.CompareTo(node.Nodes.First.Value) > 0)
            {
                node.Nodes.AddAfter(node.Nodes.First, Value);
            }
            else
            {
                node.Nodes.AddFirst(Value);
            }
        }
        private void Split(Node<T> NodeToSplit, Node<T> parent)
        {

        }
        private Node<T> SearchFor(Node<T> node, T Value)
        {
            Node<T> Parent = node;
            Node<T> current = node;
            if (Value.CompareTo(current.Nodes.Last.Value) > 0)
            {
                current = current.Children.Last();
            }
            else if (current.Type != TypeOfNode.TwoNode && Value.CompareTo(current.Nodes.First()) > 0)
            {
                current = current.Children.First.Next.Value;
            }
            else if (current.Type != TypeOfNode.TwoNode && Value.CompareTo(current.Nodes.Last.Previous) > 0)
            {
                current = current.Children.Last.Previous.Value;
            }
            current = current.Children.First();
            if(current.Type == TypeOfNode.FourNode)
            {
                 Split(current,Parent);
            }
            if (current.Children != null)
            {
                StackOfNodes.Push(current);
                return SearchFor(current, Value);
            }
            return current;
        }
        public void Insert(T Value)
        {
            if(Root == null)
            {
                Root = new Node<T>(Value);
                return;
            }
            StackOfNodes.Push(Root);
            Node<T> temp = SearchFor(Root, Value);
            AddValue(temp,Value);
        }
    }
}
