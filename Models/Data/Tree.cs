﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L3_DAVH_AFPE.Models.Data
{
    public class Tree<T> where T : IComparable
    {
        public TreeNode<T> Root { get; set; }
        public TreeNode<T> Work { get; set; }

        public int lenght = 0;
        public Tree()
        {
            Root = null;
        }


        public TreeNode<T> Insert(T newvalue, TreeNode<T> pNode)
        {
            if (pNode == null)
            {
                lenght++;
                return new TreeNode<T>(newvalue); ;
            }
            if (newvalue.CompareTo(pNode.value) < 0)
            {
                pNode.left = Insert(newvalue, pNode.left);
            }
            else if (newvalue.CompareTo(pNode.value) > 0)
            {
                pNode.right = Insert(newvalue, pNode.right);
            }
            else
            {
                return pNode;
            }
            pNode.height = 1 + max(height(pNode.left), height(pNode.right));

            int balance = getBalance(pNode);

            //Left Left Case
            if (balance > 1 && newvalue.CompareTo(pNode.left.value) < 0)
                return rightRotate(pNode);

            // Right Right Case
            if (balance < -1 && newvalue.CompareTo(pNode.right.value) > 0)
                return leftRotate(pNode);

            // Left Right Case
            if (balance > 1 && newvalue.CompareTo(pNode.left.value) > 0)
            {
                pNode.left = leftRotate(pNode.left);
                return rightRotate(pNode);
            }

            // Right Left Case
            if (balance < -1 && newvalue.CompareTo(pNode.right.value) < 0)
            {
                pNode.right = rightRotate(pNode.right);
                return leftRotate(pNode);
            }
            return pNode;

        }
        public TreeNode<T> Find(T value, TreeNode<T> node)
        {
            if (node != null)
            {
                if (value.CompareTo(node.value) == 0)
                {
                    return node;
                }
                if (value.CompareTo(node.value) < 0)
                {
                    return Find(value, node.left);
                }
                else
                {
                    return Find(value, node.right);
                }
            }

            return null;
        }
        public TreeNode<T> SearchParent(TreeNode<T> node, TreeNode<T> parent)
        {
            TreeNode<T> temp = null;
            if (node == null)
            {
                return null;
            }
            if (parent.left != null)
            {
                if (parent.left.value.CompareTo(node.value) == 0)
                {
                    return parent;
                }
            }
            if (parent.right != null)
            {
                if (parent.right.value.CompareTo(node.value) == 0)
                {
                    return parent;
                }
            }

            if (node.value.CompareTo(parent.value) < 0 && parent.left != null)
            {
                temp = SearchParent(node, parent.left);
            }
            if (node.value.CompareTo(parent.value) > 0 && parent.right != null)
            {
                temp = SearchParent(node, parent.right);
            }
            return temp;
        }

        public TreeNode<T> Delete(TreeNode<T> node, T value)
        {
            if (node == null)
            {
                return node;
            }
            if (value.CompareTo(node.value) < 0)
            {
                node.left = Delete(node.left, value);
            }
            else if (value.CompareTo(node.value) > 0)
            {
                node.right = Delete(node.right, value);
            }
            else
            {
                if (node.left == null)
                {
                    return node.right;
                }
                else if (node.right == null)
                {
                    return node.left;
                }
                else
                {
                    node.value = FindMinimum(node.right).value;
                    node.right = Delete(node.right, node.value);
                }
            }
            return node;
        }

        public TreeNode<T> FindMinimum(TreeNode<T> node)
        {
            if (node == null)
            {
                return default;
            }
            Work = node;
            TreeNode<T> minimum = Work;

            while (Work.left != null)
            {
                Work = Work.left;
                minimum = Work;
            }
            return minimum;

        }

        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        int height(TreeNode<T> N)
        {
            if (N == null)
                return 0;

            return N.height;
        }

        int getBalance(TreeNode<T> N)
        {
            if (N == null)
            {
                return 0;
            }

            return height(N.left) - height(N.right);
        }

        TreeNode<T> rightRotate(TreeNode<T> y)
        {
            TreeNode<T> x = y.left;
            TreeNode<T> z = x.right;

            x.right = y;
            y.left = z;

            y.height = max(height(y.left), height(y.right)) + 1;
            x.height = max(height(x.left), height(x.right)) + 1;

            return x;
        }

        TreeNode<T> leftRotate(TreeNode<T> x)
        {
            TreeNode<T> y = x.right;
            TreeNode<T> z = y.left;

            y.left = x;
            x.right = z;

            x.height = max(height(x.left), height(x.right)) + 1;
            y.height = max(height(y.left), height(y.right)) + 1;

            return y;
        }

    }
}
