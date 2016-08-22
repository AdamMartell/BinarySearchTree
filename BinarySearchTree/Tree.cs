using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class Tree<T> where T : IComparable
    {
        public Node<T> HeadNode;
        public Tree(T data)
        {
            HeadNode = new Node<T>(data);
        }
        public void Add(T data)
        {
            Node<T> currentNode = HeadNode;
            Node<T> nodeToPlace = new Node<T>(data);
            bool isPlaced = false;
            while (!isPlaced)
            {
                if(currentNode.data.CompareTo(nodeToPlace.data) > 0 && currentNode.leftChild != null)
                {
                    currentNode = currentNode.leftChild;
                }
                if (currentNode.data.CompareTo(nodeToPlace.data) < 0 && currentNode.rightChild != null)
                {
                    currentNode = currentNode.rightChild;
                }
                if (currentNode.data.CompareTo(nodeToPlace.data) > 0 && currentNode.leftChild == null)
                {
                    currentNode.leftChild = nodeToPlace;
                    nodeToPlace.parent = currentNode;
                    isPlaced = true;
                }
                if (currentNode.data.CompareTo(nodeToPlace.data) < 0 && currentNode.rightChild == null)
                {
                    currentNode.rightChild = nodeToPlace;
                    nodeToPlace.parent = currentNode;
                    isPlaced = true;
                }
                if(currentNode.data.CompareTo(nodeToPlace.data) == 0)
                {
                    currentNode = nodeToPlace;
                    nodeToPlace.parent = currentNode.parent;
                    isPlaced = true;
                }
            }
        }

        public void Search(T searchItem)
        {
            bool isFound = false;
            Node<T> currentNode = HeadNode;
            StringBuilder pathToItem = new StringBuilder("root");
            while (!isFound)
            {
                if(currentNode.data.CompareTo(searchItem) == 0)
                {
                    isFound = true;
                }
                else if (currentNode.data.CompareTo(searchItem) > 0 && currentNode.leftChild != null)
                {
                    currentNode = currentNode.leftChild;
                    pathToItem.Append(@"\Left");
                }
                else if (currentNode.data.CompareTo(searchItem) < 0 && currentNode.rightChild != null)
                {
                    currentNode = currentNode.rightChild;
                    pathToItem.Append(@"\Right");
                }
                else
                {
                    isFound = true;
                    pathToItem = new StringBuilder("no node matches the specified search");
                }
            }
            Console.WriteLine(pathToItem);
        }
        public void Balance()
        {
            List<T> treeList = toList();
            Tree<T> newTree = new Tree<T>(treeList[(treeList.Count/2)]);
            reconstruct(treeList.Where(x => treeList.IndexOf(x) < (treeList.Count / 2)).ToList(), newTree);
            reconstruct(treeList.Where(x => treeList.IndexOf(x) > (treeList.Count / 2)).ToList(), newTree);
            HeadNode = newTree.HeadNode;
        }
        private void reconstruct(List<T> list, Tree<T> tree)
        {
            if(list.Count > 0)
            {
                tree.Add(list[(list.Count / 2)]);
                reconstruct(list.Where(x => list.IndexOf(x) < (list.Count / 2)).ToList(), tree);
                reconstruct(list.Where(x => list.IndexOf(x) > (list.Count / 2)).ToList(), tree);
            }
        }
        public List<T> toList()
        {
            List<T> treeList = new List<T>();
            Node<T> currentNode = HeadNode;
            treeList.Add(currentNode.data);
            while (true)
            {
                if((currentNode == HeadNode) && (currentNode.rightChild == null ? true : treeList.Contains(currentNode.rightChild.data)) && (currentNode.leftChild == null ? true : treeList.Contains(currentNode.leftChild.data)))
                {
                    break;
                }
                else if(treeList.Contains(currentNode.data) && currentNode.leftChild != null && !treeList.Contains(currentNode.leftChild.data))
                {
                    currentNode = currentNode.leftChild;
                    treeList.Add(currentNode.data);
                }
                else if (treeList.Contains(currentNode.data) && currentNode.rightChild != null && !treeList.Contains(currentNode.rightChild.data))
                {
                    currentNode = currentNode.rightChild;
                    treeList.Add(currentNode.data);
                }
                else if(currentNode.rightChild == null ? true : treeList.Contains(currentNode.rightChild.data) && currentNode.leftChild == null ? true : treeList.Contains(currentNode.leftChild.data))
                {
                    currentNode = currentNode.parent;
                }
            }
            treeList.Sort();
            return treeList;
        }
    }
}
