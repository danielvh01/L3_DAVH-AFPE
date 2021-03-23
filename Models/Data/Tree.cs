using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L3_DAVH_AFPE.Models.Data
{
    public class Tree<T> where T : IComparable
    {
        #region Variables
        public TreeNode<T> Root { get; set; }
        public TreeNode<T> Work { get; set; }

        public int lenght = 0;
        public Tree()
        {
            Root = null;
        }
        #endregion

        #region Methods
        public TreeNode<T> Insert(T newvalue, TreeNode<T> pNode)
        {
            TreeNode<T> temp = null;
            if (pNode == null)
            {
                temp = new TreeNode<T>(newvalue);
                if (lenght == 0)
                {
                    Root = temp;
                }
                lenght++;
                return temp;
            }
            if (newvalue.CompareTo(pNode.value) < 0)
            {
                pNode.left = Insert(newvalue, pNode.left);
            }
            else if (newvalue.CompareTo(pNode.value) > 0)
            {
                pNode.right = Insert(newvalue, pNode.right);
            }
            else { // If value of node is repeated, it wont be inserted.
                return pNode;
            }

            pNode.height = 1 + max(height(pNode.left), height(pNode.right)); 

            int balance = getBalance(pNode);

            if (balance > 1 && newvalue.CompareTo(pNode.left.value) < 0) //If node becomes with desbalance, it will be compared with the 4 possible cases of rotations.
                               //Left->Left
            {
                return rightRotation(pNode);
            }
            if (balance < -1 && newvalue.CompareTo(pNode.right.value) > 0) // Right->Right case
            {
                return leftRotate(pNode);
            }
            if (balance > 1 && newvalue.CompareTo(pNode.left.value) > 0)// Left->Right Case
            {
                pNode.left = leftRotate(pNode.left);
                return rightRotation(pNode);
            }
            if (balance < -1 && newvalue.CompareTo(pNode.right.value) < 0)  // Right->Left Case
            {
                pNode.right = leftRotate(pNode.right);
                return leftRotate(pNode);
            }
            return pNode;
        }
        //Get the updated height of the entire tree
        int height(TreeNode<T> N)
        {
            if (N == null)
                return 0;

            return N.height;
        }
        //Function will validate the condition
        //If it is true will return a 
        //If it is false will return b
        int max(int a, int b)
        {
            return (a > b) ? a : b;
        }

        public TreeNode<T> rightRotation(TreeNode<T> node)
        {
            TreeNode<T> x = node.left;
            TreeNode<T> T2 = node.right;

            x.right = node;
            node.left = T2;

            node.height = (max(height(node.left),height(node.right))+1);
            x.height = (max(height(x.left), height(x.right))+1);

            return x;
        }

        public TreeNode<T> leftRotate(TreeNode<T> x)
        {
            TreeNode<T> y = x.right;
            TreeNode<T> T2 = y.left;
            
            y.left = x;
            x.right = T2;
            
            x.height = (max(height(x.left),height(x.right)) + 1);
            y.height = (max(height(y.left),height(y.right)) + 1);
            
            return y;
        }

        //Get the balance factor of ancestor node to check whether this node became unbalanced
        int getBalance(TreeNode<T> N)
        {
            if (N == null)
                return 0;

            return height(N.left) - height(N.right);
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
            node.height =  max(height(node.left), height(node.right))+1;

            int balance = getBalance(node);

            if (balance > 1 && getBalance(node.left) >=0) //If node becomes with desbalance, it will be compared with the 4 possible cases of rotations.
                                                                         //Left->Left
            {
                return rightRotation(node);
            }
            if (balance > 1 && getBalance(node.left) < 0) // Left->Right case
            {
                node.left = leftRotate(node.left);
                return rightRotation(node);
            }
            if (balance < -1 && getBalance(node.right) <= 0)// Right->Right Case
            {
                return leftRotate(node);
            }
            if (balance < -1 && getBalance(node.right) > 0)  // Right->Left Case
            {
                node.right = rightRotation(node.right);
                return leftRotate(node);
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
        #endregion

    }
}
