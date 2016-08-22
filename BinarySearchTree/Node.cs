using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Node<T> where T : IComparable
    {
        public T data;
        public Node<T> leftChild;
        public Node<T> rightChild;
        public Node<T> parent;
        public Node(T data)
        {
            this.data = data;
        }
    }
}
