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
        public Node<T> Root;
        private void AddValue(Node<T> node,T Value)
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
        
        public void AddChildren(Node<T> NodeToSplit,LinkedList<Node<T>> OldChildren)
        {
            if (OldChildren.Count == 0) return;
            NodeToSplit.Children.First.Value.Children.AddFirst(OldChildren.First.Value);
            NodeToSplit.Children.First.Value.Children.AddLast(OldChildren.First.Next.Value);

            NodeToSplit.Children.Last.Value.Children.AddFirst(OldChildren.Last.Previous.Value);
            NodeToSplit.Children.Last.Value.Children.AddLast(OldChildren.Last.Value);
        }
        LinkedList<Node<T>> Clone(LinkedList<Node<T>> nodes)
        {
            LinkedList<Node<T>> temp = new LinkedList<Node<T>>();
            foreach(var thing in nodes)
            {
                temp.AddLast(thing);
            }
            return temp;
        }
        private void Split(Node<T> NodeToSplit, Node<T> parent)
        {
            T theMiddle = NodeToSplit.Nodes.First.Next.Value; 
            LinkedList<Node<T>> OldChildren = Clone(NodeToSplit.Children);
            NodeToSplit.Children.Clear();
            if (NodeToSplit == parent)
            {
                //move up middle 
                // make the other two previous roots children of the new root
                // make the previous children of the old root children of the new children
                
                
                T a = NodeToSplit.Nodes.First.Value;
                NodeToSplit.Nodes.RemoveFirst();
                NodeToSplit.Children.AddFirst(new Node<T>(a));

                T b = NodeToSplit.Nodes.Last.Value;
                NodeToSplit.Nodes.RemoveLast();
                NodeToSplit.Children.AddLast(new Node<T>(b));

                AddChildren(NodeToSplit, OldChildren);

                Root = NodeToSplit;
                return;
            }
            NodeToSplit.Nodes.Remove(theMiddle);
            AddValue(parent, theMiddle); 
            //Splitting the 'a' node from the 'c' node in an 'abc' node key thing
            T thing = NodeToSplit.Nodes.First();
            NodeToSplit.Nodes.Remove(NodeToSplit.Nodes.First);
            //Adding the removed 'a' node
            parent.Children.AddBefore(parent.Children.Last, new Node<T>(thing));
            AddChildren(NodeToSplit, OldChildren);
        }
        private Node<T> SearchFor(Node<T> node, T Value)
        {
            Node<T> Parent = node;
            Node<T> current = node;
            if (node == Root && current.Type == TypeOfNode.FourNode)
            {
                Split(current, Parent);
            }
            if ( current.Children.Count != 0)
            {
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
                else
                {
                    current = current.Children.First();

                }
                if (current.Type == TypeOfNode.FourNode)
                {
                    Split(current, Parent);
                }
                if (current.Children != null)
                {
                    return SearchFor(current, Value);
                }
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
            
            Node<T> temp = SearchFor(Root, Value);
            AddValue(temp,Value);
        }
    }
}
