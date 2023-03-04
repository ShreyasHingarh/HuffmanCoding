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
        public LinkedList<(T,int)> Nodes;
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
            Nodes = new LinkedList<(T, int)>();
            Nodes.AddFirst(new LinkedListNode<(T,int)>((Value, 1)));
        }
    }
    internal class B_Tree<T> where T : IComparable
    {
        public Node<T> Root;
        private void AddValue(Node<T> node,T Value)
        {
            if (Value.CompareTo(node.Nodes.Last.Value) > 0)
            {
                node.Nodes.AddLast(new LinkedListNode<(T, int)>((Value, 1)));
            }
            else if (node.Type != TypeOfNode.TwoNode && Value.CompareTo(node.Nodes.Last.Previous.Value) > 0)
            {
                node.Nodes.AddBefore(node.Nodes.Last, new LinkedListNode<(T, int)>((Value, 1)));
            }
            else if (node.Type != TypeOfNode.TwoNode && Value.CompareTo(node.Nodes.First.Value) > 0)
            {
                node.Nodes.AddAfter(node.Nodes.First, new LinkedListNode<(T, int)>((Value, 1)));
            }
            else
            {
                node.Nodes.AddFirst(new LinkedListNode<(T, int)>((Value, 1)));
            }
        }

        public void AddAndRemoveChildren(Node<T> parentChild, LinkedList<Node<T>> OldChildren)
        {
            Node<T> temp = OldChildren.First.Value;
            OldChildren.Remove(OldChildren.First);
            parentChild.Children.AddLast(temp);
            
        }
        public void AddChildren(LinkedList<Node<T>> ParentsChildren, LinkedList<Node<T>> OldChildren)
        {
            foreach (var parentChild in ParentsChildren)
            {
                if (parentChild.Children.Count != 0) continue;
                AddAndRemoveChildren(parentChild, OldChildren);
                AddAndRemoveChildren(parentChild, OldChildren);
            }

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
            (T,int) theMiddle = NodeToSplit.Nodes.First.Next.Value; 
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

                if(OldChildren.Count != 0)
                {
                    AddChildren(parent.Children, OldChildren);
                }

                Root = NodeToSplit;
                return;
            }
            NodeToSplit.Nodes.Remove(theMiddle);
            AddValue(parent, theMiddle); 
            //Splitting the 'a' node from the 'c' node in an 'abc' node key thing
            (T,int) thing = NodeToSplit.Nodes.First();
            NodeToSplit.Nodes.Remove(NodeToSplit.Nodes.First);
            //Adding the removed 'a' node
            parent.Children.AddBefore(parent.Children.Last, new Node<T>(thing));
            // this won't work, would asign 4 children to one node.
            if(OldChildren.Count != 0)
            {
                AddChildren(parent.Children, OldChildren);
            }

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
