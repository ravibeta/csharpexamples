using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWT
{
    public class BinaryTreeNode<T>
    {
        public BinaryTreeNode(T data) : this(data, null, null) { }
        public BinaryTreeNode(T data, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
        {
            this.Data = data;
            this.Left = left;
            this.Right = right;
        }

        public T Data { get; set; }
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var root = new BinaryTreeNode<int>(1,
                new BinaryTreeNode<int>(2, null, new BinaryTreeNode<int>(4)),
                new BinaryTreeNode<int>(3, new BinaryTreeNode<int>(5), null));
            PrintBreadthNodes(root);
        }

        static void PrintBreadthNodes(BinaryTreeNode<int> root)
        {
            if (root == null) return;
            var list = new List<BinaryTreeNode<int>>() { null };
            list.Add(root);
            while (list.Count > 0)
            {
                var item = list.First();
                list.RemoveAt(0);
                if (item != null)
                {
                    Console.Write(item.Data + " ");
                    if (item.Left != null) list.Add(item.Left);
                    if (item.Right != null) list.Add(item.Right);
                }
                else
                {
                    if (list.Count == 0) return;
                    list.Add(null);
                    Console.WriteLine();
                }
            }
        }
    }
}
