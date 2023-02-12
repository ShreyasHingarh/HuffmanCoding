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
        public Node<T> LocatePositionOfNewNode(T Value)
        {
            Node<T> Current = Root;
            while(Current.Children.Count != 0)
            {
                if (Value.CompareTo(Current.Nodes.Last.Value) > 0)
                {
                    Current = Current.Children.Last();
                    continue;
                } 
                if(Current.Type != TypeOfNode.TwoNode && Value.CompareTo(Current.Nodes.First()) > 0)
                {
                    Current = Current.Children.First.Next.Value;
                    
                    continue;
                }
                if (Current.Type == TypeOfNode.FourNode && Value.CompareTo(Current.Nodes.Last.Previous) > 0)
                {
                    Current = Current.Children.Last.Previous.Value;
                    continue;
                }
                Current = Current.Children.First();
                 
            }

            return Current;
        }
        public bool AddValue(Node<T> node,T Value)
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
            return node.Type == TypeOfNode.FourNode;
        }

        public void ActualSplit()
        {
            
        }
        private Node<T> Split(Node<T> node)
        {
        }
        public void Insert(T Value)
        {
            if(Root == null)
            {
                Root = new Node<T>(Value);
                return;
            }
            //locate where to add value
            //add it 
            //split to make balanced
        }
    }
}
